using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Shipper"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Shippers")]
    public partial class Shipper
    {
        [PrimaryKey, Identity]
        public int ShipperID { get; set; }

        [Column, NotNull]
        public string CompanyName { get; set; }

        [Column, Nullable]
        public string Phone { get; set; }

        [Association(ThisKey = "ShipperID", OtherKey = "ShipVia", CanBeNull = false)]
        public IEnumerable<Order> Orders { get; set; }
    }
}
