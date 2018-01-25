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

public partial class ProductLists : System.Web.UI.Page
{
    string strsearch = "";
    string order = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = "";
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                try
                {
                    //Response.Write("select p.*,(select CategoryName from " + customUtility.DBPrefix + "category where id=p.categoryid)CategoryName from " + customUtility.DBPrefix + "subcategory p where p.id=" + Request.QueryString["id"].ToString());
                    DataTable dtcat = customUtility.GetTableData("select p.*,(select CategoryName from " + customUtility.DBPrefix + "category where id=p.categoryid)CategoryName from " + customUtility.DBPrefix + "subcategory p where p.id=" + Request.QueryString["id"].ToString()).Tables[0];
                    if (dtcat.Rows.Count > 0)
                    {
                        hlcat.NavigateUrl = "subcategories.aspx?pid=" + dtcat.Rows[0]["categoryid"].ToString();
                        hlcat.Text = dtcat.Rows[0]["categoryname"].ToString();
                        lblSubCat.Text = dtcat.Rows[0]["Subcategoryname"].ToString();
                    }

                    fillproducts();
                    //Response.Write(str);
                    //Response.End();
                }
                catch
                {
                    Response.Redirect("default.aspx");
                }
            }
        }
    }
    public void fillproducts()
    {
        order = "";
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
            order = ViewState["sort"].ToString();
        else
        {
            if (Session["cpsort"] != null && Session["cpsort"].ToString() != "")
                order = Session["cpsort"].ToString();
        }
        //Response.Write(order + "___");
        if (Request.QueryString["sub"] != null && Request.QueryString["sub"].ToString() != "")
        {
            if (Request.QueryString["sub"].ToString() == "0")
            {
                strsearch = "select p.*,(select CategoryName from " + customUtility.DBPrefix + "category where status=1 and id=p.categoryid)CategoryName,(select Subcategoryname from " + customUtility.DBPrefix + "subcategory where status=1 and id=p.subcategoryid)Subcategoryname from " + customUtility.DBPrefix + "product p where p.status=1 and p.id=" + Request.QueryString["id"].ToString() + order;
                Label1.Visible = false;
                Label2.Visible = false;
            }
            else
                strsearch = "select p.*,(select CategoryName from " + customUtility.DBPrefix + "category where status=1 and id=p.categoryid)CategoryName,(select Subcategoryname from " + customUtility.DBPrefix + "subcategory where status=1 and id=p.subcategoryid)Subcategoryname from " + customUtility.DBPrefix + "product p where p.status=1 and p.subcategoryid=" + Request.QueryString["id"].ToString() + order;
        }
        else
            strsearch = "select p.*,(select CategoryName from " + customUtility.DBPrefix + "category where status=1 and id=p.categoryid)CategoryName,(select Subcategoryname from " + customUtility.DBPrefix + "subcategory where status=1 and id=p.subcategoryid)Subcategoryname from " + customUtility.DBPrefix + "product p where p.status=1 and p.subcategoryid=" + Request.QueryString["id"].ToString() + order;
        DataSet dsprod = customUtility.GetTableData(strsearch);
        if (dsprod.Tables[0].Rows.Count == 0)
        {
            pnlmsg.Visible = true;
        }
        else
        {
            //dlsprod.DataSource = dsprod;
            //dlsprod.DataBind();
            GotoPaging(dsprod);
        }
    }
    protected void dlsprod_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
            Session["cpsort"] = ViewState["sort"].ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
            DataRowView dr = (DataRowView)e.Item.DataItem;
            ((Label)e.Item.FindControl("lblformula")).Text = writeFormula(dr["formula"].ToString());
            try
            {
                double dd = Convert.ToDouble(dr["mweight"]);
                //Response.Write(string.Format("{0:00.00}", dd) + "__");
                ((Label)e.Item.FindControl("lblWeight")).Text = string.Format("{0:0.00}", dd);
            }
            catch
            {
                ((Label)e.Item.FindControl("lblWeight")).Text = dr["mweight"].ToString();
            }
            
            //if (dr.Row["details"].ToString() != null && dr.Row["details"] != "")
            //{
            //    if (dr.Row["details"].ToString().Length > 50)
            //    {
            //        ((Label)e.Item.FindControl("lblDesc")).Text = dr.Row["details"].ToString();
            //        ((HyperLink)e.Item.FindControl("hlMore")).Visible = true;
            //        ((HyperLink)e.Item.FindControl("hlMore")).NavigateUrl = "~/NewsDetail.aspx?id=" + dr.Row["id"].ToString();
            //    }
            //    else
            //    {
            //        ((Label)e.Item.FindControl("lblDesc")).Text = dr.Row["details"].ToString();
            //        ((HyperLink)e.Item.FindControl("hlMore")).Visible = true;
            //        ((HyperLink)e.Item.FindControl("hlMore")).NavigateUrl = "~/NewsDetail.aspx?id=" + dr.Row["id"].ToString();
            //    }
            //}
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
    protected void GotoPaging(DataSet ds)
    {
        int PageSize =20;
        DataSetPaging dpage = new DataSetPaging(ds, PageSize);
        string pageNo = "0";
        long ret;
        if (Request.QueryString["page1"] != null)
            if (Int64.TryParse(Request.QueryString["page1"].ToString(), out ret))
                pageNo = Request.QueryString["page1"].ToString();
        dlsprod.DataSource = dpage.BindList(Convert.ToInt64(pageNo));
        dlsprod.DataBind();
        string tmpString = "";
        //tmpString += "Browsing " + dpage.CurrentRecordPosition.ToString() + " - " + dpage.MaxRecordPosition.ToString() + " of " + dpage.RecordCount.ToString() + " LIstings &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //tmpString += "Browsing " + dpage.CurrentRecordPosition.ToString() + " - " + dpage.MaxRecordPosition.ToString() + " of " + dpage.RecordCount.ToString() + " LIstings &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        pnlPager.Controls.Add(new LiteralControl(tmpString));
        //pnlPagerbottom.Controls.Add(new LiteralControl(tmpString));
        HyperLink lnk;
        string pageName = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        string[] arrQuery = Request.QueryString.ToString().Split(Convert.ToChar('&'));
        foreach (string tmpQuery in arrQuery)
        {
            string[] arrOption = tmpQuery.Split(Convert.ToChar('='));
            if (arrOption[0].ToString().ToLower() != "page1")
                pageName += tmpQuery + "&";
        }
        string linksSeparator = "&nbsp;&nbsp;";
        if (dpage.OutPageIndex > 1)
        {
            lnk = new HyperLink();
            lnk.Text = "Prev";
            lnk.CssClass = "link";
            lnk.Font.Bold = true;
            lnk.NavigateUrl = pageName + "page1=" + Convert.ToString(dpage.OutPageIndex - 1);
            pnlPager.Controls.Add(lnk);
            //pnlPagerbottom.Controls.Add(lnk);
            pnlPager.Controls.Add(new LiteralControl(linksSeparator));
            //pnlPagerbottom.Controls.Add(new LiteralControl(linksSeparator));
        }

        for (int i = 1; i <= dpage.PageCount; i++)
        {
            if (i == dpage.OutPageIndex)
            {
                if (dpage.PageCount != 1)
                {
                    Label lab = new Label();
                    lab.Text = i.ToString();
                    lab.Font.Bold = true;
                    lab.ForeColor = System.Drawing.Color.Red;
                    pnlPager.Controls.Add(lab);
                }
            }
            else
            {
                lnk = new HyperLink();
                lnk.Text = i.ToString();
                lnk.CssClass = "link";
                lnk.Font.Bold = true;
                lnk.NavigateUrl = pageName + "page1=" + i.ToString();
                pnlPager.Controls.Add(lnk);
            }
            pnlPager.Controls.Add(new LiteralControl(linksSeparator));
        }

        if (dpage.OutPageIndex < dpage.PageCount)
        {
            lnk = new HyperLink();
            lnk.Text = "Next";
            lnk.NavigateUrl = pageName + "page1=" + Convert.ToString(dpage.OutPageIndex + 1);
            lnk.CssClass = "link";
            lnk.Font.Bold = true;
            pnlPager.Controls.Add(lnk);
            pnlPager.Controls.Add(new LiteralControl(linksSeparator));
        }
    }

    /// <summary>
    /// Sorting in Datalist
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbProd_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by p.productname desc")
            {
                ViewState["sort"] = " order by p.productname asc";
            }
            else
            {
                ViewState["sort"] = " order by p.productname desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by p.productname desc";
        }
        fillproducts();
    }
    protected void lbCAS_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by p.cas desc")
            {
                ViewState["sort"] = " order by p.cas asc";
            }
            else
            {
                ViewState["sort"] = " order by p.cas desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by p.cas desc";
        }
        fillproducts();
    }
    protected void lbFormula_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by p.Formula desc")
            {
                ViewState["sort"] = " order by p.Formula asc";
            }
            else
            {
                ViewState["sort"] = " order by p.Formula desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by p.Formula desc";
        }
        fillproducts();
    }
    protected void lbMweight_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by p.mWeight desc")
            {
                ViewState["sort"] = " order by p.mWeight asc";
            }
            else
            {
                ViewState["sort"] = " order by p.mWeight desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by p.mWeight desc";
        }
        fillproducts();
    }
}
