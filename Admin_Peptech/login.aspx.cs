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

public partial class Admin_Peptech_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsrName.Focus();
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://peptechcorp.com") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://peptechcorp.com"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://peptechcorp.com", "http://www.peptechcorp.com").Replace("https://peptechcorp.com", "https://www.peptechcorp.com"));
            }
        }
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
                    Response.Redirect(Server.UrlDecode(Request.QueryString["requestpath"].ToString()));
                else
                    Response.Redirect("Default.aspx");
            }
        }
        lblErrorMsg.Visible = true;
        lblErrorMsg.Text = "User name 0r Password is Incorrect !";
        txtUsrName.Focus();
    }
}
