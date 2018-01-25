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

public partial class Admin_Peptech_ManageCategory_EditCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    pnlSubmit.DefaultButton = "btnupdate";
                    btnupdate.Visible = true;
                    DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category where id=" + Request.QueryString["id"]);
                    if (dsservice.Tables[0].Rows.Count > 0)
                    {
                        //Response.Write(dsservice.Tables[0].Rows[0]["categoryname"].ToString());
                        //Response.Write(HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["categoryname"].ToString()));
                        //Response.End();
                        txtCounties.Text = HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["categoryname"].ToString());
                    }
                    else
                        Response.Redirect("CategoryList.aspx"); 
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCategory/CategoryList.aspx");
                }
                DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greek where status=1").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                }
            }
            catch { Response.Redirect("CategoryList.aspx"); }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            Response.Write(HttpUtility.HtmlEncode(txtCounties.Text.Replace("'", "''").Trim()));
            //Response.End();
            string strupdcat = "update " + customUtility.DBPrefix + "category set categoryname='" + customUtility.writeGreekchar(txtCounties.Text.Replace("'", "''").Trim()) + "'";
            strupdcat += " where id = " + Request.QueryString["id"].ToString();

            if (customUtility.CheckDuplicateFieldValue(HttpUtility.HtmlEncode(txtCounties.Text.Replace("'", "''").Trim()), "category", "categoryname", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()))
            {
                lblmessage.Text = "category name already exists ! Try another category Name";
            }
            else
            {
                //Response.Write(strupdcat);
                //Response.End();
                customUtility.ExecuteNonQuery(strupdcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCategory/CategoryList.aspx?Upd=1");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCategory/CategoryList.aspx");
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtCounties.Text += e.CommandArgument.ToString();
    }
}
