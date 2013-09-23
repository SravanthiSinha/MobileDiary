using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.Validators;
using Inrhythm.MobileDiary.Logs;

namespace Inrhythm.MobileDiary.BAL.BusinessEntites
{
  public  class User : BusinessEntity
    {
        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _emailId;

        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        private string _mobileNo;

        public string MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; }
        }

        public User()
        {
            try{
            AddRule(new RequiredValidator("Name"));
            //AddRule(new RequiredValidator("UserId"));
            AddRule(new RequiredValidator("Password"));
            AddRule(new RequiredValidator("MobileNo"));
                AddRule(new EmailVallidator("EmailId"));
            AddRule(new IDVallidator("MobileNo"));
            AddRule(new LengthValidator("Password", 6, 20));
            AddRule(new LengthValidator("MobileNo", 10, 10));
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

        }
    }
}
