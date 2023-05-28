using Microsoft.Data.SqlClient;
using Project.ConnectionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Logic
{
    public class Tables
    {
        public static void CreateTable(string name, string query)
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                string checkIfTableExists = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{name}'";
                using (SqlCommand commmand = new SqlCommand(checkIfTableExists, conn))
                {
                    conn.Open();
                    int numberOfTables = (int)commmand.ExecuteScalar();
                    if (numberOfTables > 0)
                    {
                        Console.WriteLine($"Table with a name '{name}' already exists");
                    }
                    else
                    {
                        commmand.CommandText = query;
                        commmand.ExecuteNonQuery();
                        Console.WriteLine($"Table named {name} was sucessfuly created");
                    }

                }
            }
        }
    }
}
