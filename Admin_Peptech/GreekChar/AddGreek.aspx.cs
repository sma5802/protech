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

public partial class Admin_Peptech_GreekChar_AddGreek : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["add"] != null && Request.QueryString["add"].ToString() != "")
            {
                btnsubmit.Visible = true;

                pnlSubmit.DefaultButton = "btnsubmit";
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/GreekChar/Greekcharlist.aspx");
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string straddcat = "insert into " + customUtility.DBPrefix + "GreekChar(GreekChar,status) values('" + txtCounties.Text.Replace("'", "''").Trim() + "',1)";
        if (customUtility.CheckDuplicateFieldValue(txtCounties.Text.Replace("'", "''").Trim(), "GreekChar", "GreekChar", customUtility.CompareType.text, ""))
        {
            lblmessage.Text = "Greek Character already exists ! Try another Greek Character";
        }
        else
        {
            customUtility.ExecuteNonQuery(straddcat);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/GreekChar/Greekcharlist.aspx?add=1");
        }

    }

}
