using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Micro.ORM.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="Category"/> class.
    /// </summary>
    [Table(Schema = "dbo", Name = "Categories")]
    public class Category
    {
        [PrimaryKey, Identity]
        public int CategoryID { get; set; }

        [Column, NotNull]
        public string CategoryName { get; set; }

        [Column, Nullable]
        public string Description { get; set; }

        [Column, Nullable]
        public byte[] Picture { get; set; }

        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID", CanBeNull = false)]
        public IEnumerable<Product> Products { get; set; }
    }
}
