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

public partial class Admin_Peptech_ViewQuoteRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Bind();
        }
    }
    //public void Bind()
    //{
    //    DataTable dtrequest = customUtility.GetTableData("select (ri.fname+' '+ri.Lname) as name,ri.*,(select country from peptech_country where id=ri.country)countryname,(select state from peptech_state where id=ri.state)statename from Peptech_RequestInfo ri where Enquirytype='Quote Request' order by posteddate desc").Tables[0];
    //    if (dtrequest.Rows.Count > 0)
    //    {
    //        grdcms.DataSource = dtrequest;
    //        grdcms.DataBind();
    //    }
    //}
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        DropDownList ddlpage = (DropDownList)grdcms.BottomPagerRow.FindControl("ddlpage");
        grdcms.PageSize = int.Parse(ddlpage.SelectedValue);

    }
    protected void grdcms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower() == "del")
        {
            bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "RequestInfo where id=" + e.CommandArgument.ToString());
            if (del.ToString().ToLower().Equals("true"))
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Record Deleted Successfully";
               // Bind();
                grdcms.DataBind();
            }
        }
    }
}
