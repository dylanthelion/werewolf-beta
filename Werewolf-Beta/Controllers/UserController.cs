using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Werewolf_Beta.Helpers;
using Werewolf_Beta.Models;

namespace Werewolf_Beta.Controllers
{
    public class UserController : ApiController
    {
        WerewolfContext db = new WerewolfContext();

        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {
            Tuple<bool, HttpResponseMessage> validator = ControllerCRUDValidators.ValidateCreateUser(user);
            if(!validator.Item1)
            {
                return validator.Item2;
            }

            user.Experience = 0;
            user.Level = 1;
            user.Tokens = 0;

            db.AllUsers.Add(user);
            db.SaveChanges();
            int id = (from check in db.AllUsers
                      where check.UserName == user.UserName
                      select check.ID).FirstOrDefault();

            return new HttpResponseMessage
            {
                Content = new StringContent(JArray.FromObject(new List<String>() { String.Format("User successfully created! Your user ID: {0}", id) }).ToString(), Encoding.UTF8, "application/json")
            };
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUser(string password, int id)
        {
            User user = db.AllUsers.Find(id);
            if(user == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid user id" }).ToString(), Encoding.UTF8, "application/json")
                };
            }
            if(user.Password != password)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid password" }).ToString(), Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage
            {
                Content = new StringContent(JArray.FromObject(new List<User>() { user }).ToString(), Encoding.UTF8, "application/json")
            };
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage EditUser([FromBody] User newUser)
        {
            Tuple<bool, HttpResponseMessage> validator = ControllerCRUDValidators.ValidateEditUser(newUser);
            if(!validator.Item1)
            {
                return validator.Item2;
            }
            User oldUser = db.AllUsers.Find(newUser.ID);
            if(newUser.Experience != 0)
            {
                oldUser.Experience = newUser.Experience;
            }
            if (newUser.FBLoginToken != null)
            {
                oldUser.FBLoginToken = newUser.FBLoginToken;
            }
            if (newUser.GoogleLoginToken != null)
            {
                oldUser.GoogleLoginToken = newUser.GoogleLoginToken;
            }
            if (newUser.Level != 0)
            {
                oldUser.Level = newUser.Level;
            }
            if (newUser.NemesisID != 0)
            {
                oldUser.NemesisID = newUser.NemesisID;
            }
            if (newUser.Tokens != 0)
            {
                oldUser.Tokens = newUser.Tokens;
            }
            if (newUser.UserName != null)
            {
                oldUser.UserName = newUser.UserName;
            }
            db.Entry(oldUser).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpResponseMessage
            {
                Content = new StringContent(JArray.FromObject(new List<String>() { String.Format("User successfully updated! Your user ID: {0}", oldUser.ID) }).ToString(), Encoding.UTF8, "application/json")
            };
        }

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage DeleteUser(int id, string password, string username)
        {
            Tuple<bool, HttpResponseMessage> validator = ControllerCRUDValidators.ValidateNameAndPassword(id, username, password);
            if(!validator.Item1)
            {
                return validator.Item2;
            }
            User deleted = db.AllUsers.Find(id);
            db.AllUsers.Remove(deleted);
            db.Entry(deleted).State = EntityState.Deleted;
            db.SaveChanges();

            return new HttpResponseMessage
            {
                Content = new StringContent(JArray.FromObject(new List<String>() { "User successfully deleted!" }).ToString(), Encoding.UTF8, "application/json")
            };
        }
    }
}