using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceChat.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace ServiceChat
{
    interface IDbWork
    {
        void UpdateDbMess(List<Message> messages);
        void UpdateDbUsers(List<User> users);
        List<Message> ReadMessFromDb(); 
        List<User> ReadUserFromDb();
    }
}
