using System;
using System.IO;
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

public partial class Forgetpassowrd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void imgsubmit_onclick(object sender, ImageClickEventArgs e)
    {
        string SqlStrPwd = "select * from " + customUtility.DBPrefix + "memberlist where email='" + txtemail.Text.Replace("'", "''").Trim() + "'";
        DataTable dtpwd = customUtility.GetTableData(SqlStrPwd).Tables[0];
        if (dtpwd.Rows.Count > 0)
        {
            string subject = "";
            string msg = "";
            string SqlStrMail = "select * from " + customUtility.DBPrefix + "Messages where id=2";
            DataSet dsMail = customUtility.GetTableData(SqlStrMail);
            if (dsMail.Tables[0].Rows.Count > 0)
            {
                subject = dsMail.Tables[0].Rows[0]["subject"].ToString();
                msg = HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["text"].ToString());
                msg = msg.Replace("##FirstName##", dtpwd.Rows[0]["fname"].ToString().Replace("''", "'"));
                msg = msg.Replace("##LastName##", dtpwd.Rows[0]["lname"].ToString().Replace("''", "'"));
                msg = msg.Replace("##UserName##", dtpwd.Rows[0]["username"].ToString().Replace("''", "'"));
                msg = msg.Replace("##Password##", customUtility.DecryptData(dtpwd.Rows[0]["password"].ToString()).Replace("''", "'"));
            }
            customUtility.SendMailHtmlFromat("admin@peptechcorp.com", dtpwd.Rows[0]["email"].ToString().Replace("''", "'"), subject, msg);

            lblmsg.Visible = true;
            lblmsg.Text = "Your Password has been sent to your email-id ";
            Response.Redirect("Login.aspx?f=1");
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "We can’t find your email in our database.  Please check the spelling and try again.";

        }
    }
}
