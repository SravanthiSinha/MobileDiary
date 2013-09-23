using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using System.Globalization;

namespace Inrhythm.MobileDiary.BAL.Validators
{
   
        public class EmailVallidator : BusinessRule
        {
            public EmailVallidator(string propertyName)
                : base(propertyName)
            {
                ErrorMessage =   propertyName + " is not valid.";
            }

            bool invalid = false;

            public override bool Validate(BusinessEntity businessEntity)
            {

                string email = GetPropertyValue(businessEntity).ToString();

                email = Regex.Replace(email, @"(@)(.+)$", this.DomainMapper,
                                       RegexOptions.None);


                if (Regex.IsMatch(email,
                          @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                          RegexOptions.IgnoreCase))

                    invalid = true;
                return invalid;

            }
            private string DomainMapper(Match match)
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            }
        }
    }




