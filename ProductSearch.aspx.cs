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

public partial class ProductSearch : System.Web.UI.Page
{
    int rowcnt = 0;
    public string strvalue="";
    protected void Page_Load(object sender, EventArgs e)
    {
        //productnamesearch search = new productnamesearch();

        //search.searchproduct = Session["psearch"].ToString(); 
        
        //Response.Write(Session["psearch"]);
        //Response.End();
        if (!IsPostBack)
        {
            if (Request.Cookies["search"] != null && Request.Cookies["search"].Value.ToString() != "")
            {
                Session["psearch"] = Request.Cookies["search"].Value.ToString();
                HttpCookie myCookie = Request.Cookies["search"];
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
            }
            if (Session["psearch"] != null && Session["psearch"].ToString() != "")
            {

                //SqlDataSource sqlSearchProducts = new SqlDataSource();
                fillsearch();
                if (Request.QueryString["search"] != null && Request.QueryString["search"].ToString() != "")
                {
                    //GridView grdSearchProducts;
                    grdSearchProducts.EmptyDataText = "Please enter your Search Keyword";
                }
                else
                {
                    txtsearch1.Text = Session["psearch"].ToString().Replace("''", "'");
                }

                if (!IsPostBack)
                    if (Request.QueryString["query"] != null && Request.QueryString["query"].ToString() != "")
                        txtsearch1.Text = Request.QueryString["query"].ToString();
            }
            else
            {
                if (Request.QueryString["search"] != null && Request.QueryString["search"].ToString() != "")
                {
                    //GridView grdSearchProducts;
                    grdSearchProducts.EmptyDataText = "Please enter your Search Keyword";
                }
                else
                    Response.Redirect("default.aspx");
            }
        }
    }
    public void fillsearch()
    {
        string keyword = Session["psearch"].ToString();
        string searchkey = "";
        if (keyword.StartsWith("*") || keyword.EndsWith("*"))
        {
            searchkey = "'" + keyword.Replace("*", "%") + "'";
            //Response.Write("*string*");
        }
        else
        {
            searchkey = "'%" + keyword.Replace("*", "%") + "%'";
            //Response.Write("str*ing");
        }


        string qry = "";
        if (Request.QueryString["search"] != null && Request.QueryString["search"].ToString() != "")
        {
            qry = "";
            txtsearch1.Text = "";
            grdSearchProducts.EmptyDataText = "Please enter your Search Keyword";
        }
        else
        {
            qry = "select P.*,(select categoryname from pep$tech$corp.peptech_category where status=1 and id=P.categoryid)Category_Name,(select subcategoryname from peptech_subcategory where status=1 and id=P.subcategoryid)SubCategory_Name from peptech_product P where P.status=1 and (P.productname like " +
                    "" + searchkey + " or P.CAS like " + searchkey + " or P.formula like " + searchkey + " or P.MWeight like " + searchkey + " or P.categoryid in (select id from pep$tech$corp.peptech_category where categoryname like " + searchkey + " and status=1) " +
                    "or P.subcategoryid in (select id from peptech_subcategory where subcategoryname like " + searchkey + " and status=1) or P.id in(select productid from peptech_catalog where status=1 and catalogname like " + searchkey + "))";
        }
        //Response.Write(qry);
        try
        {
            sqlSearchProducts.SelectCommand = qry;
            sqlSearchProducts.DataBind();
            grdSearchProducts.DataBind();
        }
        catch
        {
            sqlSearchProducts.SelectCommand = "";
            sqlSearchProducts.DataBind();
            grdSearchProducts.DataBind();
        }
    }
    protected void imgsearchgo_Click(object sender, ImageClickEventArgs e)
    {
        Session["psearch"] = txtsearch1.Text.Replace("'", "''");
        //fillsearch();
        Response.Redirect("productsearch.aspx");//?query=" + txtsearch1.Text);
    }
    protected void imgstrcuture_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Quicksearch.aspx");
    }
    protected void grdSearchProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearchProducts.PageIndex = e.NewPageIndex;
        fillsearch();
    }
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DropDownList ddlPage = (DropDownList)grdSearchProducts.BottomPagerRow.FindControl("ddlPage");
        grdSearchProducts.PageSize = int.Parse(ddlPage.SelectedValue);
        fillsearch();
    }

    protected void grdSearchProducts_DataBound(object sender, EventArgs e)
    {
        if (grdSearchProducts.Rows.Count > 0)
        {
            rowcnt = int.Parse(grdSearchProducts.Rows.Count.ToString());
            Label lblPage = (Label)grdSearchProducts.BottomPagerRow.FindControl("lblPage");
            Label lblrecord = (Label)grdSearchProducts.BottomPagerRow.FindControl("lblrecord");
            Label lblPages = (Label)grdSearchProducts.BottomPagerRow.FindControl("lblPages");
            DropDownList ddlPage = (DropDownList)grdSearchProducts.BottomPagerRow.FindControl("ddlPage");
            lblPages.Text = grdSearchProducts.PageCount.ToString();


            string keyword = Session["psearch"].ToString();
            string searchkey = "";
            if (keyword.StartsWith("*") || keyword.EndsWith("*"))
                searchkey = "'" + keyword.Replace("*", "%") + "'";
            else
                searchkey = "'%" + keyword.Replace("*", "%") + "%'";

            string qry = "select count(P.id) cc from peptech_product P where P.status=1 and P.productname like " +
                "" + searchkey + " or P.CAS like " + searchkey + " or P.formula like " + searchkey + " or P.MWeight like " + searchkey + " or P.categoryid in (select id from pep$tech$corp.peptech_category where categoryname like " + searchkey + " and status=1) " +
                "or P.subcategoryid in (select id from peptech_subcategory where subcategoryname like " + searchkey + " and status=1) " +
                    "or P.subcategoryid in (select id from peptech_subcategory where subcategoryname like " + searchkey + " and status=1) or P.id in(select productid from pep$tech$corp.peptech_catalog where status=1 and catalogname like " + searchkey + ")";
            
            DataTable dt = customUtility.GetTableData(qry).Tables[0];
            lblrecord.Text = dt.Rows[0]["cc"].ToString();
            lblPage.Text = Convert.ToString(grdSearchProducts.PageIndex + 1);
            ddlPage.SelectedValue = grdSearchProducts.PageSize.ToString();
        }
    }
    protected void grdSearchProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView dr = (DataRowView)e.Row.DataItem;
            ((Label)e.Row.FindControl("lblformula")).Text = writeFormula(dr["formula"].ToString());
            try
            {
                double dd = Convert.ToDouble(dr["mweight"]);
                //Response.Write(string.Format("{0:00.00}", dd) + "__");
                ((Label)e.Row.FindControl("lblWeight")).Text = string.Format("{0:0.00}", dd);
            }
            catch
            {
                ((Label)e.Row.FindControl("lblWeight")).Text = dr["mweight"].ToString();
            }
        }
    }
    protected string writeFormula(string numberString)
    {
        string strbig = "";
        string strsmal = "";

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
        //Response.Write(strbig);
        return strbig;
    }
    protected void grdSearchProducts_Sorting(object sender, GridViewSortEventArgs e)
    {
        fillsearch();
    }
}
