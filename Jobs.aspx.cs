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

public partial class Jobs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtJobs = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"jobs where status=1").Tables[0];
            if (dtJobs.Rows.Count > 0)
            {
                dlsNews.DataSource = dtJobs;
                dlsNews.DataBind();
            }
            DataTable dtcmsjobs = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"edit where id=14").Tables[0];
            if (dtcmsjobs.Rows.Count > 0)
            {
                lblcmsjob.Text = HttpUtility.HtmlDecode(dtcmsjobs.Rows[0]["pagedata"].ToString());
            }
        }
    }
    protected void dlscities_ItemDataBound(object sender, DataListItemEventArgs e)
    {

    }
}
