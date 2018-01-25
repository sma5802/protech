using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for ExportDataSet
/// </summary>
[Serializable]
public static class ExportDataSetInMultiFormat
{

        	/// <summary>
	/// Web Utility Function For Exporting Data Set to Specified Format
	/// </summary>
	/// <param name="dsResults">Result Data Set</param>
	/// <param name="enExport">Export Enum Values</param>
	/// <param name="strColDelim">Column Delimiter value</param>
	/// <param name="strRowDelim"></param>
	/// <param name="strFileName"></param>
	public static void ExportDataSet(DataSet dsResults , ExportFormat enExport,string strColDelim, string strRowDelim, string strFileName)
	{
		DataGrid dgExport = new DataGrid();		
		dgExport.AllowPaging =false;
		dgExport.DataSource =dsResults;
		dsResults.DataSetName ="WebERP";
		dgExport.DataMember =  dsResults.Tables[0].TableName;
		dgExport.DataBind();			
		System.Web.HttpContext.Current.Response.Clear();			
		System.Web.HttpContext.Current.Response.Buffer= true;
		System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
		System.Web.HttpContext.Current.Response.Charset = "";
		System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" +strFileName );
		switch(enExport.ToString().ToLower())
		{
			case "xls":
			{
				System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";										
				System.IO.StringWriter oStringWriter = new System.IO.StringWriter();										
				System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
				dgExport.RenderControl(oHtmlTextWriter);
				System.Web.HttpContext.Current.Response.Write(oStringWriter.ToString());
				break;
			}
			case "custom":
			{
				string strText;							
				System.Web.HttpContext.Current.Response.ContentType = "text/txt";													
				System.IO.StringWriter oStringWriter = new System.IO.StringWriter();					
				System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);					
				dgExport.RenderControl(oHtmlTextWriter);
				strText = oStringWriter.ToString();					
				strText = ParseToDelim(strText ,strRowDelim,strColDelim);
				System.Web.HttpContext.Current.Response.Write(strText);
				break;
			}
			case "csv":
			{
				string strText;		
				strRowDelim = System.Environment.NewLine;
				strColDelim = ",";
				System.Web.HttpContext.Current.Response.ContentType = "text/txt";
				System.IO.StringWriter oStringWriter = new System.IO.StringWriter();					
				System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);										
				dgExport.RenderControl(oHtmlTextWriter);
				strText = oStringWriter.ToString();					
				strText = ParseToDelim(strText ,strRowDelim,strColDelim);
				System.Web.HttpContext.Current.Response.Write(strText);
				break;
			}			
			case "tsv": //tab seperated
			{
				string strText;		
				strRowDelim = System.Environment.NewLine;
				strColDelim = "\t";
				System.Web.HttpContext.Current.Response.ContentType = "text/txt";													
				System.IO.StringWriter oStringWriter = new System.IO.StringWriter();					
				System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);										
				dgExport.RenderControl(oHtmlTextWriter);
				strText = oStringWriter.ToString();					
				strText = ParseToDelim(strText ,strRowDelim,strColDelim);
				System.Web.HttpContext.Current.Response.Write(strText);
				break;
			}
			case "xml":
			{
				System.Web.HttpContext.Current.Response.ContentType = "text/xml";					
				System.Web.HttpContext.Current.Response.Write(dsResults.GetXml());										
				break;
			}
			case "htm":
			{					
				System.Web.HttpContext.Current.Response.ContentType = "text/html";
				System.IO.StringWriter oStringWriter = new System.IO.StringWriter();					
				System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
				dgExport.RenderControl(oHtmlTextWriter);					
				System.Web.HttpContext.Current.Response.Write(oStringWriter.ToString());					
				break;
			}
		}


        System.Web.HttpContext.Current.Response.End();
	}



    #region "Export To a Delim Format"
    public static string ParseToDelim(string strText, string  strRowDelim , string strColDelim)
    {
	    Regex objReg = new Regex(@"(>\s+<)",RegexOptions.IgnoreCase);					
	    strText = objReg.Replace(strText,"><");
	    strText = strText.Replace(System.Environment.NewLine,"");
	    strText = strText.Replace("</td></tr><tr><td>",strRowDelim);
	    strText = strText.Replace("</td><td>",strColDelim);
	    objReg = new Regex(@"<[^>]*>",RegexOptions.IgnoreCase);					
	    strText = objReg.Replace(strText,"");				
	    strText = System.Web.HttpUtility.HtmlDecode(strText);
	    return strText;
    }
    #endregion
    		
    public enum ExportFormat
    {
	    XML,
	    XLS,
	    HTML,
	    CSV,
	    CUSTOM,
	    TSV	
    }
}
