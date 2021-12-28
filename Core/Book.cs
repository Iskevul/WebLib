namespace Core
{
    public class Book
    {
        public int ID_Book { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int ID_Author { get; set; }
        public int ID_Department { get; set; }
        public int Quantity { get; set; }

        public Book()
        {

        }

        public Book(string name, int year, int id_author, int id_department, int quantity)
        {
            Name = name;
            Year = year;
            ID_Author = id_author;
            ID_Department = id_department;
            Quantity = quantity;
        }
    }
}