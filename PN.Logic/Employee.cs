namespace PN.Logic
{
    public class Employee
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private bool IsAdmin { get; set; }
        public Employee(int id, string name, bool isAdmin)
        {
            Id = id;
            Name = name;
            IsAdmin = false;
        }
    }
}