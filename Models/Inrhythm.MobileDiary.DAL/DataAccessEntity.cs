using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Data.Common;
using Inrhythm.MobileDiary.Logs;

namespace Inrhythm.MobileDiary.DAL
{
    public class DataAccessEntity
    {
        static SqlConnection con = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Data Source=IRSDSK114\SQLEXPRESS;Initial Catalog=MobileDiary;Integrated Security=True");
        bool executed = false;

        public bool ExecuteSql(string spname, Dictionary<string, string> parameters)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    SqlParameter sqlparameter = new SqlParameter(parameter.Key, parameter.Value);
                    cmd.Parameters.Add(sqlparameter);
                }
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    executed = true;
            }
            catch (SqlException sqlex)
            {
                LogException(sqlex);
                throw new Exception("Error Processing Request");
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw new Exception("Error Processing Request");
            }
            finally
            {
                con.Close();
            }
            return executed;
        }

        public static DataSet GetData(string spname, Dictionary<string, string> parameters)
        {
            con.Open();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sdad = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(spname, con);

                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    SqlParameter sqlparameter = new SqlParameter(parameter.Key, parameter.Value);
                    cmd.Parameters.Add(sqlparameter);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                sdad.SelectCommand = cmd;
                sdad.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                LogException(sqlex);
                throw new Exception("Error Processing Request");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("Error Processing Request");
            }
            finally
            {
                con.Close();
            }
            return ds;
        }
        public static DataSet GetData(string spname)
        {
            con.Open();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sdad = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;
                sdad.SelectCommand = cmd;
                sdad.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                LogException(sqlex);
                throw new Exception("Error Processing Request");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("Error Processing Request");
            }
            finally
            {
                con.Close();
            }
            return ds;
        }

        private static void LogException(SqlException sqlex)
        {
            LogFile log = new LogFile();

            string errorMessage;
            errorMessage = "Exception Number : " + sqlex.Number +
                         "(" + sqlex.Message + ") has occurred";

            foreach (SqlError sqle in sqlex.Errors)
            {
                errorMessage = "Message: " + sqle.Message +
                             " Number: " + sqle.Number +
                             " Procedure: " + sqle.Procedure +
                             " Server: " + sqle.Server +
                             " Source: " + sqle.Source +
                             " State: " + sqle.State +
                             " Severity: " + sqle.Class +
                             " LineNumber: " + sqle.LineNumber;

            }
            log.Create(errorMessage);
        }

        private static void LogException(Exception ex)
        {
            string errorMessage;
            errorMessage = ex.Message + " has occurred";
            LogFile log = new LogFile();
            log.Create(errorMessage);
        }
    }
}
