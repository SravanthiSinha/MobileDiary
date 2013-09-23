using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.Logs;
using Inrhythm.MobileDiary.BAL.Validators;

namespace Inrhythm.MobileDiary.BAL.BusinessEntites
{
   public class Entry : BusinessEntity
    {
        private int _entryId;

        public int EntryId
        {
            get { return _entryId; }
            set { _entryId = value; }
        }
        private int _journalId;

        public int JournalId
        {
            get { return _journalId; }
            set { _journalId = value; }
        }

      


        private string _notes;

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public Entry()
        {
            try
            {
                AddRule(new RequiredValidator("EntryId"));
                AddRule(new RequiredValidator("JournalId"));
                AddRule(new RequiredValidator("Notes"));
                AddRule(new RequiredValidator("Time"));
            }
            catch (Exception ex)

            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

        }


        
    }
}
