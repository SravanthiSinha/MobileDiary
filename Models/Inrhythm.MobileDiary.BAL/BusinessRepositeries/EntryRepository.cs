using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Inrhythm.MobileDiary.DAL;
using Inrhythm.MobileDiary.Logs;

using Inrhythm.MobileDiary.BAL.BusinessEntites;

namespace Inrhythm.MobileDiary.BAL.BusinessRepositeries
{
    public class EntryRepository : BusinessRepository
    {

        public override bool Create(BusinessEntity be)
        {
            bool done = false;
            try
            {
                parameters = new Dictionary<string, string>();

                Entry entry = (Entry)be;
                //using stored procedure           

                parameters.Add("@Notes", entry.Notes);
                parameters.Add("@JournalId", entry.JournalId.ToString());
                parameters.Add("@Time ", entry.Time.ToString());

                if (dataAccessEntity.ExecuteSql("InsertEntry", parameters))
                    done = true;


            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;

        }
        public override bool Update(BusinessEntity be)
        {
            parameters = new Dictionary<string, string>();
            bool done = false;
            Entry entry = (Entry)be;
            //using stored procedure    
            try
            {
                parameters.Add("@Entryid", entry.EntryId.ToString());

                parameters.Add("@Notes", entry.Notes);
                parameters.Add("@JournalId", entry.JournalId.ToString());
                parameters.Add("@Time ", entry.Time.ToString());

                if (dataAccessEntity.ExecuteSql("UpdateEntry", parameters))
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;

        }
        public override bool Delete(BusinessEntity be)
        {
            parameters = new Dictionary<string, string>();
            bool done = false;
            Entry entry = (Entry)be;
            //using stored procedure    
            try
            {
                parameters.Add("@Entryid", entry.EntryId.ToString());

                if (dataAccessEntity.ExecuteSql("DeleteEntry", parameters))
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        public Entry GetEntryById(BusinessEntity be)
        {
            Entry entry = (Entry)be;
            Entry newEntry = new Entry();
            //using stored procedure    
            try
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("@Entryid", entry.EntryId.ToString());

                DataSet ds = DataAccessEntity.GetData("GetEntryById", parameters);
                DataTable dt = ds.Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    newEntry.EntryId = entry.EntryId;
                    newEntry.Notes = r["Notes"].ToString();

                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());
                    newEntry.Time = DateTime.Parse(r["Time"].ToString());
                }


            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }


            return newEntry;
        }

        public List<Entry> GetEntriesByJId(BusinessEntity be)
        {
            Dictionary<string, string> parameter = new Dictionary<string, string>();

            Journal jour = (Journal)be;
            List<Entry> Entries = new List<Entry>();


            try
            {
                parameter.Add("@id", jour.JournalId.ToString());

                //using stored procedure  
                DataSet ds = DataAccessEntity.GetData("GetEntriesByJournalId", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Entry newEntry = new Entry();

                    newEntry.EntryId = int.Parse(r["EntryId"].ToString());

                    newEntry.Notes = r["Notes"].ToString();
                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());
                    newEntry.Time = DateTime.Parse(r["Time"].ToString());
                    Entries.Add(newEntry);

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return Entries;


        }
        public int GetMaxId()
        {

            int UserMaxId = 0;
            //using stored procedure 
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetMaxEntryId");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        UserMaxId = int.Parse(r["MaxEntryId"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return UserMaxId;
        }

        public static List<Entry> GetEntries()
        {

            //using sql queries
            //string qry = "select * from Entries";

            List<Entry> Entries = new List<Entry>();

            // ds = DataAccessEntity.GetData(qry);
            //using stored procedure  
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetEntries");
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Entry newEntry = new Entry();

                    newEntry.EntryId = int.Parse(r["EntryId"].ToString());

                    newEntry.Notes = r["Notes"].ToString();

                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());
                    newEntry.Time = DateTime.Parse(r["Time"].ToString());
                    Entries.Add(newEntry);

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return Entries;



        }
        public static List<Entry> GetEntriesbyDate(int uid, int year, int month, int date)
        {
            Dictionary<string, string> parameter = new Dictionary<string, string>();

            List<Entry> Entries = new List<Entry>();
            try
            {
                parameter.Add("@UserId", uid.ToString());
                parameter.Add("@Year", year.ToString());
                parameter.Add("@Month", month.ToString());
                parameter.Add("@Day", date.ToString());

                //using stored procedure  
                DataSet ds = DataAccessEntity.GetData("GetEntriesByDate", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Entry newEntry = new Entry();

                    newEntry.EntryId = int.Parse(r["EntryId"].ToString());

                    newEntry.Notes = r["Notes"].ToString();
                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());
                    newEntry.Time = DateTime.Parse(r["Time"].ToString());
                    Entries.Add(newEntry);

                }

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return Entries;



        }

    }
}

