using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.Validators;
using Inrhythm.MobileDiary.Logs;

namespace Inrhythm.MobileDiary.BAL.BusinessEntites
{
  public  class Journal : BusinessEntity
    {
        private int _journalId;

        public int JournalId
        {
            get { return _journalId; }
            set { _journalId = value; }
        }
        private DateTime _journalDate;
        public DateTime JournalDate
        {
            get { return _journalDate; }
            set { _journalDate = value; }
        }
        private string _journalName;

        public string JournalName
        {
            get { return _journalName; }
            set { _journalName = value; }
        }

       
        public Journal()
        {
            try{
            AddRule(new RequiredValidator("JournalName"));
            AddRule(new RequiredValidator("JournalId"));
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
        }
    }
}
