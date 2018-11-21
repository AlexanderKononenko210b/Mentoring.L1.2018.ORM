using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Tools;
using Micro.ORM.Infrastructure;
using Micro.ORM.Infrastructure.Models;
using NUnit.Framework;

namespace MicroORM.Test
{
    [TestFixture]
    public class MicroORMTest
    {
        private readonly string _connectionString = "NorthwindConection";

        [Test]
        public void Query_Task_2_1()
        {
            using (var connection = new NorthwindDB(_connectionString))
            {
                var result = connection.Products
                    .Select(product => new
                    {
                        Product = product.ProductName,
                        Category = product.Category.CategoryName,
                        Supplier = product.Supplier.CompanyName
                    });

                foreach (var product in result)
                {
                    Console.WriteLine("{0} | {1} | {2}", product.Product,
                        product.Category, product.Supplier);
                }
            }
        }

        [Test]
        public void Query_Task_2_2()
        {
            using (var connection = new NorthwindDB(_connectionString))
            {
                var result = connection.Employees
                    .Join(connection.EmployeeTerritories,
                        employee => employee.EmployeeID,
                        employeeTerritories => employeeTerritories.EmployeeID,
                        (employee, employeeTerritories) => new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            TerritoryId = employeeTerritories.TerritoryID
                        })
                    .Join(connection.Territories,
                        employee => employee.TerritoryId,
                        territory => territory.TerritoryID,
                        (employee, territory) => new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            RegionId = territory.RegionID
                        })
                    .Join(connection.Regions,
                        employee => employee.RegionId,
                        region => region.RegionID,
                        (employee, region) => new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Region = region.RegionDescription
                        })
                    .Distinct();

