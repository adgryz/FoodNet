using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace CoreDatabaseSeed
{
    class Program
    {
        private static string ConnectionString = "Server =.\\SQLEXPRESS;Initial Catalog = FoodNet; Integrated Security = true";
        static void Main(string[] args)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    string projectBasePath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
                    string seedPath = projectBasePath + "Seed.sql";
                    cmd.CommandText = File.ReadAllText(seedPath);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
