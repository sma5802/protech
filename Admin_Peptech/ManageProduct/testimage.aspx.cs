using System;
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

public partial class Admin_Peptech_ManageProduct_testimage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblServerTime.Text = string.Format("{0:D}", System.DateTime.Now);
        if (Session["AdminID"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/login.aspx?requestpath=" + Request.ServerVariables["SCRIPT_NAME"]);

        //else
        //    lblhead.Text = "Welcome " + Session["admintitle"].ToString();
    }
    protected void lnkSignout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session["AdminID"] = "";
        Session["admintitle"] = "";
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/login.aspx");

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        HttpFileCollection hfc = HttpContext.Current.Request.Files;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength > 0)
            {
                hpf.SaveAs(Server.MapPath("~/TestImage/" + System.IO.Path.GetFileName(hpf.FileName)));
            }
        }

        //HttpFileCollection uploadFiles = HttpContext.Current.Request.Files;
        //Response.Write(hdImage.Value);

        //for (int i = 0; i < int.Parse(hdImage.Value); i++)
        //{
        //    Response.Write(30);
        //    HttpPostedFile uploadFile = uploadFiles[hdImage.Value];
        //    uploadFile.SaveAs(Server.MapPath("~/ProductImage/" + Path.GetFileName(uploadFile.FileName)));
        //}      
        //HttpFileCollection uploads = HttpContext.Current.Request.Files;
        //string UploadFolder = "~/ProductImage";
        //int imagecount = 0;
        //string Spath = "";
        //try
        //{
        //    for (int i = 0; i < Convert.ToInt16(hdImage.Value.ToString()); i++)
        //    {
        //        HttpPostedFile uploaded=uploads[i];
        //        uploaded.SaveAs( UploadFolder + "/" + Path.GetFileName(uploaded.FileName));
        //        //ImageHandler newImg = new ImageHandler(uploads[i], UploadFolder);
        //        //newImg.ThumbnailHeight = 250;
        //        //newImg.ThumbnailWidth = 250;

        //        //string newFilename = newImg.NewFilename;
        //        //Spath = UploadFolder + "/" + newFilename;
        //        //if (newImg.UploadImage() && newImg.ResizeImage())
        //        //{
        //        //    Response.Write(39);
        //        //    newImg.ThumbnailPrefix = "Small_";
        //        //    newImg.ThumbnailHeight = 130;
        //        //    newImg.ThumbnailWidth = 90;
        //        //    newImg.ResizeImage();

        //        //    newImg.ThumbnailPrefix = "xtraSmall_";
        //        //    newImg.ThumbnailHeight = 35;
        //        //    newImg.ThumbnailWidth = 35;
        //        //    newImg.ResizeImage();
        //        //    newImg.ThumbnailPrefix = "Large_";
        //        //    newImg.ThumbnailWidth = 377;
        //        //    newImg.ThumbnailHeight = 280;
        //        //    newImg.ResizeImage();
        //        //    Response.Write("update " + customUtility.DBPrefix + "product set ProductImage='" + newFilename + "'where ProductImage='" + newFilename + "')");
        //        //    Response.End();
        //        //Response.Write("update " + customUtility.DBPrefix + "product set ProductImage='" + Path.GetFileName(uploaded.FileName) + "'where ProductImage='" + Path.GetFileName(uploaded.FileName) + "')");
        //        //Response.End();
        //        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "product set ProductImage='" + Path.GetFileName(uploaded.FileName) + "'where ProductImage='" + Path.GetFileName(uploaded.FileName) + "')");
        //        }
        //}
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
    }
}
