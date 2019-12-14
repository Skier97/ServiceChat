using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//без нее не получается добавить пользователя
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }


    }
}