namespace Core
{
    public class Department
    {
        public int ID_Department { get; set; }
        public string Name { get; set; }

        public Department()
        {

        }

        public Department(int id, string name)
        {
            ID_Department = id;
            Name = name;
        }
    }
}
