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
using System.IO;
using UserClass;

public partial class Admin_Peptech_ManageProduct_MultipleImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //HttpFileCollection uploads = HttpContext.Current.Request.Files["Images"];
        //for (int i = 0; i < Convert.ToInt16(hdImage.Value.ToString()); i++)
        //Page.RegisterStartupScript("autorowgenation", "<script type='text/javascript'>GenerateRow();</script>");

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
      

       
    }
}
