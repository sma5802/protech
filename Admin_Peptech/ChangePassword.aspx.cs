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

public partial class Admin_Peptech_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Session["AdminID"] != null)
        {
            string ss = "select ID from " + customUtility.DBPrefix + "Admin where  password='" + txtPwd.Text + "' and id=" + Session["AdminID"];
            DataSet ds = customUtility.GetTableData(ss);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Admin set password='" + txtNewPwd.Text.Replace("'", "''") + "' where id=" + Session["AdminID"]))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Your Password has been updated successfully";
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Your Password could not be updated.Please Try Again!!";
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Your have entered Incorrect Old Password";
            }
        }
    }
}
