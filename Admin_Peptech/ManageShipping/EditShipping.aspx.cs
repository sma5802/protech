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

public partial class Admin_Peptech_ManageShipping_EditShipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                    getnews();
            }
            catch { Response.Redirect("ShippingList.aspx"); }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        if (customUtility.CheckDuplicateFieldValue(ddlcountry1.Text.Replace("'", "''").Trim(), "shipping", "location", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()))
        {
            lblmessage.Text = "Shipping Service already exists ! Try another Shipping Service.";
        }
        else
        {
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shipping set location='" +ddlcountry1.SelectedItem.Text + "',service='" + txtService.Text.Replace("'", "''") + "',price='" + txtPrice.Text.Replace("'","''").Replace("$","") + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageShipping/ShippingList.aspx?Upd=1");
        }
        //if()
        //{
        //bool editnews = customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "jobs set JobPosition='" + txtPosition.Text.Replace("'", "''").Trim() + "',Location='" + txtLocation.Text.Replace("'", "''") + "',Description='" + HttpUtility.HtmlEncode(FCKeditor1.Value.Replace("'", "''").Trim()) + "',Requirement='" + HttpUtility.HtmlEncode(FCKeditor2.Value.Replace("'", "''").Trim()) + "' where id=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
        //if (editnews == true)

        //}
    }


    protected void getnews()
    {
        DataTable dtgetnews = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shipping where id=" + Convert.ToInt32(Request.QueryString["id"].ToString())).Tables[0];
        if (dtgetnews.Rows.Count > 0)
        {
            ddlcountry1.Items.FindByText(dtgetnews.Rows[0]["location"].ToString()).Selected=true;
            txtService.Text = dtgetnews.Rows[0]["service"].ToString().Replace("''", "'");
            if (dtgetnews.Rows[0]["price"].ToString() != null)
            {
                txtPrice.Text = dtgetnews.Rows[0]["price"].ToString();
            }
    
              
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtService.Text = "";
        //txtPrice.Text = "";
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageShipping/ShippingList.aspx");
    }
}
