using DB_con;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Pinga.Models
{
    public class SecurityClass:IDisposable
    {
        #region "properties"
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }


        ConnectionCls obj_con = null;


        //Default Constructor
        public SecurityClass()
        {
            obj_con = new ConnectionCls();
        }
        #endregion
        #region "Method"
        public SecurityClass Checkuserlogin(string UserName, string Password)
        {
            try
            {
               obj_con.clearParameter();
                obj_con.addParameter("@UserName", UserName);

                obj_con.addParameter("@Password", Password);
                DataTable dt = ConvertDataReaderToDataTable(obj_con.ExecuteReader("RE_Users_Login", CommandType.StoredProcedure));
                return ConvertToOjbect(dt);
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("RE_Users_Login : " + ex.Message);
            }
        }
        public SecurityClass ConvertToOjbect(DataTable dt)
        {
            SecurityClass security = new SecurityClass();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["UserID"].ToString()))
                    security.UserID = Convert.ToString(dt.Rows[i]["UserID"]);
                else
                    security.UserID = string.Empty;

            }
            return security;
        }
        public DataTable ConvertDataReaderToDataTable(IDataReader dr)
        {
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        public void Dispose()
        {
            obj_con.closeConnection();
            System.GC.SuppressFinalize(this);
        }
        #endregion
    }
}