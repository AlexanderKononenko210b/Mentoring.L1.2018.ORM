using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="CustomerCustomerDemo"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "CustomerCustomerDemo")]
    public class CustomerCustomerDemo
    {
        [PrimaryKey(1), NotNull]
        public string CustomerID { get; set; }

        [PrimaryKey(2), NotNull]
        public string CustomerTypeID { get; set; }
        
        [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = false)]
        public CustomerDemographic FK_CustomerCustomerDemo { get; set; }

        [Association(ThisKey = "CustomerID", OtherKey = "CustomerID", CanBeNull = false)]
        public Customer Customer { get; set; }
    }
}
