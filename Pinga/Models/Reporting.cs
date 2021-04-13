using DB_con;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace Pinga.Models
{
    public class Reporting : IDisposable
    {


        [DisplayName("ProjectID")]
        public string ProjectID { get; set; }
        [DisplayName("Charger Name")]
        public string ChargeID { get; set; }

        [DisplayName("ProjectName")]
        public string CheckStatusID { get; set; }
        public string ProjectName { get; set; }



        ConnectionCls obj_con = null;


        //Default Constructor
        public Reporting()
        {
            obj_con = new ConnectionCls();
        }







        public List<System.Web.Mvc.SelectListItem> GetProjectName()
        {
            try
            {
                obj_con.clearParameter();

                DataTable dt = ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_mstProject_GetProjectName", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<System.Web.Mvc.SelectListItem> lst = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "Select All";
                sli1.Value = "-1";
                lst.Add(sli1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(dt.Rows[i][1]);
                    sli.Value = Convert.ToString(dt.Rows[i][0]);

                    lst.Add(sli);

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_mstProject_GetProjectName");
            }
            finally
            {
                obj_con.closeConnection();
            }
        }
        public List<System.Web.Mvc.SelectListItem> GetCheckStatus()
        {
            try
            {
                obj_con.clearParameter();

                DataTable dt = ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetCheckStatus", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<System.Web.Mvc.SelectListItem> lst = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "Select All";
                sli1.Value = "-1";
                lst.Add(sli1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(dt.Rows[i][1]);
                    sli.Value = Convert.ToString(dt.Rows[i][0]);

                    lst.Add(sli);

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_GetCheckStatus");
            }
            finally
            {
                obj_con.closeConnection();
            }
        }

        public List<System.Web.Mvc.SelectListItem> GetChargerName()
        {
            try
            {
                obj_con.clearParameter();

                DataTable dt = ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_mstCharge_GetchargeName", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<System.Web.Mvc.SelectListItem> lst = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                sli1.Text = "Select All";
                sli1.Value = "-1";
                lst.Add(sli1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(dt.Rows[i][1]);
                    sli.Value = Convert.ToString(dt.Rows[i][0]);

                    lst.Add(sli);

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("Sp_mstCharge_GetchargeName");
            }
            finally
            {
                obj_con.closeConnection();
            }
        }
        public List<Dictionary<string, object>> GetBlockName(string ProjectID)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@ProjectID", ProjectID);
                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_mstBlock_GetBlockNamebyProjectID", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_mstBlock_GetBlockNamebyProjectID : " + ex.Message);
            }
        }

        public List<Dictionary<string, object>> GetAppNo(string BlockID)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@BlockID", BlockID);
                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_App_AppNobyBlockID", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_App_AppNobyBlockID : " + ex.Message);
            }
        }

        public List<Dictionary<string, object>> GetLocationName()
        {
            try
            {
                obj_con.clearParameter();
               
                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetLocation", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_GetLocation : " + ex.Message);
            }
        }
        public List<Dictionary<string, object>> GetReport(string Appid, string From, string TO)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@Appid", Appid);
                obj_con.addParameter("@From", From);
                obj_con.addParameter("@To", TO);

                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_Getreport_ByApp", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_Getreport_ByApp : " + ex.Message);
            }
        }

        public List<Dictionary<string, object>> GetReportForAreawise(string Appid, string CharID, string blockID, string ProjectID, string From, string TO)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@Appid", Appid);
                obj_con.addParameter("@CharID", CharID);
                obj_con.addParameter("@blockID", blockID);
                obj_con.addParameter("@ProjectID", ProjectID);
                obj_con.addParameter("@From", From);
                obj_con.addParameter("@To", TO);

                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetreportAreawise", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_GetreportAreawise : " + ex.Message);
            }
        }
        public List<Dictionary<string, object>> GetReportForMonthlyBillCollection(string ProjectID, string blockID, string Appid, string selectMonth)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@ProjectID", ProjectID);
                obj_con.addParameter("@blockID", blockID);
                obj_con.addParameter("@Appid", Appid);
                obj_con.addParameter("@SelectMonth", selectMonth);
               

                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetMonthlyBillcollection", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_GetMonthlyBillcollection : " + ex.Message);
            }
        }
        public List<Dictionary<string, object>> GetReportProjectWiseReport(string ProjectID, string From, string TO)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@ProjectID", ProjectID);
                obj_con.addParameter("@From", From);
                obj_con.addParameter("@To", TO);

                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetreportProjectwise", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_GetreportProjectwise : " + ex.Message);
            }
        }
        public List<Dictionary<string, object>> GetReportChequeDetail(string CheckStatusID, string ProjectID, string blockID, string Appid, string From, string TO)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@CheckStatusID", CheckStatusID);
                obj_con.addParameter("@ProjectID", ProjectID);
                obj_con.addParameter("@blockID", blockID);
                obj_con.addParameter("@Appid", Appid);
                obj_con.addParameter("@From", From);
                obj_con.addParameter("@To", TO);

                return ConnectionCls.JsonUtils.ConvertDataTableToJsonString(ConvertDatareadertoDataTable(obj_con.ExecuteReader("Sp_GetreportChequeDetail", CommandType.StoredProcedure)));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("Sp_GetreportChequeDetail : " + ex.Message);
            }
        }
        public DataTable ConvertDatareadertoDataTable(IDataReader dr)
        {
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }


        //Convert DataTable To List method







        public void Dispose()
        {
            obj_con.closeConnection();
            System.GC.SuppressFinalize(this);
        }


    }
}