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

public partial class Admin_Peptech_ManageImage_EditImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "Category order by categoryname").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ListItem li = new ListItem();
                    li.Text = HttpUtility.HtmlDecode(dt.Rows[i]["categoryname"].ToString().Replace("''","'"));
                    li.Value = dt.Rows[i]["id"].ToString();
                    ddlCategory.Items.Add(li);
                }
                //ddlCategory.DataSource = dt;
                //ddlCategory.DataTextField = "categoryname";
                //ddlCategory.DataValueField = "id";
                //ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));

            }
            //DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory order by subcategoryname").Tables[0];
            //if (dt1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt1.Rows.Count; i++)
            //    {

            //        ListItem li = new ListItem();
            //        li.Text = HttpUtility.HtmlDecode(dt1.Rows[i]["subcategoryname"].ToString());
            //        li.Value = dt1.Rows[i]["id"].ToString();
            //        ddlSubCategory.Items.Add(li);
            //    }
            //    //ddlSubCategory.DataSource = dt1;
            //    //ddlSubCategory.DataTextField = "subcategoryname";
            //    //ddlSubCategory.DataValueField = "id";
            //    //ddlSubCategory.DataBind();
            //    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
            //}
           
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
                                li.Text = HttpUtility.HtmlDecode(dt1.Rows[i]["subcategoryname"].ToString().Replace("''", "'"));
                                li.Value = dt1.Rows[i]["id"].ToString();
                                ddlSubCategory.Items.Add(li);
                            }
                        }
                        
                        ddlSubCategory.SelectedValue= dsservice.Tables[0].Rows[0]["subcategoryid"].ToString();
                    }
                    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                    //if (dsservice.Tables[0].Rows[0]["subcategoryid"].ToString()!="")
                    //{
                    //    ddlSubCategory.SelectedValue = dsservice.Tables[0].Rows[0]["subcategoryid"].ToString();
                    //    //ddlSubCategory.Items.FindByValue(dsservice.Tables[0].Rows[0]["subcategoryid"].ToString()).Selected = true;
                    //}
                    if (dsservice.Tables[0].Rows[0]["id"].ToString() != "" && dsservice.Tables[0].Rows[0]["id"] !=null)
                    {
                        DataTable dtProd =customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where categoryid=" + dsservice.Tables[0].Rows[0]["categoryid"].ToString() + " and subcategoryid is null order by productname").Tables[0];
                        if (dtProd.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtProd.Rows.Count; i++)
                            {

                                ListItem li = new ListItem();
                                li.Text = HttpUtility.HtmlDecode(dtProd.Rows[i]["productname"].ToString().Replace("''", "'"));
                                li.Value = dtProd.Rows[i]["id"].ToString();
                                ddlProduct.Items.Add(li);
                            }
                            ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                            ddlProduct.SelectedValue = dsservice.Tables[0].Rows[0]["id"].ToString();
                        }
                        else
                        {
                            DataTable dtProd1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where subcategoryid=" + dsservice.Tables[0].Rows[0]["subcategoryid"].ToString() + "  order by productname").Tables[0];
                            if (dtProd1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtProd1.Rows.Count; i++)
                                {

                                    ListItem li = new ListItem();
                                    li.Text = HttpUtility.HtmlDecode(dtProd1.Rows[i]["productname"].ToString().Replace("''", "'"));
                                    li.Value = dtProd1.Rows[i]["id"].ToString();
                                    ddlProduct.Items.Add(li);
                                }
                                ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                                ddlProduct.SelectedValue = dsservice.Tables[0].Rows[0]["id"].ToString();
                            }
                        }
                      
                    }

                   
                       // ddlProduct.Items.FindByValue(dsservice.Tables[0].Rows[0]["id"].ToString()).Selected = true;
                   // }
                    if (dsservice.Tables[0].Rows[0]["ProductImage"].ToString() != "" && dsservice.Tables[0].Rows[0]["ProductImage"] != null)
                        Image1.ImageUrl = "~/ProductImage/" + dsservice.Tables[0].Rows[0]["ProductImage"].ToString();
                    else
                        Image1.ImageUrl = "../../images/imafe-not-found.jpg";
                }
                
            }
            else if (Request.QueryString["edit"] != null && Request.QueryString["edit"].ToString() != "")
            {

                //btnupdate.Visible = false;
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_peptech/ManageImage/ImageList.aspx");
            }
           
        }

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "")
        {
            if (ddlCategory.SelectedIndex.ToString() != "0")
            {
                //Response.Write("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "'");
                DataTable dt1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "subcategory where categoryid='" + ddlCategory.SelectedValue.ToString() + "'").Tables[0];
                ddlSubCategory.Items.Clear();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dt1.Rows[i]["subcategoryname"].ToString());
                        li.Value = dt1.Rows[i]["id"].ToString();
                        ddlSubCategory.Items.Add(li);
                        ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                        //ddlProduct.Items.Clear();
                        // ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                    }
                }
                else
                {
                    ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                }
                //Response.Write("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid is null");
                //Response.End();
                DataTable dtprod = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid='0'").Tables[0];
                ddlProduct.Items.Clear();
                if (dtprod.Rows.Count > 0)
                {
                    for (int i = 0; i < dtprod.Rows.Count; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dtprod.Rows[i]["productname"].ToString().Replace("''", "'"));
                        li.Value = dtprod.Rows[i]["id"].ToString();
                        ddlProduct.Items.Add(li);
                        ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                    }
                }
                else
                {
                    ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                }
                //else
                //{
                //    ddlSubCategory.Items.Insert(0,new ListItem("<--Please Select-->",""));
                //    DataTable dtprod = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where categoryid='" + ddlCategory.SelectedValue.ToString() + "' and subcategoryid is null").Tables[0];
                //    ddlProduct.Items.Clear();
                //    if (dtprod.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dtprod.Rows.Count; i++)
                //        {
                //            ListItem li = new ListItem();
                //            li.Text = HttpUtility.HtmlDecode(dtprod.Rows[i]["productname"].ToString());
                //            li.Value = dtprod.Rows[i]["id"].ToString();
                //            ddlProduct.Items.Add(li);
                //            ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                //        }
                //    }
                //    else
                //    {
                //        ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
                //    }
                //}
                
               
            }
        }
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCategory.SelectedValue != "")
        {
            if (ddlSubCategory.SelectedIndex.ToString() != "0")
            {
                ddlProduct.Items.Clear();
                DataTable dtprod = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "product where subcategoryid='" + ddlSubCategory.SelectedValue.ToString() + "'").Tables[0];
                if (dtprod.Rows.Count > 0)
                {
                    for (int i = 0; i < dtprod.Rows.Count; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = HttpUtility.HtmlDecode(dtprod.Rows[i]["productname"].ToString().Replace("''", "'"));
                        li.Value = dtprod.Rows[i]["id"].ToString();
                        ddlProduct.Items.Add(li);
                    }
                   
                }
                ddlProduct.Items.Insert(0, new ListItem("<--Please Select-->", ""));
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
            //if (ddlSubCategory.SelectedValue.ToString() != "")
            //{
            //    strupdcat += "subcategoryid=" + ddlSubCategory.SelectedValue.ToString() + ",";
            //}
            strupdcat += " ProductName='" + ddlProduct.SelectedItem.Text.Replace("'", "''") + "'";
            if (fuPImage.HasFile)
            {
                if (chkfile(fuPImage.FileName))
                {
                    fuPImage.SaveAs(Server.MapPath("~/ProductImage/" + fuPImage.FileName));

                    //FileUpload1.SaveAs(Server.MapPath("~/UploadResume/" + filetxt));
                    //ImageHandler newImg = new ImageHandler(fuPImage.PostedFile, UploadFolder);
                    //bool uploadLrg = newImg.UploadImage();
                    //newFilename = newImg.NewFilename;
                    //strupdcat += ",ProductImage='" + newFilename.ToString() + "'";

                    strupdcat += ",ProductImage='" + fuPImage.FileName.ToString() + "'";



                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Files having extension .png ,.jpg ,.jpeg ,.gif are allowed";
                }
            }

            strupdcat += " where id=" + Request.QueryString["id"].ToString();
            //Response.Write(strupdcat);
            //Response.End();
            customUtility.ExecuteNonQuery(strupdcat);
            if (Request.QueryString["image"] != null && Request.QueryString["image"].ToString() != "")
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageImage/NoImageList.aspx?Upd=1");
            else
                Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageImage/ImageList.aspx?Upd=1");
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
        Response.Redirect(ConfigurationManager.AppSettings["WebSitePath"].ToString() + "Admin_Peptech/ManageImage/ImageList.aspx");
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
            //ddlSubCategory.DataSource = dt1;
            //ddlSubCategory.DataTextField = "subcategoryname";
            //ddlSubCategory.DataValueField = "id";
            //ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, new ListItem("<--Please Select-->", ""));
        }
        Image1.Visible = false;
    }
}
