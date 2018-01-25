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

public partial class Admin_Peptech_Order_PrintOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sitepath = ConfigurationManager.AppSettings["websitepath"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
                if (Request.QueryString["userid"] != null && Request.QueryString["userid"].ToString() != "")
                    fillformview();
                else
                    Response.Write("<script type='text/javascript'>window.close();</script>");
            else
                Response.Write("<script type='text/javascript'>window.close();</script>");

        }
    }

    protected void fillformview()
    {
        DataSet ds;
        Label lbl;
        int len;
        string couponid = "";
        string cno, cno1;
        string OrderNo = Request.QueryString["ID"].ToString();
        string userid = Request.QueryString["userid"].ToString();
        float tax_shipping = 0F;
        int userno = 0;
        string test = "select O.*,O.ShippingCharge as OrdShipCharg,O.salestax as TaxAmount from " + customUtility.DBPrefix + "order O inner join " + customUtility.DBPrefix + "orderdetail OD on O.orderno=OD.orderno where O.OrderNo='" + OrderNo + "'";
        ds = customUtility.GetTableData(test);
        if (ds.Tables[0].Rows.Count > 0)
        {
            FormView1.DataSource = ds;
            FormView1.DataBind();

            string getuserfromorder = "select userno from " + customUtility.DBPrefix + "order where orderno='" + OrderNo + "'";
            DataSet dsuserorder = customUtility.GetTableData(getuserfromorder);
            if (dsuserorder.Tables[0].Rows.Count > 0)
            {
                userno = Convert.ToInt32(dsuserorder.Tables[0].Rows[0]["userno"].ToString());
                string getuserinfo = "select email from " + customUtility.DBPrefix + "memberlist where id=" + userno;
                DataSet dsgetuserinfo = customUtility.GetTableData(getuserinfo);
                if (dsgetuserinfo.Tables[0].Rows.Count > 0)
                {
                    lbl = (Label)FormView1.FindControl("lblemail");
                    lbl.Text = dsgetuserinfo.Tables[0].Rows[0]["email"].ToString();
                }
            }

            Label lblPurchaseOrder = (Label)FormView1.FindControl("lblPurchaseOrder");
            string getPurchaseOrder = "select PurchaseOrder from " + customUtility.DBPrefix + "order where orderno='" + OrderNo + "'";
            DataSet dsPurchaseOrder = customUtility.GetTableData(getPurchaseOrder);
            if (dsPurchaseOrder.Tables[0].Rows.Count > 0)
            {
                if (dsPurchaseOrder.Tables[0].Rows[0]["PurchaseOrder"].ToString() != "")
                    lblPurchaseOrder.Text = dsPurchaseOrder.Tables[0].Rows[0]["PurchaseOrder"].ToString();
                else
                    lblPurchaseOrder.Text = "None";
            }

            couponid = ds.Tables[0].Rows[0]["DiscountcouponID"].ToString();
            Label lblispaid = (Label)FormView1.FindControl("lblispaid");
            if (ds.Tables[0].Rows[0]["Ispaid"].ToString().ToLower().Equals("true"))
                lblispaid.Text = "Yes";
            else
                lblispaid.Text = "No";

            Label lbltotalamount = (Label)FormView1.FindControl("lbltotalamount");
            Label lblTax = (Label)FormView1.FindControl("lblTax");

            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0]["TaxAmount"].ToString()))
                tax_shipping = Convert.ToSingle(ds.Tables[0].Rows[0]["TaxAmount"]) + Convert.ToSingle(ds.Tables[0].Rows[0]["OrdShipCharg"]);

            //  code for coupon info
            //FormView fv_coupon = (FormView)FormView1.FindControl("frmcoupon");


            //if (couponid != null && !couponid.Equals(""))
            //{
            //    ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "individualcoupon where couponid=" + couponid);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        fv_coupon.DataSource = ds;
            //        fv_coupon.DataBind();
            //        Label lbldiscountamount = (Label)fv_coupon.FindControl("lbldiscountamount");
            //        Label lblcoupontype = (Label)fv_coupon.FindControl("lblcoupontype");
            //        if (ds.Tables[0].Rows[0]["CouponType"].ToString().ToLower().Equals("individual"))
            //            lblcoupontype.Text = "Individual Coupon";
            //        else if (ds.Tables[0].Rows[0]["CouponType"].ToString().ToLower().Equals("freeship"))
            //            lblcoupontype.Text = "Free Shiping Coupon";
            //        else if (ds.Tables[0].Rows[0]["CouponType"].ToString().ToLower().Equals("giftcertificate"))
            //            lblcoupontype.Text = "Gift Certificate";
            //        if (ds.Tables[0].Rows[0]["discountmode"].ToString().ToLower().Equals("percent"))
            //            lbldiscountamount.Text = ds.Tables[0].Rows[0]["discountamt"].ToString() + " %";
            //        else
            //        {
            //            lbldiscountamount.Text = String.Format("{0:c}", ds.Tables[0].Rows[0]["discountamt"]);

            //        }
            //    }
            //    else
            //    {
            //        FormView1.FindControl("trcoupon").Visible = false;
            //        FormView1.FindControl("trhrrol").Visible = false;
            //    }
            //}
            //else
            //{
            //    FormView1.FindControl("trcoupon").Visible = false;
            //    FormView1.FindControl("trhrrol").Visible = false;
            //}

            //code for billing info
            DataSet dsbillinfo;
            dsbillinfo = customUtility.GetTableData("select ml.*,(select Country from " + customUtility.DBPrefix + "Country where id=ml.Country)Countryname from " + customUtility.DBPrefix + "billinfo ml where OrderNo='" + OrderNo + "'");
            FormView fv_bill = (FormView)FormView1.FindControl("FormView_billing");
            if (dsbillinfo.Tables[0].Rows.Count > 0)
            {
                fv_bill.Visible = true;
                fv_bill.DataSource = dsbillinfo;
                fv_bill.DataBind();
                Label lblstatebill = (Label)fv_bill.FindControl("lblstatebill");
                //if(!dsbillinfo.Tables[0].Rows[0]["country"].ToString().Equals("254"))
                //    lblstatebill.Text = dsbillinfo.Tables[0].Rows[0]["bilother"].ToString();

            }
            else
            {
                fv_bill.Visible = false;

            }

            //code for shipping info
            ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "shippinfo where OrderNo='" + OrderNo + "'");
            FormView fv_shipp = (FormView)FormView1.FindControl("FormView_Shipping");
            if (ds.Tables[0].Rows.Count > 0)
            {
                fv_shipp.DataSource = ds;
                fv_shipp.DataBind();
                Label lblctship = (Label)fv_shipp.FindControl("lblcountryship");
                Label lblshipingstate = (Label)fv_shipp.FindControl("lblshipingstate");
                if (ds.Tables[0].Rows[0]["state"].ToString() == "Others")
                    lblshipingstate.Text = ds.Tables[0].Rows[0]["Province"].ToString();

                Label lblfedex = (Label)fv_shipp.FindControl("lblfedex");
                if (ds.Tables[0].Rows[0]["fedexaccountnumber"].ToString() == null || ds.Tables[0].Rows[0]["fedexaccountnumber"].ToString() == "")
                    lblfedex.Visible = false;

                string countryname;
                countryname = customUtility.GetFieldName(ds.Tables[0].Rows[0]["Country"].ToString(), "country", "country", "id", customUtility.CompareType.text, "");
                lblctship.Text = countryname;
            }
            else
            {
                fv_shipp.Visible = false;
            }



            //code for Hear From

            //DataSet dh = customUtility.GetTableData("select source from " + customUtility.DBPrefix + "hearfrom where userid='" + userid + "'");
            //Repeater rpthear = (Repeater)FormView1.FindControl("rpthear");
            //rpthear.DataSource = dh;
            //rpthear.DataBind();

            ////code for purchase items

            ds = customUtility.GetTableData("select *,(select productname from " + customUtility.DBPrefix + "product where id in (select productid from " + customUtility.DBPrefix + "catalog where id=" + customUtility.DBPrefix + "orderdetail.productid)) as prodname from " + customUtility.DBPrefix + "orderdetail where OrderNo='" + OrderNo + "'");
            GridView GV;
            GV = (GridView)FormView1.FindControl("GridView1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = ds;
                GV.DataBind();
            }
            else
            {
                GV.Visible = false;
            }
        }
        else
            Response.Write("<script type='text/javascript'>window.close();</script>");
    }
}
