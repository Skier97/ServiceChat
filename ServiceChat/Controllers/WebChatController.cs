using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using ServiceChat.Models;
using System.Web.Http.Cors;
using System.Data.SqlClient;
using System.Data.Entity;

namespace ServiceChat.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebChatController : ApiController
    {
        ChatContext dbChat = new ChatContext();
            //В веб конфиге разобраться с путями, задавать относитльный путь до дб

        FileDb db = new FileDb();

        [HttpGet]
        public int GetIdNewUser()
        {
            var users = dbChat.Users;
            int number = 1;

            while(SearchNewNumber(users, number) == true)
            {
                number++;
            }

            return number;
        }


        [HttpPost]
        public void AddMessage(Message mess)
        {
            if (mess.IdMessage != null)
            {
                dbChat.Messages.Add(mess);
                dbChat.SaveChanges();
            }
        }

        [HttpPost]
        public void AddUser(User user)
        {
            dbChat.Users.Add(user);
            dbChat.SaveChanges();
        }

        [HttpGet]
        public IHttpActionResult EnterUser(int id, string password)
        {
            //User tmpUser = null;
            var users = dbChat.Users;

            foreach (User u in users)
            {
                if ((u.IdUser == id)&&(u.Password.Replace(" ", "") == password))
                {
                    return Json(u.NameUser.Replace(" ",""));
                }
            }
            return Json(0);
        }

        [HttpGet]
        public IHttpActionResult IsUser(int id, string password, bool flagEnterUser)
        {
            if (flagEnterUser == true)
            {
                User tmpUser = null;
                var users = dbChat.Users;
                var messages = dbChat.Messages;

                foreach (User u in users)
                {
                    if ((u.IdUser == id) && (u.Password.Replace(" ","") == password))//Replace, потому что длина паролей у всех разное
                    {                                                                    //и остаются пробелы тогда в ячейке из БД
                        tmpUser = u;
                        
                    }
                }
                return Json(GetNewMessageUser(tmpUser, messages));//ответ - получение непрочитанных сообщений юзера
            }
            return Json(HttpStatusCode.BadRequest);
            
        }

        private List<InfoMessages> GetNewMessageUser(User user, DbSet<Message> messages)
        {
            var tmpListMess = new List<InfoMessages>();
            
            foreach (Message m in messages)
            {
                if ((m.IdRecip == user.IdUser) && (m.IsRead == 0))
                {
                    m.IsRead = 1;
                    var message = new InfoMessages();

                    message.IdSend = m.IdSend;
                    message.TextMessage = m.TextMessage;
                    message.Time = m.Time;

                    tmpListMess.Add(message);

                }
            }
            GetUserName(ref tmpListMess);
            dbChat.SaveChanges();
            return tmpListMess;
        }

        private void GetUserName(ref List<InfoMessages> listMess)
        {
            for (int i = 0; i < listMess.Count; i++)
            {
                var mess = listMess[i];
                var user = from us in dbChat.Users
                               where us.IdUser == mess.IdSend
                               select us;
                var nameUser = user.FirstOrDefault<User>().NameUser.Replace(" ", "");
                listMess[i].NameSend = nameUser;
            }

        }

        private bool SearchNewNumber(DbSet<User> users, int number)
        {
            //bool flagSearch = false;
            foreach(User us in users)
            {
                if(us.IdUser == number)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
