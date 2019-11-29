﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceChat.Models
{
    public class Message
    {
        public int IdSend { get; set; }
        //public string NameSend { get; set; }
        public int IdRecip { get; set; }
        public string TextMessage { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }

        public Message(int idSend, int idRecip, string message)
        {
            this.IdSend = idSend;
            //this.NameSend = nameSend;
            this.IdRecip = idRecip;
            this.TextMessage = message;
            this.Time = DateTime.Now;
            this.IsRead = false;
        }
    }
}