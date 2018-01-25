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
public partial class ProcessRD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    DataTable dtFacility = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"edit where id=12").Tables[0];
        //    if (dtFacility.Rows.Count > 0)
        //    {
        //        lblcmsfacility.Text = HttpUtility.HtmlDecode(dtFacility.Rows[0]["pagedata"].ToString());
        //    }
        //}
    }
}
