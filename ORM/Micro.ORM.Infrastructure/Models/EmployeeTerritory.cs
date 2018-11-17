using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="EmployeeTerritory"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "EmployeeTerritories")]
    public partial class EmployeeTerritory
    {
        [PrimaryKey(1), NotNull]
        public int EmployeeID { get; set; }

        [PrimaryKey(2), NotNull]
        public string TerritoryID { get; set; }

        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = false)]
        public Employee Employee { get; set; }

        [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = false)]
        public Territory Territory { get; set; }
    }
}
