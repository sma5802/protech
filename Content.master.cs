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

public partial class Content : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://peptechcorp.com") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://peptechcorp.com"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://peptechcorp.com", "http://www.peptechcorp.com").Replace("https://peptechcorp.com", "https://www.peptechcorp.com"));
            }
            else
            {
                //if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("/creditcard.aspx"))
                //{
                //    if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://"))
                //        Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                //}
                //else
                //{
                //    if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://"))
                //        Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
                //}
            }
        }

        
        if (Session["SessionID"] == null)
        {
            Session["SessionID"] = this.Session.SessionID;
        }
        Session["UserID"] = Session["SessionID"];

        if (Session["mainuserid"] != null && Session["title"] != null)
        {
            s3.Visible = false;
            lnkmyaccount.Visible = true;
            lblUser.Text = "Welcome&nbsp;" + Session["title"].ToString();
            lnkstatus.Text = "Sign Out";
            hypsignin.Visible = false;
        }
        else
        {
            s1.Visible = false;
            s2.Visible = false;
            hypsignin.Text = "<a href='Login.aspx' class='blueheading' style='font-weight:bold'>Sign In!</a>";
            lnkstatus.Text = "New Account";
        }

        try
        {
            string SqlStrItem = "select total from " + customUtility.DBPrefix + "shoppingBagTmp where Userid='" + Session["UserID"] + "' ";
            DataTable dtitemcount = customUtility.GetTableData(SqlStrItem).Tables[0];
            int itemcount = dtitemcount.Rows.Count;
            int i = 0;
            float totalitem = 0;
            int ss;
            for (i = 0; i < itemcount; i++)
            {
                totalitem += float.Parse(dtitemcount.Rows[i]["Total"].ToString());
            }
            float chrg = 0F;
            DataTable dtchrg = customUtility.GetTableData("select shippingcharge from " + customUtility.DBPrefix + "shippingtmp where userid='" + Session["userid"] + "'").Tables[0];
            if (dtchrg.Rows.Count > 0)
            {
                chrg = float.Parse(dtchrg.Rows[0]["shippingcharge"].ToString());
            }


            if (Request.Url.ToString().Contains("CreditCard.aspx"))
            {
                lnkbasket.Visible = true;
                lnkbasket.Text = "My Cart &nbsp;" + itemcount.ToString() + " items " + "-" + string.Format("{0:C}", totalitem + chrg);
            }
            else
            {
                lnkbasket.Visible = true;
                lnkbasket.Text = "My Cart &nbsp;" + itemcount.ToString() + " items " + "-" + string.Format("{0:C}", totalitem);
            }
        }
        catch { }
    }
    protected void lnkmyaccount_Click(object sender, EventArgs e)
    {
        if (Session["mainuserid"] != null && Session["mainuserid"].ToString() != "")
            Response.Redirect("Useraccount.aspx");
        else
            Response.Redirect("Login.aspx?requestpath=Useraccount.aspx");
    }
   
    protected void lnkstatus_click(object sender, EventArgs e)
    {
        if (lnkstatus.Text.ToString().ToLower().Equals("new account"))
            Response.Redirect("Registration.aspx");
        else
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
