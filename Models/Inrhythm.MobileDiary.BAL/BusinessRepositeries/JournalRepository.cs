using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Inrhythm.MobileDiary.DAL;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.Logs;

namespace Inrhythm.MobileDiary.BAL.BusinessRepositeries
{
    public class JournalRepository : BusinessRepository
    {


        public override bool Create(BusinessEntity be)
        {
            parameters = new Dictionary<string, string>();
            bool done = false;
            try
            {
                Journal journal = (Journal)be;
                //using stored procedure  
                parameters.Add("@Date", journal.JournalDate.ToString());
                parameters.Add("@Name", journal.JournalName);

                if (dataAccessEntity.ExecuteSql("InsertJournal", parameters))
                {
                    done = true;

                }

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
            Journal journal = (Journal)be;
            //using stored procedure  
            try
            {
                parameters.Add("@JournalId", journal.JournalDate.ToString());
                parameters.Add("@Date", journal.JournalDate.ToString());
                parameters.Add("@Name", journal.JournalName);

                if (dataAccessEntity.ExecuteSql("UpdateJournal", parameters))
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
            try
            {
                Journal journal = (Journal)be;
                //using stored procedure  
                parameters.Add("@JournalId", journal.JournalDate.ToString());

                if (dataAccessEntity.ExecuteSql("DeleteJournals", parameters))
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        public Journal GetJournalById(BusinessEntity be)
        {
            parameters = new Dictionary<string, string>();
            Journal journal = (Journal)be;
            Journal newEntry = new Journal();
            //using stored procedure  
            try
            {
                parameters.Add("@JournalId", journal.JournalDate.ToString());

                DataSet ds = DataAccessEntity.GetData("GetJournalById", parameters);
                DataTable dt = ds.Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    newEntry.JournalId = journal.JournalId;
                    newEntry.JournalName = r["Name"].ToString();

                    newEntry.JournalDate = DateTime.Parse(r["Date"].ToString());
                }


            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }


            return newEntry;
        }
        public static List<Journal> GetJournals()
        {


            List<Journal> Journals = new List<Journal>();
            //using stored procedure  
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetJournals");
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Journal newEntry = new Journal();

                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());

                    newEntry.JournalName = r["Name"].ToString();

                    newEntry.JournalDate = DateTime.Parse(r["Date"].ToString());
                    Journals.Add(newEntry);

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return Journals;



        }

        public static List<Journal> GetJournalsByTag(string tag)
        {
            Dictionary<string, string> parameter = new Dictionary<string, string>();

            //needs to be implemenetd
            parameter.Add("@TagName", tag);
            List<Journal> Journals = new List<Journal>();
            //using stored procedure  
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetJournalsByTag", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Journal newEntry = new Journal();

                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());

                    newEntry.JournalName = r["Name"].ToString();

                    newEntry.JournalDate = DateTime.Parse(r["Date"].ToString());
                    Journals.Add(newEntry);

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return Journals;



        }
        public int GetMaxId()
        {

            int JournalMaxId = 0;
            //using stored procedure 
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetMaxJournalId");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        JournalMaxId = int.Parse(r["MaxJournalId"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return JournalMaxId;
        }

        public List<Journal> GetMonthJournals(int id)
        {
            List<Journal> Journals = new List<Journal>();
            //using stored procedure  

            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("@Id", id.ToString());
                DataSet ds = DataAccessEntity.GetData("GetJournalsByMonth", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    Journal newEntry = new Journal();

                    newEntry.JournalId = int.Parse(r["JournalId"].ToString());

                    newEntry.JournalName = r["Name"].ToString();

                    newEntry.JournalDate = DateTime.Parse(r["Date"].ToString());
                    Journals.Add(newEntry);

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return Journals;

        }

        public List<string> GetYears(BusinessEntity be)
        {
            List<string> years = new List<string>();
            try
            {
                User user = (User)be;
                //using stored procedure  
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("@UserId", user.UserId.ToString());
                DataSet ds = DataAccessEntity.GetData("GetYearsOfJournals", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {

                    years.Add((r["Year"].ToString()));

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return years;
        }


        public List<string> GetMonths(int userid, int year)
        {
            List<string> months = new List<string>();
            try
            {
                //using stored procedure  
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("@UserId", userid.ToString());
                parameter.Add("@year", year.ToString());
                DataSet ds = DataAccessEntity.GetData("GetMonthsOfJournals", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {

                    months.Add(r["month"].ToString());

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return months;
        }

        public List<string> GetDates(int userid, int year, int month)
        {
            List<string> dates = new List<string>();
            //using stored procedure  

            try
            {
                Dictionary<string, string> parameter = new Dictionary<string, string>();
                parameter.Add("@UserId", userid.ToString());
                parameter.Add("@year", year.ToString());
                parameter.Add("@month", month.ToString());
                DataSet ds = DataAccessEntity.GetData("GetDatesOfJournals", parameter);
                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {

                    dates.Add((r["day"].ToString()));

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return dates;
        }
        public string JournalExists(string userid)
        {
            string result = "";
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            try
            {
                parameter.Add("@UserId", userid.ToString());
                DataSet ds = DataAccessEntity.GetData("CheckJournalByDate", parameter);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    result = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                else
                    result = "NotFound";
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return result;
        }

    }

}
