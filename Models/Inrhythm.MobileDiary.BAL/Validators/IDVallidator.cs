using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
namespace Inrhythm.MobileDiary.BAL.Validators
{
    public class IDVallidator : BusinessRule
    {
        public IDVallidator(string propertyName)
            : base(propertyName)
        {
            ErrorMessage = "Value for this property " + propertyName + " is required.";
        }

        public override bool Validate(BusinessEntity businessEntity)
        {
            long rs;
            string str = GetPropertyValue(businessEntity).ToString();
            return Int64.TryParse(str, out rs);
        }
    }
}
