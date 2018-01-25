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
        //if (!Page.IsPostBack)
        //{
            DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Category order by categoryname").Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "categoryname";
                ddlCategory.DataValueField = "id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));

            }
            DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory order by subcategoryname").Tables[0];
            if (dt1.Rows.Count > 0)
            {
                ddlSubCategory.DataSource = dt1;
                ddlSubCategory.DataTextField = "subcategoryname";
                ddlSubCategory.DataValueField = "id";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
            }
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                //btnupdate.Visible = true;
                DataSet dsservice = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where id=" + Request.QueryString["id"]);
                if (dsservice.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.Items.FindByValue(dsservice.Tables[0].Rows[0]["categoryid"].ToString()).Selected = true;
                    ddlSubCategory.Items.FindByValue(dsservice.Tables[0].Rows[0]["subcategoryid"].ToString()).Selected = true;
                    txtProduct.Text = dsservice.Tables[0].Rows[0]["ProductName"].ToString();
                    txtCAS.Text = dsservice.Tables[0].Rows[0]["CAS"].ToString().Replace("''", "'");
                    txtFormula.Text = dsservice.Tables[0].Rows[0]["Formula"].ToString().Replace("''", "'");
                    txtWeight.Text = dsservice.Tables[0].Rows[0]["mweight"].ToString().Replace("''", "'");
                    if (dsservice.Tables[0].Rows[0]["ProductImage"].ToString() != "" && dsservice.Tables[0].Rows[0]["ProductImage"] != null)
                    {
                        Image1.ImageUrl = "~/ProductImage/" + dsservice.Tables[0].Rows[0]["ProductImage"].ToString();
                    }
                    else
                    {
                        Image1.ImageUrl = "~/ProductImage/" + dsservice.Tables[0].Rows[0]["ProductImage"].ToString();
                    }
                }
                DataTable dtCatalog = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "catalog where productid=" + Request.QueryString["id"].ToString()+" order by id asc").Tables[0];
                ViewState["noOfRow"] = dtCatalog.Rows.Count.ToString();
                if (dtCatalog.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCatalog.Rows.Count; i++)
                    {
                        HtmlTableRow tblRow = new HtmlTableRow();
                        HtmlTableCell tblCell = new HtmlTableCell();
                        HtmlTableCell tblCell1 = new HtmlTableCell();
                        tblCell.Width = "18%";
                        tblCell1.Width = "78%";
                        tblCell1.Align = "left";
                        HiddenField hid = new HiddenField();
                        hid.ID = "hid" + i.ToString();
                        hid.Value = dtCatalog.Rows[i]["id"].ToString();

                        Label lblCatalog = new Label();
                        lblCatalog.ID = "catalog"+i.ToString();
                        lblCatalog.Attributes.Add("runat","server");
                        lblCatalog.Text = "Catalog Name:";
                        lblCatalog.Attributes.Add("Font-Bold", "True");
                        tblCell.Controls.Add(lblCatalog);
                        tblCell.Controls.Add(hid);
                        tblRow.Cells.Add(tblCell);


                        TextBox txtCatalog = new TextBox();
                        txtCatalog.ID = "txtcatalog" + i.ToString();
                        txtCatalog.Text = dtCatalog.Rows[i]["catalogname"].ToString();
                        txtCatalog.EnableViewState = true;
                        txtCatalog.Attributes.Add("runat", "server");
                        tblCell1.Controls.Add(txtCatalog);
                        tblRow.Cells.Add(tblCell1);

                       
                        HtmlTableRow tblRow1 = new HtmlTableRow();
                        HtmlTableCell tblCell2 = new HtmlTableCell();
                        HtmlTableCell tblCell3 = new HtmlTableCell();
                        tblCell2.Width = "18%";
                        tblCell3.Width = "78%";
                        tblCell3.Align = "left";
                        Label lblCatalog1 = new Label();
                        lblCatalog1.ID = "lblQuantity" + i.ToString();
                        lblCatalog1.Attributes.Add("runat", "server");
                        lblCatalog1.Text = "Quantity:";
                        lblCatalog1.Attributes.Add("Font-Bold", "True");
                        tblCell2.Controls.Add(lblCatalog1);
                        tblRow1.Cells.Add(tblCell2);

                        TextBox txtCatalog1 = new TextBox();
                        txtCatalog1.ID = "txtQuantity" + i.ToString();
                        txtCatalog1.Text = dtCatalog.Rows[i]["quantity"].ToString();
                        txtCatalog1.EnableViewState = true;
                        txtCatalog1.Attributes.Add("runat", "server");
                        tblCell3.Controls.Add(txtCatalog1);
                        tblRow1.Cells.Add(tblCell3);

                        HtmlTableRow tblRow2 = new HtmlTableRow();
                        HtmlTableCell tblCell4 = new HtmlTableCell();
                        HtmlTableCell tblCell5 = new HtmlTableCell();
                        tblCell4.Width = "18%";
                        tblCell5.Width = "78%";
                        tblCell5.Align = "left";
                        Label lblCatalog2 = new Label();
                        lblCatalog2.ID = "lblPrice" + i.ToString();
                        lblCatalog2.Attributes.Add("runat", "server");
                        lblCatalog2.Text = "Price:";
                        lblCatalog2.Attributes.Add("Font-Bold", "True");
                        tblCell4.Controls.Add(lblCatalog2);
                        tblRow2.Cells.Add(tblCell4);

                        TextBox txtCatalog2 = new TextBox();
                        txtCatalog2.ID = "txtPrice" + i.ToString();
                        txtCatalog2.Text = dtCatalog.Rows[i]["price"].ToString();
                        txtCatalog2.EnableViewState = true;
                        txtCatalog2.Attributes.Add("runat", "server");
                        tblCell5.Controls.Add(txtCatalog2);
                        tblRow2.Cells.Add(tblCell5);

                        tblGenerate.Rows.Add(tblRow);
                        tblGenerate.Rows.Add(tblRow1);
                        tblGenerate.Rows.Add(tblRow2);
                        PlaceHolder1.Controls.Add(tblGenerate);
                        //Response.Write(txtHour.Text);
                    }

                    // Response.Write(((TextBox)tblGenerate.Rows[0].Cells[1].Controls[0]).Text);

                }

            }
            else if (Request.QueryString["edit"] != null && Request.QueryString["edit"].ToString() != "")
            {

                //btnupdate.Visible = false;
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_peptech/ManageProduct/ProductList.aspx");
            }
       // }

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
                    ddlSubCategory.DataSource = dt1;
                    ddlSubCategory.DataTextField = "subcategoryname";
                    ddlSubCategory.DataValueField = "id";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, "<--Please Select-->");
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
            string strupdcat = "update " + customUtility.DBPrefix + "product set categoryid='" + ddlCategory.SelectedValue.ToString() + "',subcategoryid='" + ddlSubCategory.SelectedValue.ToString() + "',ProductName='" + txtProduct.Text.Replace("'", "''").Trim() + "',CAS='" + txtCAS.Text.Replace("'", "''") + "'";
            strupdcat += ",Formula='"+txtFormula.Text.Replace("'","''")+"',MWeight='"+txtWeight.Text.Replace("'","''")+"'";
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
            //if (chkfile(fuPImage.FileName))
            //{
            //    fuPImage.SaveAs(Server.MapPath("~/ProductImage/"+fuPImage.FileName.ToString()));
            //    strupdcat += ",ProductImage='" + fuPImage.FileName.ToString() + "'";
            //}
            strupdcat+="where id=" + Request.QueryString["id"].ToString();
            bool check = customUtility.CheckDataExists("select * from " + customUtility.DBPrefix + "product where id!=" + Request.QueryString["id"].ToString() + " and productname='" + txtProduct.Text.Replace("'", "''") + "'");
            if (check == true)
            {
                lblmessage.Text = "Product Name already exists ! Try another Product Name.";
            }
            else
            {
                customUtility.ExecuteNonQuery(strupdcat);
               // Response.Write("select * from " + customUtility.DBPrefix + "catalog where productid=" + Request.QueryString["id"].ToString() + " order by id asc");
               // Response.End();
                DataTable dtCatalog = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "catalog where productid=" + Request.QueryString["id"].ToString()+" order by id asc").Tables[0];
                ViewState["noOfRow"] = dtCatalog.Rows.Count.ToString();
                if (dtCatalog.Rows.Count > 0)
                {
                 for (int i = 0; i < dtCatalog.Rows.Count; i++)
                    {
                        Response.Write(Request.Form["txtCatalog" + i].ToString());
                        Response.End();
                        if (dtCatalog.Rows[i]["id"].ToString() == Request.Form["hid"+i].ToString())
                        {
                            string Updatecatalog = "update " + customUtility.DBPrefix + "catalog set CatalogName='" + Request.Form["txtCatalog" + i].ToString() + "',Quantity='" + Request.Form["txtQuantity" + i].ToString() + "',price='" + Request.Form["txtPrice" + i].ToString() + "' where id=" + Request.Form["hid" + i].ToString() + "";
                            customUtility.ExecuteNonQuery(Updatecatalog);
                        }
                        else
                        {

                        }
                    }
                }
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
}
