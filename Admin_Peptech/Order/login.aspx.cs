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

public partial class Order_Peptech_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsrName.Focus();
    }
    protected void imgSubmit_Click1(object sender, EventArgs e)
    {
        srcaccount.SelectParameters["username"].DefaultValue = txtUsrName.Text.Replace("'", "''").ToString().Trim();
        srcaccount.SelectParameters["password"].DefaultValue = txtPwd.Text.Replace("'", "''").ToString().Trim();
        IEnumerable iteratorObject = srcaccount.Select(DataSourceSelectArguments.Empty);

        foreach (DataRowView record in iteratorObject)
        {
            if (record.Row["username"].ToString().ToLower().Trim().Equals(txtUsrName.Text.Replace("'", "''").ToString().Trim()) && record.Row["password"].ToString().ToLower().Trim().Equals(txtPwd.Text.Replace("'", "''").ToString().Trim()))
            {
                Session["AdminID"] = record.Row["id"].ToString();
                Session["admintitle"] = record.Row["username"].ToString();
                if (Request.QueryString["requestpath"] != null && Request.QueryString["requestpath"].ToString() != "")
                    Response.Redirect(Request.QueryString["requestpath"].ToString());
                else
                    Response.Redirect("Default.aspx");
            }
        }
        lblErrorMsg.Visible = true;
        lblErrorMsg.Text = "User name 0r Password is Incorrect !";
        txtUsrName.Focus();
    }
}
