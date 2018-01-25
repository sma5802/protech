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

public partial class Admin_Peptech_ManageSubcategory_AddSubcategory : System.Web.UI.Page
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
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageSubCategory/SubCategoryList.aspx");
            }
            DataTable dtcategory = customUtility.GetTableData("select id,categoryname from "+customUtility.DBPrefix+"category where status=1").Tables[0];
            if(dtcategory.Rows.Count>0)
            {
                for (int i = 0; i < dtcategory.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dtcategory.Rows[i]["categoryname"].ToString());
                    li.Value =dtcategory.Rows[i]["id"].ToString();
                    ddlCategory.Items.Add(li);
                }
                ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));
                //    ddlCategory.DataSource = dtcategory;
                //ddlCategory.DataTextField = "categoryname";
                //ddlCategory.DataValueField = "id";
                //ddlCategory.DataBind();
                //ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));
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
        string straddcat = "insert into " + customUtility.DBPrefix + "subcategory(Categoryid,subcategoryname,status) values('"+ddlCategory.SelectedValue+"','" + customUtility.writeGreekchar(txtSubcategory.Text.Replace("'", "''").Trim()) + "',1)";
        if (customUtility.CheckDuplicateFieldValue(customUtility.writeGreekchar(txtSubcategory.Text.Replace("'", "''").Trim()), "subcategory", "subcategoryname", customUtility.CompareType.text, ""))
        {
            lblmessage.Text = "sub category name already exists ! Try another sub category Name";
        }
        else
        {
            customUtility.ExecuteNonQuery(straddcat);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManagesubCategory/subCategoryList.aspx?add=1");
        }

    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtSubcategory.Text += e.CommandArgument.ToString();
    }
}
