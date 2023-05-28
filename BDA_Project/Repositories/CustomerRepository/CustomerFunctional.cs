using Microsoft.Data.SqlClient;
using Project.ConnectionHelper;

namespace Project.Repositories.CustomerRepository
{
    public class CustomerFunctional
    {
        public static void AddCustomer()
        {
            try
            {
                Console.Write("Enter the name of a customer: ");
                string name = Console.ReadLine();
                Console.Write("Enter the address of a customer: ");
                string address = Console.ReadLine();
                using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
                {
                    string queryAddCustomer = $"INSERT INTO Customer Values('{name}', '{address}')";
                    using (SqlCommand command = new SqlCommand(queryAddCustomer, conn))
                    {
                        conn.Open();
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT SCOPE_IDENTITY()";
                        int customerId = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Customer saved succesfully under the Id№{customerId}");
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

        public static void GetAllCustomers()
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                string queryGetAllCustomers = "SELECT * FROM Customer";
                using (SqlCommand command = new SqlCommand(queryGetAllCustomers, conn))
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("\t\tList of all customers:");
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
