using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;

namespace Pinga.GlobalModels
{
    public class GlobalUtils
    {
        public static string IsPatient
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["IsPatient"]);
            }
            set
            {
                HttpContext.Current.Session["IsPatient"] = value;
            }
        }
        public static string[] Groups;

        public static void isLogedIn()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserId"])))
                HttpContext.Current.Response.Redirect("~/Security/SignIn");



        }

        public static string ConvertDataTableTojSonString(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (Convert.ToString(col.DataType.Name) == "Byte[]")
                    {
                        if (dr[col] != DBNull.Value)
                        {
                            try
                            {
                                row.Add(col.ColumnName, "data:image;base64," + Convert.ToBase64String((byte[])dr[col]));
                            }
                            catch (Exception ex)
                            {
                                row.Add(col.ColumnName, ex.Message);
                            }
                        }
                        else
                        {
                            row.Add(col.ColumnName, "");
                        }
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                tableRows.Add(row);
            }
            return serializer.Serialize(tableRows);
        }

        public static List<Dictionary<String, Object>> ConvertDataTableToIdictionary(DataTable dataTable)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (Convert.ToString(col.DataType.Name) == "Byte[]")
                    {
                        if (dr[col] != DBNull.Value)
                        {
                            try
                            {
                                row.Add(col.ColumnName, "data:image;base64," + Convert.ToBase64String((byte[])dr[col]));
                            }
                            catch (Exception ex)
                            {
                                row.Add(col.ColumnName, ex.Message);
                            }
                        }
                        else
                        {
                            row.Add(col.ColumnName, "");
                        }
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                tableRows.Add(row);
            }
            return tableRows;
        }

        public static string UserId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserId"]));
                return Convert.ToString(HttpContext.Current.Session["UserId"]);
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }
        public static string Clinicid
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["Clinicid"]));
                return Convert.ToString(HttpContext.Current.Session["Clinicid"]);
            }
            set
            {
                HttpContext.Current.Session["Clinicid"] = value;
            }
        }
        public static string PatientID
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["PatientID"]));
                return Convert.ToString(HttpContext.Current.Session["PatientID"]);
            }
            set
            {
                HttpContext.Current.Session["PatientID"] = value;
            }
        }
        public static string PatientEmail
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["PatientEmail"]));
                return Convert.ToString(HttpContext.Current.Session["PatientEmail"]);
            }
            set
            {
                HttpContext.Current.Session["PatientEmail"] = value;
            }
        }

        public static string Email
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["Email"]));
                return Convert.ToString(HttpContext.Current.Session["Email"]);
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
            }
        }
        public static string PatientGuid
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["PatientGuid"]));
                return Convert.ToString(HttpContext.Current.Session["PatientGuid"]);
            }
            set
            {
                HttpContext.Current.Session["PatientGuid"] = value;
            }
        }
        public static string PatientFirstName
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["PatientFirstName"]));
                return Convert.ToString(HttpContext.Current.Session["PatientFirstName"]);
            }
            set
            {
                HttpContext.Current.Session["PatientFirstName"] = value;
            }
        }

        public static string InsuranceCompanyId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["InsuranceCompanyId"]));
                return Convert.ToString(HttpContext.Current.Session["InsuranceCompanyId"]);
            }
            set
            {
                HttpContext.Current.Session["InsuranceCompanyId"] = value;
            }
        }

        public static string MemberUserId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["MemberUserId"]));
                return Convert.ToString(HttpContext.Current.Session["MemberUserId"]);
            }
            set
            {
                HttpContext.Current.Session["MemberUserId"] = value;
            }
        }

        public static string ClubLocationId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["ClubLocationId"]));
                return Convert.ToString(HttpContext.Current.Session["ClubLocationId"]);
            }
            set
            {
                HttpContext.Current.Session["ClubLocationId"] = value;
            }
        }

        public static string membername
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["membername"]));
                return Convert.ToString(HttpContext.Current.Session["membername"]);
            }
            set { HttpContext.Current.Session["membername"] = value; }
        }

        public static string UserName
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserName"]));
                return Convert.ToString(HttpContext.Current.Session["UserName"]);
            }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static string UserRoleId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserRoleId"]));
                return Convert.ToString(HttpContext.Current.Session["UserRoleId"]);
            }
            set { HttpContext.Current.Session["UserRoleId"] = value; }
        }
        public static string RoleID
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["RoleID"]));
                return Convert.ToString(HttpContext.Current.Session["RoleID"]);
            }
            set { HttpContext.Current.Session["RoleID"] = value; }
        }

        public static string UserRole
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserRole"]));
                return Convert.ToString(HttpContext.Current.Session["UserRole"]);
            }
            set { HttpContext.Current.Session["UserRole"] = value; }
        }

        //private static byte[] InsuranceCompanyLogo;
        public static byte[] InsCompanyLogo
        {
            get
            {
                return ((byte[])(HttpContext.Current.Session["InsCompanyLogo"]));
            }
            set
            {
                HttpContext.Current.Session["InsCompanyLogo"] = value;
            }
        }

        public static string GuidId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["GuidId"]));
                return Convert.ToString(HttpContext.Current.Session["GuidId"]);
            }
            set { HttpContext.Current.Session["GuidId"] = value; }
        }

        public static string checkUserSession()
        {
            if (string.IsNullOrEmpty(Convert.ToString(UserId)))
            {
                if (!HttpContext.Current.Request.CurrentExecutionFilePath.ToString().ToLower().Equals("/security/signin"))
                {
                    try
                    {
                        HttpContext.Current.Response.Redirect("~/Security/SignIn");
                    }
                    catch (Exception ex)
                    { }
                }
            }

            return "";
        }

        public static class JsonUtils
        {
            public static List<Dictionary<String, Object>> ConvertDataTableToJsonString(DataTable dataTable)
            {

                List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

                Dictionary<String, Object> row;

                foreach (DataRow dr in dataTable.Rows)
                {
                    row = new Dictionary<String, Object>();
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (Convert.ToString(col.DataType.Name) == "Byte[]")
                        {
                            if (dr[col] != DBNull.Value)
                            {
                                try
                                {
                                    row.Add(col.ColumnName, "data:image;base64," + Convert.ToBase64String((byte[])dr[col]));
                                }
                                catch (Exception ex)
                                {
                                    row.Add(col.ColumnName, ex.Message);
                                }
                            }
                            else
                            {
                                row.Add(col.ColumnName, "");
                            }
                        }
                        else if (Convert.ToString(col.DataType.Name).ToLower() == "datetime" || Convert.ToString(col.DataType.Name).ToLower() == "date")
                        {
                            if (dr[col] != DBNull.Value)
                            {
                                try
                                {
                                    row.Add(col.ColumnName, Convert.ToDateTime(dr[col]).ToString("dd/MM/yyyy"));
                                }
                                catch (Exception ex)
                                {
                                    row.Add(col.ColumnName, ex.Message);
                                }
                            }
                            else
                            {
                                row.Add(col.ColumnName, "");
                            }
                        }
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    tableRows.Add(row);
                }
                return (tableRows);
            }
        }
        public static string checkSession(string myStr)
        {
            if (string.IsNullOrEmpty(myStr) && string.IsNullOrEmpty(IsPatient))
            {
                if (!HttpContext.Current.Request.CurrentExecutionFilePath.ToString().ToLower().Equals("/security/signin")
                    && !HttpContext.Current.Request.CurrentExecutionFilePath.ToString().ToLower().Equals("/security/patientsignin")
                    && !HttpContext.Current.Request.CurrentExecutionFilePath.ToString().ToLower().Equals("/"))
                {
                    try
                    {
                        HttpCookie myCookie = HttpContext.Current.Request.Cookies.Get("UserOrPatient");
                        if (myCookie.Value == "Patient")
                        {
                            HttpContext.Current.Response.Redirect("~/security/patientsignin");
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("~/Security/SignIn");
                        }

                        //if (HttpContext.Current.Request.CurrentExecutionFilePath.ToString().ToLower().Equals("/patient/"))
                        //{
                        //    HttpContext.Current.Response.Redirect("~/security/patientsignin");
                        //}
                        //else {
                        //    HttpContext.Current.Response.Redirect("~/Security/SignIn");
                        //}
                    }
                    catch (Exception ex)
                    { }
                }
            }
            return "";
        }


        public static string EncryptData2(string str)
        {
            string SEncryptionKey = WebConfigurationManager.AppSettings["EncryptData"].ToString();
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            Byte[] key = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                //key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                key = getbytefortripledes(SEncryptionKey);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

                Byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(str);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {

                return string.Empty;

            }
        }

        public static string DecryptData2(string stringToDecrypt)
        {

            string SEncryptionKey = WebConfigurationManager.AppSettings["EncryptData"].ToString();

            Byte[] key = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                Byte[] inputByteArray = new Byte[stringToDecrypt.Length];
                //  Dim inputByteArray(stringToDecrypt.Length) As Byte
                //key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                key = getbytefortripledes(SEncryptionKey);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {

                return string.Empty;

            }
        }
        public static string DecryptDataforaccno(string stringToDecrypt)
        {

            string SEncryptionKey = WebConfigurationManager.AppSettings["EncryptData"].ToString();

            Byte[] key = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                Byte[] inputByteArray = new Byte[stringToDecrypt.Length];
                //  Dim inputByteArray(stringToDecrypt.Length) As Byte
                //key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                key = getbytefortripledes(SEncryptionKey);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {

                return stringToDecrypt;

            }
        }
        public static byte[] getbytefortripledes(string key)
        {
            Byte[] key3 = new Byte[7];
            //Dim key3(7) As Byte
            Byte[] finalkey = new Byte[24];
            // Dim finalkey(23) As Byte
            int j = 0;
            // Dim j As Integer = 0
            for (int i = 0; i < 24; i++)
            {
                if (i == 0 || i == 8 || i == 16)
                {
                    key3 = System.Text.Encoding.UTF8.GetBytes(Left(key, 8));
                    if (key.Length >= 0)
                    {
                        key = key.Substring(8);
                    }
                    j = 0;
                }
                finalkey[i] = key3[j];
                j = j + 1;
            }
            return finalkey;
        }

        public static string Left(Object str, Int32 length)
        {
            string inputString = string.Empty;
            try
            {
                if (str != null)
                    inputString = str.ToString();

                return inputString.Substring(0, length);
            }
            catch (Exception ex)
            {
                throw new Exception("LEFT(" + GetStr(str) + ", " + length + "): " + ex.Message);
            }
        }

        public static string GetStr(Object str)
        {
            if (str == null)
                return String.Empty;

            return str.ToString();
        }

        public static string UserClubLocationId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserClubLocationId"]));
                return Convert.ToString(HttpContext.Current.Session["UserClubLocationId"]);
            }
            set
            {
                HttpContext.Current.Session["UserClubLocationId"] = value;
            }
        }

        public static string UserClubChainId
        {
            get
            {
                checkSession(Convert.ToString(HttpContext.Current.Session["UserClubChainId"]));
                return Convert.ToString(HttpContext.Current.Session["UserClubChainId"]);
            }
            set
            {
                HttpContext.Current.Session["UserClubChainId"] = value;
            }
        }

        //public static string sentEmail(string Host, string FromMail, string To, string Subject, string Body, Int32 Port, string userid, string Password, Boolean ssl) {
        //    try
        //    {
        //        MailMessage mailmsg = new MailMessage();
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = Host; // WebConfigurationManager.AppSettings["Host"].ToString(); // "192.168.10.181";

        //        mailmsg.From = new MailAddress(FromMail); ; // new MailAddress(WebConfigurationManager.AppSettings["From"].ToString());
        //        mailmsg.To.Add(To);

        //        mailmsg.Subject = Subject;
        //        mailmsg.Body = Body;
        //        mailmsg.IsBodyHtml = true;
        //        smtp.Port = Port; // Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString()); // ;
        //        smtp.Credentials = new System.Net.NetworkCredential(userid, Password);
        //        smtp.EnableSsl = ssl; // ;
        //        smtp.Send(mailmsg);
        //        return "Successfully sent";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        public static string sentEmail(string FromMail, string To, string Subject, string Body)
        {
            try
            {
                MailMessage mailmsg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Convert.ToString(WebConfigurationManager.AppSettings["Host"]); // WebConfigurationManager.AppSettings["Host"].ToString(); // "192.168.10.181";

                mailmsg.From = new MailAddress(FromMail); ; // new MailAddress(WebConfigurationManager.AppSettings["From"].ToString());
                mailmsg.To.Add(To);

                mailmsg.Subject = Subject;
                mailmsg.Body = Body;
                mailmsg.IsBodyHtml = true;
                smtp.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]); // Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString()); // ;
                smtp.Credentials = new System.Net.NetworkCredential(Convert.ToString(WebConfigurationManager.AppSettings["UserName"]), Convert.ToString(WebConfigurationManager.AppSettings["Password"]));
                smtp.EnableSsl = Convert.ToBoolean(Convert.ToBoolean(WebConfigurationManager.AppSettings["ssl"])); // ;
                smtp.Send(mailmsg);
                return "Mail Successfully Sent";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string sentEmailWithAttach(string Host, string FromMail, string To, string Subject, string Body, Int32 Port, string userid, string Password, Boolean ssl, string filePath)
        {
            try
            {
                MailMessage mailmsg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host; // WebConfigurationManager.AppSettings["Host"].ToString(); // "192.168.10.181";

                mailmsg.From = new MailAddress(FromMail); ; // new MailAddress(WebConfigurationManager.AppSettings["From"].ToString());
                mailmsg.To.Add(To);

                mailmsg.Subject = Subject;
                mailmsg.Body = Body;
                mailmsg.IsBodyHtml = true;
                smtp.Port = Port; // Convert.ToInt32(WebConfigurationManager.AppSettings["Port"].ToString()); // ;
                smtp.Credentials = new System.Net.NetworkCredential(userid, Password);
                smtp.EnableSsl = ssl; // ;


                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(filePath);
                mailmsg.Attachments.Add(attachment);


                smtp.Send(mailmsg);
                return "Successfully sent";
            }
            catch (Exception ex)
            {
                return "Exception: " + ex.Message;
            }
        }

        public static string selectedLanguage
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["selectedLanguage"]);
            }
            set
            {
                HttpContext.Current.Session["selectedLanguage"] = (string.IsNullOrEmpty(value) ? "eng_" : value);
            }
        }

       

    }
}