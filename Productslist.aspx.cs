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

public partial class Productslist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Request.QueryString["id"]!=null && Request.QueryString["id"].ToString()!="")
            {
                DataSet dsprod=customUtility.GetTableData("select * from "+customUtility.DBPrefix+"product where subcategoryid="+Request.QueryString["id"].ToString());
                if(dsprod.Tables[0].Rows.Count>0)
                {
                    dlsprod.DataSource = dsprod;
                    dlsprod.DataBind();
                }
            
            }
        }
    }
    protected void dlsprod_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
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
}
