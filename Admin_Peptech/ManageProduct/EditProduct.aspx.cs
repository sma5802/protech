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

public partial class Admin_Peptech_ManageProduct_EditProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Category order by categoryname").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["categoryname"].ToString());
                        li.Value = dt.Rows[i]["id"].ToString();
                        ddlCategory.Items.Add(li);
                    }
                    ddlCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                }

                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where id=" + Request.QueryString["id"]);
                    if (dsservice.Tables[0].Rows.Count > 0)
                    {
                        ddlCategory.Items.FindByValue(dsservice.Tables[0].Rows[0]["categoryid"].ToString()).Selected = true;
                        if (dsservice.Tables[0].Rows[0]["subcategoryid"].ToString() != "" && dsservice.Tables[0].Rows[0]["subcategoryid"] != null)
                        {
                            DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid=" + dsservice.Tables[0].Rows[0]["categoryid"].ToString() + " order by subcategoryname").Tables[0];
                            if (dt1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    ListItem li = new ListItem();
                                    li.Text = HttpUtility.HtmlDecode(dt1.Rows[i]["subcategoryname"].ToString());
                                    li.Value = dt1.Rows[i]["id"].ToString();
                                    ddlSubCategory.Items.Add(li);
                                }
                            }
                            ddlSubCategory.SelectedValue = dsservice.Tables[0].Rows[0]["subcategoryid"].ToString();
                        }
                        ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                        txtProduct.Text = HttpUtility.HtmlDecode(dsservice.Tables[0].Rows[0]["ProductName"].ToString());
                        txtCAS.Text = dsservice.Tables[0].Rows[0]["CAS"].ToString().Replace("''", "'");
                        txtFormula.Text = dsservice.Tables[0].Rows[0]["Formula"].ToString().Replace("''", "'");
                        txtWeight.Text = dsservice.Tables[0].Rows[0]["mweight"].ToString().Replace("''", "'");
                        if (dsservice.Tables[0].Rows[0]["ProductImage"].ToString() != "" && dsservice.Tables[0].Rows[0]["ProductImage"] != null)
                            Image1.ImageUrl = "~/ProductImage/" + dsservice.Tables[0].Rows[0]["ProductImage"].ToString();
                        else
                            Image1.ImageUrl = "../../images/imafe-not-found.jpg";
                    }
                    else
                        Response.Redirect("ProductList.aspx");
                }
                else if (Request.QueryString["edit"] != null && Request.QueryString["edit"].ToString() != "")
                    Response.Redirect("ProductList.aspx");
                else
                    Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_peptech/ManageProduct/ProductList.aspx");

                DataTable dtgreek = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "greek where status=1").Tables[0];
                if (dtgreek.Rows.Count > 0)
                {
                    DataList1.DataSource = dtgreek;
                    DataList1.DataBind();
                }
            }
            catch
            {
                Response.Redirect("ProductList.aspx");
            }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        txtProduct.Text += e.CommandArgument.ToString();
    }

    protected string writeGreekchar(string numberString)
    {
        string strbig = "";
        string strsmal = "";

        char[] ca = numberString.ToCharArray();
        for (int i = 0; i < ca.Length; i++)
        {
            if (ca[i] == 'α' || ca[i] == 'ß' || ca[i] == 'γ' || ca[i] == '±' || ca[i] == '•')
            {
                strbig += ca[i].ToString().Replace("α", "&alpha;").Replace("ß", "&beta;").Replace("γ", "&gamma");
            }
            else
            {
                strbig += ca[i];
            }

        }
        return strbig;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "")
        {
            if (ddlCategory.SelectedIndex.ToString() != "0")
            {
                DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "'").Tables[0];
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

                    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                }
                else
                {
                    ddlSubCategory.Items.Clear();
                    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                }
            }
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string UploadFolder = "~/ProductImage";
        string newFilename = "";
        string Spath = "";
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            string strupdcat = "update " + customUtility.DBPrefix + "product set categoryid='" + ddlCategory.SelectedValue.ToString() + "',";
            if (ddlSubCategory.SelectedIndex.ToString() != "0")
            {
                strupdcat += "subcategoryid=" + ddlSubCategory.SelectedValue.ToString() + ",";
            }
            else
            {
                strupdcat += "subcategoryid=" + ddlSubCategory.SelectedIndex.ToString() + ",";
            }
            strupdcat += "ProductName='" + customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''").Trim()) + "',CAS='" + txtCAS.Text.Replace("'", "''") + "'";
            strupdcat += ",Formula='" + txtFormula.Text.Replace("'", "''") + "',MWeight='" + txtWeight.Text.Replace("'", "''") + "'";
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
                    strupdcat += ",ProductImage='" + newFilename.ToString() + "'";
                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Files having extension .png ,.jpg ,.jpeg ,.gif are allowed";
                }
            }
            strupdcat += "where id=" + Request.QueryString["id"].ToString();
            bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where id!=" + Request.QueryString["id"].ToString() + " and mweight='" + txtWeight.Text.Replace("'", "''") + "' and formula='" + txtFormula.Text.Replace("'", "''") + "' and productname='" + customUtility.writeGreekchar(txtProduct.Text.Replace("'", "''")) + "'");
            if (check == true)
                lblmessage.Text = "Product Name already exists ! Try another Product Name.";
            else
            {
                customUtility.ExecuteNonQuery(strupdcat);
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx?Upd=1");
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
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageProduct/ProductList.aspx");
        DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Category order by categoryname").Tables[0];
        if (dt.Rows.Count > 0)
        {
            ddlCategory.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ListItem li = new ListItem();
                li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["categoryname"].ToString());
                li.Value = dt.Rows[i]["id"].ToString();
                ddlCategory.Items.Add(li);
            }
            ddlCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
        }
        DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory order by subcategoryname").Tables[0];
        if (dt1.Rows.Count > 0)
        {
            ddlSubCategory.Items.Clear();
            ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
        }
        txtProduct.Text = "";
        txtCAS.Text = "";
        txtFormula.Text = "";
        txtWeight.Text = "";
        Image1.Visible = false;
    }
}
