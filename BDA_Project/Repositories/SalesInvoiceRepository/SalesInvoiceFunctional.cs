using Microsoft.Data.SqlClient;
using Project.ConnectionHelper;

namespace Project.Repositories.SalesInvoiceRepository
{
    public class SalesInvoiceFunctional
    {
        public static void AddSalesInvoice()
        {
            try
            {
                Console.Write("Enter the customer Id: ");
                int customerId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the article Id: ");
                int articleId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the number of articles: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
                {
                    string queryCheckIfCustomerExists = $"SELECT COUNT(*) FROM Customer WHERE customerId = {customerId}";
                    using (SqlCommand command = new SqlCommand(queryCheckIfCustomerExists, conn))
                    {
                        conn.Open();
                        if ((int)command.ExecuteScalar() != 1)
                        {
                            Console.WriteLine($"It is only possible to add a Sales Invoice on a customer that already exists. Customer with Id {customerId} was not found.");
                            return;
                        }

                        string queryCheckArticleExistence = $"SELECT COUNT(*) FROM Article WHERE articleId = {articleId}";
                        command.CommandText = queryCheckArticleExistence;
                        if ((int)command.ExecuteScalar() != 1)
                        {
                            Console.WriteLine($"It is only possible to add a Sales Invoice with an existing article. Article with Id {articleId} wasn't found");
                            return;
                        }

                        string queryAddSalesInvoice = $"INSERT INTO SalesInvoice VALUES({customerId}, {articleId}, {quantity})";
                        command.CommandText = queryAddSalesInvoice;
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT SCOPE_IDENTITY()";
                        int salesInvoiceId = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Sales Invoice saved succesfully under Id№{salesInvoiceId}");

                    }
                }
            
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine("An invalid type of input.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, somethins went wrong. Here is what it might be: " + ex.Message);
            }

        }

        public static void GetAllSalesInvoices()
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                string queryGetAllSalesInvoices = "SELECT * FROM SalesInvoice";
                using (SqlCommand command = new SqlCommand(queryGetAllSalesInvoices, conn))
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("\t\tList of all sales invoices:");
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i) + ": " + reader[i].ToString() + "\t");
                        }
                        Console.WriteLine();

                    }
                }
            }
        }
    }
}
