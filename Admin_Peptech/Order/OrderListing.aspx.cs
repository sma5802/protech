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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

public partial class Admin_Peptech_Order_OrderListing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String includeCalanderScript = Page.ResolveClientUrl("js_calendar/calendar.js");
        Page.ClientScript.RegisterClientScriptInclude("CalenderScript", includeCalanderScript);
        if (Session["AdminID"] == null || Session["AdminID"] == "")
            Response.Redirect("../login.aspx?requestpath=" + Request.Url.ToString());
      
        if (!IsPostBack)
        {
            //if (Request.QueryString["status"] != null && Request.QueryString["status"].ToString() != "")
            //{
            //    DropDownListStatusSearch.Items.FindByText(Request.QueryString["status"].ToString()).Selected = true;
                fillgridview();
            //}
            //else
            //{
            //   fillgridview();
            //}
        }

    }
    protected void fillgridview()
    {

        DataSet ds;

        //if (DropDownListStatusSearch.SelectedValue != "All Order")
        //{
     
            ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "order where status=1 order by orderID desc");
            //Response.Write("select *,(select FirstName+' '+LastName from " + customUtility.DBPrefix + "_user where " + customUtility.DBPrefix + "user.id=" + customUtility.DBPrefix + "order.UserId) as name from " + customUtility.DBPrefix + "order where orderStatus='" + DropDownListStatusSearch.SelectedValue + "'order by orderID desc");
            //Response.End();
        //}
        //else
        //{
        //    ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "order order by orderID desc");
        //}

        //ds = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "order where OrderStatus='" + Request.QueryString["status"].ToString() + "' order by orderID desc");
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }
    protected void OnCommand_Delete(object sender, CommandEventArgs e)
    {
        customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "order where orderno='" + e.CommandArgument.ToString() + "'");
        customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "orderdetail where orderno='" + e.CommandArgument.ToString() + "'");
        customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "shippinfo where orderno='" + e.CommandArgument.ToString() + "'");
        fillgridview();
    }
    protected void GridViw1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string dtFrom = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
        string dtTo = Convert.ToDateTime(txtToDate.Text).ToShortDateString();
        DataSet ds;
        string strselect = "select * from " + customUtility.DBPrefix + "order where OrderDate >= '" + dtFrom + "' and orderdate <='" + dtTo + "' order by orderno desc";
      
        ds = customUtility.GetTableData(strselect);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void Gridview1_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Row.FindControl("ImageStatus");
            CheckBox objchb = (CheckBox)e.Row.FindControl("chbispaid");
          //  DropDownList drp = (DropDownList)e.Row.FindControl("DropDownListStatus");
            //drp.SelectedValue = drv["OrderStatus"].ToString();

            if (drv.Row["Ispaid"].ToString().ToLower().Equals("true"))
            {
                img.ImageUrl = "../../images/status-on.gif";
                objchb.Checked = true;
                objchb.Enabled = false;
            }
            else
            {
                img.ImageUrl = "../../images/status-off.gif";
                objchb.Checked.Equals(false);
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        string hidorderid = "", errormsg = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HiddenField objhidorderid = (HiddenField)GridView1.Rows[i].FindControl("hidorderid");
            CheckBox objchb = (CheckBox)GridView1.Rows[i].FindControl("chbispaid");

            if (objchb.Checked && objchb.Enabled)
            {

                #region Send Gift Certificate
                string orderno = customUtility.GetFieldName(objhidorderid.Value, "order", "orderno", "orderid", customUtility.CompareType.number, "");
                string userid = customUtility.GetFieldName(objhidorderid.Value, "order", "UserID", "orderid", customUtility.CompareType.number, "");

                string SQLquery = "select * from " + customUtility.DBPrefix + "individualcoupon where orderno='" + orderno + "' and coupontype='Giftcertificate'";
                DataTable dtgift = customUtility.GetTableData(SQLquery).Tables[0];
                if (dtgift.Rows.Count > 0)
                {
                    for (int num = 0; num < dtgift.Rows.Count; num++)
                    {
                        //send mail to receiver gift certificate
                        string receiveremail, receivermessage, receivername, message = "", sendermemail, sendername;
                        string ssub;

                        receiveremail = dtgift.Rows[num]["receiveremail"].ToString();
                        receivername = dtgift.Rows[num]["receivername"].ToString();
                        receivermessage = dtgift.Rows[num]["message"].ToString().Replace(",", " ");

                        string giftid = dtgift.Rows[num]["couponid"].ToString();
                        string tmpimagename = "";


                        sendermemail = customUtility.GetFieldName(userid, "user", "email", "ID", customUtility.CompareType.number, "");
                        sendername = customUtility.GetFieldName(userid, "user", "Firstname", "ID", customUtility.CompareType.number, "") + " " + customUtility.GetFieldName(userid, "user", "Lastname", "ID", customUtility.CompareType.number, "");
                        ssub = "Gift certificate form larakent.com";
                        DataTable dt = customUtility.GetTableData("select pagedata from " + customUtility.DBPrefix + "edit where pagename='Gift Certificate Email' and displaytype='Email'").Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            message = dt.Rows[0]["pagedata"].ToString();

                            Bitmap bmp;

                            //From..
                            bmp = GenerateImage("~/GiftCertOrgImages/larakent_01.jpg", "#000000", 100, 162, 258, 57,
                                sendername, 14, "Arial", FontStyle.Bold, StringAlignment.Near);
                            tmpimagename = giftid + "tmplarakent_01.jpg";

                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#000000", 100, 180, 258, 57,
                                sendermemail, 14, "Arial", FontStyle.Regular, StringAlignment.Near);

                            tmpimagename = giftid + "larakent_01.jpg";

                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                            //delete the tmp file
                            //if File.Exists(
                            message = message.Replace("/userfiles/image/larakent_01.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/" + tmpimagename);

                            //To...
                            bmp = GenerateImage("~/GiftCertOrgImages/larakent_03.jpg", "#FFFFFF", 100, 6, 258, 57,
                                receivername, 14, "Arial", FontStyle.Bold, StringAlignment.Near);

                            tmpimagename = giftid + "tmplarakent_03.jpg";
                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 22, 258, 57,
                                receiveremail, 14, "Arial", FontStyle.Regular, StringAlignment.Near);

                            tmpimagename = giftid + "tmp2larakent_03.jpg";

                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 40, 300, 70,
                             receivermessage, 14, "Arial", FontStyle.Regular, StringAlignment.Near);
                            tmpimagename = giftid + "larakent_03.jpg";
                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                            message = message.Replace("/userfiles/image/larakent_03.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/" + tmpimagename);
                            //delete the file


                            tmpimagename = giftid + "larakent_02.jpg";
                            //Amount..
                            string giftprice = "";
                            giftprice = String.Format("{0:c}", dtgift.Rows[num]["discountamt"]);
                            bmp = GenerateImage("~/GiftCertOrgImages/larakent_02.jpg", "#000000", 146, 187, 63, 30,
                                giftprice, 16, "Arial", FontStyle.Bold, StringAlignment.Near);
                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                            message = message.Replace("/userfiles/image/larakent_02.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/" + tmpimagename);

                            string couponno = dtgift.Rows[num]["couponno"].ToString();
                            //Coupon Code & Issue Date..
                            bmp = GenerateImage("~/GiftCertOrgImages/larakent_04.jpg", "#FFFFFF", 10, 40, 258, 30,
                                couponno, 16, "Verdana", FontStyle.Regular, StringAlignment.Far);


                            tmpimagename = giftid + "tmplarakent_04.jpg";

                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);



                            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 98, 165, 30,
                                DateTime.Today.ToLongDateString(), 14, "Verdana", FontStyle.Regular, StringAlignment.Far);

                            tmpimagename = giftid + "larakent_04.jpg";
                            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);


                            message = message.Replace("/userfiles/image/larakent_04.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/" + tmpimagename);
                            message = message.Replace("/userfiles/image/larakent_05.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/larakent_05.jpg");
                            message = message.Replace("/userfiles/image/larakent_06.jpg", "http://larakent.com.tmp.secure-xp.net/User_GiftCertificates/larakent_06.jpg");
                            //delete the file

                            bmp.Dispose();
                            bmp = null;

                            /*        if (File.Exists(Server.MapPath("~/User_GiftCertificates/tmplarakent_03.jpg")))
                                        File.Delete(Server.MapPath("~/User_GiftCertificates/tmplarakent_03.jpg"));

                                    if (File.Exists(Server.MapPath("~/User_GiftCertificates/tmplarakent_04.jpg")))
                                        File.Delete(Server.MapPath("~/User_GiftCertificates/tmplarakent_04.jpg"));*/



                            //message = message.Replace("##RECEIVERNAME##", receivername);
                            //message = message.Replace("##PRICE##", String.Format("{0:c}", dtgift.Rows[num]["discountamt"]));
                            //message = message.Replace("##REFERENCENO##", dtgift.Rows[num]["couponno"].ToString());
                            // message = message.Replace("##MESSAGE##", dtgift.Rows[num]["message"].ToString());
                            // message = message.Replace("##SENDERNAME##", sendername);
                        }
                        //Response.Write(HttpUtility.HtmlDecode(message));

                        customUtility.SendMailHtmlFromat(sendermemail, receiveremail, ssub, HttpUtility.HtmlDecode(message));



                    }
                }

                #endregion
                hidorderid += objhidorderid.Value + ",";
            }

        }
        if (!hidorderid.Equals(""))
        {
            Response.Write(hidorderid);
            Response.End();
            hidorderid = hidorderid.Substring(0, hidorderid.Length - 1);
            //customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "order set Ispaid=1 where orderid in (" + hidorderid + ")");
            string[] order = hidorderid.Split(',');
            for (int i = 0; i < order.Length; i++)
            {
                DataTable dtusr = customUtility.GetTableData("select userno,orderno from " + customUtility.DBPrefix + "Order where id=" + order[i]).Tables[0];
                if (dtusr.Rows.Count > 0)
                {
                    sendPaymentMail(dtusr.Rows[0]["userno"].ToString(), dtusr.Rows[0]["orderno"].ToString());
                }
            }
            fillgridview();
        }
        else
        {
            errormsg = "Please select atleast one not paid order.";
            lblmessage.Text = errormsg;
            lblmessage.Visible = true;
        }
    }
    public void sendPaymentMail(string userno,string orderno)
    {
        string sub = "";
        string subadmin = "";
        DataTable dtemail = customUtility.GetTableData("select email from " + customUtility.DBPrefix + "memberlist where id=" + userno).Tables[0];
        if (dtemail.Rows.Count > 0)
        {
            //string SqlStrItem = "Select * from " + customUtility.DBPrefix + "shoppingBagTmp where Userid='" + Session["UserID"] + "' ";
            //DataTable dtitemcount = customUtility.GetTableData(SqlStrItem).Tables[0];
            //int itemcount = dtitemcount.Rows.Count;
            //int i = 0;
            //float totalitem = 0;
            //int ss;
            //for (i = 0; i < itemcount; i++)
            //{
            //    totalitem += float.Parse(dtitemcount.Rows[i]["Total"].ToString());
            //}


            //Billing Info......
            #region Get billing info
            DataTable dtBill = customUtility.GetTableData("select ml.*,(select country from " + customUtility.DBPrefix + "country where id=ml.Country) countryname from " + customUtility.DBPrefix + "billinfo ml where ml.OrderNo ='" + orderno + "'").Tables[0];
            if (dtBill.Rows.Count <= 0)
            {
                return;
            }

            DataRow drBill = dtBill.Rows[0];
            //int BillID = Convert.ToInt32(drBill["Id"]);
            string BFirstName = Convert.ToString(drBill["FirstName"]);
            string BLastName = Convert.ToString(drBill["LastName"]);
            string BAddress1 = Convert.ToString(drBill["address1"]);
            string BAddress2 = "";
            BAddress2 = Convert.ToString(drBill["address2"]);
            if (BAddress2 != "")
                BAddress2 = ", " + BAddress2;
            string BCity = Convert.ToString(drBill["City"]);
            //string BState = customUtility.GetFieldName(Convert.ToString(drBill["BilState"]), "state", "state", "id", customUtility.CompareType.number, "");
            //string BCountry = customUtility.GetFieldName(Convert.ToString(drBill["BilCountry"]), "Country", "Country", "id", customUtility.CompareType.number, "");
            string BState = Convert.ToString(drBill["state"]);
            string BCountry = Convert.ToString(drBill["countryname"]);
            string BZip = drBill["Zip"].ToString();
            string BPhone = Convert.ToString(drBill["Phone"]);
            string BEmail = Convert.ToString(drBill["Email"]);
            // }


            #endregion

            //get Ship info...
            #region Get Shiping info
            //DataTable dtShip = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "ShippInfo sf where OrderNo ='" + Session["orderno"].ToString() + "'").Tables[0];
            DataTable dtShip = customUtility.GetTableData("select sf.*,(select country from " + customUtility.DBPrefix + "country where id=sf.country)countryname from " + customUtility.DBPrefix + "ShippInfo sf where sf.OrderNo ='" + orderno + "'").Tables[0];
            if (dtShip.Rows.Count <= 0)
            {
                return;
            }
            DataRow drShip = dtShip.Rows[0];
            int ShippID = Convert.ToInt32(drShip["ShippID"]);
            string SFirstName = Convert.ToString(drShip["FirstName"]);
            string SLastName = Convert.ToString(drShip["LastName"]);

            string SAddress = Convert.ToString(drShip["Address1"]);
            string SCity = Convert.ToString(drShip["City"]);
            string SState = Convert.ToString(drShip["state"]);//customUtility.GetFieldName(Convert.ToString(drShip["State"]), "state", "state", "id", customUtility.CompareType.number, "");
            //string SCountry = customUtility.GetFieldName(Convert.ToString(drShip["Country"]), "Country", "Country", "id", customUtility.CompareType.number, "");
            string SCountry = Convert.ToString(drShip["countryname"]);
            string SZip = drShip["Zip"].ToString();
            string SPhone = Convert.ToString(drShip["Phone"]);
            string SEmail = Convert.ToString(drShip["Email"]);
            // }

            #endregion

            //Get Card Info...

            //#region Get Card info
            //CreditCardInfo card = null;
            //CreditCardStruct cardStruct = null;


            //if (Session[customUtility.DBPrefix + "CardInfo"] == null)
            //{
            //    //lblMessage.Text = "Card has been expired";
            //    return;
            //}
            //card = (CreditCardInfo)Session[customUtility.DBPrefix + "CardInfo"];
            //cardStruct = card.GetCreditCardInfo();

            //if (cardStruct == null)
            //{
            //    //lblMessage.Text = msgCardInfoProtect;
            //    //lblMessage.Text = "msgCardInfoProtect";
            //    return;
            //}

            //#endregion


            #region Mail Message Content
            // Order information send to client. 
            string MailMessage = "";
            
            string strOrderEmail = "select * from " + customUtility.DBPrefix + "Messages where Type='Order Email' ";
            //Response.Write(strOrderEmail);
            //  Response.End();
            DataSet dsOrderEmail = customUtility.GetTableData(strOrderEmail);
            if (dsOrderEmail.Tables[0].Rows.Count > 0)
            {
                sub = dsOrderEmail.Tables[0].Rows[0]["subject"].ToString();
                subadmin = dsOrderEmail.Tables[0].Rows[0]["subjectadmin"].ToString();
                MailMessage = dsOrderEmail.Tables[0].Rows[0]["text"].ToString();
            }
            #endregion


            #region make Mail Message
            //MailMessage = MailMessage.Replace("##orderno##", Session["orderno"].ToString());
            MailMessage = MailMessage.Replace("##orderno##", orderno);


            MailMessage = MailMessage.Replace("##First Name##", BFirstName);
            MailMessage = MailMessage.Replace("##Last Name##", BLastName);
            MailMessage = MailMessage.Replace("##Bill Address##", BAddress1 + BAddress2);
            MailMessage = MailMessage.Replace("##City##", BCity);
            MailMessage = MailMessage.Replace("##State##", BState);
            MailMessage = MailMessage.Replace("##Zip##", BZip);
            MailMessage = MailMessage.Replace("##Phone##", BPhone);
            MailMessage = MailMessage.Replace("##Email##", BEmail);
            MailMessage = MailMessage.Replace("##country##", BCountry);
            MailMessage = MailMessage.Replace("##Ship First Name##", SFirstName);
            MailMessage = MailMessage.Replace("##Ship Last Name##", SLastName);
            MailMessage = MailMessage.Replace("##Ship Address##", SAddress);
            MailMessage = MailMessage.Replace("##Ship city##", SCity);
            MailMessage = MailMessage.Replace("##Ship state##", SState);
            MailMessage = MailMessage.Replace("##Ship Zip##", SZip);
            MailMessage = MailMessage.Replace("##Ship Phone##", SPhone);
            MailMessage = MailMessage.Replace("##Ship Email##", SEmail);
            MailMessage = MailMessage.Replace("##Ship country##", SCountry);


            string BeforeLoop, WithinLoop, AfterLoop;

            BeforeLoop = customUtility.ExtractString(MailMessage, 0, "", "##Startcart##");
            WithinLoop = customUtility.ExtractString(MailMessage, 0, "##Startcart##", "##Endcart##");
            AfterLoop = customUtility.ExtractString(MailMessage, 0, "##Endcart##", "");

            double discountamount = 0;
            float OrderAmount1 = 0F;
            float OrderDiscount1 = 0F;
            float NetAmount1 = 0F;
            float ShippingCharge = 0F;
            string middleMessage = "";
            string tmpWithinLoop = "";
            string sMessage = "";
            string newstr = "";
            //string strOrderMail = "select *,qty*price as totalprice,total from " + customUtility.DBPrefix + "orderdetail where OrderNo ='" + Session["orderno"].ToString() + "'";
            string strOrderMail = "select *,qty*price as totalprice,total,(select productname from " + customUtility.DBPrefix + "product where id in (select productid from " + customUtility.DBPrefix + "catalog where id=" + customUtility.DBPrefix + "orderdetail.productid)) as prodname from " + customUtility.DBPrefix + "orderdetail where OrderNo ='" + orderno + "'";
            DataTable dtOrderMail = customUtility.GetTableData(strOrderMail).Tables[0];
            if (dtOrderMail.Rows.Count > 0)
            {
                for (int tempOrderCountx = 0; tempOrderCountx < dtOrderMail.Rows.Count; tempOrderCountx++)
                {
                    string productcode = "";
                    string P_Id = "";
                    P_Id = dtOrderMail.Rows[tempOrderCountx]["ProductId"].ToString();
                    tmpWithinLoop = WithinLoop;
                    tmpWithinLoop = tmpWithinLoop.Replace("##ProductName##", dtOrderMail.Rows[tempOrderCountx]["productname"].ToString() + "<br>" + dtOrderMail.Rows[tempOrderCountx]["prodname"].ToString());

                    tmpWithinLoop = tmpWithinLoop.Replace("##Price##", String.Format("{0:c}", dtOrderMail.Rows[tempOrderCountx]["price"]));
                    tmpWithinLoop = tmpWithinLoop.Replace("##Quantity##", dtOrderMail.Rows[tempOrderCountx]["Qty"].ToString());

                    tmpWithinLoop = tmpWithinLoop.Replace("##Option##", newstr);
                    tmpWithinLoop = tmpWithinLoop.Replace("##TotalPrice##", String.Format("{0:c}", dtOrderMail.Rows[tempOrderCountx]["total"]));

                    discountamount = Convert.ToDouble(dtOrderMail.Rows[tempOrderCountx]["price"]) * Convert.ToInt32(dtOrderMail.Rows[tempOrderCountx]["Qty"]) - Convert.ToDouble(dtOrderMail.Rows[tempOrderCountx]["total"]);
                    tmpWithinLoop = tmpWithinLoop.Replace("##discount1##", String.Format("{0:c}", discountamount));

                    middleMessage = middleMessage + tmpWithinLoop;

                    sMessage = BeforeLoop + middleMessage + AfterLoop;

                }
                //string strOrderamount = "select * from " + customUtility.DBPrefix + "order where OrderNo ='" + Session["orderno"].ToString() + "'";
                string strOrderamount = "select * from " + customUtility.DBPrefix + "order where OrderNo ='" + orderno + "'";
                //customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "order set ispaid=1 where orderno='" + orderno + "'");

                DataTable dtOrderamount = customUtility.GetTableData(strOrderamount).Tables[0];
                if (dtOrderamount.Rows.Count > 0)
                {
                    sMessage = sMessage.Replace("##orderdate##", string.Format("{0:d}",dtOrderamount.Rows[0]["OrderDate"]));
                    OrderAmount1 = Convert.ToSingle(dtOrderamount.Rows[0]["OrderAmount"]);
                    OrderDiscount1 = Convert.ToSingle(dtOrderamount.Rows[0]["OrderDiscount"]);
                    NetAmount1 = Convert.ToSingle(dtOrderamount.Rows[0]["NetAmount"]);
                    ShippingCharge = Convert.ToSingle(dtOrderamount.Rows[0]["shippingcharge"]);
                    sMessage = sMessage.Replace("##subTotal##", String.Format("{0:c}", OrderAmount1));
                    //sMessage = sMessage.Replace("##discount##", String.Format("{0:c}", OrderDiscount1));
                    sMessage = sMessage.Replace("##netamount##", String.Format("{0:c}", NetAmount1));
                    // sMessage = sMessage.Replace("##shipping##", String.Format("{0:c}", TotalShipCharges));
                    sMessage = sMessage.Replace("##shipping##", String.Format("{0:c}", ShippingCharge));
                }
            }
            //Response.Write(sMessage);
            //Response.End();

            #endregion
            //customUtility.SendMailHtmlFromat("service@peptechcorp.com", BEmail.ToString(), "Peptech: Order Detail(for Billing Person)", sMessage);
            customUtility.SendMailHtmlFromat("service@peptechcorp.com", dtemail.Rows[0]["email"].ToString(), sub, sMessage);
            customUtility.SendMailHtmlFromat("service@peptechcorp.com", "service@peptechcorp.com", subadmin, sMessage);
        }
    }

    protected Bitmap GenerateImage(string filename, string BrushColor, int leftOffset, int topOffset,
      int rectWidth, int rectHeight, string drawingText, float fontSize, string fontName, FontStyle fontStyle, StringAlignment horizontalAlign)
    {
        // Create a new 32-bit bitmap image.
        Bitmap bitmap;

        bitmap = new Bitmap(Server.MapPath(filename));

        // Create a graphics object for drawing.
        Graphics g = Graphics.FromImage(bitmap);
        g.SmoothingMode = SmoothingMode.HighQuality;
        Rectangle rect = new Rectangle(leftOffset, topOffset, rectWidth, rectHeight);

        /*        // Fill in the background.
                SolidBrush hatchBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(BrushColor));
                //HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
                g.FillRectangle(hatchBrush, rect);*/

        // Set up the text font.
        SizeF size;
        fontSize++;
        Font font;
        // Adjust the font size until the text fits within the image.
        do
        {
            fontSize--;
            font = new Font(fontName, (float)FontUnit.Point((int)fontSize).Unit.Value, fontStyle, GraphicsUnit.World);
            size = g.MeasureString(drawingText, font);
        } while (size.Width > rect.Width);

        // Set up the text format.
        StringFormat format = new StringFormat();
        format.Alignment = horizontalAlign;
        format.LineAlignment = StringAlignment.Near;

        // Create a path using the text and warp it randomly.
        GraphicsPath path = new GraphicsPath();
        path.AddString(drawingText, font.FontFamily, (int)font.Style, font.Size, rect, format);

        SolidBrush solidBrush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(BrushColor));
        g.FillPath(solidBrush, path);

        // Clean up.
        font.Dispose();
        //hatchBrush.Dispose();
        g.Dispose();

        // Set the image.
        return bitmap;
    }

    protected void SearchByOrder_Click(object sender, EventArgs e)
    {
        fillgridview();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    HiddenField hf = (HiddenField)gr.FindControl("hidorderid");
        //    DropDownList drp = (DropDownList)gr.FindControl("DropDownListStatus");
        //    //Response.Write(drp.SelectedValue.ToString());
        //    //Response.End();
        //    string UpdateUser = "update " + customUtility.DBPrefix + "Order set OrderStatus='" + drp.SelectedValue + "' where OrderId=" + hf.Value;
        //    //Response.Write(UpdateUser);
        //    //Response.End();
        //    customUtility.ExecuteNonQuery(UpdateUser);
        //}

        // to update paid status and send gift Certificate....
        lblmessage.Visible = false;
        string hidorderid = "", errormsg = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            HiddenField objhidorderid = (HiddenField)GridView1.Rows[i].FindControl("hidorderid");
            CheckBox objchb = (CheckBox)GridView1.Rows[i].FindControl("chbispaid");

            if (objchb.Checked && objchb.Enabled)
            {

                //#region Send Gift Certificate
                //string orderno = customUtility.GetFieldName(objhidorderid.Value, "order", "orderno", "orderid", customUtility.CompareType.number, "");
                //string userid = customUtility.GetFieldName(objhidorderid.Value, "order", "UserID", "orderid", customUtility.CompareType.number, "");

                //string SQLquery = "select * from " + customUtility.DBPrefix + "individualcoupon where orderno='" + orderno + "' and coupontype='Giftcertificate'";
                //DataTable dtgift = customUtility.GetTableData(SQLquery).Tables[0];
                //if (dtgift.Rows.Count > 0)
                //{
                //    for (int num = 0; num < dtgift.Rows.Count; num++)
                //    {
                //        //send mail to receiver gift certificate
                //        string receiveremail, receivermessage, receivername, message = "", sendermemail, sendername;
                //        string ssub;

                //        receiveremail = dtgift.Rows[num]["receiveremail"].ToString();
                //        receivername = dtgift.Rows[num]["receivername"].ToString();
                //        receivermessage = dtgift.Rows[num]["message"].ToString().Replace(",", " ");

                //        string giftid = dtgift.Rows[num]["couponid"].ToString();
                //        string tmpimagename = "";


                //        sendermemail = customUtility.GetFieldName(userid, "user", "email", "ID", customUtility.CompareType.number, "");
                //        sendername = customUtility.GetFieldName(userid, "user", "Firstname", "ID", customUtility.CompareType.number, "") + " " + customUtility.GetFieldName(userid, "user", "Lastname", "ID", customUtility.CompareType.number, "");
                //        ssub = "Gift certificate form AkadWelling.com";
                //        DataTable dt = customUtility.GetTableData("select pagedata from " + customUtility.DBPrefix + "edit where pagename='Gift Certificate Email' and displaytype='Email'").Tables[0];

                //        if (dt.Rows.Count > 0)
                //        {
                //            message = dt.Rows[0]["pagedata"].ToString();

                //            Bitmap bmp;

                //            //From..
                //            bmp = GenerateImage("~/GiftCertOrgImages/AkdWelling_01.jpg", "#000000", 100, 162, 258, 57,
                //                sendername, 14, "Arial", FontStyle.Bold, StringAlignment.Near);

                //            tmpimagename = giftid + "tmpAkdWelling_01.jpg";
                //            //Response.Write(Server.MapPath("User_GiftCertificates") + "\\" + tmpimagename);
                //            //Response.End();
                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                //            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#000000", 100, 180, 258, 57,
                //                sendermemail, 14, "Arial", FontStyle.Regular, StringAlignment.Near);

                //            tmpimagename = giftid + "AkdWelling_01.jpg";

                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                //            //delete the tmp file
                //            //if File.Exists(
                //            message = message.Replace("/userfiles/image/AkdWelling_01.jpg", "http://www.akadwelling.com/User_GiftCertificates/" + tmpimagename);

                //            //To...
                //            bmp = GenerateImage("~/GiftCertOrgImages/AkdWelling_03.jpg", "#FFFFFF", 100, 6, 258, 57,
                //                receivername, 14, "Arial", FontStyle.Bold, StringAlignment.Near);

                //            tmpimagename = giftid + "tmpAkdWelling_03.jpg";
                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                //            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 22, 258, 57,
                //                receiveremail, 14, "Arial", FontStyle.Regular, StringAlignment.Near);

                //            tmpimagename = giftid + "tmp2AkdWelling_03.jpg";

                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                //            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 40, 300, 70,
                //             receivermessage, 14, "Arial", FontStyle.Regular, StringAlignment.Near);
                //            tmpimagename = giftid + "AkdWelling_03.jpg";
                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);
                //            message = message.Replace("/userfiles/image/AkdWelling_03.jpg", "http://www.akadwelling.com/User_GiftCertificates/" + tmpimagename);
                //            //delete the file


                //            tmpimagename = giftid + "AkdWelling_02.jpg";
                //            //Amount..
                //            string giftprice = "";
                //            giftprice = String.Format("{0:c}", dtgift.Rows[num]["discountamt"]);
                //            bmp = GenerateImage("~/GiftCertOrgImages/AkdWelling_02.jpg", "#000000", 146, 187, 63, 30,
                //                giftprice, 16, "Arial", FontStyle.Bold, StringAlignment.Near);
                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);

                //            message = message.Replace("/userfiles/image/AkdWelling_02.jpg", "http://www.akadwelling.com/User_GiftCertificates/" + tmpimagename);

                //            string couponno = dtgift.Rows[num]["couponno"].ToString();
                //            //Coupon Code & Issue Date..
                //            bmp = GenerateImage("~/GiftCertOrgImages/AkdWelling_04.jpg", "#FFFFFF", 10, 40, 258, 30,
                //                couponno, 16, "Verdana", FontStyle.Regular, StringAlignment.Far);


                //            tmpimagename = giftid + "tmpAkdWelling_04.jpg";

                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);



                //            bmp = GenerateImage("~/User_GiftCertificates/" + tmpimagename, "#FFFFFF", 100, 98, 165, 30,
                //                DateTime.Today.ToLongDateString(), 14, "Verdana", FontStyle.Regular, StringAlignment.Far);

                //            tmpimagename = giftid + "AkdWelling_04.jpg";
                //            bmp.Save(Server.MapPath("~/User_GiftCertificates") + "\\" + tmpimagename, ImageFormat.Jpeg);


                //            message = message.Replace("/userfiles/image/AkdWelling_04.jpg", "http://www.akadwelling.com/User_GiftCertificates/" + tmpimagename);
                //            message = message.Replace("/userfiles/image/AkdWelling_05.jpg", "http://www.akadwelling.com/User_GiftCertificates/AkdWelling_05.jpg");
                //            message = message.Replace("/userfiles/image/AkdWelling_06.jpg", "http://www.akadwelling.com/User_GiftCertificates/AkdWelling_06.jpg");
                //            //delete the file

                //            bmp.Dispose();
                //            bmp = null;

                //            /*        if (File.Exists(Server.MapPath("~/User_GiftCertificates/tmpAkdWelling_03.jpg")))
                //                        File.Delete(Server.MapPath("~/User_GiftCertificates/tmpAkdWelling_03.jpg"));

                //                    if (File.Exists(Server.MapPath("~/User_GiftCertificates/tmpAkdWelling_04.jpg")))
                //                        File.Delete(Server.MapPath("~/User_GiftCertificates/tmpAkdWelling_04.jpg"));*/



                //            //message = message.Replace("##RECEIVERNAME##", receivername);
                //            //message = message.Replace("##PRICE##", String.Format("{0:c}", dtgift.Rows[num]["discountamt"]));
                //            //message = message.Replace("##REFERENCENO##", dtgift.Rows[num]["couponno"].ToString());
                //            // message = message.Replace("##MESSAGE##", dtgift.Rows[num]["message"].ToString());
                //            // message = message.Replace("##SENDERNAME##", sendername);
                //        }
                //        //Response.Write(HttpUtility.HtmlDecode(message));

                //        customUtility.SendMailHtmlFromat(sendermemail, receiveremail, ssub, HttpUtility.HtmlDecode(message));



                //    }
                //}

                //#endregion
                hidorderid += objhidorderid.Value + ",";
            }
        }

        if (!hidorderid.Equals(""))
        {
            hidorderid = hidorderid.Substring(0, hidorderid.Length - 1);
            //Response.Write("update " + customUtility.DBPrefix + "order set Ispaid=1,OrderStatus='Confirmed' where orderid in (" + hidorderid + ")");
            //Response.End();
            customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "order set Ispaid=1,OrderStatus='Confirmed' where orderid in (" + hidorderid + ")");
            string[] order = hidorderid.Split(',');
            for (int i = 0; i < order.Length; i++)
            {
              
                DataTable dtusr = customUtility.GetTableData("select userno,orderno from " + customUtility.DBPrefix + "Order where orderid=" + order[i]).Tables[0];
                if (dtusr.Rows.Count > 0)
                {
                    sendPaymentMail(dtusr.Rows[0]["userno"].ToString(), dtusr.Rows[0]["orderno"].ToString());
                }
            }
            fillgridview();
        }
        else
        {
            //errormsg = "Please select atleast one not paid order.";
            //lblmessage.Text = errormsg;
            //lblmessage.Visible = true;
        }


        // fill grid view after updation
        fillgridview();
    }
    protected void lnkdownload_click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Response.Redirect("downloadexcel.aspx?status=order" + "&stdate=" + txtstdate.Text + "&enddate=" + txtenddate.Text);
        }
    }
}
