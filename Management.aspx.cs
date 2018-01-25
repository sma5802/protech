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

public partial class Management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtManagement = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"edit where id=11").Tables[0];
            if (dtManagement.Rows.Count > 0)
            {
                lblcmsManagement.Text = HttpUtility.HtmlDecode(dtManagement.Rows[0]["pagedata"].ToString());
            }
        }
    }
}
