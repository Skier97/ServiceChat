using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using ServiceChat.Models;

namespace ServiceChat.Controllers
{
    public class WebChatController : ApiController
    {
            //В веб конфиге разобраться с путями, задавать относитльный путь до дб
        List<Message> messages;
        List<User> users;
        FileDb db = new FileDb();

        [HttpGet]
        public IHttpActionResult Get()
        {
            users = db.ReadUserFromDb();
            return Json(users);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            users = db.ReadUserFromDb();
            return Json(users[id - 1]);
        }

        [HttpPost]
        public void AddMessage(Message mess)
        {
            messages = db.ReadMessFromDb();
            messages.Add(mess);
            db.UpdateDbMess(messages);
        }

        [HttpGet]
        public IHttpActionResult IsUser(int id, string password)
        {
            User tmpUser = null;
            users = db.ReadUserFromDb();
            messages = db.ReadMessFromDb();
            for (int i = 0; i < users.Count; i++)
            {
                if ((users[i].Id == id) && (users[i].Password == password))
                {
                    tmpUser = users[i];
                    
                }
            }
            return Json(GetAllMessageUser(tmpUser, messages));
        }

        private List<Message> GetAllMessageUser(User user, List<Message> messages)
        {
            var tmpListMess = new List<Message>();
            for (int i=0; i<messages.Count; i++)
            {
                if(messages[i].IdRecip == user.Id)
                {
                    messages[i].IsRead = true;
                    tmpListMess.Add(messages[i]);
                }
            }
            db.UpdateDbMess(messages);
            return tmpListMess;
        }
    }
}
