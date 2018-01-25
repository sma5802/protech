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

public partial class Admin_Peptech_ManageCatalog_EditCatalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataTable dtProduct = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product").Tables[0];
                if (dtProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {

                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dtProduct.Rows[i]["productname"].ToString());
                        li.Value = dtProduct.Rows[i]["id"].ToString();
                        ddlProduct.Items.Add(li);
                    }
                    //ddlProduct.DataSource = dtProduct;
                    //ddlProduct.DataTextField = "productname";
                    //ddlProduct.DataValueField = "id";
                    //ddlProduct.DataBind();
                    ddlProduct.Items.Insert(0, "<--Please Select-->");
                }
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "catalog where id=" + Request.QueryString["id"]);
                    if (dsservice.Tables[0].Rows.Count > 0)
                    {
                        if (dsservice.Tables[0].Rows[0]["productid"].ToString() != "")
                        {
                            ddlProduct.Items.FindByValue(dsservice.Tables[0].Rows[0]["productid"].ToString()).Selected = true;
                        }
                        txtProduct.Text = HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["catalogname"].ToString());
                        txtQuantity.Text = dsservice.Tables[0].Rows[0]["quantity"].ToString();
                        txtPrice.Text = dsservice.Tables[0].Rows[0]["price"].ToString();
                    }
                    else
                        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx");
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx");
                }
                DataTable dtgreek = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greek where status=1").Tables[0];
                if (dtgreek.Rows.Count > 0)
                {
                    DataList1.DataSource = dtgreek;
                    DataList1.DataBind();
                }
            }
            catch { Response.Redirect("CatalogList.aspx"); }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtProduct.Text += e.CommandArgument.ToString();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            string strupdcat = "update " + customUtility.DBPrefix + "catalog set productid='"+ddlProduct.SelectedValue+"',catalogname='" + customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''").Trim()) + "',";
            strupdcat += "quantity='" + txtQuantity.Text.Replace("'","''")+"',price='"+txtPrice.Text.Replace("'","''")+"'";
            strupdcat += " where id = " + Request.QueryString["id"].ToString();

            if (customUtility.CheckDuplicateFieldValue(customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''").Trim()), "catalog", "catalogname", customUtility.CompareType.text, " and id!=" + Request.QueryString["id"].ToString()+" and productid="+ddlProduct.SelectedValue))
            {
                lblmessage.Text = "catalog name already exists ! Try another catalog Name";
            }
            else
            {
                customUtility.ExecuteNonQuery(strupdcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx?Updte=1");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageCatalog/CatalogList.aspx");
        DataTable dtProduct = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product").Tables[0];
        if (dtProduct.Rows.Count > 0)
        {
            ddlProduct.Items.Clear();
            for (int i = 0; i < dtProduct.Rows.Count; i++)
            {

                ListItem li = new ListItem();
                li.Text = HttpUtility.HtmlDecode(dtProduct.Rows[i]["productname"].ToString());
                li.Value = dtProduct.Rows[i]["id"].ToString();
                ddlProduct.Items.Add(li);
            }
            //ddlProduct.Items.Clear();
            //ddlProduct.DataSource = dtProduct;
            //ddlProduct.DataTextField = "productname";
            //ddlProduct.DataValueField = "id";
            //ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, "<--Please Select-->");
        }
        txtProduct.Text = "";
        txtQuantity.Text = "";
        txtPrice.Text = "";
    }
}
