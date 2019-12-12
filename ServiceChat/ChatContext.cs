using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ServiceChat.Models;

namespace ServiceChat
{
    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("DBConnection")
        { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}