using MongoDB.Driver;
using System;
using System.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace hands_on_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DIO!");

            ExecuteSampleQuerySQLServer();
            ExecuteSampleQueryMongoDB();
            
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }

        private static void ExecuteSampleQueryMongoDB()
        {
            var events = new MongoClient("mongodb://root:MongoDB2021!@localhost:27017/")
                .GetDatabase("hands-on")
                .GetCollection<Event>("Events");

            events.InsertOne(
                new Event(
                    "DIO Labs", 
                    "O Mundo dos bancos de dados relacionais (SQL) e nao relacionais (NoSQL)",
                    DateTime.Parse("2021-10-20 14:00:00")
                )
            );

            var eventsList = events.Find(e => true).ToList();

            var options = new JsonSerializerOptions {
                WriteIndented = true
            };

            Console.WriteLine(JsonSerializer.Serialize(eventsList, options));
        }

        private static void ExecuteSampleQuerySQLServer()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = "tcp:127.0.0.1,1433",
                    UserID = "sa",
                    Password = "Pass@word",
                    InitialCatalog = "master"
                };

                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                connection.Open();

                String sql = "SELECT name, collation_name FROM sys.databases";

                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
