using EF.Infrastructure.Models;
using System.Data.Entity.Migrations;

namespace EF.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NorthwindContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NorthwindContext context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category
                {
                    CategoryName = "Oil",
                    Description = "Oil production",
                    Picture = null
                },
                new Category
                {
                    CategoryName = "Gas",
                    Description = "Gas production",
                    Picture = null
                },
                new Category
                {
                    CategoryName = "Beverages",
                    Description = "Soft drinks, coffees, teas, beers, and ales",
                    Picture = null
                });

            context.Regions.AddOrUpdate(r => r.RegionID,
                new Region
                {
                    RegionID = 5,
                    RegionDescription = "Eastern Europe"
                },
                new Region
                {
                    RegionID = 6,
                    RegionDescription = "Western Europe"
                });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory
                {
                    TerritoryID = "344567",
                    RegionID = 5,
                    TerritoryDescription = "Downtown"
                },
                new Territory
                {
                    TerritoryID = "234567",
                    RegionID = 6,
                    TerritoryDescription = "Eastern town"
                });
        }
    }
}
