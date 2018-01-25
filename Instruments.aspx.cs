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

public partial class Instruments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtInstruments = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"edit where id=2").Tables[0];
            if(dtInstruments.Rows.Count>0)
            {
                lblcmsInstruments.Text = HttpUtility.HtmlDecode(dtInstruments.Rows[0]["pagedata"].ToString());
            }
        }
    }
}
