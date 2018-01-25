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

public partial class Mycart : System.Web.UI.Page
{
    private CheckBox cb;
    private HiddenField hddid;
    private DataSet ds;
    private int hddval;
    private string hidAllValue = "";
    private int rowcnt = 0;
    private int rowindex;
    private int order;
    private int id;
    private string cat;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["npath"] = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        Session["npath"] += "/" + HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        Session["npath"] += "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        if (!IsPostBack)
        {
            if (Session["NewCartPacket"] != null)
            {
                NewCartPacket nPacket = (NewCartPacket)Session["NewCartPacket"];
                string errorString;
                if (Session["UserID"] != null)
                {
                    ShoppingCart.AddToCart(Session["UserID"].ToString(), nPacket, out errorString);
                    Session["NewCartPacket"] = null;
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            //bindGrid();
        }
    }

    //private void bindGrid()
    //{
    //    grdListProperty.DataSource = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "ShoppingBagTMP where userid='" + Session["UserId"].ToString() + "'");
    //    grdListProperty.DataBind();
    //    if (grdListProperty.Rows.Count <= 0)
    //    {
    //        lblsub_total.Text = "0";
    //        lbltotal.Text = "0";
    //        Session["NewCartPacket"] = null;
    //        Response.Redirect("emptyshopbag.aspx");
    //    }
    //}

    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdListProperty.PageIndex = e.NewPageIndex;
        grdListProperty.DataBind();
    }

    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DataTable dt = customUtility.GetTableData("select sum(total) as total from " + customUtility.DBPrefix + "shoppingBagTmp where userid='" + Session["UserId"].ToString() + "'").Tables[0];
                lblsub_total.Text = String.Format("{0:c}", dt.Rows[0]["total"]);
                lblsubtotal.Text = dt.Rows[0]["total"].ToString();
                lbltotal.Text = String.Format("{0:c}", dt.Rows[0]["total"]);

                float gtotal = 0F;
                DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                if (dtship.Rows.Count > 0)
                {
                    //gtotal = Convert.ToSingle(lblsubtotal.Text) + Convert.ToSingle(dtship.Rows[0]["shippingcharge"].ToString());
                    gtotal = Convert.ToSingle(lblsubtotal.Text);
                    lbltotal.Text = string.Format("{0:c}", gtotal);
                }

                //int itemcount = grdListProperty.Rows.Count;
                //((LinkButton)Master.FindControl("lnkbasket")).Text = "Your Basket &nbsp;" + itemcount.ToString() + " items " + "-" + string.Format("{0:C}", dt.Rows[0]["total"]);

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            float strprice = Convert.ToSingle(Convert.ToSingle(dr["qty"]) * (Convert.ToSingle(dr["price"].ToString())));
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shoppingbagtmp set total=" + strprice.ToString() + " where tempid=" + dr.Row["tempid"].ToString());
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shoppingbagtmp set alltotal=" + strprice.ToString() + "+ shippingcharge where tempid=" + dr.Row["tempid"].ToString());
            ((Label)e.Row.FindControl("lblSubTotal")).Text = string.Format("{0:c}", strprice);
        }
    }

    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            setActive(hidAllValue, 0);
            lblmessage.Text = "Product(s) removed successfully.";
            lblmessage.ForeColor = System.Drawing.Color.Green;
            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            //Label  lblmessage;
            lblmessage.Visible = true;
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void setActive(string val, int status_value)
    {
        customUtility.ExecuteNonQuery("Delete from " + customUtility.DBPrefix + "ShoppingBagTMP where TempID in (" + val + ")");
        grdListProperty.DataBind();
        //bindGrid();
    }
    
    protected void lnkCheckout_Click(object sender, EventArgs e)
    {
        if (Session["mainuserid"] != null)
        {
            Response.Redirect("Billing-Shipping.aspx");
        }
        else
        {
            Session["RedirPath"] = "Billing-Shipping.aspx";
            Response.Redirect("Login.aspx");
        }
    }
    protected void mycart_updatecommand(object sender, CommandEventArgs e)
    {
        int ret;
        string errorString = "";
        if (Page.IsValid)
        {
            switch (e.CommandName.ToString().ToLower())
            {
                case "updatecart":
                    if (e.CommandArgument.ToString() == "all")
                    {
                        ShoppingCart.UpdateAllCart("checkid", grdListProperty, out errorString);
                        //bindGrid();
                        grdListProperty.DataBind();
                        lblmessage.Visible = true;
                        lblmessage.ForeColor = System.Drawing.Color.Red;
                        lblmessage.Text = errorString;
                    }
                    else if (Int32.TryParse(e.CommandArgument.ToString(), out ret))
                        if (ShoppingCart.UpdateCart(e.CommandArgument.ToString(), "checkid", grdListProperty, out errorString))
                            grdListProperty.DataBind();
                        //bindGrid();
                        else
                        {
                            lblmessage.Visible = true;
                            lblmessage.ForeColor = System.Drawing.Color.Red;
                            lblmessage.Text = errorString;
                        }
                    break;

                case "emptycart":
                    if (ShoppingCart.EmptyCart())
                    {
                        Session["NewCartPacket"] = null;
                        grdListProperty.DataBind();
                        //bindGrid();
                    }
                    break;
            }
        }
    }
    protected void LinkButton1_Addtocart(object sender, CommandEventArgs e)
    {
        DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shipping where id=" + e.CommandArgument).Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblship.Visible = true;
            //lblShipping.Text = string.Format("{0:c}", dt.Rows[0]["price"]);
            DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
            if (dtship.Rows.Count > 0)
                customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shippingTMP set shippingcharge=" + dt.Rows[0]["price"].ToString() + ",shipid=" + dt.Rows[0]["id"].ToString() + " where  userid='" + Session["UserID"].ToString() + "'");
            else
                customUtility.ExecuteNonQuery("insert into  " + customUtility.DBPrefix + "shippingTMP (userid,shippingcharge,shipid) values('" + Session["UserID"].ToString() + "'," + dt.Rows[0]["price"].ToString() + "," + dt.Rows[0]["id"].ToString() + ")");

            float gtotal = Convert.ToSingle(lblsubtotal.Text) + Convert.ToSingle(dt.Rows[0]["price"].ToString());
            lbltotal.Text = string.Format("{0:c}", gtotal);
        }
    }
    protected void grdListProperty_DataBound(object sender, EventArgs e)
    {
        if (grdListProperty.Rows.Count <= 0)
        {
            Session["NewCartPacket"] = null;
            Response.Redirect("emptyshopbag.aspx");
        }
    }
}