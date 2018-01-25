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

public partial class Admin_Peptech_ManageJobList_Editjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                getnews();
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (customUtility.CheckDuplicateFieldValue(txtPosition.Text.Replace("'", "''").Trim(), "jobs", "JobPosition", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()))
            {
                lblmessage.Text = "Job Position already exists ! Try another Job Position.";
            }
            else
            {
                customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "jobs set JobPosition='" + txtPosition.Text.Replace("'", "''").Trim() + "',Location='" + txtLocation.Text.Replace("'", "''") + "',Description='" + HttpUtility.HtmlEncode(FCKeditor1.Value.Replace("'", "''").Trim()) + "',Requirement='" + HttpUtility.HtmlEncode(FCKeditor2.Value.Replace("'", "''").Trim()) + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                 Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageJobList/Joblist.aspx?up=1");
            }
        //if()
        //{
        //bool editnews = customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "jobs set JobPosition='" + txtPosition.Text.Replace("'", "''").Trim() + "',Location='" + txtLocation.Text.Replace("'", "''") + "',Description='" + HttpUtility.HtmlEncode(FCKeditor1.Value.Replace("'", "''").Trim()) + "',Requirement='" + HttpUtility.HtmlEncode(FCKeditor2.Value.Replace("'", "''").Trim()) + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
        //if (editnews == true)
           
        //}
    }


    protected void getnews()
    {
        DataTable dtgetnews = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "jobs where id=" + Convert.ToInt32(Request.QueryString["id"].ToString())).Tables[0];
        if (dtgetnews.Rows.Count > 0)
        {
            txtPosition.Text = dtgetnews.Rows[0]["JobPosition"].ToString().Replace("''", "'");
            txtLocation.Text = dtgetnews.Rows[0]["Location"].ToString().Replace("''", "'");
            FCKeditor1.Value = HttpUtility.HtmlDecode(dtgetnews.Rows[0]["Description"].ToString().Replace("''", "'"));
            FCKeditor2.Value = HttpUtility.HtmlDecode(dtgetnews.Rows[0]["Requirement"].ToString().Replace("''", "'"));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtPosition.Text = "";
        txtLocation.Text = "";
        FCKeditor1.Value = "";
        FCKeditor2.Value = "";
    }
}
