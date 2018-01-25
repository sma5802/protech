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
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtHome = customUtility.GetTableData("select pagedata from " + customUtility.DBPrefix + "edit where home=1 and pagename='Home Page'").Tables[0];
        if(dtHome.Rows.Count>0)
        {
            lblContent.Text = dtHome.Rows[0]["pagedata"].ToString();
        }
        DataTable dtHomelist = customUtility.GetTableData("select top 2 description from "+customUtility.DBPrefix+"homelist order by id asc").Tables[0];
        if (dtHomelist.Rows.Count > 0 && dtHomelist.Rows.Count>1)
        {
            lblServices.Text = dtHomelist.Rows[0]["description"].ToString();
            lblQuality.Text = dtHomelist.Rows[1]["description"].ToString();
        }
        if (dtHomelist.Rows.Count > 0)
        {
            lblServices.Text = dtHomelist.Rows[0]["description"].ToString();
        }
    }
    protected void dlscities_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            if (dr.Row["details"].ToString() != null && dr.Row["details"] != "")
            {
                string strText = dr.Row["details"].ToString();
                if (strText.Length > 110)
                {
                    string strSplt = strText.Substring(0, 110);
                    int idx = strSplt.LastIndexOf(' ');
                    strSplt = strSplt.Substring(0, idx);
                    ((Label)e.Item.FindControl("lblDesc")).Text = strSplt;
                }
                else
                {
                    ((Label)e.Item.FindControl("lblDesc")).Text = dr.Row["details"].ToString();
                }
            }
        }
    }
}
