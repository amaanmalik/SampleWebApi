﻿using System;

namespace Sample.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
