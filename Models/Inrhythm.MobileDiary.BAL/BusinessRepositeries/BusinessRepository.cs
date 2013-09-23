using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.DAL;
using System.Data;

namespace Inrhythm.MobileDiary.BAL.BusinessRepositeries
{
    public abstract class BusinessRepository
    {
        public abstract bool Create(BusinessEntity be);
        public abstract bool Update(BusinessEntity be);
        public abstract bool Delete(BusinessEntity be);

        public DataAccessEntity dataAccessEntity = new DataAccessEntity();
        public Dictionary<string, string> parameters;
    }

}
