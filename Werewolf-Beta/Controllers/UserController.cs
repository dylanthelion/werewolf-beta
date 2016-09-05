using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    }
}