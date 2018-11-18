using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a <see cref="Region"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Region")]
    public class Region
    {
        [PrimaryKey, NotNull]
        public int RegionID { get; set; }

        [Column, NotNull]
        public string RegionDescription { get; set; }

        [Association(ThisKey = "RegionID", OtherKey = "RegionID", CanBeNull = false)]
        public IEnumerable<Territory> Territories { get; set; }
    }
}
