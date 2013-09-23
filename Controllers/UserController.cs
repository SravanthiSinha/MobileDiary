using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.BAL.BusinessRepositeries;
using System.Web.SessionState;
using System.Web;
using Inrhythm.MobileDiary.Logs;
namespace MobileDiary.Controllers
{
    public class UserController : ApiController
    {
        public User Get()
        {
            try
            {
                User user = new User();
                UserRepository user_rep = new UserRepository();
                user.UserId = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                return user_rep.GetUserDetails(user);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }

        public string Get(string email, string password, string mobile)
        {
            try
            {
                User user = new User();
                UserRepository user_rep = new UserRepository();
                string message = "";
                user.UserId = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                User newuser = user_rep.GetUserDetails(user);
                newuser.EmailId = email;
                newuser.Password = password;
                newuser.MobileNo = mobile;

                if (user.Validate())
                {

                    if (user_rep.Update(newuser))
                    {

                        message = "Updated";
                    }
                }
                else
                {
                    foreach (string errorMessage in user.ValidationErrorsList)
                    {
                        message += (errorMessage) + "\n";
                    }

                }
                return message;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }


        public bool Get(string password)
        {
            try
            {
                User user = new User();
                UserRepository user_rep = new UserRepository();
                user.UserId = int.Parse(HttpContext.Current.Session["User_Id"].ToString());

                User newuser = user_rep.GetUserDetails(user);
                newuser.Password = password;
                return user_rep.Authenticate(newuser);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public bool Get(string name, string password)
        {
            try
            {
                User user = new User();
                UserRepository user_rep = new UserRepository();
                user.Name = name;

                user.Password = password;
                return user_rep.Exists(user);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public string Get(string name, string email, string password, string mobileno)
        {
            try
            {
                User user = new User();
                UserRepository user_rep = new UserRepository();
                string message = "";
                user.Name = name;
                user.EmailId = email;
                user.Password = password;
                user.MobileNo = mobileno;
                if (user.Validate())
                {

                    if (user_rep.Create(user))
                    {
                        HttpContext.Current.Session.Add("IsValidSession", true);

                        HttpContext.Current.Session.Add("User_Id", user_rep.GetUserId(user));
                        message = "Inserted";
                    }
                }
                else
                {
                    foreach (string errorMessage in user.ValidationErrorsList)
                    {
                        message += (errorMessage) + "\n";
                    }

                }
                return message;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }
    }
}
