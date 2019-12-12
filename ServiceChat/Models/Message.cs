using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.Models
{
    [Table("Messages")]
    public class Message 
    {
        [Key]
        public string IdMessage { get; set; }
        public int IdSend { get; set; }
        public int IdRecip { get; set; }
        public string TextMessage { get; set; }
        public DateTime Time { get; set; }
        public int IsRead { get; set; }

/*        public Message(int idMessage, int idSend, int idRecip, string message)
        {
            this.IdMessage = idMessage;
            this.IdSend = idSend;
            this.IdRecip = idRecip;
            this.TextMessage = message;
            this.Time = DateTime.Now;
            this.IsRead = 0;
        }*/
    }
}