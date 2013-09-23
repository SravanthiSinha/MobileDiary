using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Inrhythm.MobileDiary.BAL;
using Inrhythm.MobileDiary.BAL.BusinessRepositeries;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.Logs;


namespace MobileDiary.Controllers
{
    public class EntriesController : ApiController
    {

        public IList<Entry> Get()
        {
            try
            {
                return EntryRepository.GetEntries();
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }
        public List<Entry> Get(int year, int month, int day, int dummy)
        {
            try
            {
                int userid = Convert.ToInt32(HttpContext.Current.Session["User_Id"]);
                return EntryRepository.GetEntriesbyDate(userid, year, month, day);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }

        public List<Entry> Get(string jourid)
        {
            try
            {
                Journal journal = new Journal();
                journal.JournalId = int.Parse(jourid);
                EntryRepository repository = new EntryRepository();
                return repository.GetEntriesByJId(journal);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return null;
            }
        }

        public bool Get(string id, string dummy)
        {
            try
            {
                Entry entry = new Entry();
                entry.EntryId = int.Parse(id);
                EntryRepository entry_rep = new EntryRepository();
                return entry_rep.Delete(entry);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Entry entry = new Entry();
                entry.EntryId = id;
                EntryRepository entry_rep = new EntryRepository();
                return entry_rep.Delete(entry);
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
                return false;
            }
        }

        public Entry Get(int id, string EntryId)
        {
            try
            {
                Entry e = new Entry();
                e.EntryId = int.Parse(EntryId);
                EntryRepository repository = new EntryRepository();
                return repository.GetEntryById(e);
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