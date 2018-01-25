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

public partial class Admin_Peptech_Downloads_Editdownloads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                DataTable dtDownloads = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Downloads where id=" + Request.QueryString["id"].ToString()).Tables[0];
                if (dtDownloads.Rows.Count > 0)
                {
                    txtDownloadtext.Text = dtDownloads.Rows[0]["downloadtext"].ToString();
                    txtTitle.Text = dtDownloads.Rows[0]["Title"].ToString();
                    string physicalPath = "";
                    string imagepath = "~/downloads/" + dtDownloads.Rows[0]["downloadfile"].ToString();
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
                        lbDownload.CommandName = "download";
                        lbDownload.CommandArgument = dtDownloads.Rows[0]["downloadfile"].ToString();
                        //ViewDownload.HRef = ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Downloads/" + dtDownloads.Rows[0]["downloadfile"].ToString();
                    }
                    else
                    {
                        lbDownload.Enabled = false;
                        lbDownload.Attributes.Add("onclick", "javascript:alert('File not available.');");
                        //ViewDownload.Visible = false;
                    }
                    Image1.ImageUrl = "~/DownloadsImage/Small_" + dtDownloads.Rows[0]["image"].ToString();
                }
            }
            else
                Response.Redirect("DownloadList.aspx");
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string UploadFolder = "~/DownloadsImage";
        string newFilename = "";
        string Spath = "";
        string Updatedownloads = "update " + customUtility.DBPrefix + "Downloads set downloadtext='" + txtDownloadtext.Text.Replace("'", "''") + "',Title='" + txtTitle.Text.Replace("'", "''") + "' ";
        if (FileUpload1.HasFile)
        {
            FileUpload1.SaveAs(Server.MapPath("~/Downloads/" + FileUpload1.FileName.ToString().Replace(" ", "_")));
            Updatedownloads += ",downloadfile='" + FileUpload1.FileName.ToString().Replace(" ", "_").Replace("'", "''") + "'";
        }
        if (FileUpload2.HasFile)
        {
            if (chkfile(FileUpload2.FileName))
            {
                ImageHandler newImg = new ImageHandler(FileUpload2.PostedFile, UploadFolder);
                bool uploadLrg = newImg.UploadImage();
                newFilename = newImg.NewFilename;

                Spath = UploadFolder + "/" + newFilename;
                ImageResizing imgsize = new ImageResizing();

                imgsize.NewFileName = "Small_" + newFilename;
                imgsize.FolderName = UploadFolder;
                imgsize.ThumbnailHeight = 51;
                imgsize.ThumbnailWidth = 44;
                imgsize.ResizeImage(Server.MapPath(Spath));

                imgsize.NewFileName = "Thumb_" + newFilename;
                imgsize.FolderName = UploadFolder;
                imgsize.ThumbnailHeight = 51;
                imgsize.ThumbnailWidth = 44;
                imgsize.ResizeImage(Server.MapPath(Spath));
                Updatedownloads += ",image='" + newFilename.ToString() + "'";
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Files having extension .pdf,.doc,.docx,.png ,.jpg ,.jpeg ,.gif are allowed";
            }
        }
        Updatedownloads += "where id=" + Request.QueryString["id"].ToString();
        bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "Downloads where id!=" + Request.QueryString["id"].ToString() + " and Title='" + txtTitle.Text.Replace("'", "''") + "'");
        if (check == true)
        {
            lblmessage.Text = "Downloads Title already exists ! Try another Downloads Title.";
        }
        else
        {
            customUtility.ExecuteNonQuery(Updatedownloads);
            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/Downloads/DownloadList.aspx?Upd=1");
        }
    }
    public Boolean chkfile(string file)
    {
        string fileext = Path.GetExtension(file).ToLower();
        switch (fileext)
        {
            case ".doc":
                return true; break;
            case ".docx":
                return true; break;
            case ".pdf":
                return true; break;
            case ".gif":
                return true; break;
            case ".jpg":
                return true; break;
            case ".jpeg":
                return true; break;
            case ".png":
                return true; break;
            default:
                return false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DownloadList.aspx");
         //txtDownloadtext.Text = "";
         //txtTitle.Text = "";
         //ViewDownload.Visible = false;
         //Image1.Visible = false;
    }

    protected void lbDownload_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "download")
        {
            Response.ContentType = "File";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + e.CommandArgument);
            Response.WriteFile(Server.MapPath("~/Downloads/" + e.CommandArgument));
        }
    }
}
