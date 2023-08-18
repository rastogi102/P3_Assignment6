using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConAppPIS
{
    internal class Program
    {
        static string connectionString = "server=DESKTOP-MFQ8M0P;Database=ProductlnventoryDB;trusted_connection=true;";

        static void Main(string[] args)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database.");

                    while (true)
                    {
                        Console.WriteLine("\nProduct Inventory System Menu:");
                        Console.WriteLine("1. View Product Inventory");
                        Console.WriteLine("2. Add New Product");
                        Console.WriteLine("3. Update Product Quantity");
                        Console.WriteLine("4. Remove Product");
                        Console.WriteLine("5. Exit");
                        Console.Write("Enter your choice: ");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                ViewProductInventory(connection);
                                break;
                            case 2:
                                AddNewProduct(connection);
                                break;
                            case 3:
                                UpdateProductQuantity(connection);
                                break;
                            case 4:
                                RemoveProduct(connection);
                                break;
                            case 5:
                                Console.WriteLine("Exiting...");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        static void ViewProductInventory(SqlConnection connection)
        {
            Console.WriteLine("Product Inventory:");
            string query = "SELECT * FROM Products";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Price: {reader["Price"]}, Quantity: {reader["Quantity"]}, Manufacturing Date: {reader["MfDate"]}, Expiry Date: {reader["ExpDate"]}");
                    }
                }
            }
        }

        static void AddNewProduct(SqlConnection connection)
        {
            Console.WriteLine("Add a new product:");
            Console.Write("Product Name: ");
            string productName = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Manufacturing Date: ");
            DateTime mfDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Expiry Date: ");
            DateTime expDate = Convert.ToDateTime(Console.ReadLine());

            string query = "INSERT INTO Products (ProductName, Price, Quantity, MfDate, ExpDate) VALUES (@ProductName, @Price, @Quantity, @MfDate, @ExpDate)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@MfDate", mfDate);
                command.Parameters.AddWithValue("@ExpDate", expDate);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("New product added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add the new product.");
                }
            }
        }

        static void UpdateProductQuantity(SqlConnection connection)
        {
            Console.WriteLine("Update product quantity:");
            Console.Write("Enter Product ID: ");
            int productID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new quantity: ");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            string query = "UPDATE Products SET Quantity = @Quantity WHERE ProductID = @ProductID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductID", productID);
                command.Parameters.AddWithValue("@Quantity", newQuantity);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product quantity updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update product quantity.");
                }
            }
        }

        static void RemoveProduct(SqlConnection connection)
        {
            Console.WriteLine("Remove a product:");
            Console.Write("Enter Product ID: ");
            int productID = Convert.ToInt32(Console.ReadLine());

            string query = "DELETE FROM Products WHERE ProductID = @ProductID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductID", productID);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product removed successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to remove product.");
                }
            }
        }
    }
}