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
public partial class Admin_Peptech_ManageHome_AddHomelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        bool addnews = customUtility.AddToTable("insert " + customUtility.DBPrefix + "homelist(Title,description)values('" + ddlTitle.SelectedValue.ToString() + "','" + HttpUtility.HtmlDecode(FCKeditor1.Value).Replace("'", "''").Trim() + "')");
        if (addnews == true)
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageHome/HomeList.aspx?add=1");
    }
}
