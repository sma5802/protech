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

public partial class subcategories : System.Web.UI.Page
{
    string order = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pid"] != null && Request.QueryString["pid"].ToString() != "")
            {
                try
                {
                    DataTable dtcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category where status=1 and id=" + Request.QueryString["pid"].ToString().Replace("'", "''")).Tables[0];
                    if (dtcat.Rows.Count > 0)
                    {
                        lblcat.Text = HttpUtility.HtmlDecode(dtcat.Rows[0]["categoryname"].ToString());
                        lblprodcat.Text = HttpUtility.HtmlDecode(dtcat.Rows[0]["categoryname"].ToString());
                        lblprodsubcat.Text = HttpUtility.HtmlDecode(dtcat.Rows[0]["categoryname"].ToString());

                        DataSet dtsubcat = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where status=1 and categoryid=" + Request.QueryString["pid"].ToString().Replace("'", "''"));
                        if (dtsubcat.Tables[0].Rows.Count == 0)
                        {
                            pnlSubcategory.Visible = false;
                            //pnlmsg.Visible = true;
                        }
                        else
                        {
                            pnlSubcategory.Visible = true;
                            pnlmsg.Visible = false;
                            dlsprod.DataSource = dtsubcat;
                            dlsprod.DataBind();
                            GotoPaging(dtsubcat);
                        }
                        if (dtsubcat.Tables[0].Rows.Count == 0)
                        {
                            fillproducts(0);
                        }
                        else
                            fillproducts(1);
                    }
                }
                catch
                { 
                    Response.Redirect("default.aspx"); 
                }
            }
        }
    }

    public void fillproducts(int i)
    {
        order = "";
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
            order = ViewState["sort"].ToString();
        else
        {
            if (Session["cssort"] != null && Session["cssort"].ToString() != "")
                order = Session["cssort"].ToString();
        }
        //Response.Write(ViewState["sort"] + "____");
        DataSet dtprod = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where status=1 and categoryid=" + Request.QueryString["pid"].ToString() + " " + order);
        if (dtprod.Tables[0].Rows.Count == 0)
        {
            pnlproducts.Visible = false;
        }
        else
        {
            dlsproduct.DataSource = dtprod;
            dlsproduct.DataBind();
            pnlproducts.Visible = true;
            pnlprodmsg.Visible = false;
            GotoPaging1(dtprod);
        }

        if ((i == 0) && (dtprod.Tables[0].Rows.Count == 0))
        {
            pnlboth.Visible = true;
        }
    }

    protected void GotoPaging(DataSet ds)
    {

        int PageSize = 20;
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

    protected void GotoPaging1(DataSet ds)
    {
        
        int PageSize = 20;
        DataSetPaging dpage = new DataSetPaging(ds, PageSize);
        string pageNo = "0";
        long ret;
        if (Request.QueryString["page"] != null)
            if (Int64.TryParse(Request.QueryString["page"].ToString(), out ret))
                pageNo = Request.QueryString["page"].ToString();
        dlsproduct.DataSource = dpage.BindList(Convert.ToInt64(pageNo));
        dlsproduct.DataBind();
        string tmpString = "";
        //tmpString += "Browsing " + dpage.CurrentRecordPosition.ToString() + " - " + dpage.MaxRecordPosition.ToString() + " of " + dpage.RecordCount.ToString() + " LIstings &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //tmpString += "Browsing " + dpage.CurrentRecordPosition.ToString() + " - " + dpage.MaxRecordPosition.ToString() + " of " + dpage.RecordCount.ToString() + " LIstings &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        pnlPager1.Controls.Add(new LiteralControl(tmpString));
        //pnlPagerbottom.Controls.Add(new LiteralControl(tmpString));
        HyperLink lnk;
        string pageName = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        string[] arrQuery = Request.QueryString.ToString().Split(Convert.ToChar('&'));
        foreach (string tmpQuery in arrQuery)
        {
            string[] arrOption = tmpQuery.Split(Convert.ToChar('='));
            if (arrOption[0].ToString().ToLower() != "page")
                pageName += tmpQuery + "&";
        }
        string linksSeparator = "&nbsp;&nbsp;";
        if (dpage.OutPageIndex > 1)
        {
            lnk = new HyperLink();
            lnk.Text = "Prev";
            lnk.CssClass = "link";
            lnk.Font.Bold = true;
            lnk.NavigateUrl = pageName + "page=" + Convert.ToString(dpage.OutPageIndex - 1);
            pnlPager1.Controls.Add(lnk);
            //pnlPagerbottom.Controls.Add(lnk);
            pnlPager1.Controls.Add(new LiteralControl(linksSeparator));
            //pnlPagerbottom.Controls.Add(new LiteralControl(linksSeparator));
        }

        for (int i = 1; i <= dpage.PageCount; i++)
        {
            if (i == dpage.OutPageIndex)
            {
                if (dpage.PageCount != 1)
                {
                    Label lab = new Label();
                    if (i % 35 == 0)
                        lab.Text = i.ToString() + "<br>";
                    else
                        lab.Text = i.ToString();
                    lab.Font.Bold = true;
                    lab.ForeColor = System.Drawing.Color.Red;
                    pnlPager1.Controls.Add(lab);

                }

            }
            else
            {
                lnk = new HyperLink();
                if (i % 35 == 0)
                    lnk.Text = i.ToString()+"<br>";
                else
                    lnk.Text = i.ToString();
                lnk.CssClass = "link";
                lnk.Font.Bold = true;
                lnk.NavigateUrl = pageName + "page=" + i.ToString();
                pnlPager1.Controls.Add(lnk);

            }
            pnlPager1.Controls.Add(new LiteralControl(linksSeparator));
        }

        if (dpage.OutPageIndex < dpage.PageCount)
        {
            lnk = new HyperLink();
            lnk.Text = "Next";
            lnk.NavigateUrl = pageName + "page=" + Convert.ToString(dpage.OutPageIndex + 1);
            lnk.CssClass = "link";
            lnk.Font.Bold = true;
            pnlPager1.Controls.Add(lnk);
            pnlPager1.Controls.Add(new LiteralControl(linksSeparator));
        }
    }
    protected void dlsproduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
            Session["cssort"] = ViewState["sort"].ToString();
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
            if (ViewState["sort"].ToString() == " order by productname desc")
            {
                ViewState["sort"] = " order by productname asc";
            }
            else
            {
                ViewState["sort"] = " order by productname desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by productname desc";
        }
        fillproducts(0);
    }
    protected void lbCAS_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by cas desc")
            {
                ViewState["sort"] = " order by cas asc";
            }
            else
            {
                ViewState["sort"] = " order by cas desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by cas desc";
        }
        fillproducts(0);
    }
    protected void lbFormula_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by Formula desc")
            {
                ViewState["sort"] = " order by Formula asc";
            }
            else
            {
                ViewState["sort"] = " order by Formula desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by Formula desc";
        }
        fillproducts(0);
    }
    protected void lbMweight_Click(object sender, EventArgs e)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() != "")
        {
            if (ViewState["sort"].ToString() == " order by mWeight desc")
            {
                ViewState["sort"] = " order by mWeight asc";
            }
            else
            {
                ViewState["sort"] = " order by mWeight desc";
            }
        }
        else
        {
            ViewState["sort"] = " order by mWeight desc";
        }
        fillproducts(0);
    }
}
