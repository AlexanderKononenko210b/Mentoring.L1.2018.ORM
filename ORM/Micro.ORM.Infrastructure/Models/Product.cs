using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Product"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Products")]
    public class Product
    {
        [PrimaryKey, Identity]
        public int ProductID { get; set; }

        [Column, NotNull]
        public string ProductName { get; set; }

        [Column, Nullable]
        public int? SupplierID { get; set; }

        [Column, Nullable]
        public int? CategoryID { get; set; }

        [Column, Nullable]
        public string QuantityPerUnit { get; set; }

        [Column, Nullable]
        public decimal? UnitPrice { get; set; }

        [Column, Nullable]
        public short? UnitsInStock { get; set; }

        [Column, Nullable]
        public short? UnitsOnOrder { get; set; }

        [Column, Nullable]
        public short? ReorderLevel { get; set; }

        [Column, NotNull]
        public bool Discontinued { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = true)]
        public Category Category { get; set; }

        [Association(ThisKey = "SupplierID", OtherKey = "SupplierID", CanBeNull = true)]
        public Supplier Supplier { get; set; }

        [Association(ThisKey = "ProductID", OtherKey = "ProductID", CanBeNull = false)]
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
