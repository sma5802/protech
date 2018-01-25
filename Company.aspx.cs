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

public partial class Company : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtCompany = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"edit where id=10").Tables[0];
        if (dtCompany.Rows.Count > 0)
        {
            lblContent.Text = HttpUtility.HtmlDecode(dtCompany.Rows[0]["pagedata"].ToString().Replace("''", "'"));
        }
    }
}
