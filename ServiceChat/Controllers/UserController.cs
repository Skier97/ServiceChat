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
        Dictionary<int, string> messages = new Dictionary<int, string> { { 1, "message1" }, { 2, "message2" }, { 3, "message3"}, { 3, "message4"} };
        List<User> users = new List<User>() { new User(1, "Sergey", "pass1"), new User(2, "Olga", "pass2") };

        // GET api/values
        public IHttpActionResult Get()
        {
            return Json(users);
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            return Json(users[id]);
        }

        // POST api/values
        [HttpPost]
        public void AddMessage(User user, string message)
        {
            messages.Add(user.Id, message);
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
