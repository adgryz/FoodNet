using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace DatabaseSeed
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
                    string projectPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                    string seedPath = projectPath + "\\Seed.sql";
                    cmd.CommandText = File.ReadAllText(seedPath);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
