using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
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

public partial class Admin_Peptech_ManageProduct_BatchUpload : System.Web.UI.Page
{
    string[] excelsheets;
    string column = "";
    string columnfield = "";
    string path = "";
    string filename = "";
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

        }
        if (ViewState["path"] != null && ViewState["path"].ToString() != "")
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ViewState["path"] + @";Extended Properties=""Excel 8.0;HDR=YES;""";
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter = factory.CreateDataAdapter();
            DbCommand selectCommand = factory.CreateCommand();
            //selectCommand.CommandText = "SELECT categoryname,Subcategoryname,ProductName,Cas,Formula,Mweight,ProductImage,CatalogName,Quantity,Price,Catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
            selectCommand.CommandText = "SELECT productcategory,productSubcategory,RankingOrder,ProductName,Cas,Formula,Mweight,Catalog1,Qty1,Price1,catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
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
        }
       
    }
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
                    ViewState["path"] = path;
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
                        //for (int j = 0; j < excelsheets.Length; j++)
                        //{
                            DataTable dtgetcolumn;
                           // string[] restrictions = { null, null, excelsheets[j], null };
                            //dtgetcolumn = objconnection.GetSchema("Columns", restrictions);
                            //for (int col = 0; col < dtgetcolumn.Rows.Count; col++)
                            //{
                            //    column += dtgetcolumn.Rows[col]["Column_name"].ToString() + ",";
                            //}
                            //columnfield = column.Substring(0, column.Length - 1);
                            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Excel 8.0;HDR=YES;""";
                            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                            DbDataAdapter adapter = factory.CreateDataAdapter();
                            adapter = factory.CreateDataAdapter();
                            DbCommand selectCommand = factory.CreateCommand();
                            //selectCommand.CommandText = "SELECT categoryname,Subcategoryname,ProductName,Cas,Formula,Mweight,ProductImage,CatalogName,Quantity,Price,Catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
                            selectCommand.CommandText = "SELECT productcategory,productSubcategory,RankingOrder,ProductName,Cas,Formula,Mweight,Catalog1,Qty1,Price1,catalog2,Qty2,Price2,Catalog3,Qty3,Price3,Catalog4,Qty4,Price4 FROM [Sheet1$]";
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
                            for (int l = 0; l < cities.Tables[0].Rows.Count; l++)
                            {
                                DataTable dtcat = customUtility.GetTableData("select id from " + customUtility.DBPrefix + "category where categoryname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["Productcategory"].ToString().Replace("'","''")) + "'").Tables[0];
                                if (dtcat.Rows.Count > 0)
                                {
                                    //Response.Write(cities.Tables[0].Rows[i]["ProductSubcategory"].ToString());
                                    if (cities.Tables[0].Rows[l]["ProductSubcategory"].ToString() != "")
                                    {
                                        DataTable dtsubcat = customUtility.GetTableData("select Id from " + customUtility.DBPrefix + "subcategory where Subcategoryname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductSubcategory"].ToString().Replace("'", "''")) + "'").Tables[0];
                                        if (dtsubcat.Rows.Count > 0)
                                        {
                                            bool check = customUtility.CheckDataExists("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'");
                                            if (check == true)
                                            {
                                                prodid = int.Parse(customUtility.GetAField("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'"));
                                            }
                                            else
                                            {
                                                prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                                //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                                //for (int j = 8; j < 19; j+3)
                                                int m = 7;
                                                while (m < 18)
                                                {

                                                    if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                                    {
                                                        customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                                    }
                                                    m = m + 3;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int subcatid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "subcategory(Categoryid,Subcategoryname,Status)values(" + dtcat.Rows[0]["id"].ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["Productsubcategory"].ToString().Replace("'", "''")) + "',1)");
                                            bool check = customUtility.CheckDataExists("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'");
                                            if (check == true)
                                            {
                                                prodid = int.Parse(customUtility.GetAField("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'"));
                                            }
                                            else
                                            {
                                                prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + subcatid + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                                //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                                //for (int j = 8; j < 19; j+3)
                                                int m = 7;
                                                while (m < 18)
                                                {

                                                    if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                                    {
                                                        customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                                    }
                                                    m = m + 3;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bool check = customUtility.CheckDataExists("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'");
                                        if (check == true)
                                        {
                                            prodid = int.Parse(customUtility.GetAField("select id from " + customUtility.DBPrefix + "product where mweight='" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "' and formula='" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "'"));
                                        }
                                        else
                                        {
                                            prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + dtcat.Rows[0]["id"].ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                            //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                            //for (int j = 8; j < 19; j+3)
                                            int m = 7;
                                            while (m < 18)
                                            {
                                                if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                                {
                                                    customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                                }
                                                m = m + 3;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    int id = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "category(categoryname,status)values('" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["productcategory"].ToString().Replace("'", "''")) + "',1)");
                                    if (cities.Tables[0].Rows[l]["ProductSubcategory"].ToString() != "")
                                    {
                                        DataTable dtsubcat = customUtility.GetTableData("select Id from " + customUtility.DBPrefix + "subcategory where Subcategoryname='" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductSubcategory"].ToString().Replace("'", "''")) + "'").Tables[0];
                                        if (dtsubcat.Rows.Count > 0)
                                        {
                                           
                                            prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + id + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                            //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                            //for (int j = 8; j < 19; j+3)
                                            int m = 7;
                                            while (m < 18)
                                            {

                                                if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                                {
                                                    customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                                }
                                                m = m + 3;
                                            }
                                        }
                                        else
                                        {
                                            int subcatid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "subcategory(Categoryid,Subcategoryname,Status)values(" + id + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["Productsubcategory"].ToString()) + "',1)");
                                            prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + id + "," + subcatid + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                            //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                            //for (int j = 8; j < 19; j+3)
                                            int m = 7;
                                            while (m < 18)
                                            {

                                                if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                                {
                                                    customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                                }
                                                m = m + 3;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        prodid = customUtility.AddToTableReturnID("insert into " + customUtility.DBPrefix + "product(categoryid,ProductName,CAS,Formula,MWeight,Rankingorder,status)values(" + id + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l]["ProductName"].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l]["CAS"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["Formula"].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString().Replace("'", "''") + "'," + cities.Tables[0].Rows[l]["RankingOrder"].ToString().Replace("'", "''") + ",1)");
                                        //customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,status)values(" + dtcat.Rows[0]["id"].ToString() + "," + dtsubcat.Rows[0]["id"].ToString() + ",'" + cities.Tables[0].Rows[l]["ProductName"].ToString() + "','" + cities.Tables[0].Rows[l]["CAS"].ToString() + "','" + cities.Tables[0].Rows[l]["Formula"].ToString() + "','" + cities.Tables[0].Rows[l]["MWeight"].ToString() + "',1)");
                                        //for (int j = 8; j < 19; j+3)
                                        int m = 7;
                                        while (m < 18)
                                        {

                                            if (cities.Tables[0].Rows[l][m].ToString().Trim() != "")
                                            {
                                                customUtility.ExecuteNonQuery("insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,Price,Unit,Status)values(" + prodid.ToString() + ",'" + customUtility.writeGreekchar(cities.Tables[0].Rows[l][m].ToString().Replace("'", "''")) + "','" + cities.Tables[0].Rows[l][m + 1].ToString().Replace("'", "''") + "','" + cities.Tables[0].Rows[l][m + 2].ToString().Replace("'", "''") + "',1,1)");
                                            }
                                            m = m + 3;
                                        }
                                    }
                                }
                            }
                        //}

                    }
                    lblmessage.Visible = true;
                    lblmessage.Text = "Batch Product Added Successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            //lblmessage.Text = ex.Message;
            lblmessage.Text = "There is some techinal difficulties with the Uploading...Please try again";
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex > 0)
        {
            GridView1.PageIndex = e.NewPageIndex;
            // code of fill grid view
            GridView1.DataBind();
        }
    }
}
