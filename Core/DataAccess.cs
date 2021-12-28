using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace Core
{
    public static class DataAccess
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        public static IDbConnection connection = new SqlConnection(connStr);
        public static List<Book> GetAllBooks()
        {
            try { return connection.Query<Book>("select * from Book").AsList(); }
            catch { return null; }
        }
        public static List<Book> GetMyBooks()
        {
            try { return connection.Query<Book>("select * from Book where [ID_Book] IN (select ID_Book from Ticket)").AsList(); }
            catch { return null; }
        }
        public static List<Department> GetAllDepartments()
        {
            try { return connection.Query<Department>("select * from Department").AsList(); }
            catch { return null; }
        }
        public static List<Author> GetAllAuthors()
        {
            try { return connection.Query<Author>("select * from Author").AsList(); }
            catch { return null; }
        }
        public static Book GetBook(int id)
        {
            try { return connection.Query<Book>($"select * from Book where ID_Book = {id}").FirstOrDefault(); }
            catch { return null; }
        }
        public static Author GetAuthor(int id)
        {
            try { return connection.Query<Author>($"select * from Author where ID_Author = {id}").FirstOrDefault(); }
            catch { return null; }
        }
        public static Department GetDepartment(int id)
        {
            try { return connection.Query<Department>($"select * from Department where ID_Department = {id}").FirstOrDefault(); }
            catch { return null; }
        }

        public static void AddDepartment(Department d)
        {
            connection.Query($"insert into [dbo].[Department] (Name) values ('{d.Name}')");
        }
        public static void AddBook(Book b)
        {
            connection.Query($"insert into [dbo].[Book] (Name, Year, ID_Author, ID_Department, Quantity) values ('{b.Name}', {b.Year}, {b.ID_Author}, {b.ID_Department}, {b.Quantity})");
        }
        public static void AddAuthor(Author a)
        {
            connection.Query($"insert into [dbo].[Author] (Surname, Name) values ('{a.Surname}', '{a.Name}')");
        }
        public static void DeleteBook(int id)
        {
            connection.Query($"delete from [dbo].[Ticket] where [ID_Book] = {id}");
            connection.Query($"delete from [dbo].[Book] where [ID_Book] = {id}");
        }
        public static void UpdateBook(int id, Book b)
        {
            connection.Query(@$"update [dbo].[Book] 
                                set [Name] = '{b.Name}'
                                set [Year] = {b.Year}
                                set [ID_Author] = '{b.ID_Author}'
                                set [ID_Department] = {b.ID_Department}
                                set [Quantity] = {b.Quantity}
                                where [ID_Book] = {id}");
        }
        public static void TakeBook(int id)
        {
            connection.Query($"insert into [dbo].[Ticket] (ID_Book, ID_User) values ({id}, 1)");
            connection.Query($"update [dbo].[Book] set [Quantity] = (select Quantity from Book where ID_Book = {id}) - 1");
        }

        public static void ReturnBook(int id)
        {
            connection.Query($"delete from [dbo].[Ticket] where [ID_Book] = {id}");
            connection.Query($"update [dbo].[Book] set [Quantity] = (select Quantity from Book where ID_Book = {id}) + 1");
        }
    }
}
