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

public partial class Admin_Peptech_ManageCatalog_AddCatalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["add"] != null && Request.QueryString["add"].ToString() != "")
            {
                btnsubmit.Visible = true;
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx");
            }
            //Response.Write("select * from " + customUtility.DBPrefix + "product where productname is not null order by productname");
            //Response.End();
            DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where productname is not null").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["productname"].ToString());
                    li.Value = dt.Rows[i]["id"].ToString();
                    ddlProduct.Items.Add(li);
                }
                //ddlProduct.DataSource = dt;
                //ddlProduct.DataTextField = "productname";
                //ddlProduct.DataValueField = "id";
                //ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, "<--Please Select-->");
            }
            DataTable dtgreek = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greek where status=1").Tables[0];
            if (dtgreek.Rows.Count > 0)
            {
                DataList1.DataSource = dtgreek;
                DataList1.DataBind();
            }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtProduct.Text += e.CommandArgument.ToString();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
            string straddcat = "insert into " + customUtility.DBPrefix + "catalog(productid,CatalogName,Quantity,Price,Unit,Status) values('" + ddlProduct.SelectedValue.ToString() + "','" + customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''").Trim()) + "','" + txtQuantity.Text.Replace("'", "''") + "','" + txtPrice.Text.Replace("'", "''") + "',1,1)";
                //bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid='" + ddlSubCategory.SelectedValue.ToString() + "'");
               if (customUtility.CheckDuplicateFieldValue(txtProduct.Text.Replace("'", "''").Trim(), "catalog", "CatalogName", customUtility.CompareType.text, " and productid=" + ddlProduct.SelectedValue))
                {
                    lblmessage.Text = "Catalog Name already exists for this Product ! Try another catalogname.";
                }
                else
                {
                    customUtility.ExecuteNonQuery(straddcat);
                    Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx?add=1");
                }
           

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlProduct.Items.Clear();
        //txtProduct.Text = "";
        //txtQuantity.Text = "";
        //txtPrice.Text = "";
        //DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where productname is not null").Tables[0];
        //if (dt.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {

        //        ListItem li = new ListItem();
        //        li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["productname"].ToString());
        //        li.Value = dt.Rows[i]["id"].ToString();
        //        ddlProduct.Items.Add(li);
        //    }
        //    //ddlProduct.DataSource = dt;
        //    //ddlProduct.DataTextField = "productname";
        //    //ddlProduct.DataValueField = "id";
        //    //ddlProduct.DataBind();
        //    ddlProduct.Items.Insert(0, "<--Please Select-->");
           
        //}
        Response.Redirect("CatalogList.aspx");
    }
}
