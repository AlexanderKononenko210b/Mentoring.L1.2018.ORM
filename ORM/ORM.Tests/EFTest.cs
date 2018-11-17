﻿using EF.Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;

namespace ORM.Tests
{
    /// <summary>
    /// Tests for EF
    /// </summary>
    [TestFixture]
    public class EFTest
    {
        private readonly string _connectionString = "NorthwindConection";

        [Test]
        public void Query_Task_1_1()
        {
            using (var context = new NorthwindContext(_connectionString))
            {
                var searchCategory = "Beverages";
                var result = context.Orders.Include(order => order.Customer)
                                           .Where(order => order.Order_Details
                                               .Select(orderDetail => orderDetail.Product)
                                               .Any(product => product.CategoryID.HasValue && 
                                                    product.Category.CategoryName.Equals(searchCategory, StringComparison.InvariantCulture)));

                foreach (var order in result)
                {
                    Console.WriteLine($"Order: {order.OrderID} | Customer: {order.Customer.CompanyName}");
                    foreach (var orderDetail in order.Order_Details)
                    {
                        Console.WriteLine("\t {0} | {1}", orderDetail.Product.ProductName, orderDetail.Product.Category.CategoryName);
                    }
                }
            }
        }
    }
}