using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Territory"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Territories")]
    public class Territory
    {
        [PrimaryKey, NotNull]
        public string TerritoryID { get; set; }

        [Column, NotNull]
        public string TerritoryDescription { get; set; }

        [Column, NotNull]
        public int RegionID { get; set; }

        [Association(ThisKey = "RegionID", OtherKey = "RegionID", CanBeNull = false)]
        public Region Region { get; set; }

        [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = false)]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }
    }

}
