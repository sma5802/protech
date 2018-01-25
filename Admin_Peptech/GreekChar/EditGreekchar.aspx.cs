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

public partial class Admin_Peptech_GreekChar_EditGreekchar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                pnlSubmit.DefaultButton = "btnupdate";
                btnupdate.Visible = true;
                DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greekchar where id=" + Request.QueryString["id"]);
                if (dsservice.Tables[0].Rows.Count > 0)
                {
                    txtCounties.Text = HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["greekchar"].ToString());
                }
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/GreekChar/Greekcharlist.aspx");
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            Response.Write(HttpUtility.HtmlEncode(txtCounties.Text.Replace("'", "''").Trim()));
            //Response.End();
            string strupdcat = "update " + customUtility.DBPrefix + "greekchar set greekchar='" + HttpUtility.HtmlEncode(txtCounties.Text.Replace("'", "''").Trim()) + "'";
            strupdcat += " where id = " + Request.QueryString["id"].ToString();

            if (customUtility.CheckDuplicateFieldValue(HttpUtility.HtmlEncode(txtCounties.Text.Replace("'", "''").Trim()), "greekchar", "greekchar", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()))
            {
                lblmessage.Text = "Greek Character already exists ! Try another Greek Character";
            }
            else
            {
                //Response.Write(strupdcat);
                //Response.End();
                customUtility.ExecuteNonQuery(strupdcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/GreekChar/Greekcharlist.aspx?Upd=1");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCounties.Text = "";
    }
}
