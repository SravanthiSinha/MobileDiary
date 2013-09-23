using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.DAL;
using Inrhythm.MobileDiary.Logs;

namespace Inrhythm.MobileDiary.BAL.BusinessRepositeries
{
    public class EntryJournalRepository
    {


        public bool Create(int Entry1, int User1, int Journal1)
        {
            DataAccessEntity dataAccessEntity = new DataAccessEntity();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            bool done = false;

            try
            {
                //using stored procedure           

                parameters.Add("@EntryId", Entry1.ToString());
                parameters.Add("@UserId", User1.ToString());
                parameters.Add("@JournalId ", Journal1.ToString());
                //using sql queries
                // string qry = "insert into Entries values('" + entry.EntryName + "','" + entry.Notes + "','" + entry.JournalId + "','" + entry.Time+ ")";
                // if (dataAccessEntity.ExecuteSql(qry))
                if (dataAccessEntity.ExecuteSql("InsertEntryJournal", parameters))
                    done = true;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;


        }




    }
}
