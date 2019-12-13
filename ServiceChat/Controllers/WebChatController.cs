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
        //List<Message> messages;
        //List<User> users;
        FileDb db = new FileDb();

        [HttpGet]
        public IHttpActionResult Get()
        {
            //users = db.ReadUserFromDb();
            var users = dbChat.Users;
            return Json(users);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var users = db.ReadUserFromDb(); //?? Фаил?
            return Json(users[id - 1]);
        }

        [HttpPost]
        public void AddMessage(Message mess)
        {
            //?? На сервере также должны быть проверки, сообщение не пустое например
            dbChat.Messages.Add(mess);
            dbChat.SaveChanges();
        }

        [HttpGet]
        public IHttpActionResult IsUser(int id, string password)
        {
            //User tmpUser = null;
            var users = dbChat.Users;
            //var messages = dbChat.Messages;

            foreach (User u in users)
            {
                if ((u.Id == id)&&(u.Password.Replace(" ", "") == password))
                {
                    return Json(u.NameUser.Replace(" ",""));
                }
            }
            return Json(0); //?? 0?
        }

        [HttpGet]
        public IHttpActionResult IsUser(int id, string password, bool flagUser) //?? Оба используются? Для чего FlagUser? из названия не понятно
        {
            if (flagUser == true)
            {
                User tmpUser = null;
                var users = dbChat.Users;
                var messages = dbChat.Messages;
                /*for (int i = 0; i < users.Count; i++) //?? Старайся ненужный код удалять а не собирать комментарии
                {
                    if ((users[i].Id == id) && (users[i].Password == password))
                    {
                        tmpUser = users[i];
                        return Json(GetNewMessageUser(tmpUser, messages));
                    }
                }*/
                foreach (User u in users)
                {
                    if ((u.Id == id) && (u.Password.Replace(" ","") == password))//?? зачем replace
                    {
                        tmpUser = u;
                        
                    }
                }
                return Json(GetNewMessageUser(tmpUser, messages)); //?? это проверка isUser или получение новых сообщений?
            }
            return Json(0);//?? Почему 0? Лучше статус, типа Бэд реквест или обьект ответа
            
        }

        private List<InfoMessages> GetNewMessageUser(User user, DbSet<Message> messages) //?? GetNewUserMessages
        {
            var tmpListMess = new List<InfoMessages>();
            
            foreach (Message m in messages)
            {
                if ((m.IdRecip == user.Id) && (m.IsRead == 0))
                {
                    m.IsRead = 1;
                    var message = new InfoMessages();
                    
                    //message.NameSend = nameUser.ToString();
                    message.IdSend = m.IdSend;
                    //message.IdRecip = m.IdRecip;
                    //message.IsRead = m.IsRead;
                    message.TextMessage = m.TextMessage;
                    message.Time = m.Time;

                    tmpListMess.Add(message);

                }
            }
            GetNameUser(ref tmpListMess);
            dbChat.SaveChanges();
            return tmpListMess;
        }

        private void GetNameUser(ref List<InfoMessages> listMess) //?? Может лучше GetUserName, звучит привычнее
        {
            for (int i = 0; i < listMess.Count; i++)
            {
                var mess = listMess[i];
                var user = from us in dbChat.Users
                               where us.Id == mess.IdSend
                               select us;
                var nameUser = user.FirstOrDefault<User>().NameUser.Replace(" ", ""); //?? Зачем реплэйс, а если не найдет юзера?
                listMess[i].NameSend = nameUser; //?? Опять же Username?
            }

        }
    }
}
