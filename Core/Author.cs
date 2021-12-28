namespace Core
{
    public class Author
    {
        public int ID_Author { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }

        public Author()
        {

        }

        public Author(int id, string surname, string name)
        {
            ID_Author = id;
            Surname = surname;
            Name = name;
        }
    }
}
