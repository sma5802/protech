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

public partial class Billing_Shipping : System.Web.UI.Page
{
    string UserId = "";
    string billid = "";
    string shipid = "", usr = "";
    DataTable dt;
    float total = 0;
    float stotal = 0;
    float salestax = 0;
    float stax = 0;
    float shipp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["flag"] = 1;

        if (Session["UserID"] != null)
            UserId = Session["UserID"].ToString();
        else
            Response.Redirect("SessionExipred.aspx");

        if (Session["mainuserid"] == null)
        {
            Session["RedirPath"] = "Billing-Shipping.aspx";
            Response.Redirect("Login.aspx");
        }
        DataTable dtcheck = customUtility.GetTableData("Select tempid from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'").Tables[0];
        if (dtcheck.Rows.Count <= 0)
            Response.Redirect("emptyshopbag.aspx");
        if (!IsPostBack)
        {
            BindShippingGrid();
            string strsqluser = "select * from " + customUtility.DBPrefix + "MemberList where id =" + Session["mainuserid"].ToString();
            //Response.Write(strsqluser);
            //Response.End();
            DataSet dsuser = customUtility.GetTableData(strsqluser);
            if (dsuser.Tables[0].Rows.Count > 0)
            {
                txtFirstName.Text = dsuser.Tables[0].Rows[0]["fname"].ToString();
                txtLastName.Text = dsuser.Tables[0].Rows[0]["lname"].ToString();
                txtphone1.Text = dsuser.Tables[0].Rows[0]["phone"].ToString();
                txtpostal.Text = dsuser.Tables[0].Rows[0]["BilZip"].ToString();
                txtStreetaddress.Text = dsuser.Tables[0].Rows[0]["BilStreet1"].ToString();
                txtStreetaddress2.Text = dsuser.Tables[0].Rows[0]["BilStreet2"].ToString();
                txtemail.Text = dsuser.Tables[0].Rows[0]["email"].ToString();
                txtcity.Text = dsuser.Tables[0].Rows[0]["BilCity"].ToString();
                ddlcountry.SelectedValue = dsuser.Tables[0].Rows[0]["BilCountry"].ToString();
                if (dsuser.Tables[0].Rows[0]["BilCountry"].ToString().Equals("254"))
                {
                    string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + dsuser.Tables[0].Rows[0]["BilCountry"].ToString();
                    DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
                    if (dtState.Rows.Count > 0)
                    {
                        txtBillotherstate.Visible = false;
                        ddlstate.Visible = true;
                        ddlstate.Items.Clear();
                        ddlstate.Items.Insert(0, new ListItem("Select State","0"));
                        ddlstate.DataSource = dtState;
                        ddlstate.DataBind();
                        ddlstate.SelectedValue = dsuser.Tables[0].Rows[0]["BilState"].ToString();
                    }
                }
                else
                {
                    ddlstate.Visible = false;
                    txtBillotherstate.Visible = true;
                    txtBillotherstate.Text = dsuser.Tables[0].Rows[0]["BilOther"].ToString().Replace("'", "''");
                }
            }
        }
    }

    private void BindShippingGrid()
    {
        if (!IsPostBack)
        {
            grdShipping.DataSource = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Shipping where status=1 and location='United States'").Tables[0];
            grdShipping.DataBind();
        }
        else
        {
            calculateshipping();
            if (ddlcountry1.SelectedValue == "254")
                grdShipping.DataSource = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Shipping where status=1 and location='" + ddlcountry1.SelectedItem.Text + "'").Tables[0];
            else
                grdShipping.DataSource = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Shipping where status=1 and location='Outside United States'").Tables[0];
            grdShipping.DataBind();
        }
    }

    protected void grdShipping_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            CheckBox chkshipping = (CheckBox)e.Row.FindControl("chkshipping");
            chkshipping.Attributes.Add("onclick", "toggleMenu(" + "this" + ",'" + dv["price"].ToString() + "');");

            //ImageButton imgaddshipping = (ImageButton)e.Row.FindControl("imgaddshipping");
            if (dv["price"].ToString() == "0")
            {
                ((Label)e.Row.FindControl("lblPrice")).Text = "No Charge";
                ((Label)e.Row.FindControl("lblLocation")).Text = "Anywhere";
                //imgaddshipping.ImageUrl = "images/enter_FedEx_account.gif";
            }
            else
            {
                ((Label)e.Row.FindControl("lblPrice")).Text = string.Format("{0:c}", dv["price"]);
                ((Label)e.Row.FindControl("lblLocation")).Text = dv["Location"].ToString();
            }
        }
    }

    protected void ddlcountry_DataBound(object sender, EventArgs e)
    {
        if (Session["title"] != null && Session["mainuserid"] != null)
        {
            
        }
        else
            Response.Redirect("Login.aspx?requestpath=" + Request.Url.ToString());
    }
    protected void chkshipping_CheckedChanged(object sender, EventArgs e)
    {
        if (chkshipping.Checked == true)
        {
            ViewState["flag"] = 0;
            if (Session["title"] != null && Session["mainuserid"] != null)
            {
                DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                if (dtship.Rows.Count > 0)
                {
                    //tbshiping.Visible = true;
                }
                txtFirstName1.Text = txtFirstName.Text.Replace("''", "'");
                txtLastName1.Text = txtLastName.Text.Replace("''", "'");
                txtphone2.Text = txtphone1.Text.Replace("''", "'");
                txtpostal1.Text = txtpostal.Text.Replace("''", "'");
                txtStreetaddress1.Text = txtStreetaddress.Text.Replace("''", "'");
                txtStreetaddress3.Text = txtStreetaddress2.Text.Replace("''", "'");
                txtemail1.Text = txtemail.Text.Replace("''", "'");
                txtcity1.Text = txtcity.Text.Replace("''", "'");
                ddlcountry1.SelectedValue = ddlcountry.SelectedValue;

                if (ddlcountry.SelectedValue != "254")
                {
                    ddlstate1.Visible = false;
                    txtshipotherstate.Visible = true;
                    txtshipotherstate.Text = txtBillotherstate.Text;
                    BindShippingGrid();
                }
                else
                {
                    string SqlStrState = "select * from " + customUtility.DBPrefix + "state where countryid= " + ddlcountry.SelectedValue;
                    DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
                    if (dtState.Rows.Count > 0)
                    {
                        ddlstate1.Items.Clear();
                        ddlstate1.Items.Insert(0, new ListItem("Select State"));
                        ddlstate1.DataSource = dtState;
                        ddlstate1.DataTextField = "state";
                        ddlstate1.DataValueField = "id";
                        ddlstate1.DataBind();
                    }
                    ddlstate1.SelectedValue = ddlstate.SelectedValue;
                }
            }
            else
                Response.Redirect("signin.aspx?requestpath=" + Request.Url.ToString());
        }
        else
        {
            txtFirstName1.Text = "";
            txtLastName1.Text = "";
            txtphone2.Text = "";
            txtpostal1.Text = "";
            txtStreetaddress1.Text = "";
            txtStreetaddress3.Text = "";
            txtemail1.Text = "";
            txtcity1.Text = "";
            txtshipotherstate.Text = "";
            txtshipotherstate.Visible = false;
            ddlstate1.Visible = true;
            string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + ddlcountry.SelectedValue;
            DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
            if (dtState.Rows.Count > 0)
            {
                ddlstate1.Items.Clear();
                ddlstate1.Items.Insert(0, new ListItem("Select State"));
                ddlstate1.DataSource = dtState;
                ddlstate1.DataTextField = "state";
                ddlstate1.DataValueField = "id";
                ddlstate1.DataBind();
            }
            ddlcountry1.SelectedIndex = 0;
            ViewState["flag"] = 1;

            customUtility.ExecuteNonQuery("delete from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'");
            //tbshiping.Visible = false;
            
        }
        BindShippingGrid();
    }

    public void insertToShipTable(string ord)
    {
        string billstate = "";
        if (ddlcountry.SelectedValue == "254")
            billstate = ddlstate.SelectedItem.Text;
        else
            billstate = txtBillotherstate.Text.Replace("'", "''");
        string sqlinsertbill = "insert into " + customUtility.DBPrefix + "BillInfo (firstname,lastname,address1,address2,city,state,province,country,zip,phone,email,sessionid,orderno,orderdate,userid)" +
            " values ('" + txtFirstName.Text.Replace("'", "''") + "','" + txtLastName.Text.Replace("'", "''") + "','" + txtStreetaddress.Text.Replace("'", "''") + "','" + txtStreetaddress2.Text.Replace("'", "''") + "','" + txtcity.Text.Replace("'", "''") + "','" + billstate + "','" + billstate + "','" + ddlcountry.SelectedValue + "','" + txtpostal.Text.Replace("'", "''") + "','" + txtphone1.Text + "','" + txtemail.Text.Replace("'", "''") + "','" + Session.SessionID.ToString() + "','" + ord + "'," + System.DateTime.Today.Date.ToShortDateString() + ",'" + UserId + "')";

        int bill = customUtility.AddToTableReturnID(sqlinsertbill);

        string statevalue = "";
        if (ddlcountry1.SelectedValue == "254")
            statevalue = ddlstate1.SelectedItem.Text;
        else
            statevalue = txtshipotherstate.Text.Replace("'", "''");

        DataTable dtfedex = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "fedex where userid='" + Session["UserID"].ToString() + "'").Tables[0];
        if (dtfedex.Rows.Count > 0)
        {
            string sqlInsertToBill = "insert into " + customUtility.DBPrefix + "ShippInfo(FirstName,LastName,Address1,city,state,Province,country,zip,phone,Email,SessionId,OrderNo,orderdate,UserId,fedexaccountnumber)";
            sqlInsertToBill += "values('" + txtFirstName1.Text.Replace("'", "''") + "','" + txtLastName1.Text.Replace("'", "''") + "','" + txtStreetaddress1.Text.Replace("'", "''") + " " + txtStreetaddress3.Text.Replace("'", "''") + "','" + txtcity1.Text.Replace("'", "''") + "','" + statevalue + "','" + statevalue + "','" + ddlcountry1.SelectedValue + "','" + txtpostal1.Text.Replace("'", "''") + "','" + txtphone2.Text + "','" + txtemail1.Text.Replace("'", "''") + "','" + Session.SessionID.ToString() + "','" + ord + "'," + System.DateTime.Today.Date.ToShortDateString() + ",'" + UserId + "','" + dtfedex.Rows[0]["fedexaccountnumber"].ToString() + "')";
            int shippp = customUtility.AddToTableReturnID(sqlInsertToBill);
            Session["shipid"] = shippp;
        }
        else
        {
            string sqlInsertToBill = "insert into " + customUtility.DBPrefix + "ShippInfo(FirstName,LastName,Address1,city,state,Province,country,zip,phone,Email,SessionId,OrderNo,orderdate,UserId)";
            sqlInsertToBill += "values('" + txtFirstName1.Text.Replace("'", "''") + "','" + txtLastName1.Text.Replace("'", "''") + "','" + txtStreetaddress1.Text.Replace("'", "''") + " " + txtStreetaddress3.Text.Replace("'", "''") + "','" + txtcity1.Text.Replace("'", "''") + "','" + statevalue + "','" + statevalue + "','" + ddlcountry1.SelectedValue + "','" + txtpostal1.Text.Replace("'", "''") + "','" + txtphone2.Text + "','" + txtemail1.Text.Replace("'", "''") + "','" + Session.SessionID.ToString() + "','" + ord + "'," + System.DateTime.Today.Date.ToShortDateString() + ",'" + UserId + "')";
            int shippp = customUtility.AddToTableReturnID(sqlInsertToBill);
            Session["shipid"] = shippp;
        }
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate.SelectedValue != "67")
        {
            txtBillotherstate.Visible = false;
            txtBillotherstate.Text = "";
        }
        else
            txtBillotherstate.Visible = true;
    }

    protected void ddlstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstate1.SelectedValue != "67")
            txtshipotherstate.Visible = false;
        else
            txtshipotherstate.Visible = true;
    }

    protected void ddlcountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcountry1.SelectedValue == "254")
        {
            ddlstate1.Visible = true;
            txtshipotherstate.Visible = false;
            string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + ddlcountry1.SelectedValue;
            DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
            if (dtState.Rows.Count > 0)
            {
                ddlstate1.Items.Clear();
                ddlstate1.Items.Insert(0, new ListItem("Select State"));
                ddlstate1.DataSource = dtState;
                ddlstate1.DataTextField = "state";
                ddlstate1.DataValueField = "id";
                ddlstate1.DataBind();
            }
        }
        else
        {
            txtshipotherstate.Text = "";
            ddlstate1.Visible = false;
            txtshipotherstate.Visible = true;
        }
    }
    private void calculateshipping()
    {
        for (int i = 0; i < grdShipping.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdShipping.Rows[i].FindControl("chkshipping");
            if (chk.Checked == true)
            {
                int id = Convert.ToInt32(((HiddenField)grdShipping.Rows[i].FindControl("hdnid")).Value);

                DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shipping where id=" + id).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["price"].ToString() == "0")
                        pnlFedexAccountnumber.Visible = true;

                    lblship.Visible = true;
                    lblShipping.Text = string.Format("{0:c}", dt.Rows[0]["price"]);
                    DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                    if (dtship.Rows.Count > 0)
                        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shippingTMP set shippingcharge=" + dt.Rows[0]["price"].ToString() + ",shipid=" + dt.Rows[0]["id"].ToString() + " where  userid='" + Session["UserID"].ToString() + "'");
                    else
                        customUtility.ExecuteNonQuery("insert into  " + customUtility.DBPrefix + "shippingTMP (userid,shippingcharge,shipid) values('" + Session["UserID"].ToString() + "'," + dt.Rows[0]["price"].ToString() + "," + dt.Rows[0]["id"].ToString() + ")");
                }
            }
        }
    }
    
    protected void lnkCheckout_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            if (Session["title"] != null && Session["mainuserid"] != null)
            {
                DataTable dtcheck=customUtility.GetTableData("Select tempid from "+customUtility.DBPrefix+"ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'").Tables[0];
                if (dtcheck.Rows.Count <= 0)
                    Response.Redirect("emptyshopbag.aspx");
                //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "fedex(userid,fedexaccountnumber)values('" + Session["UserID"].ToString() + "','" + txtfedex.Text.Replace("'", "''") + "')");
                lnkCheckout.OnClientClick = null;
                for (int i = 0; i < grdShipping.Rows.Count; i++)
                {
                    //No Charge
                    CheckBox chkshipping = (CheckBox)grdShipping.Rows[i].FindControl("chkshipping");
                    Label lblPrice = (Label)grdShipping.Rows[i].FindControl("lblPrice");
                    
                    if (chkshipping.Checked == true)
                    {
                        int id = Convert.ToInt32(((HiddenField)grdShipping.Rows[i].FindControl("hdnid")).Value);

                        DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shipping where id=" + id).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            lblship.Visible = true;
                            lblShipping.Text = string.Format("{0:c}", dt.Rows[0]["price"]);
                            DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                            if (dtship.Rows.Count > 0)
                                customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "shippingTMP set shippingcharge=" + dt.Rows[0]["price"].ToString() + ",shipid=" + dt.Rows[0]["id"].ToString() + " where  userid='" + Session["UserID"].ToString() + "'");
                            else
                                customUtility.ExecuteNonQuery("insert into  " + customUtility.DBPrefix + "shippingTMP (userid,shippingcharge,shipid) values('" + Session["UserID"].ToString() + "'," + dt.Rows[0]["price"].ToString() + "," + dt.Rows[0]["id"].ToString() + ")");
                        }
                        
                        if (lblPrice.Text == "No Charge")
                        {
                            if (txtfedex.Text == "")
                            {
                                lblfedmsg.Visible = true;
                                lblfedmsg.Text = "Please enter FedEx Account number.";
                            }
                            else
                            {
                                customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "fedex(userid,fedexaccountnumber)values('" + Session["UserID"].ToString() + "','" + txtfedex.Text.Replace("'", "''") + "')");
                            }
                        }
                    }
                }
                /// <summary>
                /// Generate Billing sessions for Skipjack
                /// </summary>
                Session["streetaddress"] = txtStreetaddress.Text;
                Session["streetaddress2"] = txtStreetaddress2.Text;
                Session["city"] = txtcity.Text;
                Session["zipcode"] = txtpostal.Text;
                
                
                if (ddlcountry.SelectedValue == "254")
                {
                    Session["country"] = "US";
                    string STcode = chkST(ddlstate.SelectedItem.Text);
                    Session["state"] = STcode;
                }
                else
                {
                    Session["country"] = ddlcountry.SelectedItem.Text;
                    Session["state"] = txtBillotherstate.Text;
                }
                Session["shiptophone"] = txtphone1.Text;
                Session["email"] = txtemail.Text;

                /// <summary>
                /// Generate Shipping sessions for Skipjack
                /// Session for Skipjack Shipping
                /// </summary>
                Session["shiptoname"] = txtFirstName1.Text + " " + txtLastName1.Text;
                Session["shiptostreetaddress"] = txtStreetaddress1.Text;
                Session["shiptostreetaddress2"] = txtStreetaddress3.Text;
                Session["shiptocity"] = txtcity1.Text;
                if (ddlcountry1.SelectedValue == "254")
                {
                    Session["shiptocountry"] = "US";
                    string STcode = chkST(ddlstate1.SelectedItem.Text);
                    Session["shiptostate"] = STcode;
                }
                else
                {
                    Session["shiptocountry"] = ddlcountry1.SelectedItem.Text;
                    Session["shiptostate"] = txtshipotherstate.Text;
                }
                Session["shiptozipcode"] = txtpostal1.Text;

                DataTable dtshipvalue = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                if (dtshipvalue.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtshipvalue.Rows[0]["shippingcharge"].ToString()) == 0)
                    {
                        DataTable dtfedex = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "fedex where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                        if (dtfedex.Rows.Count == 0)
                        {
                            Response.Write("Not Going");
                            //lblmsg.Visible = true;
                            //lblmsg.Text = "Please Enter Your FedEx Account Number.";
                        }
                        else
                        {
                            DataSet ds;
                            float stax = 0;
                            float schrg = 0;
                            float weight = 0;
                            float TotalPrice = 0;
                            int BillID = 0;
                            int ShippID = 0;
                            float OrderAmount = 0;
                            float TotalDiscount = 0;
                            int DiscountcoupounID = 0;
                            float ShippingCharge = 0;
                            float TotalShipCharges = 0;
                            int ordincr = 0;
                            int ord = 0;
                            float shipcharge = 0;
                            string fedexno = "";

                            string OrderNo;
                            string getcount = "select count(*)as ordcount from " + customUtility.DBPrefix + "order";
                            DataTable dtgetcount = customUtility.GetTableData(getcount).Tables[0];
                            if (dtgetcount.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dtgetcount.Rows[0]["ordcount"].ToString()) == 0)
                                    ord = 0;
                                else
                                {
                                    string getno = "select max(orderid) as maxorder from " + customUtility.DBPrefix + "order";
                                    DataTable dtgetno = customUtility.GetTableData(getno).Tables[0];
                                    if (dtgetno.Rows.Count > 0)
                                        ord = Convert.ToInt32(dtgetno.Rows[0]["maxorder"].ToString());
                                }
                            }

                            while (true)
                            {
                                ordincr += 1;
                                int ordamt = ord + ordincr;
                                OrderNo = "PT00" + ordamt;
                                if (!customUtility.CheckDuplicateFieldValue(OrderNo, "order", "orderno", customUtility.CompareType.text, ""))
                                    break;
                            }

                            string strShoppingCartBag = "select * from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'";
                            DataTable dtShoppingCartBag = customUtility.GetTableData(strShoppingCartBag).Tables[0];
                            if (dtShoppingCartBag.Rows.Count > 0)
                            {
                                for (int temp = 0; temp < dtShoppingCartBag.Rows.Count; temp++)
                                    OrderAmount = OrderAmount + Convert.ToSingle(dtShoppingCartBag.Rows[temp]["Total"]);

                                TotalPrice = OrderAmount;
                            }

                            DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                            if (dtship.Rows.Count > 0)
                            {
                                shipcharge = (float)Convert.ToSingle(dtship.Rows[0]["shippingcharge"].ToString());
                                Session["shippamt"] = shipcharge;
                                TotalPrice = TotalPrice + shipcharge;
                            }

                            if (Session[customUtility.DBPrefix + "CouponInfo"] != null)
                            {
                                discountinfo discount = (discountinfo)Session[customUtility.DBPrefix + "CouponInfo"];
                                if (!(discount.counponType.ToLower().Equals("freeship")))
                                    TotalDiscount = discount.CouponDiscountAmount;
                                else
                                    TotalDiscount = 0;
                                DiscountcoupounID = Convert.ToInt32(discount.CouponID);
                                TotalPrice = TotalPrice - TotalDiscount;
                            }

                            Session["amt"] = string.Format("{0:0.00}", TotalPrice);
                            string OrderDate = System.DateTime.Today.Date.ToShortDateString();

                            //Insert into order
                            string fedexaccountnumber = "";

                            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "fedex set mainuserid='" + Session["mainuserid"].ToString() + "' where userid='" + Session["userid"].ToString() + "'");
                            DataTable dtfedex1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "fedex where userid='" + Session["userid"].ToString() + "'").Tables[0];
                            if (dtfedex1.Rows.Count > 0)
                                fedexaccountnumber = dtfedex1.Rows[0]["fedexaccountnumber"].ToString();

                            //customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "Order where UserID='" + Session["UserID"].ToString().Replace("'", "''") + "' and sessionID='" + Session.SessionID + "'");
                            string strOrder = "Insert Into " + customUtility.DBPrefix + "Order (OrderNo,UserID,BillingId,ShippingID,OrderStatus,Status,orderdate,orderAmount,orderDiscount,salestax,netAmount,sessionID,IsPaid,TransactionID,DiscountCouponID,shippingcharge,userno,Fedexaccountnumber) values('" + OrderNo + "','" + Session["UserID"].ToString() + "'," + BillID + "," + ShippID + ",'Pending',0,'" + OrderDate + "'," + OrderAmount + "," + TotalDiscount + "," + TotalShipCharges + "," + TotalPrice + ",'" + Session.SessionID + "',0,'TID'," + DiscountcoupounID + "," + shipcharge + "," + Convert.ToInt32(Session["mainuserid"].ToString()) + ",'" + fedexaccountnumber.ToString() + "')";
                            int ReturnOrderID = customUtility.AddToTableReturnID(strOrder);

                            //insert into order detail table
                            bool OrderHaveGiftCertificate = false;
                            string orderstring = "";
                            string strOrderDetail = "select *,(select productname from " + customUtility.DBPrefix + "product where id=(select productid from " + customUtility.DBPrefix + "catalog where id=" + customUtility.DBPrefix + "ShoppingBagTMP.productid))as chemicalname from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'";
                            DataTable dtOrderDetail = customUtility.GetTableData(strOrderDetail).Tables[0];
                            if (dtOrderDetail.Rows.Count > 0)
                            {
                                for (int tempOrderCount = 0; tempOrderCount < dtOrderDetail.Rows.Count; tempOrderCount++)
                                {
                                    string strOrderIndividualInsert = "Insert Into " + customUtility.DBPrefix + "OrderDetail (OrderNo,ProductID,ProductName,Price,taxamount,Qty,Total,Orderdate,Status,OrderStatus,SelectedColour,SelectedSize,Manufacturer) ";
                                    strOrderIndividualInsert += " values('" + OrderNo + "'," + dtOrderDetail.Rows[tempOrderCount]["productid"] + ",'" + dtOrderDetail.Rows[tempOrderCount]["productname"].ToString().Replace("'", "''") + "'," + dtOrderDetail.Rows[tempOrderCount]["price"] + ",0," + dtOrderDetail.Rows[tempOrderCount]["Qty"] + "," + dtOrderDetail.Rows[tempOrderCount]["Total"] + ",'" + OrderDate + "',1,0,'" + dtOrderDetail.Rows[tempOrderCount]["SelectedGroupOption1"].ToString() + "','" + dtOrderDetail.Rows[tempOrderCount]["SelectedGroupOption2"].ToString() + "','" + dtOrderDetail.Rows[tempOrderCount]["Manufacturer"].ToString() + "')";
                                    orderstring += dtOrderDetail.Rows[tempOrderCount]["productname"] + "~" + dtOrderDetail.Rows[tempOrderCount]["chemicalname"].ToString() + "~" + string.Format("{0:0.00}", dtOrderDetail.Rows[tempOrderCount]["price"]) + "~" + dtOrderDetail.Rows[tempOrderCount]["Qty"] + "~N~||";
                                    customUtility.ExecuteNonQuery(strOrderIndividualInsert);
                                }
                            }
                            Session["orderstring"] = orderstring;
                            Session["orderno"] = OrderNo;
                            insertToShipTable(OrderNo);
                            Response.Redirect("CreditCard.aspx");
                        }
                    }
                    else
                    {
                        DataSet ds;
                        float stax = 0;
                        float schrg = 0;
                        float weight = 0;
                        float TotalPrice = 0;
                        int BillID = 0;
                        int ShippID = 0;
                        float OrderAmount = 0;
                        float TotalDiscount = 0;
                        int DiscountcoupounID = 0;
                        float ShippingCharge = 0;
                        float TotalShipCharges = 0;
                        int ordincr = 0;
                        int ord = 0;
                        float shipcharge = 0;
                        string fedexno = "";

                        string OrderNo;
                        string getcount = "select count(*)as ordcount from " + customUtility.DBPrefix + "order";
                        DataTable dtgetcount = customUtility.GetTableData(getcount).Tables[0];
                        if (dtgetcount.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtgetcount.Rows[0]["ordcount"].ToString()) == 0)
                                ord = 0;
                            else
                            {
                                string getno = "select max(orderid) as maxorder from " + customUtility.DBPrefix + "order";
                                DataTable dtgetno = customUtility.GetTableData(getno).Tables[0];
                                if (dtgetno.Rows.Count > 0)
                                    ord = Convert.ToInt32(dtgetno.Rows[0]["maxorder"].ToString());
                            }
                        }

                        while (true)
                        {
                            ordincr += 1;
                            int ordamt = ord + ordincr;
                            OrderNo = "PT00" + ordamt;
                            if (!customUtility.CheckDuplicateFieldValue(OrderNo, "order", "orderno", customUtility.CompareType.text, ""))
                                break;
                        }

                        string strShoppingCartBag = "select * from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'";
                        DataTable dtShoppingCartBag = customUtility.GetTableData(strShoppingCartBag).Tables[0];
                        if (dtShoppingCartBag.Rows.Count > 0)
                        {
                            for (int temp = 0; temp < dtShoppingCartBag.Rows.Count; temp++)
                                OrderAmount = OrderAmount + Convert.ToSingle(dtShoppingCartBag.Rows[temp]["Total"]);
                            TotalPrice = OrderAmount;
                        }

                        DataTable dtship = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippingTMP where userid='" + Session["UserID"].ToString() + "'").Tables[0];
                        if (dtship.Rows.Count > 0)
                        {
                            shipcharge = (float)Convert.ToSingle(dtship.Rows[0]["shippingcharge"].ToString());
                            Session["shippamt"] = shipcharge;
                            TotalPrice = TotalPrice + shipcharge;
                        }

                        if (Session[customUtility.DBPrefix + "CouponInfo"] != null)
                        {
                            discountinfo discount = (discountinfo)Session[customUtility.DBPrefix + "CouponInfo"];
                            if (!(discount.counponType.ToLower().Equals("freeship")))
                                TotalDiscount = discount.CouponDiscountAmount;
                            else
                                TotalDiscount = 0;
                            DiscountcoupounID = Convert.ToInt32(discount.CouponID);
                            TotalPrice = TotalPrice - TotalDiscount;
                        }

                        Session["amt"] = string.Format("{0:0.00}", TotalPrice);
                        string OrderDate = System.DateTime.Today.Date.ToShortDateString();

                        //Insert into order
                        string fedexaccountnumber = "";

                        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "fedex set mainuserid='" + Session["mainuserid"].ToString() + "' where userid='" + Session["userid"].ToString() + "'");
                        DataTable dtfedex1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "fedex where userid='" + Session["userid"].ToString() + "'").Tables[0];
                        if (dtfedex1.Rows.Count > 0)
                            fedexaccountnumber = dtfedex1.Rows[0]["fedexaccountnumber"].ToString();

                        //customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "Order where UserID='" + Session["UserID"].ToString().Replace("'", "''") + "' and sessionID='" + Session.SessionID + "'");
                        string strOrder = "Insert Into " + customUtility.DBPrefix + "Order (OrderNo,UserID,BillingId,ShippingID,OrderStatus,Status,orderdate,orderAmount,orderDiscount,salestax,netAmount,sessionID,IsPaid,TransactionID,DiscountCouponID,shippingcharge,userno,Fedexaccountnumber) values('" + OrderNo + "','" + Session["UserID"].ToString() + "'," + BillID + "," + ShippID + ",'Pending',0,'" + OrderDate + "'," + OrderAmount + "," + TotalDiscount + "," + TotalShipCharges + "," + TotalPrice + ",'" + Session.SessionID + "',0,'TID'," + DiscountcoupounID + "," + shipcharge + "," + Convert.ToInt32(Session["mainuserid"].ToString()) + ",'" + fedexaccountnumber.ToString() + "')";
                        int ReturnOrderID = customUtility.AddToTableReturnID(strOrder);

                        //insert into order detail table
                        bool OrderHaveGiftCertificate = false;
                        string orderstring = "";
                        string strOrderDetail = "select *,(select productname from " + customUtility.DBPrefix + "product where id=(select productid from " + customUtility.DBPrefix + "catalog where id=" + customUtility.DBPrefix + "ShoppingBagTMP.productid))as chemicalname from " + customUtility.DBPrefix + "ShoppingBagTMP where UserID ='" + Session["UserID"].ToString() + "'";
                        DataTable dtOrderDetail = customUtility.GetTableData(strOrderDetail).Tables[0];
                        if (dtOrderDetail.Rows.Count > 0)
                        {
                            for (int tempOrderCount = 0; tempOrderCount < dtOrderDetail.Rows.Count; tempOrderCount++)
                            {
                                string strOrderIndividualInsert = "Insert Into " + customUtility.DBPrefix + "OrderDetail (OrderNo,ProductID,ProductName,Price,taxamount,Qty,Total,Orderdate,Status,OrderStatus,SelectedColour,SelectedSize,Manufacturer) ";
                                strOrderIndividualInsert += " values('" + OrderNo + "'," + dtOrderDetail.Rows[tempOrderCount]["productid"] + ",'" + dtOrderDetail.Rows[tempOrderCount]["productname"].ToString().Replace("'", "''") + "'," + dtOrderDetail.Rows[tempOrderCount]["price"] + ",0," + dtOrderDetail.Rows[tempOrderCount]["Qty"] + "," + dtOrderDetail.Rows[tempOrderCount]["Total"] + ",'" + OrderDate + "',1,0,'" + dtOrderDetail.Rows[tempOrderCount]["SelectedGroupOption1"].ToString() + "','" + dtOrderDetail.Rows[tempOrderCount]["SelectedGroupOption2"].ToString() + "','" + dtOrderDetail.Rows[tempOrderCount]["Manufacturer"].ToString() + "')";
                                orderstring += dtOrderDetail.Rows[tempOrderCount]["productname"] + "~" + dtOrderDetail.Rows[tempOrderCount]["chemicalname"].ToString() + "~" + string.Format("{0:0.00}", dtOrderDetail.Rows[tempOrderCount]["price"]) + "~" + dtOrderDetail.Rows[tempOrderCount]["Qty"] + "~N~||";
                                customUtility.ExecuteNonQuery(strOrderIndividualInsert);
                            }
                        }
                        Session["orderstring"] = orderstring;
                        Session["orderno"] = OrderNo;
                        insertToShipTable(OrderNo);
                        Response.Redirect("CreditCard.aspx");
                    }
                }
                else
                {
                    Response.Write("Not Going");
                    //lblmsg.Visible = true;
                    //lblmsg.Text = "Please select shipping charge.";
                }
            }
            else
                Response.Redirect("SessionExipred.aspx");
        }
        else
            Response.Redirect("SessionExipred.aspx");
    }

    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcountry.SelectedValue != "254")
        {
            txtBillotherstate.Text = "";
            ddlstate.Visible = true;
            txtBillotherstate.Visible = true;
            ddlstate.Visible = false;
        }
        else
        {
            ddlstate.Visible = true;
            txtBillotherstate.Visible = false;
            string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + ddlcountry.SelectedValue;
            DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
            if (dtState.Rows.Count > 0)
            {
                ddlstate.Items.Clear();
                ddlstate.Items.Insert(0, new ListItem("Select State"));
                ddlstate.DataSource = dtState;
                ddlstate.DataTextField = "state";
                ddlstate.DataValueField = "id";
                ddlstate.DataBind();
            }
        }
    }

    /// <summary>
    /// To Get US State's 2 Character State Code
    /// </summary>
    public string chkST(string state)
    {
        switch (state.ToLower())
        {
            case "alabama":
                return "AL"; break;
            case "alaska":
                return "AK"; break;
            case "arizona":
                return "AZ"; break;
            case "arkansas":
                return "AR"; break;
            case "california":
                return "CA"; break;
            case "colorado":
                return "CO"; break;
            case "connecticut":
                return "CT"; break;
            case "delaware":
                return "DE"; break;
            case "florida":
                return "FL"; break;
            case "georgia":
                return "GA"; break;
            case "hawaii":
                return "HI"; break;
            case "idaho":
                return "ID"; break;
            case "illinois":
                return "IL"; break;
            case "indiana":
                return "IN"; break;
            case "iowa":
                return "IA"; break;
            case "kansas":
                return "KS"; break;
            case "kentucky":
                return "KY"; break;
            case "louisiana":
                return "LA"; break;
            case "maine":
                return "ME"; break;
            case "maryland":
                return "MD"; break;
            case "massachusetts":
                return "MA"; break;
            case "michigan":
                return "MI"; break;
            case "minnesota":
                return "MN"; break;
            case "mississippi":
                return "MS"; break;
            case "montana":
                return "MT"; break;
            case "nebraska":
                return "NE"; break;
            case "nevada":
                return "NV"; break;
            case "new hampshire":
                return "NH"; break;
            case "new jersey":
                return "NJ"; break;
            case "new mexico":
                return "NM"; break;
            case "new york":
                return "NY"; break;
            case "north carolina":
                return "NC"; break;
            case "north dakota":
                return "ND"; break;
            case "ohio":
                return "OH"; break;
            case "oklahoma":
                return "OK"; break;
            case "oregon":
                return "OR"; break;
            case "pennsylvania":
                return "PA"; break;
            case "rhode island":
                return "RI"; break;
            case "south carolina":
                return "SC"; break;
            case "south dakota":
                return "SD"; break;
            case "tennessee":
                return "TN"; break;
            case "texas":
                return "TX"; break;
            case "utah":
                return "UT"; break;
            case "vermont":
                return "VT"; break;
            case "virginia":
                return "VA"; break;
            case "washington":
                return "WA"; break;
            case "west virginia":
                return "WV"; break;
            case "wisconsin":
                return "WI"; break;
            case "wyoming":
                return "WY"; break;
            case "washington d.c.":
                return "DC"; break;
            case "alberta":
                return "AB"; break;
            case "british columbia":
                return "BC"; break;
            case "manitoba":
                return "MB"; break;
            case "new brunswick":
                return "NB"; break;
            case "newfoundland ":
                return "NF"; break;
            case "northWest territories":
                return "NT"; break;
            case "nova scotia":
                return "NS"; break;
            case "nunavut":
                return "NU"; break;
            case "prince edward island":
                return "PE"; break;
            case "quebec":
                return "PQ"; break;
            case "saskatchewan":
                return "SK"; break;
            case "yukon territory":
                return "YT"; break;
            case "ontario":
                return "ON"; break;
            default:
                return "";
        }
    }
}