                foreach (var employeeInfo in result)
                {
                    Console.WriteLine("{0} | {1}", $"{employeeInfo.FirstName} {employeeInfo.LastName}",
                        employeeInfo.Region);
                }
            }
        }

        [Test]
        public void Query_Task_2_3()
        {
            using (var connection = new NorthwindDB(_connectionString))
            {
                var result = connection.Regions
                    .Join(connection.Territories,
                        region => region.RegionID,
                        territory => territory.RegionID,
                        (region, territory) => new
                        {
                            Region = region.RegionDescription,
                            TerritoryId = territory.TerritoryID
                        })
                    .Join(connection.EmployeeTerritories,
                        region => region.TerritoryId,
                        employeeTerritories => employeeTerritories.TerritoryID,
                        (region, employeeTerritory) => new
                        {
                            Region = region.Region,
                            EmployeeId = employeeTerritory.EmployeeID
                        })
                    .Join(connection.Employees,
                        region => region.EmployeeId,
                        employee => employee.EmployeeID,
                        (region, employee) => new
                        {
                            Region = region.Region,
                            EmployeeId = employee.EmployeeID
                        })
                    .Distinct()
                    .GroupBy(region => region.Region,
                        (region, group) => new
                        {
                            Region = region,
                            CountEmployees = group.Count(employee => employee.EmployeeId != 0)
                        });

                foreach (var regionInfo in result)
                {
                    Console.WriteLine("{0} | {1}", regionInfo.Region, regionInfo.CountEmployees);
                }
            }
        }

        [Test]
        public void Query_Task_2_4()
        {
            using (var connection = new NorthwindDB(_connectionString))
            {
                var result = connection.Employees
                    .Join(connection.Orders,
                        employee => employee.EmployeeID,
                        order => order.EmployeeID,
                        (employee, order) => new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            ShipperId = order.ShipVia
                        })
                    .Join(connection.Shippers,
                        order => order.ShipperId,
                        shipper => shipper.ShipperID,
                        (order, shipper) => new
                        {
                            FirstName = order.FirstName,
                            LastName = order.LastName,
                            Shipper = shipper.CompanyName
                        })
                    .Distinct()
                    .OrderBy(employee => employee.FirstName)
                    .GroupBy(employee => new { employee.FirstName, employee.LastName },
                        (employee, group) => new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Shippers = group.OrderBy(shipper => shipper.Shipper)
                        });

                foreach (var employeeInfo in result)
                {
                    Console.WriteLine($"{employeeInfo.FirstName} {employeeInfo.LastName}");
                    foreach (var shipper in employeeInfo.Shippers)
                    {
                        Console.WriteLine($"\t {shipper.Shipper}");
                    }
                }
            }
        }

        [Test]
        public void Query_Task_3_1()
        {
            var newEmployee = new Employee
            {
                FirstName = "Jon",
                LastName = "Volta",
                Title = "Sales Representative",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1982, 5, 25),
                HireDate = new DateTime(2010, 5, 4),
                Address = "Coventry House Miner Rd.",
                City = "London",
                Region = "WA",
                PostalCode = "EC2 7JR",
                Country = "UK",
                HomePhone = "(71) 555-7773",
                Extension = "428",
            };

            using (var connection = new NorthwindDB(_connectionString))
            {
                try
                {
                    connection.BeginTransaction();

                    var checkExistEmployee = connection.Employees.Where(employee =>
                        employee.FirstName.Equals(newEmployee.FirstName) &&
                        employee.LastName.Equals(newEmployee.LastName));

                    if (!checkExistEmployee.Any())
                    {
                        var newEmployeeId = Convert.ToInt32(connection.InsertWithIdentity(newEmployee));

                        connection.Territories
                            .Where(territory =>
                                territory.TerritoryDescription.Equals("Westboro") &&
                                territory.TerritoryDescription.Equals("Bedford"))
                            .Insert(connection.EmployeeTerritories, employeeTerritories => new EmployeeTerritory
                            {
                                TerritoryID = employeeTerritories.TerritoryID,
                                EmployeeID = newEmployeeId
                            });

                        connection.CommitTransaction();

                        var result = connection.Employees.Where(employee => employee.EmployeeID == newEmployeeId)
                            .ToArray();

                        Assert.AreEqual(newEmployee.FirstName, result[0].FirstName);
                        Assert.AreEqual(newEmployee.LastName, result[0].LastName);
                    }
                }
                catch (Exception e)
                {
                    connection.RollbackTransaction();
                }
            }
        }

        [Test]
        public void Query_Task_3_2()
        {
            var oldCategoryID = 2;
            var newCategoryID = 3;

            using (var connection = new NorthwindDB(_connectionString))
            {
                var countRowsForUpdate = connection.Products
                    .Where(product => product.CategoryID == oldCategoryID)
                    .ToArray().Length;

                var countUpdateRows = connection.Products
                    .Update(
                    target => target.CategoryID == oldCategoryID,
                    product => new Product
                    {
                        CategoryID = newCategoryID
                    });

                Assert.AreEqual(countRowsForUpdate, countUpdateRows);
            }
        }

        [Test]
        public void Query_Task_3_3()
        {
            var newProductList = new List<Product>
            {
                new Product
                {
                    ProductName = "Diesel",
                    Category = new Category
                    {
                        CategoryName = "Oil",
                        Description = "Oil and gas production",
                        Picture = null
                    },
                    Supplier = new Supplier
                    {
                        CompanyName = "PetrolUnit",
                        ContactName = "Charlotte Cooper",
                        ContactTitle = "Purchasing Manager",
                        Address = "49 Gilbert St.",
                        City = "London",
                        Region = null,
                        PostalCode = "EC1 4SD",
                        Country = "UK",
                        Phone = "(171) 555-2222",
                        Fax = null,
                        HomePage = null
                    },
                    QuantityPerUnit = "12 - 550 ml bottles",
                    UnitPrice = new decimal(50.00),
                    UnitsInStock = 0,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                },
                new Product
                {
                    ProductName = "Petrol",
                    Category = new Category
                    {
                        CategoryName = "Oil",
                        Description = "Oil and gas production",
                        Picture = new byte[0]
                    },
                    Supplier = new Supplier
                    {
                        CompanyName = "PetrolUnit",
                        ContactName = "Charlotte Cooper",
                        ContactTitle = "Purchasing Manager",
                        Address = "49 Gilbert St.",
                        City = "London",
                        Region = null,
                        PostalCode = "EC1 4SD",
                        Country = "UK",
                        Phone = "(171) 555-2222",
                        Fax = null,
                        HomePage = null
                    },
                    QuantityPerUnit = "12 - 550 ml bottles",
                    UnitPrice = new decimal(50.00),
                    UnitsInStock = 0,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false
                }
            };

            var productNames = new[] { "Diesel", "Petrol" };

            using (var connection = new NorthwindDB(_connectionString))
            {
                try
                {
                    connection.BeginTransaction();

                    foreach (var product in newProductList)
                    {
                        var existProduct = connection.Products.FirstOrDefault(prod => prod.ProductName == product.ProductName);

                        if (existProduct == null)
                        {
                            var existCategory =
                                connection.Categories.FirstOrDefault(category => category.CategoryName == product.Category.CategoryName);

                            product.CategoryID = existCategory?.CategoryID ?? Convert.ToInt32(connection.InsertWithIdentity(new Category
                            {
                                CategoryName = product.Category.CategoryName,
                                Description = product.Category.Description,
                                Picture = product.Category.Picture
                            }));

                            var existSupplier =
                                connection.Suppliers.FirstOrDefault(supplier => supplier.CompanyName == product.Supplier.CompanyName);

                            product.SupplierID = existSupplier?.SupplierID ?? Convert.ToInt32(connection.InsertWithIdentity(new Supplier
                            {
                                CompanyName = product.Supplier.CompanyName,
                                ContactName = product.Supplier.ContactName,
                                ContactTitle = product.Supplier.ContactTitle,
                                Address = product.Supplier.Address,
                                City = product.Supplier.City,
                                Region = product.Supplier.Region,
                                PostalCode = product.Supplier.PostalCode,
                                Country = product.Supplier.Country,
                                Phone = product.Supplier.Phone,
                                Fax = product.Supplier.Fax,
                                HomePage = product.Supplier.HomePage
                            }));

                            connection.InsertWithIdentity(product);
                        }
                    }

                    connection.CommitTransaction();
                }
                catch (Exception e)
                {
                    connection.RollbackTransaction();
                }

                var result = connection.Products.Where(prod => prod.ProductName.In(productNames)).ToArray();

                Assert.AreEqual(newProductList.Count, result.Length);
            }
        }

        [Test]
        public void Query_Task_3_4()
        {
            using (var connection = new NorthwindDB(_connectionString))
            {
                var notShippedOrders = connection.OrderDetails
                    .LoadWith(orderDetail => orderDetail.OrderDetailsOrder)
                    .LoadWith(orderDetail => orderDetail.OrderDetailsProduct)
                    .Where(orderDetails => orderDetails.OrderDetailsOrder.ShippedDate == null).ToList();

                if (notShippedOrders.Any())
                {
                    connection.BeginTransaction();

                    var countUpdateRows = 0;

                    try
                    {
                        foreach (var order in notShippedOrders)
                        {
                            countUpdateRows += Convert.ToInt32(connection.OrderDetails
                                .LoadWith(orderDetail => orderDetail.OrderDetailsProduct)
                                .Update(orderDetail => orderDetail.OrderID == order.OrderID && orderDetail.ProductID == order.ProductID,
                                product => new OrderDetail
                                {
                                    ProductID = connection.Products
                                    .FirstOrDefault(prod => prod.ProductID != order.ProductID && prod.CategoryID == order.OrderDetailsProduct.CategoryID).ProductID == 0 ?
                                    order.ProductID :
                                      connection.Products
                                            .FirstOrDefault(prod => prod.ProductID != order.ProductID && prod.CategoryID == order.OrderDetailsProduct.CategoryID)
                                            .ProductID
                                }));
                        }

                        connection.CommitTransaction();
                    }
                    catch (Exception e)
                    {
                        connection.RollbackTransaction();
                    }

                    Console.WriteLine($"{countUpdateRows} was updated.");
                }
                else
                {
                    Console.WriteLine("Orders for update is absent.");
                }
            }
        }
    }
}

