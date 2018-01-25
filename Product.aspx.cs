using System;
using System.IO;
using System.Text;
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

public partial class Product : System.Web.UI.Page
{
    private Int32 rowindex = 0;
    public string strformula = "";
    public string pid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["npath"] = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        Session["npath"] += "/" + HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
        Session["npath"] += "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["CNO"] != null && Request.QueryString["CNO"].ToString() != "")
                {
                    DataTable dtProduct = customUtility.GetTableData("select id from " + customUtility.DBPrefix + "product where status=1 and BaseProductNumber='" + Request.QueryString["CNO"].ToString() + "'").Tables[0];
                    if (dtProduct.Rows.Count > 0)
                    {
                        pid = dtProduct.Rows[0][0].ToString();
                    }
                }
                if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() != "")
                {
                     pid = Request.QueryString["pid"].ToString();
                }

                if(pid != null && pid != "")
                {
                    Session["pid"] = pid;
                    DataTable dtCatalog = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "catalog where status=1 and productid=" + pid).Tables[0];
                    if (dtCatalog.Rows.Count == 0)
                    {
                        grdListProperty.EmptyDataText = "No catalog available";
                        grdListProperty.DataBind();
                    }
                    else
                    {
                        grdListProperty.DataSource = dtCatalog;
                        grdListProperty.DataBind();
                    }
                    Bind();
                    

                    DataTable dtProduct = customUtility.GetTableData("select p.*,(select categoryname from " + customUtility.DBPrefix + "category where id=p.categoryid)categoryname,(select subcategoryname from " + customUtility.DBPrefix + "subcategory where id=p.subcategoryid)subcategoryname from " + customUtility.DBPrefix + "product p where status=1 and id=" + pid).Tables[0];
                    if (dtProduct.Rows.Count > 0)
                    {
                        lblProduct.Text = dtProduct.Rows[0]["productname"].ToString();
                        lblCAS.Text = dtProduct.Rows[0]["CAS"].ToString();

                        lblFormula.Text = writeFormula(dtProduct.Rows[0]["formula"].ToString());
                        double dd = Convert.ToDouble(dtProduct.Rows[0]["MWeight"]);
                        lblMWeight.Text = string.Format("{0:0.00}", dd);
                        if (dtProduct.Rows[0]["ProductImage"].ToString() != "")
                        {
                            imgProduct.ImageUrl = "~/ProductImage/" + dtProduct.Rows[0]["ProductImage"];
                        }
                        else
                            imgProduct.ImageUrl = "images/imafe-not-found.jpg";
                    }
                }
            }
            catch(Exception e1)
            {
                Response.Redirect("Categories.aspx");
            }
        }
    }
    public void Bind()
    {        
        DataTable dtCatalog = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "catalog where status=1 and productid=" + Session["pid"].ToString()).Tables[0];
        if (dtCatalog.Rows.Count > 0)
        {
            grdListProperty.DataSource = dtCatalog;
            grdListProperty.DataBind();
        }
    }
    protected string writeFormula(string numberString)
    {
        string strbig = "";

        char[] ca = numberString.ToCharArray();
        for (int i = 0; i < ca.Length; i++)
        {
            if (ca[i] > 57 || ca[i] < 48)
            {
                strbig += ca[i];
            }
            else
            {
                strbig += "<span style='FONT-SIZE: 7pt; VERTICAL-ALIGN: sub; LINE-HEIGHT: 16pt; FONT-FAMILY: verdana'>" + ca[i] + "</span>";
            }

        }
        return strbig;
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.NewPageIndex = grdListProperty.PageIndex;
        Bind();
    }
    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Catalog where id=" + e.CommandArgument).Tables[0];
        if (dt.Rows.Count > 0)
        {
            NewCartPacket nPacket = new NewCartPacket();
            nPacket.ProductID = dt.Rows[0]["ID"].ToString();
            nPacket.ProductName = dt.Rows[0]["CatalogName"].ToString();
            nPacket.Productquantity = dt.Rows[0]["Quantity"].ToString();
            TextBox txt = (TextBox)grdListProperty.Rows[Convert.ToInt32(e.CommandName)].Cells[3].Controls[1];
            nPacket.Quantity = Convert.ToInt32(txt.Text);
            nPacket.ActualPrice = Convert.ToSingle(dt.Rows[0]["Price"].ToString());
            nPacket.TotalPrice = (Convert.ToSingle(dt.Rows[0]["Price"].ToString()));
            Session["NewCartPacket"] = nPacket;
            string errorString;
            if (Session["UserID"] != null)
            {
                if (ShoppingCart.AddToCart(Session["UserId"].ToString(), nPacket, out errorString))
                    Response.Redirect("~/Mycart.aspx");
                    //Response.Redirect("~/Mycart.aspx?Catalog=" + dt.Rows[0]["CatalogName"].ToString());
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = errorString.ToString();
                }
            }
            else
            {
                Session["RedirPath"] = "~/Mycart.aspx?Catalog=" + dt.Rows[0]["CatalogName"].ToString();
                Response.Redirect("Login.aspx");
            }
        }
    }
    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            LinkButton lnkb = (LinkButton)e.Row.FindControl("LinkButton1");
            lnkb.CommandName = grdListProperty.Rows.Count.ToString();
        }
    }
}
