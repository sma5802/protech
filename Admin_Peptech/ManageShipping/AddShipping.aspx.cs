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

public partial class Admin_Peptech_ManageShipping_AddShipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string straddcat = "insert into " + customUtility.DBPrefix + "shipping(Location,Service,Price,status) values('" + ddlcountry1.SelectedItem.Text  + "','" + txtService.Text.Replace("'", "''").Trim() + "','" +txtPrice.Text.Replace("'","''") + "',1)";
        if (customUtility.CheckDuplicateFieldValue(txtService.Text.Replace("'", "''").Trim(), "shipping", "Location", customUtility.CompareType.text, ""))
        {
            lblmessage.Text = "Shipping Service already exists ! Try another Shipping Service";
        }
        else
        {
            customUtility.ExecuteNonQuery(straddcat);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageShipping/ShippingList.aspx?add=1");
        }
    }
}
