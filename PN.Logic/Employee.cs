namespace PN.Logic
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool IsAdmin { get; private set; }
        public string Username { get; private set; }   
        public string Password { get; private set; }   
        public Employee(int id, string name, bool isAdmin, string username, string password)
        {
            Id =  id;
            Name = name;
            IsAdmin = isAdmin;
            Username = username;
            Password = password;
        }
    }
}