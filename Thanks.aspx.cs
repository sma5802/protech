using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UserClass;

public partial class Thanks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string subject = "";
        string msg = "";
        if (Request.QueryString["fname"] != null && Request.QueryString["fname"].ToString() != "")
        {
            if (Request.QueryString["lname"] != null && Request.QueryString["lname"].ToString() != "")
            {
                lblhead.Text = " Registration Successful";
                //lblthanks.Text = "<br />Dear &nbsp;" + Request.QueryString["fname"].ToString() + "&nbsp;" + Request.QueryString["lname"].ToString() + "<br />You have been successfully registered as a member of peptechcorp.com. <br />A mail has been sent to your email address.<br /> Please login to your account and enjoy the features of the site.";
                string SqlStrMail = "select * from " + customUtility.DBPrefix + "Messages where id=4";
                DataSet dsMail = customUtility.GetTableData(SqlStrMail);
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    string fname="<strong> " + Request.QueryString["fname"].ToString() + "<strong> ";
                    string lname = "<strong> " + Request.QueryString["lname"].ToString() + "<strong> ";
                    subject = dsMail.Tables[0].Rows[0]["subject"].ToString();
                    msg = HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["text"].ToString());
                    msg = msg.Replace("##FirstName##", fname);
                    msg = msg.Replace("##LastName##", lname); 
                }
                lblthanks.Text = msg;
            }
        }
    }
}
