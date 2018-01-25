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

public partial class ManageMail_Message : System.Web.UI.Page
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
        //GridView grdcms;
        //FormView fb;
        if (grdcms.SelectedIndex >= 0)
        {
            HiddenField hf = (HiddenField)frmdetail.Row.FindControl("HiddenField1");
            if (hf.Value == "4")
            {
                HtmlControl ht = (HtmlControl)frmdetail.Row.FindControl("onsite");
                ht.Visible = false;
            }
        }
    }
}
