using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using UserClass;

public partial class CreditCard : System.Web.UI.Page
{
    float total = 0;
    float stotal = 0;
    float salestax = 0;
    float stax = 0;
    float shipp = 0;
    string UserId;
    float shippingcharge = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------Master page section
        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://peptechcorp.com") || HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://peptechcorp.com"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://peptechcorp.com", "http://www.peptechcorp.com").Replace("https://peptechcorp.com", "https://www.peptechcorp.com"));
            }
            //else
            //{
            //    if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("/creditcard.aspx"))
            //    {
            //        if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://"))
            //            Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
            //    }
            //    else
            //    {
            //        if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("https://"))
            //            Response.Redirect(Request.Url.ToString().Replace("https://", "http://"));
            //    }
            //}
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

        //--------Credit Card  Section
        if (Session["mainuserid"] == null || Session["mainuserid"] == "")
            Response.Redirect(ConfigurationManager.AppSettings["websitepath1"]+"login.aspx");

        string scriptBlock = "<script type=\"text/javascript\">\n";
        scriptBlock += "var ClientID, CardNumber, CardType, ExpMon, ExpYear, CVVNo;\n";
        scriptBlock += "window.onload = function(){\n";
       // scriptBlock += "ClientID = \"" + ((ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1")).ClientID + "\";\n";
        scriptBlock += "CardNumber = document.getElementById(ClientID + \"_txtCreditCardNumber\");\n";
        scriptBlock += "CardType = document.getElementById(ClientID + \"_cboCardType\");\n";
        scriptBlock += "ExpMon = document.getElementById(ClientID + \"_cboExpMonth\");\n";
        scriptBlock += "ExpYear = document.getElementById(ClientID + \"_cboExpYear\");\n";
        scriptBlock += "CVVNo = document.getElementById(ClientID + \"_txtCSVNumber\");\n";
        scriptBlock += "Terms = document.getElementById(ClientID + \"_chkTerms\");\n";
        scriptBlock += "}</script>";

        if (!ClientScript.IsClientScriptBlockRegistered(this.GetType(), "CreditCardInit"))
            ClientScript.RegisterClientScriptBlock(this.GetType(), "CreditCardInit", scriptBlock);

        string scriptUrl = "js/creditcard_validation.js";
        if (!ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "CreditCardValidator"))
            ClientScript.RegisterClientScriptInclude(this.GetType(), "CreditCardValidator", scriptUrl);

        if (Session["UserID"] != null)
        {
            if (Session["UserID"].ToString().Length != 0)
            {
                UserId = Session["UserID"].ToString();
            }
        }

        if (!Page.IsPostBack)
        {
            DataTable dtcheck = customUtility.GetTableData("select orderid from " + customUtility.DBPrefix + "order where UserID='" + Session["UserID"].ToString().Replace("'", "''") + "'").Tables[0];
            if (dtcheck.Rows.Count <= 0)
                Response.Redirect(ConfigurationManager.AppSettings["websitepath1"]+"emptyshopbag.aspx");
            if (Session["orderno"] != null)
                hidorderno.Value = Session["orderno"].ToString();

            rblorder.Items.FindByValue("0").Selected = true;
            for (int i = 1; i <= 12; i++)
                cboExpMonth.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0')));

            for (int i = DateTime.Today.Year; i <= (DateTime.Today.Year + 20); i++)
                cboExpYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            Session["shipp"] = shipp;
            stotal = shipp;
            ViewState["stotal"] = stotal;
            fillgridview();

            if (Session["mainuserid"] != null || Session["mainuserid"] != "")
            {
                string strpostatus = "select PurchaseOrderNo from " + customUtility.DBPrefix + "Memberlist where id=" + Session["mainuserid"];
                DataTable dtpostatus = customUtility.GetTableData(strpostatus).Tables[0];
                if (dtpostatus.Rows.Count > 0)
                {
                    if (dtpostatus.Rows[0]["PurchaseOrderNo"].ToString().ToLower().Equals("true"))
                        rblorder.Enabled = true;
                    else
                        rblorder.Enabled = false;
                }
            }
        }
    }

    protected void fillgridview()
    {
        DataSet ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "orderdetail where orderno=(select top 1 orderno from " + customUtility.DBPrefix + "order where userid='" + Session["UserID"] + "' order by orderid desc)");
        GridView_cart.DataSource = ds;
        GridView_cart.DataBind();
        if (GridView_cart.Rows.Count <= 0)
        {
            lblsub_total.Text = "0";
            lbltotal.Text = "0";
            //Response.Redirect("emptyshopbag.aspx");
        }
    }


    protected void GridView_cart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "DeleteRow")
        {
            customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "ShoppingBagTMP where TempID=" + e.CommandArgument.ToString());
            fillgridview();
            // to find item in cart
            cartItem();
        }
    }

    public void cartItem()
    {
        MasterPage ma = (MasterPage)Page.Master;
        Label lblcart = (Label)ma.FindControl("lblcart");
        string cart = "select * from " + customUtility.DBPrefix + "ShoppingBagTMP where userid='" + Session["UserID"] + "'";
        DataTable dt = customUtility.GetTableData(cart).Tables[0];
        lblcart.Text = dt.Rows.Count.ToString();
    }

    protected void GridView_cart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        float tot;
        float lasttotal;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            string pid = (DataBinder.Eval(e.Row.DataItem, "productid")).ToString();
            Label lbltot = (Label)e.Row.FindControl("Label5");
            lbltot.Text = (DataBinder.Eval(e.Row.DataItem, "total")).ToString();
            tot = (float)Convert.ToSingle(lbltot.Text);
            lbltot.Text = string.Format("{0:c}", tot);
            total += (float)Convert.ToSingle(tot);
            lblsub_total.Text = total.ToString();
            //shippingcharge = float.Parse(drv["shippingcharge"].ToString());
            //lblShipping.Text = string.Format("{0:c}", shippingcharge);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            salestax = (float)Convert.ToSingle((total * stax) / 100);
            ViewState["salestax"] = salestax;
            lblsub_total.Text = String.Format("{0:c}", total);
            lblship.Visible = true;
            lblShipping.Text = string.Format("{0:c}", lblShipping.Text);
            lbltotal.Text = String.Format("{0:c}", total + stotal + salestax + shippingcharge);

            float gtotal = 0F;
            DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
            if (dtship.Rows.Count > 0)
            {
                gtotal = Convert.ToSingle(total) + Convert.ToSingle(stotal) + Convert.ToSingle(salestax) + Convert.ToSingle(dtship.Rows[0]["shippingcharge"].ToString());
                lblship.Visible = true;
                lblShipping.Text = String.Format("{0:c}", dtship.Rows[0]["shippingcharge"]);
                Session["shippamt"] = String.Format("{0:0.00}", dtship.Rows[0]["shippingcharge"]);
                lbltotal.Text = string.Format("{0:c}", gtotal);
            }
        }
    }

    protected bool calculateAmount(string id, float discountamt, string mode, string coupontype, out string errorMessage)
    {
        DataTable dt;
        float totalamt = 0F;
        float coupondiscount = 0F;
        string couponcode = customUtility.GetFieldName(id, "Individualcoupon", "couponno", "CouponID", customUtility.CompareType.number, "");
        dt = customUtility.GetTableData("select sum(total) as total from " + customUtility.DBPrefix + "shoppingBagTmp tmp where UserID='" + Session["UserID"].ToString() + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            totalamt = Convert.ToSingle(dt.Rows[0]["total"].ToString());
            if (mode.Equals("percent"))
                coupondiscount = totalamt * (discountamt / 100);
            else
                coupondiscount = discountamt;

            if (coupondiscount > totalamt)
            {
                errorMessage = "Sorry! Purchase amount is less then coupon amount.";
                return false;
            }
            if (coupondiscount == totalamt)
            {
                errorMessage = "BothAreSame";
                Panel1.Visible = false;
                Session["UsedCouponWithSameAmount"] = "true";
            }
            lbltotal.Visible = true;
            lbltotal.Text = String.Format("{0:c}", totalamt);
            errorMessage = "";
            return true;
        }
        else
        {
            errorMessage = "";
            return false;
        }
    }


    protected void imgPlaceOrder_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCardHolderName.Text != "")
        {
            CreditCardInfo card = new CreditCardInfo();
            card.CardHolderName = txtCardHolderName.Text;
            card.CardNumber = txtCreditCardNumber.Text;
            card.CardType = cboCardType.SelectedValue;
            card.CSVNumber = txtCSVNumber.Text;
            card.ExpiryDate = cboExpMonth.SelectedItem + cboExpYear.SelectedValue.Substring(cboExpYear.SelectedValue.Length - 2);
            Session[customUtility.DBPrefix + "CardInfo"] = card;

            customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'");

            customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "fedex where userid='" + Session["UserID"].ToString() + "'");
            Session["accountnumber"] = txtCreditCardNumber.Text;
            Session["month"] = cboExpMonth.SelectedItem.Text;
            Session["year"] = cboExpYear.SelectedValue;
            Session["cvv2"] = txtCSVNumber.Text;
            Session["sjname"] = txtCardHolderName.Text;
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "order set status=1 where orderno='" + Session["orderno"] + "'");
            DataTable dt = customUtility.GetTableData("select company from " + customUtility.DBPrefix + "memberlist where id=" + Session["mainuserid"]).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Session["comment"] = dt.Rows[0]["company"].ToString();
            }
            HttpCookie myOrder = new HttpCookie("myOrder", Session["orderno"].ToString());
            Response.Cookies.Add(myOrder);
            //Response.Redirect(ConfigurationManager.AppSettings["websitepath1"]+"SkipPayment.aspx");
            Response.Redirect("SkipPayment.aspx");
        }
        else
        {
            lblmsg.Text = "Please enter Card Holder Name";
        }
    }

    protected void rblorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblorder.SelectedValue == "0")
        {
            trorder.Visible = false;
            ImageButton1.Visible = false;
            Panel1.Visible = true;
        }
        else
        {
            rblorder.Items.FindByValue("1").Selected = true;
            Panel1.Visible = false;
            trorder.Visible = true;
            ImageButton1.Visible = true;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["mainuserid"] != null)
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Order set status=1, PurchaseOrder='" + txtPurchaseorderNumber.Text + "' where orderno='" + Session["orderno"] + "' and UserID='" + Session["UserID"].ToString() + "'");
        customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'");
        customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "fedex where userid='" + Session["UserID"].ToString() + "'");
        Session["orderno"] = hidorderno.Value.ToString();
        HttpCookie myOrder = new HttpCookie("myOrder", hidorderno.Value.ToString());
        Response.Cookies.Add(myOrder);
        Response.Redirect("Confirmation.aspx");
    }

    protected void lnkmyaccount_Click(object sender, EventArgs e)
    {
        if (Session["mainuserid"] != null && Session["mainuserid"].ToString() != "")
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath1"].ToString() + "Useraccount.aspx");
        else
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath1"].ToString() + "Login.aspx?requestpath=Useraccount.aspx");
    }

    protected void lnkstatus_click(object sender, EventArgs e)
    {
        if (lnkstatus.Text.ToString().ToLower().Equals("new account"))
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath1"].ToString() + "Registration.aspx");
        else
        {
            Session.Abandon();
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath1"].ToString() + "Login.aspx");
        }
    }
}
