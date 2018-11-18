using System;
using System.Configuration;
using LinqToDB;
using LinqToDB.Data;
using NUnit.Framework;
using LinqToDB.Mapping;

namespace Linq2DbSample
{
    [Table("Categories")]
    public class Category
    {
        [Column("CategoryID")]
        [PrimaryKey]
        [Identity]
        public int Id { get; set; }

        [Column("CategoryName")]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }
    }

    public class Northwind : DataConnection
    {
        public Northwind()
            : base("NorthwindConection")
        { }

        public ITable<Category> Categories => GetTable<Category>();
    }

    [TestFixture]
    public class Linq2DbTest
    {
        [Test]
        public void TestMethod1()
        {
            using (var connection = new Northwind() /*new DataConnection("Northwind")*/)
            {
                foreach (var cat in connection.Categories /*connection.GetTable<Category>()*/)
                {
                    Console.WriteLine("{0} {1} | {2}", cat.Id,
                        cat.Name, cat.Description);
                }
            }
        }
    }
}
