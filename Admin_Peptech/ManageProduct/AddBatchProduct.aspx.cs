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
using Microsoft.Office.Core;
using Microsoft.Office;

public partial class Admin_Peptech_ManageProduct_AddBatchProduct : System.Web.UI.Page
{
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
}
}

protected void btnsubmit_Click(object sender, EventArgs e)
{
  try
   {
   Int32 prodid;
   if (fuPImage.HasFile)
    {
     if (chkfile(fuPImage.FileName))
     {
     fuPImage.SaveAs(Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName));
     string path = Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName);
            //fuPImage.SaveAs(Server.MapPath("~/" + fuPImage.FileName));
           // string path = Server.MapPath("~/" + fuPImage.FileName);
     string filename = fuPImage.FileName;
     string column = "";
     string excelConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Excel 8.0;HDR=YES;""";
     OleDbConnection objconnection = new OleDbConnection(excelConnectionString);
     objconnection.Open();
     DataTable dt = objconnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
     if (dt != null)
     {
      int i = 0;
      string[] excelsheets = new string[dt.Rows.Count];
      foreach (DataRow row in dt.Rows)
      {
       excelsheets[i] = row["TABLE_NAME"].ToString();
       i++;
      }
     for (int k = 0; k < excelsheets.Length; k++)
      {
      string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Excel 8.0;HDR=YES;""";
      DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
      DbDataAdapter adapter = factory.CreateDataAdapter();
      DbCommand selectCommand = factory.CreateCommand();
      DataTable dtgetcolumn;
      string[] restrictions = { null, null, excelsheets[k], null };
      dtgetcolumn = objconnection.GetSchema("Columns", restrictions);
      for (int col = 0; col < dtgetcolumn.Rows.Count; col++)
      {
       column += dtgetcolumn.Rows[col]["Column_name"].ToString() + ",";
      }
      string columnfield = column.Substring(0, column.Length - 1);
            //selectCommand.CommandText = "SELECT categoryname,Subcategoryname,ProductName,Cas,Formula,Mweight,ProductImage,CatalogName,Quantity,Price,Catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
            // selectCommand.CommandText = "SELECT productcategory,productSubcategory,RankingOrder,ProductName,Cas,Formula,Mweight,ProductImage,Catalog1,Qty1,Price1,catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
            //selectCommand.CommandText = "SELECT productcategory,productSubcategory,RankingOrder,ProductName,Cas,Formula,Mweight,ProductImage,Catalog1,Qty1,Price1,catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [" + excelsheets[k] + "$]";
      selectCommand.CommandText = "SELECT " + columnfield + " FROM [" + excelsheets[k] + "]";
      //Response.Write(excelsheets[k]+"<br />");
      Response.Write("SELECT " + columnfield + " FROM [" + excelsheets[k] + "]");
      DbConnection connection = factory.CreateConnection();
      connection.ConnectionString = connectionString;
      selectCommand.Connection = connection;
      adapter.SelectCommand = selectCommand;
      DataSet cities = new DataSet();
      adapter.Fill(cities);
      GridView1.DataSource = cities;
      GridView1.DataBind();
      for (int x = 0; x < cities.Tables[0].Rows.Count; x++)
       {
        DataTable dtcat = customUtility.GetTableData("select id from " + customUtility.DBPrefix + "category where categoryname='" + cities.Tables[0].Rows[x]["Productcategory"].ToString() + "'").Tables[0];
        if (dtcat.Rows.Count > 0)
         {
           DataTable dtsubcat = customUtility.GetTableData("select Id from " + customUtility.DBPrefix + "subcategory where Subcategoryname='" + cities.Tables[0].Rows[x]["ProductSubcategory"].ToString() + "'").Tables[0];
           if (dtsubcat.Rows.Count > 0)
            {
            // Response.Write("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,productimage,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[x]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[x]["CAS"].ToString() + "','" + cities.Tables[0].Rows[x]["Formula"].ToString() + "','" + cities.Tables[0].Rows[x]["MWeight"].ToString() + "','" + cities.Tables[0].Rows[x]["ProductImage"].ToString() + "',1)");
             prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,productimage,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[x]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[x]["CAS"].ToString() + "','" + cities.Tables[0].Rows[x]["Formula"].ToString() + "','" + cities.Tables[0].Rows[x]["MWeight"].ToString() + "','" + cities.Tables[0].Rows[x]["ProductImage"].ToString() + "',1)");
             customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[x]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[x]["CAS"].ToString() + "','" + cities.Tables[0].Rows[x]["Formula"].ToString() + "','" + cities.Tables[0].Rows[x]["MWeight"].ToString() + "',1)");
            //for (int j = 8; j < 19; j+3)
             int j = 8;
             while (j < cities.Tables[0].Rows.Count)
             {
              if (cities.Tables[0].Rows[x][j].ToString().Trim() != "")
              {
              customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + cities.Tables[0].Rows[i][j].ToString() + "','" + cities.Tables[0].Rows[i][j + 1].ToString() + "','" + cities.Tables[0].Rows[i][j + 2].ToString() + "',1,1)");
              }
              j = j + 3;
             }
            }
          }
        }
     }
    }
   }
   }
  }
     catch (Exception ex)
      {
       lblmessage.Visible = true;
       lblmessage.Text = ex.Message;
            //Response.Write(ex.Message);
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
//case ".jpg":
//    return true; break;
//case ".jpeg":
//    return true; break;
//case ".png":
//    return true; break;
default:
return false;
}
}

}

