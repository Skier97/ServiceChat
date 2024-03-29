﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceChat.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ServiceChat
{
    public class FileDb:IDbWork
    {
        public void UpdateDbMess(List<Message> messages)
        {
            string jsonTasks = JsonConvert.SerializeObject(messages);
            if (File.Exists(WebConfigurationManager.AppSettings["WayToDBMess"]) == true)
            {
                using (StreamWriter sw = new StreamWriter(WebConfigurationManager.AppSettings["WayToDBMess"], false))
                {
                    sw.WriteLine(jsonTasks);
                }
            }
            
        }

        public void UpdateDbUsers(List<User> users)
        {
            string jsonTasks = JsonConvert.SerializeObject(users);
            if (File.Exists(WebConfigurationManager.AppSettings["WayToDBUser"]) == true)
            {
                using (StreamWriter sw = new StreamWriter(WebConfigurationManager.AppSettings["WayToDBUser"], false))
                {
                    sw.WriteLine(jsonTasks);
                }
            }
                
        }

        public List<Message> ReadMessFromDb()
        {
            var colMess = new List<Message>();
            using (var sr = new StreamReader(WebConfigurationManager.AppSettings["WayToDBMess"]))
            {
                string line = sr.ReadLine();

                colMess = JsonConvert.DeserializeObject<List<Message>>(line);
            }
            return colMess;
        }

        public List<User> ReadUserFromDb()
        {
            var colUser = new List<User>();
            using (var sr = new StreamReader(WebConfigurationManager.AppSettings["WayToDBUser"]))
            {
                string line = sr.ReadLine();

                colUser = JsonConvert.DeserializeObject<List<User>>(line);
            }
            return colUser;
        }
    }
}