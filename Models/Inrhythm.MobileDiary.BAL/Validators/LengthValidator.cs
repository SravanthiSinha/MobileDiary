using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;

namespace Inrhythm.MobileDiary.BAL.Validators
{
    public class LengthValidator : BusinessRule
    {
        int max_value, min_value;
        public LengthValidator(string propertyname, int min, int max)
            : base(propertyname)
        {
            max_value = max;
            min_value = min;
            ErrorMessage = " Minimum Length of " + propertyname + " should be "+min;

        }

        public override bool Validate(BusinessEntity businessEntity)
        {
            int value = (GetPropertyValue(businessEntity).ToString()).Length;

            if (value > max_value || value < min_value)
                return false;
            else
                return true;
        }


    }
}
