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
using System.IO;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                DataTable dtDownload = customUtility.GetTableData("Select * from " + customUtility.DBPrefix + "downloads where status=1 order by preference").Tables[0];
                if (dtDownload.Rows.Count > 0)
                {
                    dlscities.DataSource = dtDownload;
                    dlscities.DataBind();
                }
            }
            catch { Response.Redirect("default.aspx"); }
        }
    }
    protected void dlscities_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dv = (DataRowView)e.Item.DataItem;
            //HtmlAnchor dd=(HtmlAnchor)e.Item.FindControl("dd");
            LinkButton lb = (LinkButton)e.Item.FindControl("lb");
            string physicalPath = "";
            string imagepath = "~/downloads/" + dv["downloadfile"].ToString();
            int n1 = imagepath.LastIndexOf("/");
            string folderPath = "";
            string filename = "";
            if (n1 > 0)
            {
                folderPath = imagepath.Substring(0, n1);
                filename = imagepath.Substring(n1 + 1);
            }
            physicalPath = HttpContext.Current.Server.MapPath(folderPath);
            if (File.Exists(physicalPath + "\\" + filename))
            {
                lb.Enabled = true;
                //dd.HRef = ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Downloads/" + dv["downloadfile"].ToString();
                //dd.Target = "_blank";
            }
            else
            {
                lb.Enabled = false;
                lb.Attributes.Add("onclick", "javascript:alert('File not available.');");
                //dd.HRef = "javascript:void(0);";
            }
        }
    }
    protected void lb_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            Response.Redirect("~/Downloads/" + e.CommandArgument);
            //Response.ContentType = "File";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + e.CommandArgument);
            //Response.WriteFile(Server.MapPath("~/Downloads/" + e.CommandArgument));
            //string strUrl = "~/Downloads/" + e.CommandArgument;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
    }

}
