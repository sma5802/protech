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

public partial class Admin_Peptech_CMS_HomeList : System.Web.UI.Page
{
    protected void srccmsdetail_updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Home Page updated successfully";
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "There is some technical difficulty with the process , please try again";
        }
    }
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        DropDownList ddlpage = (DropDownList)grdcms.BottomPagerRow.FindControl("ddlpage");
        grdcms.PageSize = int.Parse(ddlpage.SelectedValue);
    }
}
