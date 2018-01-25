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

public partial class Admin_Peptech_Order_DownLoadExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["status"] != null)
        {
            if (Request.QueryString["status"].ToString() == "mailinglist")
            {
                DataSet dsExcel = customUtility.GetTableData("select * from " + customUtility.DBPrefix + Request.QueryString["status"].ToString() + " where email!='' order by ID desc");
                GridView1.DataSource = dsExcel;
                GridView1.DataBind();
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Type", "application/application/vnd.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=Mailing_List.xls");
            }
            if (Request.QueryString["status"].ToString() == "user")
            {
                DataSet dsExcel = customUtility.GetTableData("select " + customUtility.DBPrefix + "user.id,email,password,question,answer,Companyname,firstname,middlename,lastname,address1,address2,city,zip,phone,state, " + customUtility.DBPrefix + "shippingcountry.country from " + customUtility.DBPrefix + Request.QueryString["status"].ToString() + " join " + customUtility.DBPrefix + "shippingcountry on " + customUtility.DBPrefix + "user.country=" + customUtility.DBPrefix + "shippingcountry.country_id where email!='' ");
                //Response.Write("select " + customUtility.DBPrefix + "user.id,email,password,question,answer,Companyname,firstname,middlename,lastname,address1,address2,city,zip,phone,state, "+ customUtility.DBPrefix + "shippingcountry.country from " + customUtility.DBPrefix + Request.QueryString["status"].ToString() + " join " + customUtility.DBPrefix + "shippingcountry on " + customUtility.DBPrefix + "user.country=" + customUtility.DBPrefix + "shippingcountry.country_id where email!='' ");
                //Response.End();
                GridView1.DataSource = dsExcel;
                GridView1.DataBind();
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Type", "application/application/vnd.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=User_List.xls");
            }
            if (Request.QueryString["status"].ToString() == "order")
            {
                //Response.Write("select a.orderno,a.orderdate,a.orderamount,a.orderdiscount,a.salestax,a.netamount,a.ispaid,a.orderstatus,b.productname,b.price from " + customUtility.DBPrefix + "order a join " + customUtility.DBPrefix + "orderdetail b on a.orderno=b.orderno where a.orderdate between '" + Request.QueryString["stdate"].ToString() + "' and '" + Request.QueryString["enddate"].ToString() + "'");
                //Response.End();
                DataTable dtorder = customUtility.GetTableData("select a.orderno,a.orderdate,a.orderamount,a.orderdiscount,a.salestax,a.netamount,a.ispaid,a.orderstatus,b.productname,b.price from " + customUtility.DBPrefix + "order a join " + customUtility.DBPrefix + "orderdetail b on a.orderno=b.orderno where a.orderdate between '" + Request.QueryString["stdate"].ToString() + "' and '" + Request.QueryString["enddate"].ToString() + "'").Tables[0];
                if (dtorder.Rows.Count > 0)
                {
                    GridView1.DataSource = dtorder;
                    GridView1.DataBind();
                }
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Type", "application/application/vnd.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=User_List.xls");
            }

        }
    }
}
