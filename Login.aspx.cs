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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "memberlist set chngdpwd=0 where email='8017@in.com'");
        if (Request.QueryString["f"] != null)
        {
            if (Request.QueryString["f"] == "1" && Request.QueryString["f"].ToString() == "1")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Your password has been sent to your registered e-mail.";
            }
        }
        //if (!IsPostBack)
        //{
        //    MasterPage mp = Page.Master;
        //    ((Panel)mp.FindControl("pnlLogin")).Visible = false;
        //}
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = customUtility.GetTableData("select ID,fname,lname,password,chngdpwd from " + customUtility.DBPrefix + "memberlist where username='" + txtUsername.Text.Replace("'", "''") + "' and status=1 ");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["chngdpwd"].ToString().ToLower().Equals("true"))
            {
                if (txtPassword.Text == customUtility.DecryptData(ds.Tables[0].Rows[0]["password"].ToString()))
                {
                    Session["mainuserid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    Session["title"] = ds.Tables[0].Rows[0]["fname"].ToString() + ' ' + ds.Tables[0].Rows[0]["lname"].ToString();
                    if (Session["RedirPath"] != null) Response.Redirect(Session["RedirPath"].ToString()); else Response.Redirect("Useraccount.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid UserName/Password";
                    lblMessage.Visible = true;
                }
            }
            else
            {
                string scrpt = "<script type='text/javascript'>" +
                    "var div=document.getElementById('enlarge');div.style.visibility='visible';</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", scrpt);
                lblUsername.Text = txtUsername.Text;
                lbler.Text = "";
                txtEmail.Text = "";
                txtNewPassword.Text = "";
                txtNewCPassword.Text = "";
            }
        }
        else
        {
            lblMessage.Text = "Invalid UserName/Password";
            lblMessage.Visible = true;
        }
    }
    protected void imgChangePwd_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtchk = customUtility.GetTableData("select ID,fname,lname,chngdpwd from  " + customUtility.DBPrefix + "memberlist where email='" + txtEmail.Text.Replace("'", "''") + "' and username='" + lblUsername.Text.Replace("'", "''") + "' and status=1 ").Tables[0];
        if (dtchk.Rows.Count > 0)
        {
            string newpass = customUtility.EncryptData(txtNewPassword.Text);
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "memberlist set chngdpwd=1, password='" + newpass.Replace("'", "''") + "' where id=" + dtchk.Rows[0]["id"]);
            Session["mainuserid"] = dtchk.Rows[0]["ID"].ToString();
            Session["title"] = dtchk.Rows[0]["fname"].ToString() + ' ' + dtchk.Rows[0]["lname"].ToString();
            if (Session["RedirPath"] != null) Response.Redirect(Session["RedirPath"].ToString()); else Response.Redirect("Useraccount.aspx");
        }
        else
        {
            lbler.Text = "Email not registered under this Username. Try again.";
            string scrpt = "<script type='text/javascript'>" +
                        "var div=document.getElementById('enlarge');div.style.visibility='visible';</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", scrpt);
        }
    }
}
