namespace PN.Logic
{
    public class Employee
    {
        private int Id { get; set; }
        public int _employeeId { get; private set; }
        private string Name { get; set; }
        public string _name { get; private set; }
        private bool IsAdmin { get; set; }
        private string Username { get; set; }   
        private string Password { get; set; }   
        public Employee(int id, string name, bool isAdmin, string username, string password)
        {
            Id = _employeeId = id;
            Name = _name = name;
            IsAdmin = false;
            Username = username;
            Password = password;
        }
    }
}