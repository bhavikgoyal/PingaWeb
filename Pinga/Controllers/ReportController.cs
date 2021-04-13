using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Pinga.Models;

namespace Pinga.Controllers
{
    public class ReportController : Controller
    {

        public ActionResult Reporting()
        {
            Reporting obj_s = new Reporting();
            try
            {

                ViewBag.ProjectsList = obj_s.GetProjectName();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }
        public ActionResult AreaWiseReport()
        {

            Reporting obj_s = new Reporting();
            try
            {

                ViewBag.ProjectsList = obj_s.GetProjectName();
                ViewBag.ChargerNamelist = obj_s.GetChargerName();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }

        public ActionResult ProjectWiseSumaryReport()
        {

            Reporting obj_s = new Reporting();
            try
            {

                ViewBag.ProjectsList = obj_s.GetProjectName();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }
        public ActionResult AllChequeDetail()
        {
            Reporting obj_s = new Reporting();
            try
            {

                ViewBag.ProjectsList = obj_s.GetProjectName();
                ViewBag.CheckStatusList = obj_s.GetCheckStatus();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }
        public ActionResult AllInvoiceDetail()
        {
            Reporting obj_s = new Reporting();
            try
            {

                ViewBag.ProjectsList = obj_s.GetProjectName();
                ViewBag.ChargerNamelist = obj_s.GetChargerName();

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }

        public ActionResult MonthlyBillCollection()
        {
            Reporting obj_s = new Reporting();
            try
            {
                ViewBag.ProjectsList = obj_s.GetProjectName();
              
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            finally
            {
                obj_s.Dispose();
            }
        }
        public JsonResult GetBlockNameByID(string ProjectID)

        {
            if (ProjectID == "")
            {
                ProjectID = "0";
            }

            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetBlockName(ProjectID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }
        public JsonResult GetAppnoByID(string BlockID)
        {
            if (BlockID == "")
            {
                BlockID = "0";
            }
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetAppNo(BlockID), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }

        public JsonResult GetLocation(string Status)
        {
           
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetLocationName(), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }
        public JsonResult GetAllReportData(string Appid, string From, string TO)
        {
            if (Appid == "")
            {
                Appid = "0";
            }
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetReport(Appid, From, TO), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }

        public JsonResult ReportDataforAreaWiseReport(string Appid, string CharID, string blockID, string ProjectID, string From, string TO)
        {
            if (Appid == "")
            {
                Appid = "0";
            }
            if (CharID == "")
            {
                CharID = "0";
            }
            if (blockID == "")
            {
                blockID = "0";
            }
            if (ProjectID == "")
            {
                ProjectID = "0";
            }
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetReportForAreawise(Appid, CharID, blockID, ProjectID, From, TO), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }
        public JsonResult ReportDataforProjectWiseReport(string ProjectID, string From, string TO)
        {

            if (ProjectID == "")
            {
                ProjectID = "0";
            }
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetReportProjectWiseReport(ProjectID, From, TO), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }

        public JsonResult ReportDataforChequeDetail(string CheckStatusID,string Appid, string blockID, string ProjectID, string From, string TO)
        {

            if (ProjectID == "")
            {
                ProjectID = "0";
            }

            if (CheckStatusID == "")
            {
                CheckStatusID = "0";
            }
            if (Appid == "")
            {
                Appid = "0";
            }
            if (blockID == "")
            {
                blockID = "0";
            }
           
            using (Reporting obj_s = new Reporting())
                try
                {
                    return Json(obj_s.GetReportChequeDetail(CheckStatusID,ProjectID,blockID,Appid, From, TO), JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json("Exception", ex.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    obj_s.Dispose();
                }
        }
    }
}