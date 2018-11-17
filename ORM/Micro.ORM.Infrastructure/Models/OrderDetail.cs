using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="OrderDetail"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Order Details")]
    public partial class OrderDetail
    {
        [PrimaryKey(1), NotNull]
        public int OrderID { get; set; }

        [PrimaryKey(2), NotNull]
        public int ProductID { get; set; }

        [Column, NotNull]
        public decimal UnitPrice { get; set; }

        [Column, NotNull]
        public short Quantity { get; set; }

        [Column, NotNull]
        public float Discount { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID", CanBeNull = false)]
        public Order OrderDetailsOrder { get; set; }

        [Association(ThisKey = "ProductID", OtherKey = "ProductID", CanBeNull = false)]
        public Product OrderDetailsProduct { get; set; }
    }
}
