using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ConnectionHelper
{
    public class Connection
    {
            public static string GetConnectionString()
            {
                SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = "",
                    InitialCatalog = "",
                    UserID = "",
                    Password = "",
                    TrustServerCertificate = true
                };

                return sqlConnectionString.ToString();
            }
        }
    }
