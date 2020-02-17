using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TD2Sarget.Models
{
    class User
    {
        public Guid Id { get; set; }
        public string Email {get; set;}
        public string Password {get; set;}

        public ICommand LogInCommand { get; internal set; }

        public User() { }

        public User(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
        }
    }

}
