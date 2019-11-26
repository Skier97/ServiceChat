using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }

        public User(int id, string nameUser, string password)
        {
            this.Id = id;
            this.NameUser = nameUser;
            this.Password = password;
        }
    }
}