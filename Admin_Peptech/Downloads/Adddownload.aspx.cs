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
public partial class Admin_Peptech_Downloads_Adddownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["add"] != null && Request.QueryString["add"].ToString() != "")
            {
                btnsubmit.Visible = true;
                //btnupdate.Visible = false;
                pnlSubmit.DefaultButton = "btnsubmit";
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/Downloads/DownloadList.aspx");
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string UploadFolder = "~/DownloadsImage";
        string newFilename = "";
        string Spath = "";

        if (FileUpload1.HasFile)
        {
            if (FileUpload2.HasFile)
            {
                if (chkfile(FileUpload1.FileName))
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
                        FileUpload1.SaveAs(Server.MapPath("~/Downloads/" + FileUpload1.FileName.ToString().Replace(" ", "_")));

                        Int32 preference = 0;
                        DataTable dtc = customUtility.GetTableData("select max(preference)+1 as pre from " + customUtility.DBPrefix + "downloads").Tables[0];
                        if (dtc.Rows.Count > 0)
                        {
                            if (dtc.Rows[0]["pre"].ToString() != "")
                                preference = (int)dtc.Rows[0]["pre"];
                            else
                                preference = 1;
                        }
                        else
                            preference = 1;

                        string strDownload = "insert into " + customUtility.DBPrefix + "Downloads(downloadtext,Title,downloadfile,image,preference,Status)values('" + txtDownloadtext.Text.Replace("'", "''") + "','" + txtTitle.Text.Replace("'", "''") + "','" + FileUpload1.FileName.ToString().Replace(" ", "_").Replace("'","''") + "','" + newFilename.ToString() + "'," + preference + ",1)";
                        if (customUtility.CheckDuplicateFieldValue(txtTitle.Text.Replace("'", "''").Trim(), "Downloads", "Title", customUtility.CompareType.text, ""))
                        {
                            lblmessage.Text = "Title already exists ! Try another Title Name";
                        }
                        else
                        {
                            customUtility.ExecuteNonQuery(strDownload);
                            Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/Downloads/DownloadList.aspx?add=1");
                        }
                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Files having extension .pdf,.doc,.docx,.png ,.jpg ,.jpeg ,.gif,.sdf are allowed";
                    }
                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Files having extension .pdf,.doc,.docx,.png ,.jpg ,.jpeg ,.gif are allowed";
                }
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please provide Download file Image.";
            }
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please provide Download file.";
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
            case ".sdf":
                return true; break;
            default:
                return false;
        }
    }
}
