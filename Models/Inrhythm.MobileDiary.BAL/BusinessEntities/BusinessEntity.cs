using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.Validators;
using Inrhythm.MobileDiary.Logs;
namespace Inrhythm.MobileDiary.BAL.BusinessEntites
{
    public class BusinessEntity
    {
        private List<BusinessRule> _businessRules = new List<BusinessRule>();

        private List<string> _validationErrorsList = new List<string>();

        public List<string> ValidationErrorsList
        {
            get { return _validationErrorsList; }
            set { _validationErrorsList = value; }
        }


        public List<BusinessRule> BusinessRules
        {
            get { return _businessRules; }
            set { _businessRules = value; }
        }

        public void AddRule(BusinessRule businessRule)
        {
            _businessRules.Add(businessRule);
        }

        public bool Validate()
        {
            bool isValid = true;
            try
            {
                _validationErrorsList.Clear();
                foreach (BusinessRule businessRule in BusinessRules)
                {
                    if (!businessRule.Validate(this))
                    {
                        _validationErrorsList.Add(businessRule.ErrorMessage);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return isValid;
        }

    }
}
