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

public partial class Admin_Peptech_ManageSubcategory_EditSubcategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataTable dtcategory = customUtility.GetTableData("select id,categoryname from " + customUtility.DBPrefix + "category where status=1").Tables[0];
                if (dtcategory.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcategory.Rows.Count; i++)
                    {

                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dtcategory.Rows[i]["categoryname"].ToString());
                        li.Value = dtcategory.Rows[i]["id"].ToString();
                        ddlCategory.Items.Add(li);
                    }
                    //ddlCategory.DataSource = dtcategory;
                    //ddlCategory.DataTextField = "categoryname";
                    //ddlCategory.DataValueField = "id";
                    //ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));
                }
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    pnlSubmit.DefaultButton = "btnupdate";
                    btnupdate.Visible = true;
                    DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where id=" + Request.QueryString["id"]);
                    if (dsservice.Tables[0].Rows.Count > 0)
                    {
                        ddlCategory.Items.FindByValue(dsservice.Tables[0].Rows[0]["categoryid"].ToString()).Selected = true;
                        txtSubcategory.Text = HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["subcategoryname"].ToString());
                    }
                    else
                    {
                        Response.Redirect("Subcategorylist.aspx");
                    }
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
            catch
            {
                Response.Redirect("Subcategorylist.aspx");
            }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtSubcategory.Text += e.CommandArgument.ToString();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            string strupdcat = "update " + customUtility.DBPrefix + "subcategory set categoryid='"+ddlCategory.SelectedValue+"',subcategoryname='" + customUtility.writeGreekchar(txtSubcategory.Text.Replace("'", "''").Trim()) + "'";
            strupdcat += " where id = " + Request.QueryString["id"].ToString();

            if (customUtility.CheckDuplicateFieldValue(customUtility.writeGreekchar(txtSubcategory.Text.Replace("'", "''").Trim()), "subcategory", "subcategoryname", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()+" and categoryid!="+ddlCategory.SelectedValue))
            {
                lblmessage.Text = "subcategory name already exists ! Try another subcategory Name";
            }
            else
            {
                customUtility.ExecuteNonQuery(strupdcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageSubcategory/subCategoryList.aspx?Upd=1");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageSubcategory/subCategoryList.aspx");
        txtSubcategory.Text = "";
        DataTable dtcategory = customUtility.GetTableData("select id,categoryname from " + customUtility.DBPrefix + "category where status=1").Tables[0];
        if (dtcategory.Rows.Count > 0)
        {
            for (int i = 0; i < dtcategory.Rows.Count; i++)
            {

                ListItem li = new ListItem();
                li.Text = HttpUtility.HtmlDecode(dtcategory.Rows[i]["categoryname"].ToString());
                li.Value = dtcategory.Rows[i]["id"].ToString();
                ddlCategory.Items.Add(li);
            }
            //ddlCategory.Items.Clear();
            //ddlCategory.DataSource = dtcategory;
            //ddlCategory.DataTextField = "categoryname";
            //ddlCategory.DataValueField = "id";
            //ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Please Select", ""));
        }
    }
}
