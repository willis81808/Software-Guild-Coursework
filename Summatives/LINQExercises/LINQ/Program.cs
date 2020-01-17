using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            Exercise31();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        private static IEnumerable<T> GetEntriesFromWhich<T>(List<T> items, Func<T, bool> predicate)
        {
            var selected = from p in items
                           where predicate(p)
                           select p;
            return selected;
        }
       
        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            PrintProductInformation(GetEntriesFromWhich(DataLoader.LoadProducts(), p => p.UnitsInStock == 0));
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            PrintProductInformation(GetEntriesFromWhich(DataLoader.LoadProducts(), p => p.UnitsInStock > 0 && p.UnitPrice > 3));
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            var customers = GetEntriesFromWhich(DataLoader.LoadCustomers(), c => c.Region == "WA");
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            var selected = from p in DataLoader.LoadProducts()
                           select new { p.ProductName };
            foreach (var item in selected)
            {
                Console.WriteLine(item.ProductName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            var inflatedPrices = from p in DataLoader.LoadProducts()
                                 select new
                                 {
                                     p.ProductID,
                                     p.ProductName,
                                     p.Category,
                                     UnitPrice = p.UnitPrice * 1.25m,
                                     p.UnitsInStock
                                 };
            Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4}", "ProductID", "ProductName", "Category", "Price", "Stock");
            foreach (var item in inflatedPrices)
            {
                Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4}",
                    item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            var items = from p in DataLoader.LoadProducts()
                        select new
                        {
                            ProductName = p.ProductName.ToUpper(),
                            Category    = p.Category.ToUpper()
                        };
            Console.WriteLine("{0,-33} {1, -15}", "ProductName", "Category");
            foreach (var item in items)
            {
                Console.WriteLine("{0,-33} {1, -15}", item.ProductName, item.Category);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7()
        {
            // I don't see how a ternary expression could help with this?
            var items = from p in DataLoader.LoadProducts()
                                 select new
                                 {
                                     p.ProductID,
                                     p.ProductName,
                                     p.Category,
                                     p.UnitPrice,
                                     p.UnitsInStock,
                                     ReOrder =  p.UnitsInStock < 3
                                 };
            Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4, -10} {5, -10}", "ProductID", "ProductName", "Category", "Price", "Stock", "ReStock");
            foreach (var item in items)
            {
                // I used one here anyway...
                Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4, -10} {5, -10}",
                    item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.ReOrder ? "YES" : "NO");
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            var items = from p in DataLoader.LoadProducts()
                        select new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Category,
                            p.UnitPrice,
                            p.UnitsInStock,
                            StockValue = p.UnitPrice * p.UnitsInStock,
                        };
            Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4, -10} {5, -10}", "ProductID", "ProductName", "Category", "Price", "Stock", "StockValue");
            foreach (var item in items)
            {
                Console.WriteLine("{0, -10} {1,-33} {2, -15} {3, -10} {4, -10} {5, -10}",
                    item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.StockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            var evens = from n in DataLoader.NumbersA
                        where n % 2 == 0
                        select n;
            foreach (var number in evens)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            var customers = from c in DataLoader.LoadCustomers()
                            where c.Orders.Any(o => o.Total < 500)
                            select c;
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            var odds = from n in DataLoader.NumbersC
                       where n % 2 == 1
                       select n;
            foreach (var number in odds.Take(3))
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            foreach (var number in DataLoader.NumbersB.Skip(3))
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            var customers = from c in DataLoader.LoadCustomers()
                            where c.Region == "WA"
                            select new
                            {
                                c.CompanyName,
                                Date = c.Orders.Max(o => o.OrderDate)
                            };
            foreach (var c in customers)
            {
                Console.WriteLine("{0, -35} {1}", c.CompanyName, c.Date.ToShortDateString());
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            foreach (var number in DataLoader.NumbersC.TakeWhile(n => n < 6))
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            foreach (var number in DataLoader.NumbersC.SkipWhile(n => n % 3 != 0).Skip(1))
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            var products = from p in DataLoader.LoadProducts()
                           orderby p.ProductName
                           select p;
            PrintProductInformation(products);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            var products = from p in DataLoader.LoadProducts()
                           orderby p.UnitsInStock descending
                           select p;
            PrintProductInformation(products);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            var products = from p in DataLoader.LoadProducts()
                           orderby p.Category, p.UnitPrice descending
                           select p;
            PrintProductInformation(products);
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            foreach (var number in DataLoader.NumbersB.Reverse())
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            foreach (var group in DataLoader.LoadProducts().GroupBy(p => p.Category))
            {
                Console.WriteLine(group.Key.ToUpper());
                foreach (var product in group)
                {
                    Console.WriteLine($"\t{product.ProductName}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void Exercise21()
        {
            foreach (var customer in DataLoader.LoadCustomers())
            {
                Console.WriteLine(customer.CompanyName);

                var years = from o in customer.Orders
                             orderby o.OrderDate.Month
                             group o by o.OrderDate.Year;
                foreach (var year in years)
                {
                    Console.WriteLine(year.Key);
                    foreach (var order in year)
                    {
                        Console.WriteLine("\t{0} - ${1, -10}", order.OrderDate.Month, order.Total);
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            var categories = from c in DataLoader.LoadProducts()
                             group c by c.Category;
            foreach (var category in categories)
            {
                Console.WriteLine(category.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            var item = from p in DataLoader.LoadProducts()
                       where p.ProductID == 789
                       select p;
            Console.WriteLine(item.FirstOrDefault() == null ? "Does not {0}" : "Does {0}", "exist");
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            var stockNeeded = from p in DataLoader.LoadProducts()
                              where p.UnitsInStock == 0
                              group p by p.Category;
            foreach (var category in stockNeeded)
            {
                Console.WriteLine(category.Key);
            }
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            var stockNeeded = from p in DataLoader.LoadProducts()
                              orderby p.Category, p.UnitsInStock
                              group p by p.Category into category
                              where category.First().UnitsInStock > 0
                              select category;
            foreach (var category in stockNeeded)
            {
                Console.WriteLine(category.Key);
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            var odds = from n in DataLoader.NumbersA
                       where n % 2 == 1
                       select n;
            Console.WriteLine($"There are {odds.Count()} odd numbers in NumbersA");
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            var data = from c in DataLoader.LoadCustomers()
                       select new { c.CustomerID, OrderCount = c.Orders.Count() };
            foreach (var item in data)
            {
                Console.WriteLine("ID: {0, -7} OrderCount: {1}", item.CustomerID, item.OrderCount);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            var categories = from p in DataLoader.LoadProducts()
                             group p by p.Category;
            foreach (var category in categories)
            {
                Console.WriteLine("{0, 15}: {1} products", category.Key, category.Count());
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            var categories = from p in DataLoader.LoadProducts()
                             group p by p.Category;
            foreach (var category in categories)
            {
                int stocked = 0;
                foreach (var product in category)
                {
                    stocked += product.UnitsInStock;
                }
                Console.WriteLine("{0, 15}: {1} stock", category.Key, stocked);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            var cheepest = from p in DataLoader.LoadProducts()
                           orderby p.UnitPrice
                           group p by p.Category into category
                           select category.First();
            PrintProductInformation(cheepest);
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            var items = from p in DataLoader.LoadProducts()
                        group p by p.Category into c
                        orderby c.Average(i => i.UnitPrice) descending
                        select c;
            foreach (var item in items.Take(3))
            {
                Console.WriteLine(item.Key);
            }
        }
    }
}
