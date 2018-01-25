using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.Serialization;


/// <summary>
/// Summary description for ImageHandler
/// </summary>

namespace UserClass
{
    [Serializable]
    public class ImageHandler
    {

        //        private int MinSize = 1;           //in bytes -- 1 byte
        private Int64 MaxSize = 10 * 1024 * 1024;   //in bytes -- 1MB

        private int thumbnailWidth = 0;    //optional
        private int thumbnailHeight = 0;
        private string thumbnailPrefix = "Thumb_";

        private HttpPostedFile gfile;
        private string foldername = "";
        private string physicalfolderpath = "";

        private string originalFilename = "";
        private string newFilename = "";
        private string thumbnailFilename = "";

        public ImageHandler(HttpPostedFile file, string foldername)
        {
            gfile = file;
            this.foldername = foldername;
            CreateFolder(foldername);
        }

        public ImageHandler(HttpPostedFile file, string foldername, Int64 MaxFileSize)
        {
            gfile = file;
            MaxSize = MaxFileSize;
            CreateFolder(foldername);
        }

        public bool CreateFolder(string foldername)
        {

            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(foldername)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("") + "\\" + foldername);

                physicalfolderpath = HttpContext.Current.Server.MapPath(foldername);
                if (IsImage(gfile))
                {
                    originalFilename = gfile.FileName.Substring(gfile.FileName.LastIndexOf('\\') + 1);
                    newFilename = getRandomAlphaNumericFilename(physicalfolderpath, originalFilename);
                    thumbnailFilename = thumbnailPrefix + newFilename;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }


        public int ThumbnailWidth
        {
            get
            {
                return thumbnailWidth;
            }
            set
            {
                thumbnailWidth = value;
            }
        }


        public int ThumbnailHeight
        {
            get
            {
                return thumbnailHeight;
            }
            set
            {
                thumbnailHeight = value;
            }
        }

        public string ThumbnailPrefix
        {
            get
            {
                return thumbnailPrefix;
            }
            set
            {
                thumbnailPrefix = value;
            }
        }


        public string NewFilename
        {
            get
            {
                return newFilename;
            }
        }

        public string ThumbnailFilename
        {
            get
            {
                return thumbnailFilename;
            }
        }


        private bool IsImage(HttpPostedFile file)
        {
            if (file != null && Regex.IsMatch(file.ContentType, "image/\\S+") && file.ContentLength > 0 && file.ContentLength <= MaxSize)
                return true;

            return false;
        }

        public bool UploadImage()
        {
            if (!IsImage(gfile))
                return false;


            try
            {
                string filePath = HttpContext.Current.Server.MapPath(foldername + "/" + newFilename);
                gfile.SaveAs(filePath);
            }
            catch
            {
                return false;
            }

            return true;
        }



        public bool ResizeImage()
        {
            if (!IsImage(gfile))
                return false;

            using (Bitmap orgbitmap = new Bitmap(gfile.InputStream, false))
            {
                try
                {
                    int orgWidth = orgbitmap.Width;
                    int orgHeight = orgbitmap.Height;
                    
                    int newWidth = 0;
                    int newHeight = 0;

                    if (orgHeight == 0 || orgWidth == 0)
                        return false;




                    if ((orgWidth > thumbnailWidth) || (orgHeight > thumbnailHeight))
                    {

                        float widthRatio = 0F;
                        float heightRatio = 0F;
                        float newratio = 0F;

                        widthRatio = (float)orgWidth / (float)thumbnailWidth;
                        heightRatio = (float)orgHeight / (float)thumbnailHeight;

                        if (widthRatio > heightRatio)
                            newratio = (float)widthRatio;
                        else
                            newratio = (float)heightRatio;

                        newWidth = Convert.ToInt32(orgWidth / newratio);
                        newHeight = Convert.ToInt32(orgHeight / newratio);
                    }
                    else
                    {
                        newHeight = orgHeight;
                        newWidth = orgWidth;
                    }

                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(gfile.InputStream))
                    using (Bitmap bitmap = new Bitmap(image, newWidth, newHeight))
                    {
                        bitmap.Save(HttpContext.Current.Server.MapPath(foldername + "/" + thumbnailPrefix + newFilename), image.RawFormat);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }


        }


        private string GetRdm()
        {
            RandomNumberGenerator rm;
            rm = RandomNumberGenerator.Create();

            byte[] data = new byte[1];

            rm.GetNonZeroBytes(data);

            int nVal = Convert.ToInt32(data.GetValue(0));
            if ((nVal >= 48 && nVal <= 57) || (nVal >= 65 && nVal <= 90) || (nVal >= 97 && nVal <= 122))
            {
                return Convert.ToChar(nVal).ToString();
            }
            else
            {
                return GetRdm();
            }
        }


        private string getRandomAlphaNumericFilename(string PhysicalFilePath, string Filename)
        {
            int count = 0;
            string strRdm = "";
            string filetype = "";

            int n1 = Filename.LastIndexOf(".");
            if (n1 > 0)
                filetype = Filename.Substring(n1);
            else
                return Filename;

            while (count < 16)
            {
                count++;
                strRdm += GetRdm().ToString();
            }

            strRdm += filetype;

            if (Directory.Exists(PhysicalFilePath + "\\" + strRdm))
                return getRandomAlphaNumericFilename(PhysicalFilePath, Filename);
            else
                return strRdm;
        }

    }

    public class ImageResizing
    {

        // private Int64 MaxSize = 2 * 1024 * 1024;   //in bytes -- 5MB

        private int thumbnailWidth = 0;    //optional
        private int thumbnailHeight = 0;
        private string filename = "";
        private string foldername = "";



        public ImageResizing()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int ThumbnailWidth
        {
            get
            {
                return thumbnailWidth;
            }
            set
            {
                thumbnailWidth = value;
            }
        }


        public int ThumbnailHeight
        {
            get
            {
                return thumbnailHeight;
            }
            set
            {
                thumbnailHeight = value;
            }
        }

        public string FolderName
        {
            get
            {
                return foldername;
            }
            set
            {
                foldername = value;
            }

        }
        public string NewFileName
        {
            get
            {
                return filename;

            }
            set
            {
                filename = value;
            }

        }


        public bool ResizeImage(string path)
        {
            if (path != "")
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);

                using (Bitmap orgbitmap = new Bitmap(img))
                {
                    try
                    {
                        int orgWidth = orgbitmap.Width;
                        int orgHeight = orgbitmap.Height;

                        int newWidth = 0;
                        int newHeight = 0;

                        if (orgHeight == 0 || orgWidth == 0)
                            return false;




                        if ((orgWidth > thumbnailWidth) || (orgHeight > thumbnailHeight))
                        {

                            float widthRatio = 0F;
                            float heightRatio = 0F;
                            float newratio = 0F;

                            widthRatio = (float)orgWidth / (float)thumbnailWidth;
                            heightRatio = (float)orgHeight / (float)thumbnailHeight;

                            if (widthRatio > heightRatio)
                                newratio = (float)widthRatio;
                            else
                                newratio = (float)heightRatio;

                            newWidth = Convert.ToInt32(orgWidth / newratio);
                            newHeight = Convert.ToInt32(orgHeight / newratio);
                        }
                        else
                        {
                            newHeight = orgHeight;
                            newWidth = orgWidth;
                        }

                        //using (System.Drawing.Image image = System.Drawing.Image.FromStream(gfile.InputStream))
                        using (Bitmap bitmap = new Bitmap(img, newWidth, newHeight))
                        {
                            bitmap.Save(HttpContext.Current.Server.MapPath(foldername + "/" + filename), img.RawFormat);
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }




    }
}
