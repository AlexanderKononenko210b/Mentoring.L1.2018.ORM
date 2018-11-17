using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a <see cref="Order"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Orders")]
    public partial class Order
    {
        [PrimaryKey, Identity]
        public int OrderID { get; set; }

        [Column, Nullable]
        public string CustomerID { get; set; }

        [Column, Nullable]
        public int? EmployeeID { get; set; }

        [Column, Nullable]
        public DateTime? OrderDate { get; set; }

        [Column, Nullable]
        public DateTime? RequiredDate { get; set; }

        [Column, Nullable]
        public DateTime? ShippedDate { get; set; }

        [Column, Nullable]
        public int? ShipVia { get; set; }

        [Column, Nullable]
        public decimal? Freight { get; set; }

        [Column, Nullable]
        public string ShipName { get; set; }

        [Column, Nullable]
        public string ShipAddress { get; set; }

        [Column, Nullable]
        public string ShipCity { get; set; }

        [Column, Nullable]
        public string ShipRegion { get; set; }

        [Column, Nullable]
        public string ShipPostalCode { get; set; }

        [Column, Nullable]
        public string ShipCountry { get; set; }

        [Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = true)]
        public Customer Customer { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = true)]
        public Employee Employee { get; set; }

        [Association(ThisKey = "ShipVia", OtherKey = "ShipperID", CanBeNull = true)]
        public Shipper Shipper { get; set; }

        [Association(ThisKey = "OrderID", OtherKey = "OrderID", CanBeNull = false)]
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
