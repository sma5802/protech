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
           // Response.Write(customUtility.GetAField("select count(id)+1 from "+customUtility.DBPrefix+"product"));
          // LinkButton lb=(LinkButton)DataList1.FindControl("LinkButton1);
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["categoryname"].ToString());
                    li.Value = dt.Rows[i]["id"].ToString();
                    ddlCategory.Items.Add(li);
                }
                //ddlCategory.DataSource = dt;
                //ddlCategory.DataTextField = "categoryname";
                //ddlCategory.DataValueField = "id";
                //ddlCategory.DataBind();
                ddlCategory.Items.Insert(0,new ListItem("<--Please Select-->",""));
                ddlSubCategory.Items.Insert(0,new ListItem("<--Please Select-->",""));
            }
            DataTable dtgreek = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greek where status=1").Tables[0];
            if (dtgreek.Rows.Count > 0)
            {
                DataList1.DataSource = dtgreek;
                DataList1.DataBind();
            }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        Response.Write(Session["id"]);
        string product=txtProduct.Text;
        Response.Write(lblsym.Text);
        txtProduct.Text += e.CommandArgument.ToString();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCategory.SelectedIndex.ToString() != "0")
        {
            DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "' order by subcategoryname").Tables[0];

            if (dt1.Rows.Count > 0)
            {
                ddlSubCategory.Items.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dt1.Rows[i]["subcategoryname"].ToString());
                    li.Value = dt1.Rows[i]["id"].ToString();
                    ddlSubCategory.Items.Add(li);
                }
                //ddlSubCategory.DataSource = dt1;
                //ddlSubCategory.DataTextField = "subcategoryname";
                //ddlSubCategory.DataValueField = "id";
                //ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->",""));
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string UploadFolder = "~/ProductImage";
        string newFilename = "";
        string Spath = "";
        string sort = customUtility.GetAField("select count(id)+1 from "+customUtility.DBPrefix+"product");
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
        //bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid='" + ddlSubCategory.SelectedValue.ToString() + "'");
        if (ddlSubCategory.SelectedIndex!=0)
        {
            if (customUtility.CheckDuplicateFieldValue(txtProduct.Text.Replace("'", "''").Trim(), "product", "productname", customUtility.CompareType.text, " and categoryid=" + ddlCategory.SelectedValue + " and subcategoryid=" + ddlSubCategory.SelectedValue + " and formula='" + txtFormula.Text.Replace("'", "''") + "' and mweight='" + txtWeight.Text.Replace("'", "''") + "'"))
            {
                lblmessage.Text = "Product Name already exists for this category ! Try another.";
            }
            else
            {
                //string str = txtProduct.Text.Replace("'", "''");
                //writeGreekchar(str);
                //string straddcat = "insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,ProductImage,Status) values('" + ddlCategory.SelectedValue.ToString() + "'," + ddlSubCategory.SelectedValue.ToString() + ",'" + txtProduct.Text.Replace("'", "''").Trim() + "','" + txtCAS.Text.Replace("'", "''") + "','" + txtFormula.Text.Replace("'", "''") + "','" + txtWeight.Text.Replace("'", "''") + "','" + newFilename.ToString() + "',1)";
                string straddcat = "insert into " + customUtility.DBPrefix + "product(categoryid,subcategoryid,ProductName,CAS,Formula,MWeight,ProductImage,Status,sort) values('" + ddlCategory.SelectedValue.ToString() + "'," + ddlSubCategory.SelectedValue.ToString() + ",'" + customUtility.writeGreekchar(txtProduct.Text.Replace("'","''")) + "','" + txtCAS.Text.Replace("'", "''") + "','" + txtFormula.Text.Replace("'", "''") + "','" + txtWeight.Text.Replace("'", "''") + "','" + newFilename.ToString() + "',1,"+sort+")";
                //Response.Write(straddcat);
                //Response.End();
                customUtility.ExecuteNonQuery(straddcat);

                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx?add=1");
            }
        }
        else
        {
            if (customUtility.CheckDuplicateFieldValue(txtProduct.Text.Replace("'", "''").Trim(), "product", "productname", customUtility.CompareType.text, " and categoryid=" + ddlCategory.SelectedValue + " and formula='" + txtFormula.Text.Replace("'", "''") + "' and mweight='" + txtWeight.Text.Replace("'", "''") + "'"))
            {
                lblmessage.Text = "Product Name already exists for this category ! Try another.";
            }
            else
            {
                string straddcat = "insert into " + customUtility.DBPrefix + "product(categoryid,ProductName,CAS,Formula,MWeight,ProductImage,Status,sort) values('" + ddlCategory.SelectedValue.ToString() + "','" + customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''")) + "','" + txtCAS.Text.Replace("'", "''") + "','" + txtFormula.Text.Replace("'", "''") + "','" + txtWeight.Text.Replace("'", "''") + "','" + newFilename.ToString() + "',1,"+sort+")";
                customUtility.ExecuteNonQuery(straddcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx?add=1");
            }
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtProduct.Text = "";
        //txtCAS.Text = "";
        //txtFormula.Text = "";
        //txtWeight.Text = "";
        //ddlCategory.Items.Clear();
        //ddlSubCategory.Items.Clear();
        //DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "category order by categoryname").Tables[0];
        //if (dt.Rows.Count > 0)
        //{
        //    ddlCategory.DataSource = dt;
        //    ddlCategory.DataTextField = "categoryname";
        //    ddlCategory.DataValueField = "id";
        //    ddlCategory.DataBind();
        //    ddlCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
        //    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
        //}
        Response.Redirect("ProductList.aspx");
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton lb = (LinkButton)e.Item.FindControl("LinkButton1");
            lb.Attributes.Add("Onclick", "return false;");
            Response.Write(lb.Text);
            
        }
    }
}
