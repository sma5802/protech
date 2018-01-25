using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for DataSetToExcel
///Class to convert a dataset to an html stream which can be used to display the dataset
///in MS Excel
///The Convert method is overloaded three times as follows
/// 1) Default to first table in dataset
/// 2) Pass an index to tell us which table in the dataset to use
/// 3) Pass a table name to tell us which table in the dataset to use
/// </summary>
///
namespace UserClass
{
    [Serializable]
    public class DataSetToExcel
    {
        public DataSetToExcel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void Convert(DataSet ds, HttpResponse response)
        {
            //first let's clean up the response.object
            response.Clear();
            response.Charset = "";
            //set the response mime type for excel
            //response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Type", "application/vnd.ms-excel");
            response.AddHeader("Content-Disposition", "attachment; filename=products.xls");
            response.AddHeader("Content-Transfer-Encoding", "binary");
            //create a string writer
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //create an htmltextwriter which uses the stringwriter
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            //instantiate a datagrid
            DataGrid dg = new DataGrid();
            //set the datagrid datasource to the dataset passed in
            dg.DataSource = ds.Tables[0];
            //bind the datagrid
            dg.DataBind();
            //tell the datagrid to render itself to our htmltextwriter
            dg.RenderControl(htmlWrite);
            //all that's left is to output the html

            response.Write(stringWrite.ToString());
            response.End();
        }






        public static void Convert(DataSet ds, int TableIndex, HttpResponse response)
        {
            //lets make sure a table actually exists at the passed in value
            //if it is not call the base method
            if (TableIndex > ds.Tables.Count - 1)
                Convert(ds, response);

            //we've got a good table so
            //let's clean up the response.object
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            //set the response mime type for excel
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=products.xls");
            //create a string writer
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //create an htmltextwriter which uses the stringwriter
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            //instantiate a datagrid
            DataGrid dg = new DataGrid();
            //set the datagrid datasource to the dataset passed in
            dg.DataSource = ds.Tables[TableIndex];
            //bind the datagrid
            dg.DataBind();
            //tell the datagrid to render itself to our htmltextwriter
            dg.RenderControl(htmlWrite);
            //all that's left is to output the html
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();
        }




        public static void Convert(DataSet ds, String TableName, HttpResponse response)
        {
            //let's make sure the table name exists
            //if it does not then call the default method
            if (ds.Tables[TableName] is Nullable)
                Convert(ds, response);

            //we've got a good table so
            //let's clean up the response.object
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            //set the response mime type for excel
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=products.xls");
            //create a string writer
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //create an htmltextwriter which uses the stringwriter
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            //instantiate a datagrid
            DataGrid dg = new DataGrid();
            //set the datagrid datasource to the dataset passed in
            dg.DataSource = ds.Tables[TableName];
            //bind the datagrid
            dg.DataBind();
            //tell the datagrid to render itself to our htmltextwriter
            dg.RenderControl(htmlWrite);
            //all that's left is to output the html
            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();
        }


    }

}



////////////////////////////////////////////////////



