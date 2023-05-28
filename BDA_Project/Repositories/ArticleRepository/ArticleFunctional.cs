using Microsoft.Data.SqlClient;
using Project.ConnectionHelper;

namespace Project.Repositories.ArticleRepository
{
    public class ArticleFunctional
    {
        public static void AddArticle()
        {
            try
            {
                Console.Write("Enter the name of an article: ");
                string name = Console.ReadLine();
                Console.Write("Enter the price of an article: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
                {
                    string queryAddArticle = $"INSERT INTO Article(name, price) Values('{name}', '{price}')";
                    using (SqlCommand command = new SqlCommand(queryAddArticle, conn))
                    {
                        conn.Open();
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT SCOPE_IDENTITY()";
                        int articleId = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"Article saved succesfully under the Id№{articleId}");
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

        public static void GetAllArticles()
        {
            using (SqlConnection conn = new SqlConnection(Connection.GetConnectionString()))
            {
                string queryGetAllArticles = "SELECT * FROM Article";
                using (SqlCommand command = new SqlCommand(queryGetAllArticles, conn))
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("\t\tList of all articles:");
                    while(reader.Read())
                    {
                        for(int i = 0; i < reader.FieldCount; i++)
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
