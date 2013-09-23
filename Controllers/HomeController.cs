using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Inrhythm.MobileDiary.BAL;
using Inrhythm.MobileDiary.BAL.BusinessRepositeries;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using System.Web.Mvc;
using Inrhythm.MobileDiary.Logs;

namespace MobileDiary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
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
