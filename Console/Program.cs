using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Core;

namespace ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var command = Console.ReadLine();
                if (command.Trim().ToLower() == "exit")
                    return;
                Execute(command);
            }
        }
        private static void Execute(string command)
        {
            var arguments = command.Trim().Split();
            switch (arguments[0])
            {
                case "add":
                    Add(arguments);
                    break;
                case "get":
                    Get(arguments);
                    break;
                case "getall":
                    GetAll(arguments);
                    break;
                case "update":
                    Update(arguments);
                    break;
                case "delete":
                    Delete(arguments);
                    break;
                case "take":
                    Take(arguments);
                    break;
                case "return":
                    Return(arguments);
                    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }

        }

        private static void Add(string[] args)
        {
            switch (args[1])
            {
                case "author":
                    DataAccess.AddAuthor
                    (
                        new Author()
                        {
                            Surname = args[2],
                            Name = args[3],
                        }
                    );
                    break;
                case "book":
                    DataAccess.AddBook(new Book()
                    {
                        Name = args[2],
                        Year = Convert.ToInt32(args[3]),
                        ID_Author = Convert.ToInt32(args[4]),
                        ID_Department = Convert.ToInt32(args[5]),
                        Quantity = Convert.ToInt32(args[6])
                    });
                    break;
                case "department":
                    DataAccess.AddDepartment
                    (
                        new Department()
                        {
                            Name = args[2],
                        }
                    );
                    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }
        }
        private static void Get(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    var b = DataAccess.GetBook(Convert.ToInt32(args[2]));
                    Console.WriteLine($"{b.ID_Book} {b.Name} " +
                        $"{DataAccess.connection.Query<Author>(@$"select Surname, Name 
                                                                    from Author   
                                                                    where ID_Author = {b.ID_Author}").FirstOrDefault().Surname} " +
                        $"{DataAccess.connection.Query<Department>(@$"select Name 
                                                                    from Department   
                                                                    where ID_Department = {b.ID_Department}").FirstOrDefault().Name} " +
                        $"{b.Quantity}"
                             );

                    break;
                case "department":
                    var d = DataAccess.GetDepartment(Convert.ToInt32(args[2]));
                    Console.WriteLine($"{d.ID_Department} {d.Name}");
                    break;
                case "author":
                    var a = DataAccess.GetAuthor(Convert.ToInt32(args[2]));
                    Console.WriteLine($"{a.ID_Author} {a.Surname} {a.Name}");
                    break;
            }
        }
        private static void GetAll(string[] args)
        {
            switch (args[1])
            {
                case "books":
                    foreach (var a in DataAccess.GetAllBooks())
                        Console.WriteLine($"{a.ID_Book} {a.Name} " +
                            $"{DataAccess.connection.Query<Author>(@$"select Surname, Name 
                                                                      from Author   
                                                                      where ID_Author = {a.ID_Author}").FirstOrDefault().Surname} " +
                            $"{DataAccess.connection.Query<Department>(@$"select Name 
                                                                      from Department   
                                                                      where ID_Department = {a.ID_Department}").FirstOrDefault().Name} " +
                            $"{a.Quantity}"
                             );
                    break;
                case "departments":
                    foreach (var a in DataAccess.GetAllDepartments())
                        Console.WriteLine($"{a.ID_Department} {a.Name}");
                    break;
                case "authors":
                    foreach (var a in DataAccess.GetAllAuthors())
                        Console.WriteLine($"{a.ID_Author} {a.Surname} {a.Name}");
                    break;
                case "mybooks":
                    foreach (var m in DataAccess.GetMyBooks())
                        Console.WriteLine($"{m.ID_Book} {m.Name} " +
                            $"{DataAccess.connection.Query<Author>(@$"select Surname, Name 
                                                                    from Author   
                                                                    where ID_Author = {m.ID_Author}").FirstOrDefault().Surname} " +
                            $"{DataAccess.connection.Query<Department>(@$"select Name 
                                                                    from Department   
                                                                    where ID_Department = {m.ID_Department}").FirstOrDefault().Name} " +
                            $"{m.Quantity}"
                                 );
                    break;
                default:
                    Console.WriteLine($"Unknown command");
                    break;
            }
        }
        private static void Delete(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.DeleteBook(int.Parse(args[2]));
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
        private static void Update(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.UpdateBook(int.Parse(args[2]), new Book()
                    {
                        Name = args[3],
                        Year = Convert.ToInt32(args[4]),
                        ID_Author = Convert.ToInt32(args[5]),
                        ID_Department = Convert.ToInt32(args[6]),
                        Quantity = Convert.ToInt32(args[7])
                    });
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
        private static void Take(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.TakeBook(int.Parse(args[2]));
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

        private static void Return(string[] args)
        {
            switch (args[1])
            {
                case "book":
                    DataAccess.ReturnBook(int.Parse(args[2]));
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
