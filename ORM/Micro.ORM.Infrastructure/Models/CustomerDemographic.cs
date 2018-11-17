using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="CustomerDemographic"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "CustomerDemographics")]
    public class CustomerDemographic
    {
        [PrimaryKey, NotNull]
        public string CustomerTypeID { get; set; }

        [Column, Nullable]
        public string CustomerDesc { get; set; }

        [Association(ThisKey = "CustomerTypeID", OtherKey = "CustomerTypeID", CanBeNull = false)]
        public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }
    }
}
