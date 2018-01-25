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
using System.Net.Mail;

public partial class Confirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.ToString().ToLower().Contains("www.peptechcorp.com/confirmation.aspx"))
        {
            if (!IsPostBack)
            {                
                if (Session["title"] != null && Session["mainuserid"] != null && Session["orderno"] != null)
                {
                    if (Request.Cookies["myOrder"] != null)
                    {
                        HttpCookie myOrder = Request.Cookies["myOrder"];
                        string Orderno = Request.Cookies["myOrder"].Value.ToString();

                        myOrder.Expires = DateTime.Today.AddDays(-1);
                        Response.Cookies.Add(myOrder);

                        string message = "Dear Administrator, <br>&nbsp;&nbsp;A customer has placed a new order on peptechcorp.com. <a href='https://www.peptechcorp.com/Admin_Peptech/Order/OrderListing.aspx'>Click here</a> to check on this order and confirm it.";

                        lblMessage.Text = "Thank you!  Your order has been submitted successfully.<br><br>You will receive a confirmation via email shortly.<br><br>Your <b>Order Number</b> is <b>" + Orderno + "</b>";
                       
                        customUtility.SendMailHtmlFromat("service@peptechcorp.com", "service@peptechcorp.com", "New Order in PeptechCorp", message);
                      
                        //customUtility.SendMailHtmlFromat("service@peptechcorp.com", "sma5802@hotmail.com", "New Order in PeptechCorp", message);
                        customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "shoppingBagTmp where Userid='" + Session["UserID"] + "'");
                    }
                    if (Request.Cookies["myOrder"] != null && Request.Cookies["myOrder"].Value.ToString() != "")
                    {
                        HttpCookie myOrder = Request.Cookies["myOrder"];
                        myOrder.Expires = DateTime.Today.AddDays(-1);
                        Response.Cookies.Add(myOrder);
                    }
                }               
            }
            if (Request.Cookies["myOrder"] != null && Request.Cookies["myOrder"].Value.ToString() != "")
            {
                HttpCookie myOrder = Request.Cookies["myOrder"];
                myOrder.Expires = DateTime.Today.AddDays(-1);
                Response.Cookies.Add(myOrder);
            }
        }
    }
}
