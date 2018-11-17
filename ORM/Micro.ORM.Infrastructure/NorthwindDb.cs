using LinqToDB;
using LinqToDB.Data;
using Micro.ORM.Infrastructure.Models;

namespace Micro.ORM.Infrastructure
{
    /// <summary>
    /// Represents a model <see cref="NorthwindDB"/> class.
    /// </summary>
    public class NorthwindDB : DataConnection
    {
        public ITable<Category> Categories => GetTable<Category>();
        public ITable<Customer> Customers => GetTable<Customer>();
        public ITable<CustomerCustomerDemo> CustomerCustomerDemoes => GetTable<CustomerCustomerDemo>();
        public ITable<CustomerDemographic> CustomerDemographics => GetTable<CustomerDemographic>();
        public ITable<Employee> Employees => GetTable<Employee>();
        public ITable<EmployeeTerritory> EmployeeTerritories => GetTable<EmployeeTerritory>();
        public ITable<Order> Orders => GetTable<Order>();
        public ITable<OrderDetail> OrderDetails => GetTable<OrderDetail>();
        public ITable<Product> Products => GetTable<Product>();
        public ITable<Region> Regions => GetTable<Region>();
        public ITable<Shipper> Shippers => GetTable<Shipper>();
        public ITable<Supplier> Suppliers => GetTable<Supplier>();
        public ITable<Territory> Territories => GetTable<Territory>();

        public NorthwindDB(string configuration)
            : base(configuration)
        { }
    }
}
