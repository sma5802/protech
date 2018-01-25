using System;
using System.Data.SqlClient;
using System.IO;
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
using System.Data.OleDb;
using System.Data.Common;

public partial class Admin_Peptech_ManageProduct_test2 : System.Web.UI.Page
{
    string[] excelsheets;
    string column = "";
    string columnfield = "";
    string path = "";
    string filename = "";
    //DataSet cities = new DataSet();
    //DbDataAdapter adapter;
    string[] excelcount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["batch"] != null && Request.QueryString["batch"].ToString() != "")
            {
                btnsubmit.Visible = true;
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx");
            }
            //DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category order by categoryname").Tables[0];
            //if (dt.Rows.Count > 0)
            //{
            //    ddlCategory.DataSource = dt;
            //    ddlCategory.DataTextField = "categoryname";
            //    ddlCategory.DataValueField = "id";
            //    ddlCategory.DataBind();
            //    ddlCategory.Items.Insert(0, "<--Please Select-->");
            //    ddlSubCategory.Items.Insert(0, "<--Please Select-->");
            //}
        }
    }
    //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlCategory.SelectedIndex.ToString() != "0")
    //    {
    //        DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "' order by subcategoryname").Tables[0];
    //        if (dt1.Rows.Count > 0)
    //        {
    //            ddlSubCategory.DataSource = dt1;
    //            ddlSubCategory.DataTextField = "subcategoryname";
    //            ddlSubCategory.DataValueField = "id";
    //            ddlSubCategory.DataBind();
    //            ddlSubCategory.Items.Insert(0, "<--Please Select-->");
    //        }
    //    }
    //}
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (fuPImage.HasFile)
            {
                if (chkfile(fuPImage.FileName))
                {

                    fuPImage.SaveAs(Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName));
                    path = Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName);
                    //fuPImage.SaveAs(Server.MapPath("~/" + fuPImage.FileName));
                    // string path = Server.MapPath("~/" + fuPImage.FileName);
                    filename = fuPImage.FileName;

                    string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Excel 8.0;HDR=YES;""";
                    OleDbConnection objconnection = new OleDbConnection(excelConnectionString);
                    objconnection.Open();
                    DataTable dt = objconnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    
                    if (dt != null)
                    {
                        int i = 0;
                        excelsheets = new string[dt.Rows.Count];
                        excelcount = excelsheets;
                        foreach (DataRow row in dt.Rows)
                        {
                            excelsheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        for (int j = 0; j < excelsheets.Length; j++)
                        {
                            DataTable dtgetcolumn;
                            string[] restrictions = { null, null, excelsheets[j], null };
                            dtgetcolumn = objconnection.GetSchema("Columns", restrictions);
                            for (int col = 0; col < dtgetcolumn.Rows.Count; col++)
                            {
                                column += dtgetcolumn.Rows[col]["Column_name"].ToString() + ",";
                            }
                            columnfield = column.Substring(0, column.Length - 1);
                            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Excel 8.0;HDR=YES;""";
                            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                            DbDataAdapter adapter = factory.CreateDataAdapter();
                            adapter = factory.CreateDataAdapter();
                            DbCommand selectCommand = factory.CreateCommand();
                            //selectCommand.CommandText = "SELECT categoryname,Subcategoryname,ProductName,Cas,Formula,Mweight,ProductImage,CatalogName,Quantity,Price,Catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
                            selectCommand.CommandText = "SELECT productcategory,productSubcategory,RankingOrder,ProductName,Cas,Formula,Mweight,ProductImage,Catalog1,Qty1,Price1,catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
                            //Response.Write("SELECT " + columnfield + " FROM [" + excelsheets[j] + "]");
                            //selectCommand.CommandText = "SELECT " + columnfield + " FROM [" + excelsheets[j] + "]";
                            DbConnection connection = factory.CreateConnection();
                            connection.ConnectionString = connectionString;
                            selectCommand.Connection = connection;
                            adapter.SelectCommand = selectCommand;
                            DataSet cities = new DataSet();
                            adapter.Fill(cities);
                            GridView1.DataSource = cities;
                            GridView1.DataBind();
                            Int32 prodid;
                            //Response.Write(excelcount.ToString().Length);
                            //for (int k = 0; k < excelcount.ToString().Length; k++)
                            //{
                            //Response.Write(cities.Tables[0].Rows.Count);
                            //Response.End();
                            for (int l = 0; l < cities.Tables[0].Rows.Count; l++)
                            {
                                DataTable dtcat = customUtility.GetTableData("select id from " + customUtility.DBPrefix + "category where categoryname='" + cities.Tables[0].Rows[i]["Productcategory"].ToString() + "'").Tables[0];
                                if (dtcat.Rows.Count > 0)
                                {
                                    DataTable dtsubcat = customUtility.GetTableData("select Id from " + customUtility.DBPrefix + "subcategory where Subcategoryname='" + cities.Tables[0].Rows[i]["ProductSubcategory"].ToString() + "'").Tables[0];
                                    if (dtsubcat.Rows.Count > 0)
                                    {
                                        prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,productimage,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "','" + cities.Tables[0].Rows[l]["ProductImage"].ToString() + "',1)");
                                        customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                        //for (int j = 8; j < 19; j+3)
                                        int m = 8;
                                        while (m < 19)
                                        {

                                            if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                            {
                                                customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + cities.Tables[0].Rows[l][m].ToString() + "','" + cities.Tables[0].Rows[l][m + 1].ToString() + "','" + cities.Tables[0].Rows[l][m + 2].ToString() + "',1,1)");
                                            }
                                            m = m + 3;
                                        }
                                    }
                                }
                            }

                            //}
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message;
        }


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex > 0)
        {

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }



    public Boolean chkfile(string file)
    {
        string fileext = Path.GetExtension(file).ToLower();
        switch (fileext)
        {
            case ".xls":
                return true; break;
            default:
                return false;
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            //adapter.Fill(cities);

            Response.Write(excelcount.ToString().Length);
            Response.End();
            //for (int k = 0; k < excelcount.ToString().Length; k++)
            //{
            //    for (int i = 0; i < cities.Tables[0].Rows.Count; i++)
            //    {
            //    DataTable dtcat = customUtility.GetTableData("select id from " + customUtility.DBPrefix + "category where categoryname='" + cities.Tables[0].Rows[i]["Productcategory"].ToString() + "'").Tables[0];
            //    if (dtcat.Rows.Count > 0)
            //    {
            //        DataTable dtsubcat = customUtility.GetTableData("select Id from " + customUtility.DBPrefix + "subcategory where Subcategoryname='" + cities.Tables[0].Rows[i]["ProductSubcategory"].ToString() + "'").Tables[0];
            //        if (dtsubcat.Rows.Count > 0)
            //        {
            //            prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,productimage,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[i]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[i]["CAS"].ToString() + "','" + cities.Tables[0].Rows[i]["Formula"].ToString() + "','" + cities.Tables[0].Rows[i]["MWeight"].ToString() + "','" + cities.Tables[0].Rows[i]["ProductImage"].ToString() + "',1)");
            //            customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[i]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[i]["CAS"].ToString() + "','" + cities.Tables[0].Rows[i]["Formula"].ToString() + "','" + cities.Tables[0].Rows[i]["MWeight"].ToString() + "',1)");
            //            //for (int j = 8; j < 19; j+3)
            //            int j = 8;
            //            while (j < 19)
            //            {
            //                if (cities.Tables[0].Rows[i][j].ToString().Trim() != "")
            //                {
            //                    customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + cities.Tables[0].Rows[i][j].ToString() + "','" + cities.Tables[0].Rows[i][j + 1].ToString() + "','" + cities.Tables[0].Rows[i][j + 2].ToString() + "',1,1)");
            //                }
            //                j = j + 3;
            //            }
            //        }
            //    }
            //    }

            //}
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message;
            //Response.Write(ex.Message);
        }
    }
}