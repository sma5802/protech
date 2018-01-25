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
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Drawing.Drawing2D;

public partial class codelibrary_components_TempImage : System.Web.UI.Page
{
    int thumbnailWidth = 0, thumbnailHeight = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["url"] == null)
            return;


        string filePath = "~/" + Request.QueryString.Get("url");

        thumbnailHeight = 200;
        thumbnailWidth = 400;

        int tmpRet;
        if (Request.QueryString["w"] != null)
        {
            if (Int32.TryParse(Request.QueryString["w"].ToString(), out tmpRet))
                thumbnailWidth = tmpRet;
        }

        if (Request.QueryString["h"] != null)
        {
            if (Int32.TryParse(Request.QueryString["h"].ToString(), out tmpRet))
                thumbnailHeight = tmpRet;
        }

        this.Response.Clear();
        this.Response.ContentType = "image/jpeg";

        Bitmap tmpImage = ResizeImage(filePath);

        tmpImage.Save(this.Response.OutputStream, ImageFormat.Jpeg);

        tmpImage.Dispose();
        tmpImage = null;
    }



    private Bitmap ResizeImage_OldAlgo(string gfile)
    {
        //gfile = Server.MapPath(gfile);
        Bitmap bitmap = new Bitmap(Server.MapPath(gfile));
        //return bitmap;


        StreamReader strm = new StreamReader(Server.MapPath(gfile));


        /*if (!IsImage(gfile))
            return strm.BaseStream;
        */



        Bitmap orgbitmap = new Bitmap(strm.BaseStream);//, false);
        //try
        //{
            int orgWidth = orgbitmap.Width;
            int orgHeight = orgbitmap.Height;

            int newWidth = 0;
            int newHeight = 0;

            if (orgHeight == 0 || orgWidth == 0)
                return null;

            if ((orgWidth > thumbnailWidth) || (orgHeight > thumbnailHeight))
            {
                float widthRatio = 0F;
                float heightRatio = 0F;
                float newratio = 0F;

                widthRatio = (float)orgWidth / (float)thumbnailWidth;
                heightRatio = (float)orgHeight / (float)thumbnailHeight;

                if (widthRatio > heightRatio)
                    newratio = widthRatio;
                else
                    newratio = heightRatio;

                newWidth = Convert.ToInt32((float)orgWidth / newratio);
                newHeight = Convert.ToInt32((float)orgHeight / newratio);
            }
            else
            {
                newHeight = orgHeight;
                newWidth = orgWidth;
            }

            System.Drawing.Image image = System.Drawing.Image.FromStream(strm.BaseStream);
            bitmap = new Bitmap(image, newWidth, newHeight);
        
        //}
        //catch
        //{
        //    return null;
        //}



            strm.Close();


        //return strm.BaseStream;
        return bitmap;


    }







    public Bitmap ResizeImage(string gfile)
    {
     Bitmap bitmapNew; float fx,fy,f; 
     int widthTh,heightTh; float widthOrig,heightOrig;


        // create thumbnail using .net function GetThumbnailImage
       bitmapNew = new Bitmap(Server.MapPath(gfile)); // load original image
        // retain aspect ratio
         widthOrig = bitmapNew.Width;
         heightOrig = bitmapNew.Height;
         fx = widthOrig/thumbnailWidth;
         fy = heightOrig/thumbnailHeight; // subsample factors
         // must fit in thumbnail size
         f=Math.Max(fx,fy); if(f<1) f=1;
         widthTh = (int)(widthOrig/f); heightTh = (int)(heightOrig/f);
        
        bitmapNew = (Bitmap)bitmapNew.GetThumbnailImage(widthTh, heightTh,
         new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback),IntPtr.Zero);

        return bitmapNew;
    }
    

    public bool ThumbnailCallback() { return false; }
    

}
