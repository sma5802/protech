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

public partial class Admin_Peptech_ManageCategory_AddCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["add"] != null && Request.QueryString["add"].ToString() != "")
            {
                btnsubmit.Visible = true;
               
                pnlSubmit.DefaultButton = "btnsubmit";
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
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string straddcat = "insert into " + customUtility.DBPrefix + "category(categoryname,status) values('" + customUtility.writeGreekchar(txtCounties.Text.Replace("'", "''").Trim()) + "',1)";
        
        if (customUtility.CheckDuplicateFieldValue(txtCounties.Text.Replace("'", "''").Trim(), "category", "categoryname", customUtility.CompareType.text, ""))
        {
            lblmessage.Text = "category name already exists ! Try another category Name";
        }
        else
        {
            customUtility.ExecuteNonQuery(straddcat);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCategory/CategoryList.aspx?add=1");
        }

    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtCounties.Text += e.CommandArgument.ToString();
    }
}
