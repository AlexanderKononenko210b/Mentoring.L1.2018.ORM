using System;
using NUnit.Framework;

namespace EFSample
{
    [TestFixture]
    public class EfTest
    {
        [Test]
        public void TestMethod1()
        {
            using (var context = new NorthwindDb())
            {
                foreach (var cat in context.Categories)
                {
                    Console.WriteLine("{0} {1} | {2}", cat.CategoryID,
                        cat.CategoryName, cat.Description);
                }
            }
        }
    }
}
