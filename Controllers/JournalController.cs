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
using System.Collections;
using Inrhythm.MobileDiary.Logs;

namespace MobileDiary.Controllers
{
    public class JournalController : ApiController
    {
        public List<Journal> Get()
        {
            try
            {
                Journal journal = new Journal();
                JournalRepository jour_rep = new JournalRepository();                
                int id = int.Parse(HttpContext.Current.Session["User_Id"].ToString());
                return jour_rep.GetMonthJournals(id);
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
