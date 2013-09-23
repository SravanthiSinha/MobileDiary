using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;

namespace Inrhythm.MobileDiary.BAL.Validators
{
    public class CompareValidator : BusinessRule
    {
        
        public CompareValidator(string propertyname, string propertyname2,string ErrorMessage)
            : base(propertyname,propertyname2,ErrorMessage)
        {
           
            ErrorMessage = "Invalid dsahgas";
        }

        public override bool Validate(BusinessEntity businessEntity)
        {
          // int isvalid = 0;
          object[] values = GetPropertyValues(businessEntity);
          string value1 = values[0].ToString();
          string value2 = values[1].ToString();
          if (value1 == value2)
          {

              return true;

          }
          else return false;
        }


    }
}
