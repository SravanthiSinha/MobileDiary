using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;

namespace Inrhythm.MobileDiary.BAL.Validators
{
    public abstract class BusinessRule
    {
        
        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
        private string _propertyName2;

        public string PropertyName2
        {
            get { return _propertyName2; }
            set { _propertyName2 = value; }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }


        public BusinessRule(string propertyName)
        {
            _propertyName = propertyName;
            _errorMessage = _propertyName + " is invalid";
        }

        public BusinessRule(string propertyName,string errorMessage)
        {
            _propertyName = propertyName;
            _errorMessage = errorMessage;
        }
        public BusinessRule(string propertyName, string propertyName2,string errorMessage)
        {
            _propertyName = propertyName;
            _propertyName2 = propertyName2;
            _errorMessage = errorMessage;
        }

        public abstract bool Validate(BusinessEntity businessEntity);

        public object GetPropertyValue(BusinessEntity businessEntity)
        {
            return businessEntity.GetType().GetProperty(_propertyName).GetValue(businessEntity, null);
        }
        public object[] GetPropertyValues(BusinessEntity businessEntity)
        {
            object[] s= new object[2];
            s[0] = businessEntity.GetType().GetProperty(_propertyName).GetValue(businessEntity, null);
            s[1] = businessEntity.GetType().GetProperty(_propertyName2).GetValue(businessEntity, null);
            return s;
        }


    }
}
