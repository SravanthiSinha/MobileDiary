using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Inrhythm.MobileDiary.BAL.BusinessEntites;
using Inrhythm.MobileDiary.DAL;
using Inrhythm.MobileDiary.Logs;


namespace Inrhythm.MobileDiary.BAL.BusinessRepositeries
{
    public class UserRepository : BusinessRepository
    {

        public override bool Create(BusinessEntity be)
        {
            parameters = new Dictionary<string, string>();
            bool done = false;
            User user = (User)be;
            try
            {
                //using stored procedure           
                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.EmailId);
                parameters.Add("@Mobile", user.MobileNo.ToString());

                if (dataAccessEntity.ExecuteSql("InsertUser", parameters))
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        public override bool Update(BusinessEntity be)
        {
            bool done = false;
            User user = (User)be;
            try
            {
                parameters = new Dictionary<string, string>();
                //using stored procedure
                parameters.Add("@UserId", user.UserId.ToString());

                parameters.Add("@Password", user.Password);
                parameters.Add("@Email", user.EmailId);
                parameters.Add("@Mobile", user.MobileNo.ToString());


                if (dataAccessEntity.ExecuteSql("UpdateUser", parameters))
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        public override bool Delete(BusinessEntity be)
        {
            bool done = false;
            User user = (User)be;
            try
            {
                parameters = new Dictionary<string, string>();
                //using stored procedure
                parameters.Add("@UserId", user.UserId.ToString());


                if (dataAccessEntity.ExecuteSql("DeleteUser", parameters))
                    done = true;
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        public bool Authenticate(BusinessEntity be)
        {
            bool done = false;
            User user = (User)be;
            try
            {
                parameters = new Dictionary<string, string>();
                //using stored procedure          
                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);

                DataSet ds = DataAccessEntity.GetData("GetUserId", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }
        // if return value is 0 then thier is no user existings
        public int GetUserId(BusinessEntity be)
        {
            int UserId = 0;
            User user = (User)be;
            try
            {
                parameters = new Dictionary<string, string>();
                //using stored procedure          
                parameters.Add("@Name", user.Name);
                parameters.Add("@Password", user.Password);

                DataSet ds = DataAccessEntity.GetData("GetUserId", parameters);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        UserId = int.Parse(r["UserId"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return UserId;
        }
        public bool Exists(BusinessEntity be)
        {
            bool done = false;
            User user = (User)be;
            try
            {
                user.UserId = GetUserId(user);

                parameters = new Dictionary<string, string>();
                //using stored procedure          
                parameters.Add("@UserId", user.UserId.ToString());

                DataSet ds = DataAccessEntity.GetData("GetUser", parameters);
                if ((ds.Tables[0].Rows.Count) > 0)
                    done = true;

            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }
            return done;
        }

        public User GetUserDetails(BusinessEntity be)
        {

            User user = (User)be;
            try
            {
                parameters = new Dictionary<string, string>();
                //using stored procedure          
                parameters.Add("@UserId", user.UserId.ToString());

                DataSet ds = DataAccessEntity.GetData("GetUser", parameters);

                DataTable dt = ds.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    user.UserId = Int32.Parse(r["UserId"].ToString());
                    user.Name = r["Name"].ToString();
                    user.EmailId = r["Email"].ToString();
                    user.Password = r["Password"].ToString();
                    user.MobileNo = (r["Mobile"].ToString());

                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return user;
        }
        public int GetMaxId()
        {

            int UserMaxId = 0;
            //using stored procedure 
            try
            {
                DataSet ds = DataAccessEntity.GetData("GetMaxUserId");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        UserMaxId = int.Parse(r["MaxId"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile log = new LogFile();
                log.Create(ex.Message);
            }

            return UserMaxId;
        }
    }
}
