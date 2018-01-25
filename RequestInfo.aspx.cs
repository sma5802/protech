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
using UserClass;

public partial class RequestInfo : System.Web.UI.Page
{
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtComments.Attributes.Add("onkeyup", "showchars();");
        if (!IsPostBack)
        {
            Session["CaptchaImageText"] = customUtility.getRandomAlphaNumeric();
            lnkquotes.CssClass = "blueheading";

            DataTable dtRequestInfo1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "edit where pagename='Quote Request' and home=0").Tables[0];
            if (dtRequestInfo1.Rows.Count > 0)
            {
                lblTitle.Text = "Quote Request";
                lblContent.Text = HttpUtility.HtmlDecode(dtRequestInfo1.Rows[0]["pagedata"].ToString().Replace("''", "'"));
            }
        }
    }
    protected void ImgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["CaptchaImageText"] != null)
        {
            if (Session["CaptchaImageText"].ToString().Trim() != txtAccessCode.Text.Trim())
            {
                lblMes.Text = "You have entered wrong access code - please try again <br />";
                lblMes.Visible = true;
                txtAccessCode.Text = "";
                txtAccessCode.Focus();
            }
            else
            {
                if (ddlcountry.SelectedValue == "254")
                {
                    string strRequestinfo = "insert into " + customUtility.DBPrefix + "RequestInfo(FName,LName,Company,Email,Telephone,Fax,street1,street2,City,state,Zip,Country,Comments,posteddate,Enquirytype,productname,catalognumber,casnumber,quantity,purity,dateneed) values('" + txtFName.Text.Replace("'", "''") + "','" + txtLName.Text.Replace("'", "''") + "','" + txtCompany.Text.Replace("'", "''") + "','" + txtEmail.Text.Replace("'", "''") + "','" + txtTelephone.Text.Replace("'", "''") + "',";
                    strRequestinfo += "'" + txtFax.Text.Replace("'", "''") + "','" + txtStreet1.Text.Replace("'", "''") + "','" + txtStreet2.Text.Replace("'", "''") + "','" + txtCity.Text.Replace("'", "''") + "'," + ddlstate.SelectedValue.ToString() + ",'" + txtZip.Text.Replace("'", "''") + "','" + ddlcountry.SelectedValue.ToString() + "','" + txtComments.Text.Replace("'", "''") + "',getdate(),'"+ lblTitle.Text +"',";
                    strRequestinfo += "'" + txtproduct.Text.Replace("'", "''") + "','" + txtcatalognumber.Text.Replace("'", "''") + "','" + txtcas.Text.Replace("'", "''") + "','" + txtquantity.Text.Replace("'", "''") + "','" + txtpurity.Text.Replace("'", "''") + "','" + txtdate.Text.Replace("'", "''") + "')";
                    customUtility.ExecuteNonQuery(strRequestinfo);
                }
                else
                {
                    string strRequestinfo = "insert into " + customUtility.DBPrefix + "RequestInfo(FName,LName,Company,Email,Telephone,Fax,street1,street2,City,Other,Zip,Country,Comments,posteddate,Enquirytype,productname,catalognumber,casnumber,quantity,purity,dateneed) values('" + txtFName.Text.Replace("'", "''") + "','" + txtLName.Text.Replace("'", "''") + "','" + txtCompany.Text.Replace("'", "''") + "','" + txtEmail.Text.Replace("'", "''") + "','" + txtTelephone.Text.Replace("'", "''") + "',";
                    strRequestinfo += "'" + txtFax.Text.Replace("'", "''") + "','" + txtStreet1.Text.Replace("'", "''") + "','" + txtStreet2.Text.Replace("'", "''") + "','" + txtCity.Text.Replace("'", "''") + "','" + txtOther.Text.Replace("'", "''") + "','" + txtZip.Text.Replace("'", "''") + "','" + ddlcountry.SelectedValue.ToString() + "','" + txtComments.Text.Replace("'", "''") + "',getdate(),'" + lblTitle.Text + "',";
                    strRequestinfo += "'" + txtproduct.Text.Replace("'", "''") + "','" + txtcatalognumber.Text.Replace("'", "''") + "','" + txtcas.Text.Replace("'", "''") + "','" + txtquantity.Text.Replace("'", "''") + "','" + txtpurity.Text.Replace("'", "''") + "','" + txtdate.Text.Replace("'", "''") + "')";
                    customUtility.ExecuteNonQuery(strRequestinfo);
                }
                string sto, sfrom, ssubject;
                string msg = "";
                string SqlStrMail = "select * from " + customUtility.DBPrefix + "Messages where id=3";
                DataSet dsMail = customUtility.GetTableData(SqlStrMail);
                if (dsMail.Tables[0].Rows.Count > 0)
                {
                    msg = HttpUtility.HtmlDecode(dsMail.Tables[0].Rows[0]["text"].ToString());
                    msg = msg.Replace("##FirstName##", txtFName.Text);
                    msg = msg.Replace("##LastName##", txtLName.Text);
                    msg = msg.Replace("##Name##", txtFName.Text + ' ' + txtLName.Text);
                    msg = msg.Replace("##Email##", txtEmail.Text);
                    msg = msg.Replace("##Telephone##", txtTelephone.Text);
                    msg = msg.Replace("##Fax##", txtFax.Text);
                    msg = msg.Replace("##Company##", txtCompany.Text);
                    msg = msg.Replace("##Street1##", txtStreet1.Text);
                    msg = msg.Replace("##Street2##", txtStreet2.Text);
                    msg = msg.Replace("##City##", txtCity.Text);
                    if(ddlcountry.SelectedValue=="254")
                        msg = msg.Replace("##State##", ddlstate.SelectedItem.Text);
                    else
                        msg = msg.Replace("##State##", txtOther.Text);
                    msg = msg.Replace("##Zip##", txtZip.Text);
                    msg = msg.Replace("##Country##", ddlcountry.SelectedItem.Text);
                    string proddetail = "<strong>Product Information</Strong>" +
                        "<p>Product Name=" + txtproduct.Text + "</p>" +
                        "<p>Catalog Number=" + txtcatalognumber.Text + "</p>" +
                        "<p>CAS Number=" + txtcas.Text + "</p>" +
                        "<p>Quantity=" + txtquantity.Text + "</p>" +
                        "<p>Purity=" + txtpurity.Text + "</p>";
                    msg = msg.Replace("##Product##", proddetail);
                    msg = msg.Replace("##Comments Text##", txtComments.Text);
                    sto = "service@peptechcorp.com";
                    sfrom = txtEmail.Text;
                    ssubject = dsMail.Tables[0].Rows[0]["subject"].ToString();
                    customUtility.SendMailHtmlFromat(sfrom, sto, ssubject, msg);
                }

                txtFName.Text = "";
                txtLName.Text = "";
                txtCompany.Text = "";
                txtEmail.Text = "";
                txtTelephone.Text = "";
                txtFax.Text = "";
                txtStreet1.Text = "";
                txtStreet2.Text = "";
                txtCity.Text = "";
                txtOther.Text = "";
                txtComments.Text = "";
                txtZip.Text = "";
                txtAccessCode.Text = "";
                txtproduct.Text = "";
                txtpurity.Text = "";
                txtquantity.Text = "";
                txtdate.Text = "";
                txtcas.Text = "";
                txtcatalognumber.Text = "";
                ddlcountry.SelectedValue = "0";
                ddlstate.SelectedValue = "0";
                lblMsg.Visible = true;
                lblMsg.Text = "<br>Your Request Information has been successfully submitted.We Will Contact You Soon.<br>";
                Session["CaptchaImageText"] = customUtility.getRandomAlphaNumeric();
            }
        }
        else
        {
            lblMsg.Text = "<br>Due to inactivity your session has been expired - please try again <br />";
            lblMsg.Visible = true;
            Session["CaptchaImageText"] = customUtility.getRandomAlphaNumeric();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
  
    }
    protected void lnkquotes_Click(object sender, EventArgs e)
    {
        lblTitle.Text = "Quotes Request";
        lnkquotes.CssClass = "blueheading";
        lnkcatlog.CssClass = "blueheading1";
        i = 0;
        trproduct.Visible = true;
        trcatalog.Visible = true;
        trcas.Visible = true;
        trquantity.Visible = true;
        trpurity.Visible = true;
        trdate.Visible = true;

        DataTable dtRequestInfo1 = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "edit where pagename='Quote Request' and home=0").Tables[0];
        if (dtRequestInfo1.Rows.Count > 0)
        {
            lblContent.Text = HttpUtility.HtmlDecode(dtRequestInfo1.Rows[0]["pagedata"].ToString().Replace("''", "'"));
        }
    }
    protected void lnkcatlog_Click(object sender, EventArgs e)
    {
        lnkquotes.CssClass = "blueheading1";
        lnkcatlog.CssClass = "blueheading";
        lblTitle.Text = "Catalog Request";
        i = 1;

        trproduct.Visible = false;
        trcatalog.Visible = false;
        trcas.Visible = false;
        trquantity.Visible = false;
        trpurity.Visible = false;
        trdate.Visible = false;

        DataTable dtRequestInfo = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "edit where pagename='Catalog Request' and home=0").Tables[0];
        if (dtRequestInfo.Rows.Count > 0)
        {
            lblContent.Text = HttpUtility.HtmlDecode(dtRequestInfo.Rows[0]["pagedata"].ToString().Replace("''", "'"));
        }
    }
  
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcountry.SelectedValue != "254")
        {
            reqstate.ControlToValidate = "txtOther";
            reqstate.InitialValue = "";
            reqstate.ErrorMessage = "Please Enter State Name";
            txtOther.Text = "";
            ddlstate.Visible = true;
            txtOther.Visible = true;
            ddlstate.Visible = false;
        }
        else
        {
            ddlstate.Visible = true;
            txtOther.Visible = false;
            reqstate.ControlToValidate = "ddlstate";
            reqstate.InitialValue = "0";
            reqstate.ErrorMessage = "Please Select State ";
            string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + ddlcountry.SelectedValue;
            DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
            if (dtState.Rows.Count > 0)
            {
                ddlstate.Items.Clear();
                ddlstate.Items.Insert(0, new ListItem("Select State","0"));
                ddlstate.DataSource = dtState;
                ddlstate.DataTextField = "state";
                ddlstate.DataValueField = "id";
                ddlstate.DataBind();
            }
        }
    }
}
