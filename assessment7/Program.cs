using System;
using System.Data.SqlClient;
using System.Data;

namespace LibraryApp
{
    class Program
    {
        static string connectionString = "server=SANDZONE; database=LibraryDB; trusted_connection=true;";

        static void Main(string[] args)
        {
            DataSet libraryDataSet = new DataSet();

            RetrieveData(libraryDataSet);
            DisplayBookInventory(libraryDataSet);

            while (true)
            {
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Apply Changes to Database");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayBookInventory(libraryDataSet);
                        break;
                    case 2:
                        AddNewBook(libraryDataSet);
                        break;
                    case 3:
                        UpdateBookQuantity(libraryDataSet);
                        break;
                    case 4:
                        ApplyChangesToDatabase(libraryDataSet);
                        break;
                    case 5:
                        return;
                }
            }
        }

        static void RetrieveData(DataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(dataSet, "Books");
            }
        }

        static void DisplayBookInventory(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];

            Console.WriteLine("Book Inventory:");
            foreach (DataRow row in booksTable.Rows)
            {
                Console.WriteLine($"Title: {row["Title"]}, Author: {row["Author"]}, Genre: {row["Genre"]}, Quantity: {row["Quantity"]}");
            }
        }

        static void AddNewBook(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];

            DataRow newBook = booksTable.NewRow();
            Console.Write("Enter Title: ");
            newBook["Title"] = Console.ReadLine();
            Console.Write("Enter Author: ");
            newBook["Author"] = Console.ReadLine();
            Console.Write("Enter Genre: ");
            newBook["Genre"] = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            newBook["Quantity"] = int.Parse(Console.ReadLine());

            booksTable.Rows.Add(newBook);
        }


        static void UpdateBookQuantity(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];

            Console.Write("Enter Title of the book to update quantity: ");
            string titleToUpdate = Console.ReadLine();

            DataRow[] foundRows = booksTable.Select($"Title = '{titleToUpdate}'");
            if (foundRows.Length > 0)
            {
                Console.Write("Enter New Quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());
                foundRows[0]["Quantity"] = newQuantity;
            }
            else
            {
                Console.WriteLine(" Error!!! Book not found.");
            }
        }

        static void ApplyChangesToDatabase(DataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                adapter.Update(dataSet, "Books");
            }
        }
    }
}
