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

public partial class Admin_Peptech_CMS_CMSList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void srccmsdetail_updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Content Page updated successfully";
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
    protected void frmdetail_DataBound(object sender, EventArgs e)
    {




    }
}
