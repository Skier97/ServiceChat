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
    public class UserController : ApiController
    {
        static List<Message> messages = new List<Message>() { new Message(1, 2, "mess1"), new Message(2, 1, "mess2"), new Message(2, 1, "mess3") };
        static List<User> users = new List<User>() { new User(1, "Sergey", "pass1"), new User(2, "Olga", "pass2") };

        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(users);
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            return Json(users[id - 1]);
        }

        // POST api/values
        [HttpPost]
        public void AddMessage(Message mess)
        {
            messages.Add(mess);
        }

        [HttpGet]
        public IHttpActionResult IsUser(int id, string password)
        {
            User tmpUser = null;
            for (int i = 0; i < users.Count; i++)
            {
                if ((users[i].Id == id) && (users[i].Password == password))
                {
                    tmpUser = users[i];
                    
                }
            }
            return Json(GetMessageUser(tmpUser, messages));
        }

        public List<Message> GetMessageUser(User user, List<Message> messages)
        {
            var tmpListMess = new List<Message>();
            for (int i=0; i<messages.Count; i++)
            {
                if((messages[i].IdRecip == user.Id) && (messages[i].IsRead == false))
                {
                    messages[i].IsRead = true;
                    tmpListMess.Add(messages[i]);
                }
            }
            return tmpListMess;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
