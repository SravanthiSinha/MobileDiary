using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inrhythm.MobileDiary.Logs;
using Inrhythm.MobileDiary.BAL.BusinessRepositeries;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using System.Web;

namespace MobileDiary.Controllers
{
    public class NoteController : ApiController
    {
        public bool Get(string note, string journalId)
        {
            try
            {
                bool status = false;
                JournalRepository jr = new JournalRepository();
                Entry entry = new Entry();
                entry.Notes = note;
                if (journalId == null)
                    entry.JournalId = jr.GetMaxId();
                else
                {
                    entry.JournalId = int.Parse(journalId);
                }
                entry.Time = DateTime.Now;
                EntryRepository repository = new EntryRepository();
                if (repository.Create(entry))
                {
                    EntryJournalRepository repo = new EntryJournalRepository();

                    if (repo.Create(repository.GetMaxId(), int.Parse(HttpContext.Current.Session["User_Id"].ToString()), entry.JournalId))
                        return true;
                }
                return status;
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
