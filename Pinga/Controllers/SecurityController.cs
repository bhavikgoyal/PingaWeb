using Microsoft.AspNetCore.Mvc;
using Pinga.GlobalModels;
using Pinga.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinga.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(SecurityClass obj)
        {
            try
            {
                if((obj.UserName == "" || obj.UserName == null )&& (obj.Password == "" || obj.Password == null))
                {
                    ViewBag.error3 = "Please enter user name  and password";
                    
                    return View(obj);
                }
                else if (obj.UserName == "" || obj.UserName == null)
                {
                    ViewBag.error3 = "Please Enter user name";
                    return View(obj);
                }
                else if(obj.Password == "" || obj.Password == null)
                {
                    ViewBag.error3 = "Please Enter Password";
                    return View(obj);
                }
                

                if (obj.UserName != "" && obj.Password != "")
                {
                    var Pass = obj.Password;
                    Pass = Pinga.GlobalModels.GlobalUtils.EncryptData2(Pass);
                    obj.Password = Pass;
                    obj = obj.Checkuserlogin(obj.UserName, obj.Password);
                    if (obj.UserID != "0")
                    {
                        //Pinga.GlobalModels.GlobalUtils.IsPatient = "0";
                        //Pinga.GlobalModels.GlobalUtils.UserId = obj.UserID.ToString();
                        //Session["GetValues"] = obj.UserID.ToString();
                        //Pinga.GlobalModels.GlobalUtils.UserName = obj.EmailAddress.ToString();
                        //Pinga.GlobalModels.GlobalUtils.RoleID = obj.RoleID.ToString();
                                    
                      return RedirectToAction("Dashboard", "Home");
                    }
                       
                }

                else
                {
                   ViewBag.error3 = "Invalid Email or Password.";
                }
                return View(obj);

            }


            catch (Exception ex)
            {
                ViewBag.error = "Exception:-  " + ex.Message;
                ModelState.AddModelError("Inactive User", "Please Contact Admin To Active Your Account.");
                return View(obj);
            }


            finally
            {
                obj.Dispose();
            }
            //return View(obj);
        }

        public ActionResult Logout()
        {
          
                
                return RedirectToAction("UserLogin","Security");
           
        }
    }
}