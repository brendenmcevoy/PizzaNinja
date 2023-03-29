namespace PN.Logic
{
    public class Employee
    {
        public int Id { get;  set; } // Id made by the DB (Primary Key)
        public string Name { get;  set; } // Name of the Employee
        public bool IsAdmin { get;  set; } // States whether the Employee is an Admin (True) or not (False)
        public string Username { get;  set; }   // Employee username
        public string Password { get;  set; }    // Employee password
    }
}