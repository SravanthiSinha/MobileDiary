using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.BAL.BusinessRepositeries;
using Inrhythm.MobileDiary.BAL;
using System.Web;
using Inrhythm.MobileDiary.Logs;

namespace MobileDiary.Controllers
{
    public class ValuesController : ApiController
    {
        public string Get()
        {
            try
            {
                JournalRepository repository = new JournalRepository();
                return repository.JournalExists(HttpContext.Current.Session["User_Id"].ToString());
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }

        public bool Get(string id, string password)
        {
            try
            {
                User user = new User();
                user.Name = id;
                user.Password = password;

                UserRepository repository = new UserRepository();
                if (repository.Authenticate(user))
                {
                    HttpContext.Current.Session.Add("User_Id", repository.GetUserId(user));
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public bool Get(string jname)
        {
            try
            {
                User user = new User();
                Journal journal = new Journal();
                journal.JournalName = jname;
                journal.JournalDate = DateTime.Now;

                JournalRepository repository = new JournalRepository();
                if (repository.Create(journal))
                {
                    EntryJournalRepository entryJournalRepository = new EntryJournalRepository();
                    if (entryJournalRepository.Create(1, int.Parse(HttpContext.Current.Session["User_Id"].ToString()), repository.GetMaxId()))
                    {
                        HttpContext.Current.Session.Add("Journal_Id", repository.GetMaxId());
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public bool Get(string id, string password, string mobile)
        {
            try
            {
                HttpContext.Current.Session.Abandon();
                return true;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }
    }
}