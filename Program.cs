using System;
using NLog.Web;
using System.IO;
using System.Linq;
using Northwind_Console.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Northwind_Console
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                string choice;
                do
                {
                    Console.WriteLine("1) Display Categories"); //B DONE
                    Console.WriteLine("2) Add Category"); //B DONE
                    Console.WriteLine("3) Display Category and related products"); //B DONE
                    Console.WriteLine("4) Display all Categories and their related products"); //B DONE
                    Console.WriteLine("5) Edit Record from Category"); //B DONE
                    Console.WriteLine("6) Display all Products"); //C DONE
                    Console.WriteLine("7) Add Product");  //C DONE
                    Console.WriteLine("8) Edit Product"); //C DONE
                    Console.WriteLine("9) Search Product"); //C DONE
                    Console.WriteLine("10) Delete Record from Category"); //A DONE
                    Console.WriteLine("11) Delete Record from Product"); //A DONE
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();
                    logger.Info($"Option {choice} selected");
                    if (choice == "1")
                    {
                        var db = new Northwind22BTNContext();
                        var query = db.Categories.OrderBy(p => p.CategoryName);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{query.Count()} records returned");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryName} - {item.Description}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (choice == "2")
                    {
                        var db = new Northwind22BTNContext();
                        Category category = new Category();

                        Console.WriteLine("Category Name: ");
                        category.CategoryName = Console.ReadLine();

                        Console.WriteLine("Category Description: ");
                        category.Description = Console.ReadLine();

                        db.AddCategory(category);
                    }
                    else if (choice == "3")
                    {
                        var db = new Northwind22BTNContext();
                        var query = db.Categories.OrderBy(p => p.CategoryId);

                        Console.WriteLine("Select the category whose products you want to display:");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        int id = int.Parse(Console.ReadLine());
                        Console.Clear();
                        logger.Info($"CategoryId {id} selected");
                        Category category = db.Categories.Include("Products").FirstOrDefault(c => c.CategoryId == id);
                        Console.WriteLine($"{category.CategoryName} - {category.Description}");
                        foreach (Product p in category.Products)
                        {
                            Console.WriteLine(p.ProductName);
                        }
                    }
                    else if (choice == "4")
                    {
                        var db = new Northwind22BTNContext();
                        var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryName}");
                            foreach (Product p in item.Products)
                            {
                                Console.WriteLine($"\t{p.ProductName}");
                            }
                        }
                    }
                    else if (choice == "5")
                    {
                        var db = new Northwind22BTNContext();
                        Category category = new Category();

                        Console.WriteLine("Edit Category with Id: ");
                        category.CategoryId = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new cat name: ");
                        category.CategoryName = Console.ReadLine();

                        Console.WriteLine("Enter new cat desc: ");
                        category.Description = Console.ReadLine();

                        Console.WriteLine("You have changed the Categgory");

                        db.EditCategory(category);
                    }
                    else if (choice == "6")
                    {
                        var db = new Northwind22BTNContext();
                        var products = db.Products.OrderBy(p => p.ProductId);
                        foreach(Product p in products) {
                            Console.WriteLine($"{p.ProductId}: {p.ProductName}");
                            }
                    }
                    else if(choice == "7")
                    {
                        var db = new Northwind22BTNContext();
                        Product product = new Product();
                        Console.WriteLine("Enter Product Name: ");
                        product.ProductName = Console.ReadLine();

                        Console.WriteLine("Enter Supplier ID: ");
                        product.SupplierId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Category ID: ");
                        product.CategoryId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Quantity Per Unit ");
                        product.QuantityPerUnit = Console.ReadLine();

                        Console.WriteLine("Enter Unit Price: ");
                        product.UnitPrice = Decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Unit Stock: ");
                        product.UnitsInStock = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Units Order: ");
                        product.UnitsOnOrder = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Reorder Level: ");
                        product.ReorderLevel = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter Discountinued: ");
                        product.Discontinued = Boolean.Parse(Console.ReadLine());

                        db.AddProduct(product);

                        Console.WriteLine("Product has been Added");
                    }
                    else if(choice =="8")
                    {
                        var db = new Northwind22BTNContext();
                        Product product = new Product();
                        Console.WriteLine("Enter Product by ID: ");
                        product.ProductId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Product Name: ");
                        product.ProductName = Console.ReadLine();

                        Console.WriteLine("Enter New Supplier ID: ");
                        product.SupplierId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Category ID: ");
                        product.CategoryId = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Quantity Per Unit ");
                        product.QuantityPerUnit = Console.ReadLine();

                        Console.WriteLine("Enter New Unit Price: ");
                        product.UnitPrice = Decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Unit Stock: ");
                        product.UnitsInStock = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Units Order: ");
                        product.UnitsOnOrder = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Reorder Level: ");
                        product.ReorderLevel = (short)Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter New Discountinued: ");
                        product.Discontinued = Boolean.Parse(Console.ReadLine());

                        db.EditProduct(product);

                        Console.WriteLine("Product has been Changed");
                    }
                    else if(choice == "9")
                    {
                        var db = new Northwind22BTNContext();

                        Console.WriteLine("Search Query");
                        string query = Console.ReadLine();

                        IEnumerable<Product> a = db.Products.ToList().Where(p => query != null && p.ProductName.Contains(query));
                        var qls = new List<Category>();

                        Console.WriteLine(a.Count());
                        foreach (var ql in a)
                        {
                            Console.Write($"Id: {ql.ProductId} Name: {ql.ProductName}\n");
                        }
                        Console.WriteLine("Search Completed");
                    }
                    else if(choice =="10")
                    {
                        var db = new Northwind22BTNContext();
                        Product product = new Product();

                        Console.WriteLine("Enter Product ID to Delete");
                        product.ProductId = Int32.Parse(Console.ReadLine());
                        
                        db.DeleteProduct(product);
                        Console.WriteLine("Delete successful");
                    }
                    else if(choice == "11")
                    {
                        var db = new Northwind22BTNContext();
                        Category category = new Category();

                        Console.WriteLine("Enter Category ID to Delete");
                        category.CategoryId = Int32.Parse(Console.ReadLine());
                        
                        db.DeleteCategory(category);
                        Console.WriteLine("Delete successful");
                    }
                    Console.WriteLine();

                } while (choice.ToLower() != "q");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
