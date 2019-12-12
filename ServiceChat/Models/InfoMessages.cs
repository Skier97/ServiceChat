using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceChat.Models
{
    public class InfoMessages
    {
        public string NameSend { get; set; }
        public int IdSend { get; set; }
        //public int IdRecip { get; set; }
        public string TextMessage { get; set; }
        public DateTime Time { get; set; }
        //public int IsRead { get; set; }
    }
}