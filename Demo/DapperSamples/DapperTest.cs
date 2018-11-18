using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using ClassInfrastructure;
using Dapper;

namespace DapperSamples
{
    [TestFixture]
    public class DapperTest
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["NorthwindConection"].ConnectionString;

        [Test]
        public void TestMethod1()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var categories = SqlMapper.Query<Category>(
                    connection,
                    "select * from Categories");

                foreach (var cat in categories)
                {
                    Console.WriteLine("{0} {1} | {2}", cat.CategoryID,
                        cat.CategoryName, cat.Description);
                }
            }
        }
    }
}
