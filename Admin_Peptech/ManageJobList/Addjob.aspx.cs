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
using FredCK.FCKeditorV2;

public partial class Admin_Peptech_ManageJobList_Addjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string straddcat = "insert into " + customUtility.DBPrefix + "jobs(JobPosition,Location,Description,Requirement,status) values('" + txtPosition.Text.Replace("'", "''").Trim() + "','" + txtLocation.Text.Replace("'", "''").Trim() + "','" + HttpUtility.HtmlEncode(FCKeditor1.Value.Replace("'", "''")) + "','" + HttpUtility.HtmlEncode(FCKeditor2.Value.Replace("'", "''")) + "',1)";
        if (customUtility.CheckDuplicateFieldValue(txtPosition.Text.Replace("'", "''").Trim(), "jobs", "JobPosition", customUtility.CompareType.text, ""))
        {
            lblmessage.Text = "Job Positon already exists ! Try another Job Positon";
        }
        else
        {
            customUtility.ExecuteNonQuery(straddcat);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageJobList/Joblist.aspx?add=1");
        }
    }
}
