using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
//using System.Web.Mail;

using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


/// <summary>
/// Summary description for general
/// </summary>

namespace UserClass
{
    [Serializable]         
    public static class customUtility
    {
        public static string DBPrefix = System.Web.Configuration.WebConfigurationManager.AppSettings["DBPrefix"].ToString();
        public static string ImageNotFound = System.Web.Configuration.WebConfigurationManager.AppSettings["ImageNotFound"].ToString();
        public static string WebsitePath = "/";
        private static int userid;

        public static int UserID
        {
            get { return userid; }
            set { userid = value; }
        }

        public static SqlConnection ConnectDB()
        {
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString());
            return con;
        }

        public static int Command(SqlCommand cmd)
        {
            SqlConnection con = ConnectDB();
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                return cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                HttpContext.Current.Response.Write("<br>" + ex.Message);
                HttpContext.Current.Response.End();
                return 0;
            }
            finally
            {
                con.Close();
            }
        }



        public static bool CheckAdminLogin(string username, string pwd)
        {
            bool retVal = false;

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand command = new SqlCommand("Select * from " + DBPrefix + "Admin where Username='" + username + "' and [password]='" + pwd + "'", con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                        retVal = true;
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return retVal;
        }

        public static string CheckVendorLogin(string username, string pwd)
        {
            bool retVal = false;
            string vendorcode = "";
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand command = new SqlCommand("Select * from " + DBPrefix + "Vendor where Username='" + username + "' and [password]='" + pwd + "' and status=1", con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                        vendorcode = reader[1].ToString();
                    retVal = true;
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }

            }
            return vendorcode;
        }
        public static bool CheckDuplicateFieldValue(string value, string tablename, string fieldname, customUtility.CompareType comparetype, string extraclause)
        {
            bool retVal = false;

            string tmpStr = "";
            string tmpextraclause = "";
            if (comparetype == CompareType.number)
                tmpStr = value;
            else
                tmpStr = "'" + value + "'";

            if (extraclause != "")
                tmpextraclause = extraclause;

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                //HttpContext.Current.Response.Write("Select " + fieldname + " from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause);
                //HttpContext.Current.Response.End();
                SqlCommand command = new SqlCommand("Select " + fieldname + " from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause, con);
                //HttpContext.Current.Response.Write("Select " + fieldname + " from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause);
                //HttpContext.Current.Response.End();  
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                        retVal = true;
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }

            }
            return retVal;
        }


        //public static bool CheckDuplicateValueForAddno(string value1,int value2,int value3,string tablename,customUtility.CompareType comparetype, string extraclause)
        //{
        //    bool retVal = false;

        //    string tmpStr = "";
        //    string tmpextraclause = "";
        //    if (comparetype == CompareType.number)
        //        tmpStr = value1;
        //    else
        //        tmpStr = "'" + value1+ "'";

        //    if (extraclause != "")
        //        tmpextraclause = extraclause;

        //    using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
        //    {
        //        SqlCommand command = new SqlCommand("Select adno from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause, con);
        //        // HttpContext.Current.Response.Write("Select " + fieldname + " from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause);
        //        //HttpContext.Current.Response.End();  
        //        con.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        try
        //        {
        //            if (reader.Read())
        //                retVal = true;
        //        }
        //        finally
        //        {
        //            // Always call Close when done reading.
        //            reader.Close();
        //        }

        //    }
        //    return retVal;
        //}



        public static bool CheckDataExists(string SQL)
        {
            bool retVal = false;

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(SQL, con);
                con.Open();
                try
                {
                    SqlDataReader readIdentity = cmd.ExecuteReader();
                    if (readIdentity.Read())
                        retVal = true;
                }
                finally
                {
                    // Always call Close when done reading.
                    con.Close();

                }
                return retVal;
            }
        }

        public static string GetAField(string SQL)
        {
            string retVal = "";

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand cmd = new SqlCommand(SQL, con);
                con.Open();
                try
                {
                    SqlDataReader readIdentity = cmd.ExecuteReader();
                    if (readIdentity.Read())
                        retVal = readIdentity[0].ToString();
                }
                finally
                {
                    con.Close();

                }
                return retVal;
            }
        }

        public static bool AddToTable(string InsertSQL)
        {
            bool retVal = false;

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                SqlCommand command = new SqlCommand(InsertSQL, con, trans);
                try
                {
                    command.ExecuteNonQuery();
                    trans.Commit();
                    retVal = true;
                }
                finally
                {
                    // Always call Close when done reading.
                    con.Close();

                }
                return retVal;
            }


        }



        public static bool ExecuteNonQuery(string SQL)
        {
            bool retVal = false;

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();
                SqlCommand command = new SqlCommand(SQL, con, trans);
                //HttpContext.Current.Response.Write(SQL);
                //HttpContext.Current.Response.End();
                try
                {
                    command.ExecuteNonQuery();
                    trans.Commit();
                    retVal = true;
                }
                catch (Exception ex)
                {
                    //HttpContext.Current.Response.Write(SQL + "<br>" + ex.Message);
                    //HttpContext.Current.Response.End();

                }

                finally
                {
                    // Always call Close when done reading.
                    con.Close();

                }
                return retVal;
            }


        }


        //public static int ExecuteNonQuery(string SQL,bool b)
        //{
        //     int retRows = 0;

        //    using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
        //    {
        //        con.Open();
        //        SqlTransaction trans = con.BeginTransaction();
        //        SqlCommand command = new SqlCommand(SQL, con, trans);
        //        try
        //        {
        //            int count=command.ExecuteNonQuery();
        //            trans.Commit();
        //            retRows = count;
        //        }
        //        catch (Exception ex)
        //        {
        //            HttpContext.Current.Response.Write(SQL + "<br>" + ex.Message);
        //            HttpContext.Current.Response.End();

        //        }

        //        finally
        //        {
        //            // Always call Close when done reading.
        //            con.Close();

        //        }
        //        return retRows;
        //    }


        //}


        public static int AddToTableReturnID(string InsertSQL)
        {
            int retVal = 0;


            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand command = new SqlCommand(InsertSQL, con);
                con.Open();
                try
                {
                    command.ExecuteNonQuery();
                    SqlCommand cmdIdentity = new SqlCommand("Select @@IDENTITY", con);
                    //HttpContext.Current.Response.Write("Select " + fieldname + " from " + DBPrefix + tablename + " where " + fieldname + "=" + tmpStr + tmpextraclause);
                    //HttpContext.Current.Response.End();  
                    SqlDataReader readIdentity = cmdIdentity.ExecuteReader();
                    if (readIdentity.Read())
                        retVal = Convert.ToInt32(readIdentity.GetValue(0));
                }
                finally
                {
                    // Always call Close when done reading.
                    con.Close();

                }
                return retVal;
            }


        }


        public static DataSet GetTableData(string selectSQL)
        {
            DataSet retVal = null;
            //HttpContext.Current.Response.Write(selectSQL);
            //HttpContext.Current.Response.End();
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
                con.Open();
                try
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    retVal = ds;
                }
                catch
                {
                    // HttpContext.Current.Response.Write(selectSQL);
                    // HttpContext.Current.Response.End();
                }
                finally
                {
                    //  Always call Close when done reading.
                    //con.Close();

                }
                return retVal;
            }
        }


        public static bool DeleteFile(string filename, string foldername)
        {
            bool retVal = false;
            try
            {
                filename = filename.Substring(0, filename.Length - 1);
                string[] arrimage = filename.ToString().Split('+');
                for (int i = 0; i < arrimage.Length; i++)
                {
                    string filepath = HttpContext.Current.Server.MapPath(foldername + "/" + arrimage[i].ToString());
                    if (File.Exists(filepath))
                    {
                        HttpContext.Current.Response.Write(filepath);
                        File.Delete(filepath);
                        retVal = true;
                    }
                }
            }
            catch { }

            return retVal;
        }


        public static string getRandomAlphaNumeric()
        {

            RandomNumberGenerator rm;
            rm = RandomNumberGenerator.Create();

            byte[] data = new byte[3];

            rm.GetNonZeroBytes(data);

            string sRand = "";
            string sTmp = "";

            for (int nCnt = 0; nCnt <= data.Length - 1; nCnt++)
            {
                int nVal = Convert.ToInt32(data.GetValue(nCnt));

                if ((nVal >= 48 && nVal <= 57) || (nVal >= 65 && nVal <= 90) || (nVal >= 97 && nVal <= 122))
                {
                    sTmp = Convert.ToChar(nVal).ToString();
                }
                else
                {
                    sTmp = nVal.ToString();
                }

                sRand += sTmp.ToString();
            }

            return sRand;
        }


        public static string getRandomAlphaNumericWithSixteendigit()
        {
            System.Security.Cryptography.RandomNumberGenerator rm;
            rm = System.Security.Cryptography.RandomNumberGenerator.Create();

        ReStart:
            byte[] data = new byte[8];

            rm.GetNonZeroBytes(data);

            string sRand = "";
            string sTmp = "";

            for (int nCnt = 0; nCnt <= data.Length - 1; nCnt++)
            {
                int nVal = Convert.ToInt32(data.GetValue(nCnt));

                if ((nVal >= 48 && nVal <= 57) || (nVal >= 65 && nVal <= 90) || (nVal >= 97 && nVal <= 122))
                {
                    if (nVal >= 97 && nVal <= 122 && nVal > 90)
                        sTmp += Convert.ToChar(nVal - 32);
                    else
                        sTmp += Convert.ToChar(nVal).ToString();
                }
                else
                {
                    sTmp = nVal.ToString();
                }

                sRand += sTmp.ToString();
            }

            if (sRand.Length > 16)
            {
                sTmp = "";
                sTmp += sRand.Substring(0, 4) + "-";
                sTmp += sRand.Substring(4, 4) + "-";
                sTmp += sRand.Substring(8, 4) + "-";
                sTmp += sRand.Substring(12, 4);
            }
            else
                goto ReStart;

            return sTmp;

        }


        public enum CompareType
        {
            text = 1, number = 2
        }

        public static string GetFieldName(string fieldvalue, string tablename, string displayfield, string comparefield, CompareType comparefieldType, string extraClause)
        {
            if (fieldvalue == "") return "";
            string tmpcompare;
            if (comparefieldType == CompareType.text)
                tmpcompare = comparefield + "='" + fieldvalue + "' ";
            else
                tmpcompare = comparefield + "=" + fieldvalue + " ";

            string tmpSQL = "Select " + displayfield + " from " + DBPrefix + tablename + " where " + tmpcompare + extraClause;
            //HttpContext.Current.Response.Write(tmpSQL + "<br/>");
            //HttpContext.Current.Response.End();
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbConnect"].ToString()))
            {
                SqlCommand command = new SqlCommand(tmpSQL, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                        return reader[0].ToString();
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }

            }
            return "";

        }



        public static void CheckSessionExists(string sessionVariable, string loginPage, string myAccountPage)
        {

            if (HttpContext.Current.Session[sessionVariable] == null)
            {

                if (HttpContext.Current.Request.ServerVariables["REQUEST_METHOD"].ToString() != "POST")
                {

                    string currentpath = "";
                    currentpath = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                    currentpath += "/" + HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
                    currentpath += "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
                    HttpContext.Current.Session["RedirectPath"] = currentpath;
                    //      HttpContext.Current.Response.Write(HttpContext.Current.Session["RedirectPath"].ToString());
                    //    HttpContext.Current.Response.End();
                    // HttpContext.Current.Session["RedirectPath"] = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
                }

                else
                    HttpContext.Current.Session["RedirectPath"] = myAccountPage;

                HttpContext.Current.Response.Redirect(loginPage);
            }
            //HttpContext.Current.Response.Write(HttpContext.Current.Session["RedirectPath"].ToString());
            // HttpContext.Current.Response.End();


        }


        public static string ImageExists(string imagepath)
        {
            string retVal = "~/images/Image_not_Available.gif";

            try
            {

                string physicalPath = "";
                int n1 = imagepath.LastIndexOf("/");
                string folderPath = "";
                string filename = "";
                if (n1 > 0)
                {
                    folderPath = imagepath.Substring(0, n1);
                    filename = imagepath.Substring(n1 + 1);
                }
                physicalPath = HttpContext.Current.Server.MapPath(folderPath);
                if (File.Exists(physicalPath + "\\" + filename))
                    retVal = imagepath;
            }
            catch { }
            return retVal;

        }

        public static string ImageExistsadmin(string imagepath)
        {
            string retVal = "../images/Image_not_Available.gif";

            try
            {

                string physicalPath = "";
                int n1 = imagepath.LastIndexOf("/");
                string folderPath = "";
                string filename = "";
                if (n1 > 0)
                {
                    folderPath = imagepath.Substring(0, n1);
                    filename = imagepath.Substring(n1 + 1);
                }
                physicalPath = HttpContext.Current.Server.MapPath(folderPath);
                if (File.Exists(physicalPath + "\\" + filename))
                    retVal = imagepath;
            }
            catch { }
            return retVal;

        }
        public static string writeGreekchar(string numberString)
        {
            string strbig = "";
            string strsmal = "";

            char[] ca = numberString.ToCharArray();
            for (int i = 0; i < ca.Length; i++)
            {
                if (ca[i] == 'α' || ca[i] == 'ß' || ca[i] == 'γ' || ca[i] == '±' || ca[i] == '•')
                {
                    strbig += ca[i].ToString().Replace("α", "&alpha;").Replace("ß", "&beta;").Replace("γ", "&gamma;");
                }
                else
                {
                    strbig += ca[i];
                }

            }
            return strbig;
        }


        /*

        public static string ImageExists(string imagepath)
        {

            string retVal = "~/images/Image_not_Available.gif";

            try
            {

                string physicalPath = "";
                int n1 = imagepath.Substring(0, imagepath.LastIndexOf("/") - 1).LastIndexOf("/");
                string folderPath = "";
                string filename = "";
                if (n1 > 0)
                {
                    folderPath = imagepath.Substring(0, n1);
                    filename = imagepath.Substring(n1 + 1);
                }
                physicalPath = HttpContext.Current.Server.MapPath(folderPath);
               // HttpContext.Current.Response.Write(physicalPath);
                if (File.Exists(physicalPath + "\\" + filename))
                    retVal = imagepath;
            }
            catch { }
            return retVal;

        }

        */

        /*public static string SendMail(string sFrom, string sTo, string sSub, string sMsg)
         {

             MailMessage msgMail = new MailMessage();

             msgMail.To.Add(new MailAddress(sTo));
             msgMail.From = new MailAddress(sFrom);
             msgMail.Subject = sSub;


             string strBody = sMsg;
             msgMail.Body = strBody;

             SmtpClient client = new SmtpClient();
             try
             {
                 client.Send(msgMail);
             }
             catch (Exception e)
             {
                 return e.Message;
             }


             return "";

         }*/

        //public static string SendMailHtmlFromat1(string sForm, string sTo, string sSub, string sMsg)
        //{
        //    MailMessage mailmsg = new MailMessage();
        //    DataSet mailDS = ReturnMilConfig();
        //    if (System.Web.Configuration.WebConfigurationManager.AppSettings["MailAuthenticationRequired"].ToString().ToLower() == "yes")
        //    {
        //      //  mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = System.Web.Configuration.WebConfigurationManager.AppSettings["MailHostServerName"].ToString();
        //        mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;

        //        mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverport"] = 25;

        //        mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
        //       mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] = System.Web.Configuration.WebConfigurationManager.AppSettings["MailUsername"].ToString();               
        //        mailmsg.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = System.Web.Configuration.WebConfigurationManager.AppSettings["MailPassword"].ToString();

        //    }

        //    mailmsg.To = sTo;
        //    mailmsg.From = sForm;
        //    mailmsg.Subject = sSub;
        //    mailmsg.BodyFormat = MailFormat.Html;            
        //    mailmsg.Body = sMsg;
        //    try
        //    {
        //        //SmtpMail.SmtpServer = "mail.ibcnet.com";
        //        SmtpMail.SmtpServer = mailDS.Tables[0].Rows[0][1].ToString().Trim();
        //        SmtpMail.Send(mailmsg);
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //    return "";

        //}
        public static string SendMailHtmlFromat(string sFrom, string sTo, string sSub, string sMsg)
        {
            MailMessage mailmsg = new MailMessage();
            mailmsg.To.Add(sTo);
            mailmsg.To.Add("sma@peptechcorp.com"); 
            mailmsg.From = new MailAddress(sFrom);
            mailmsg.Subject = sSub;
            mailmsg.IsBodyHtml = true;
            mailmsg.Body = sMsg;
            try
            {
                //SmtpClient smtp = new SmtpClient("cudaout.media3.net", 25);  
                SmtpClient smtp = new SmtpClient("secure.emailsrvr.com");  
                smtp.Credentials = new System.Net.NetworkCredential("sma@peptechcorp.com", "P@$$w0rd");
                smtp.Send(mailmsg);
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            //return "";
        }

        public static DataSet ReturnMilConfig()
        {
            DataSet ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "mailsetting");
            return ds;
        }
        public static string SaveFile(HttpPostedFile file, string uploadFolder)
        {
            if (file.FileName != "")
            {
                // Specify the path to save the uploaded file to.
                string savePath = HttpContext.Current.Server.MapPath(uploadFolder) + "\\";

                // Get the name of the file to upload.
                int n1 = file.FileName.LastIndexOf("\\");
                string fileName = file.FileName.Substring(n1 + 1);

                // Create the path and file name to check for duplicates.
                string pathToCheck = savePath + fileName;

                // Create a temporary file name to use for checking duplicates.
                string tempfileName = "";

                // Check to see if a file already exists with the
                // same name as the file to upload.        
                if (File.Exists(pathToCheck))
                {
                    int counter = 2;
                    while (File.Exists(pathToCheck))
                    {
                        // if a file with this name already exists,
                        // prefix the filename with a number.
                        tempfileName = counter.ToString() + fileName;
                        pathToCheck = savePath + tempfileName;
                        counter++;
                    }

                    fileName = tempfileName;
                }

                // Append the name of the file to upload to the path.
                savePath += fileName;
                file.SaveAs(savePath);

                return fileName;
            }
            else
            {
                return "";
            }
        }

        public static string ExtractString(string content, int StartPos, string StartText, string EndText)
        {
            int n1, n2;
            StartText = HttpUtility.HtmlEncode(StartText);
            EndText = HttpUtility.HtmlEncode(EndText);

            if (StartText.Equals(""))
                n1 = 0;
            else
                n1 = content.IndexOf(StartText, StartPos) + StartText.Length;

            if (EndText.Equals(""))
                n2 = content.Length;
            else
                n2 = content.IndexOf(EndText, StartPos);
            //HttpContext.Current.Response.Write(content.Length+"<br>");
            //HttpContext.Current.Response.Write(n1+","+n2);
            //HttpContext.Current.Response.End();
            string FinalString;
            if (n1 >= 0 && n2 > 0)
                FinalString = content.Substring(n1, n2 - n1);
            else
                FinalString = "";

            return FinalString;
        }

        public static string FindAndReplacCensorWord(string strtext)
        {
            string[] censor = strtext.Split((" ").ToCharArray());
            DataTable dt_censor = customUtility.GetTableData("select censorword from " + customUtility.DBPrefix + "censorword where isactive=1").Tables[0];

            string strTextCencered = "";
            for (int i = 0; i < censor.Length; i++)
            {
                foreach (DataRow dr in dt_censor.Rows)
                {
                    if (censor[i].ToLower().Equals(dr["censorword"].ToString().ToLower()))
                    {
                        censor[i] = censor[i].Replace(dr["censorword"].ToString().ToLower(), "xxxxx");
                        break;
                    }
                }

                strTextCencered += censor[i] + " ";
            }
            return strTextCencered;
        }
        public static string EncryptData(string dataText)
        {
            CryptoRC4.RC4Engine es = new CryptoRC4.RC4Engine();
            es.EncryptionKey = "ab48495fdjk4950dj39405fk";
            es.InClearText = dataText;
            es.Encrypt();
            return es.CryptedText.ToString();
        }
        public static string DecryptData(string dataText)
        {
            CryptoRC4.RC4Engine es = new CryptoRC4.RC4Engine();
            es.EncryptionKey = "ab48495fdjk4950dj39405fk";
            es.CryptedText = dataText;
            es.Decrypt();
            return es.InClearText.ToString();
        }
    }

    public class productnamesearch
    {
        private string productname = "";
        public string searchproduct
        {
            get
            {
                return productname;
            }
            set
            {
                productname = value;
            }
        }
    }

    public class productsearch1
    {
        private string state = "";
        private string city = "";
        private string category = "";
        private string subcategory = "";
        private string description = "";
        public string pdesc
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string pstate
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public string pcity
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string pcategory
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        public string psubcategory
        {
            get
            {
                return subcategory;
            }
            set
            {
                subcategory = value;
            }
        }

    }


    public class Reg
    {
        private string emailid = "";
        private string password = "";

        public string regemailid
        {
            get
            {
                return emailid;
            }
            set
            {
                emailid = value;
            }
        }

        public string regpassword
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }


    public class shippingcharge
    {
        private string shippingvalue = "";
        public string shipvalue
        {
            get
            {
                return shippingvalue;
            }
            set
            {
                shippingvalue = value;
            }
        }
    }

    public class grandamount
    {
        private string totalamt = "";
        public string totalamount
        {
            get
            {
                return totalamt;
            }
            set
            {
                totalamt = value;
            }
        }
    }
}