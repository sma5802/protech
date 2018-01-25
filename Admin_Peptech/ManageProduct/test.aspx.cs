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

public partial class Admin_Peptech_ManageProduct_test : System.Web.UI.Page
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
            DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category order by categoryname").Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "<--Please Select-->");
                ddlSubCategory.Items.Insert(0, "<--Please Select-->");
            }
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCategory.SelectedIndex.ToString() != "0")
        {
            DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "' order by subcategoryname").Tables[0];
            if (dt1.Rows.Count > 0)
            {
                ddlSubCategory.DataSource = dt1;
                ddlSubCategory.DataTextField = "subcategoryname";
                ddlSubCategory.DataValueField = "id";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, "<--Please Select-->");
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //if (fuPImage.HasFile)
        //{
        //    if (chkfile(fuPImage.FileName))
        //    {
                //fuPImage.SaveAs(Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName));
                //string pathname = Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName);
                ////Response.Write(pathname);
                //string filename = fuPImage.FileName;
                //try
                //{
                //    //Response.Write(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathname + ";Extended Properties='Excel 8.0'");
                //    string sExcelConnectionString ="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\aspnet\\Peptech\\UploadProductExcel\\Book2.xls;Extended Properties=Excel 8.0;HDR=YES;IMEX=1";
                //    OleDbConnection ExcelConnection = new OleDbConnection(sExcelConnectionString);
                //    ExcelConnection.Open();
                //    OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT productname,cas,formula,mweight FROM Book2.xls", ExcelConnection);
                //    // OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [Sheet$]", ExcelConnection);
                //    OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);

                //    DataTable ExcelDataSet = new DataTable();
                //    ExcelAdapter.Fill(ExcelDataSet);
                //    Response.Write(ExcelDataSet.Rows.Count);
                //    for (int i = 0; i < ExcelDataSet.Rows.Count; i++)
                //    {
                //        string straddcat = "insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,Status) values('" + ddlCategory.SelectedValue.ToString() + "','" + ddlSubCategory.SelectedValue.ToString() + "','" + ExcelDataSet.Rows[i]["ProductName"].ToString() + "','" + ExcelDataSet.Rows[i]["CAS"].ToString() + "','" + ExcelDataSet.Rows[i]["Formula"].ToString() + "','" + ExcelDataSet.Rows[i]["MWeight"].ToString() + "',1)";
                //        Response.Write(straddcat);
                //        Response.End();
                //    }
                //    //ExcelAdapter.Fill(ExcelDataSet);
                //    //Response.Write("SELECT productname,cas,formula,mweight FROM " + filename + "");
                //    //Response.End();
                //    //GridView1.Visible = true;
                //    //btnsendmaildown.Visible = true;
                //    //btnsendmailup.Visible = true;
                //    //GridView1.DataSource = ExcelDataSet;
                //    //GridView1.DataBind();
                //    ExcelConnection.Close();
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message);
                //}

        //    }
        //}

        // String strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +"Data Source=" + strFileName + ";" +"Extended Properties='Excel 8.0;HDR=Yes'";

        //OleDbConnection connExcel = new OleDbConnection(strExcelConn);
        //OleDbCommand cmdExcel = new OleDbCommand();

        //try
        //{

        //    connExcel.Open();

        //    cmdExcel.Connection = connExcel;

        //    //Add New Row to Excel File
        //    cmdExcel.CommandText = "INSERT INTO [" + strSheetName + "$] (Col1, Col2, Col3,) " +

        //    "values ('" + Val1 + "', '" + Val2 + "', '" + Val3 + "')";

        //    cmdExcel.ExecuteNonQuery();
        //}
        try
        {
            // Response.Write(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "UploadProductExcel/" + fuPImage.FileName);
            fuPImage.SaveAs(Server.MapPath("~/UploadProductExcel/" + fuPImage.FileName));
            string sSQLTable = "Peptech_Product";
            string sExcelFileName = "~/UploadProductExcel/" + fuPImage.FileName;
            string sWorkbook = fuPImage.FileName;
            string sExcelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(sExcelFileName) + ";Extended Properties=Excel 8.0;HDR=YES;";
            //string sExcelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\\Book4.xls';Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            string sSqlConnectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ToString();

            OleDbConnection OleDbConn = new OleDbConnection(sExcelConnectionString);
            // Response.Write("SELECT [categoryid],[subcategoryid],[ProductName], [CAS], [Formula], [MWeight] FROM " + sWorkbook);
            OleDbCommand OleDbCmd = new OleDbCommand(("SELECT [categoryid],[subcategoryid],[ProductName], [CAS], [Formula], [MWeight] FROM " + sWorkbook), OleDbConn);
            OleDbConn.Open();
            OleDbDataReader dr = OleDbCmd.ExecuteReader();
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sSqlConnectionString);
            bulkCopy.DestinationTableName = sSQLTable;
            bulkCopy.WriteToServer(dr);

            OleDbConn.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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
