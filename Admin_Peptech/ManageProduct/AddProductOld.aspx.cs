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

public partial class Admin_Peptech_ManageProduct_AddProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
          
            if (Request.QueryString["add"] != null && Request.QueryString["add"].ToString() != "")
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
            string UploadFolder = "~/ProductImage";
            string newFilename = "";
            string Spath = "";
                if (fuPImage.HasFile)
                {
                    if (chkfile(fuPImage.FileName))
                    {
                        ImageHandler newImg = new ImageHandler(fuPImage.PostedFile, UploadFolder);
                        bool uploadLrg = newImg.UploadImage();
                        newFilename = newImg.NewFilename;

                        Spath = UploadFolder + "/" + newFilename;
                        ImageResizing imgsize = new ImageResizing();
                        imgsize.NewFileName = "Large_" + newFilename;
                        imgsize.FolderName = UploadFolder;
                        imgsize.ThumbnailHeight = 156;
                        imgsize.ThumbnailWidth = 221;
                        imgsize.ResizeImage(Server.MapPath(Spath));

                        imgsize.NewFileName = "Middle_" + newFilename;
                        imgsize.FolderName = UploadFolder;
                        imgsize.ThumbnailHeight = 156;
                        imgsize.ThumbnailWidth = 221;
                        imgsize.ResizeImage(Server.MapPath(Spath));

                        imgsize.NewFileName = "Small_" + newFilename;
                        imgsize.FolderName = UploadFolder;
                        imgsize.ThumbnailHeight = 100;
                        imgsize.ThumbnailWidth = 200;
                        imgsize.ResizeImage(Server.MapPath(Spath));

                        imgsize.NewFileName = "Thumb_" + newFilename;
                        imgsize.FolderName = UploadFolder;
                        imgsize.ThumbnailHeight = 75;
                        imgsize.ThumbnailWidth = 150;
                        imgsize.ResizeImage(Server.MapPath(Spath));
                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Files having extension .png ,.jpg ,.jpeg ,.gif are allowed";
                    }
                }
                string straddcat = "insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,ProductImage,Status) values('" + ddlCategory.SelectedValue.ToString() + "','" + ddlSubCategory.SelectedValue.ToString() + "','" + txtProduct.Text.Replace("'", "''").Trim() + "','" + txtCAS.Text.Replace("'", "''") + "','" + txtFormula.Text.Replace("'", "''") + "','" + txtWeight.Text.Replace("'", "''") + "','" + newFilename.ToString() + "',1)";
                //bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid='" + ddlSubCategory.SelectedValue.ToString() + "'");
                if (customUtility.CheckDuplicateFieldValue(txtProduct.Text.Replace("'", "''").Trim(), "product", "productname", customUtility.CompareType.text, " and categoryid=" + ddlCategory.SelectedValue + " and subcategoryid=" + ddlSubCategory.SelectedValue))
                {
                    lblmessage.Text = "Product Name already exists for this service category ! Try another service category.";
                }
                else
                {

                    //customUtility.ExecuteNonQuery(straddcat);
                    int id=customUtility.AddToTableReturnID(straddcat);
                    int sCount = 0;
                  
                    for (int i = 0; i < Convert.ToInt16(hdImage.Value.ToString()) / 3; i++)
                    {
                          
                        string strInsertCatalog = "insert into " + customUtility.DBPrefix + "catalog(Productid,CatalogName,Quantity,price,Unit,Status) values(" + id + ",'" + Request.Form["Caption2" + sCount].ToString().Replace("'", "''") + "','" + Request.Form["Caption1" + sCount].ToString().Replace("'", "''") + "','" + Request.Form["caption" + sCount].ToString().Replace("'", "''") + "',1,1)";
                        customUtility.ExecuteNonQuery(strInsertCatalog);
                        sCount++;
                    }
                    Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx?add=1");
                }

    }

    public Boolean chkfile(string file)
    {
        string fileext = Path.GetExtension(file).ToLower();
        switch (fileext)
        {
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
}
