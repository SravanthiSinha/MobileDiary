using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;

namespace Inrhythm.MobileDiary.BAL.Validators
{
    public class RequiredValidator : BusinessRule
    {
        public RequiredValidator(string propertyName)
            : base(propertyName)
        {
            ErrorMessage = "Value for this property " + propertyName + " is required.";
        }

        public override bool Validate(BusinessEntity businessEntity)
        {
            if (GetPropertyValue(businessEntity) != null)
            {
                return GetPropertyValue(businessEntity).ToString().Trim().Length > 0;

            }
            else
            {
                return false;
            }
        }
    }
}
