using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using Werewolf_Beta.Models;

namespace Werewolf_Beta.Helpers
{
    public static class ControllerCRUDValidators
    {
        public static Tuple<bool, HttpResponseMessage> ValidateCreateUser(User _user)
        {
            WerewolfContext db = new WerewolfContext();
            Console.WriteLine(_user);
            if(_user.UserName == null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Please include a username in your request" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            var checkForUniqueName = (from check in db.AllUsers
                                      where _user.UserName == check.UserName
                                      select check).FirstOrDefault();
            if(checkForUniqueName != null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "That  username is already taken." }).ToString(), Encoding.UTF8, "application/json")
                });
            }

            if(_user.UserName.Length < 6 || _user.UserName.Length > 20)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "User name must be no less than 6 and no greater than 20 characters long." }).ToString(), Encoding.UTF8, "application/json")
                });
            }

            if(_user.Password == null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Please include a password in your request." }).ToString(), Encoding.UTF8, "application/json")
                });
            }

            if (_user.Password.Length < 6 || _user.Password.Length > 32)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "User name must be no less than 6 and no greater than 32 characters long." }).ToString(), Encoding.UTF8, "application/json")
                });
            }

            return new Tuple<bool, HttpResponseMessage>(true, new HttpResponseMessage());
        }

        public static Tuple<bool, HttpResponseMessage> ValidateEditUser(User _user)
        {
            WerewolfContext db = new WerewolfContext();
            User checkUser = db.AllUsers.Find(_user.ID);
            if (checkUser == null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid user ID" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            if (checkUser.Password != _user.Password)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid password" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            var checkForUniqueName = (from check in db.AllUsers
                                      where _user.UserName == check.UserName
                                      select check).FirstOrDefault();
            if (checkForUniqueName != null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "That  username is already taken." }).ToString(), Encoding.UTF8, "application/json")
                });
            }

            if (_user.UserName.Length < 6 || _user.UserName.Length > 20)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "User name must be no less than 6 and no greater than 20 characters long." }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            return new Tuple<bool, HttpResponseMessage>(true, new HttpResponseMessage());
        }

        public static Tuple<bool, HttpResponseMessage> ValidateNameAndPassword(int id, string name, string password)
        {
            WerewolfContext db = new WerewolfContext();
            User checkUser = db.AllUsers.Find(id);
            if(checkUser == null)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid user ID" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            if(checkUser.Password != password)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid password" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            if(checkUser.UserName != name)
            {
                return new Tuple<bool, HttpResponseMessage>(false, new HttpResponseMessage()
                {
                    Content = new StringContent(JArray.FromObject(new List<String>() { "Invalid username" }).ToString(), Encoding.UTF8, "application/json")
                });
            }
            return new Tuple<bool, HttpResponseMessage>(true, new HttpResponseMessage());
        }
    }
}