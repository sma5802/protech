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
using System.IO;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Attributes.Add("onkeyup", "ExecuteCall(this);");
        if(!IsPostBack)
        {           
            ////DataTable dtstate = customUtility.GetTableData("select * from "+customUtility.DBPrefix+"state where isactive=1 order by id,state asc").Tables[0];
            //if (dtstate.Rows.Count > 0)
            //{
            //    ddlState.DataSource = dtstate;
            //    ddlState.DataTextField = "state";
            //    ddlState.DataValueField = "id";
            //    ddlState.DataBind();
            //    ddlState.Items.Insert(0, new ListItem("Select State",""));
            //}
           // txtOther.Visible = false;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        int checkorder = 0;
        if (chkorderno.Checked)
            checkorder = 0;
        //if (Page.IsValid)
        //{
            if (customUtility.CheckDuplicateFieldValue(txtUsername.Text.Replace("'", "''").Trim(), "MemberList", "username", customUtility.CompareType.text, ""))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Username is already taken, please choose another one.";
            }
            else
            {
                if (customUtility.CheckDuplicateFieldValue(txtEmail.Text.Replace("'", "''").Trim(), "MemberList", "Email", customUtility.CompareType.text, ""))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Email ID already exist, please choose another email Id.";
                }
                else
                {
                    try
                    {
                        string SqlStr = "";
                        if (ddlCountry.SelectedValue == "254")
                        {

                            SqlStr = "insert into " + customUtility.DBPrefix + "memberlist (username,Password,FName,LName,Company,Email,Phone,Fax,BilStreet1,BilStreet2,BilCity,BilState,BilZip,BilCountry,status,registeredon,PurchaseOrderNo) ";
                            SqlStr += " values ('" + txtUsername.Text.Replace("'", "''") + "','" + customUtility.EncryptData(txtPassword.Text).Replace("'", "''") + "','" + txtFName.Text.Replace("'", "''") + "','" + txtLName.Text.Replace("'", "''") + "',";
                            SqlStr += " '" + txtCompany.Text.Replace("'", "''") + "','" + txtEmail.Text.Replace("'", "''") + "','" + txtPhone.Text.Replace("'", "''") + "','" + txtFax.Text.Replace("'", "''") + "','" + txtStreet1.Text.Replace("'", "''") + "',";
                            SqlStr += "'" + txtStreet2.Text.Replace("'", "''") + "','" + txtCity.Text.Replace("'", "''") + "','" + ddlState.SelectedValue + "','" + txtZip.Text.Replace("'", "''") + "','" + ddlCountry.SelectedValue + "',1,getdate()," + checkorder + ")";
                            //Int32 mainuserid = customUtility.AddToTableReturnID(SqlStr);
                        }
                        else
                        {
                            SqlStr = "insert into " + customUtility.DBPrefix + "memberlist (username,Password,FName,LName,Company,Email,Phone,Fax,BilStreet1,BilStreet2,BilCity,BilZip,BilCountry,bilother,status,registeredon,PurchaseOrderNo) ";
                            SqlStr += " values ('" + txtUsername.Text.Replace("'", "''") + "','" + customUtility.EncryptData(txtPassword.Text).Replace("'", "''") + "','" + txtFName.Text.Replace("'", "''") + "','" + txtLName.Text.Replace("'", "''") + "',";
                            SqlStr += " '" + txtCompany.Text.Replace("'", "''") + "','" + txtEmail.Text.Replace("'", "''") + "','" + txtPhone.Text.Replace("'", "''") + "','" + txtFax.Text.Replace("'", "''") + "','" + txtStreet1.Text.Replace("'", "''") + "',";
                            SqlStr += "'" + txtStreet2.Text.Replace("'", "''") + "','" + txtCity.Text.Replace("'", "''") + "','" + txtZip.Text.Replace("'", "''") + "','" + ddlCountry.SelectedValue + "','" + txtOther.Text.Replace("'", "''") + "',1,getdate()," + checkorder + ")";
                        }
                        //Response.Write(SqlStr);
                        //Response.End();
                        Int32 mainuserid = customUtility.AddToTableReturnID(SqlStr);
                        //sending mail
                        string subject = "";
                        string msg = "";
                        string adminmsg = "";
                        string SqlStrMail = "select * from " + customUtility.DBPrefix + "Messages where id=1";
                        DataSet dsMail = customUtility.GetTableData(SqlStrMail);
                        if (dsMail.Tables[0].Rows.Count > 0)
                        {
                            string SqlStrUser = "select * from " + customUtility.DBPrefix + "memberlist where email='"+ txtEmail.Text +"'";
                            DataSet dsUser = customUtility.GetTableData(SqlStrUser);
                            if (dsUser.Tables[0].Rows.Count > 0)
                            {
                                subject = dsMail.Tables[0].Rows[0]["subject"].ToString();
                                adminmsg = "<p>Dear&nbsp;&nbsp;Admin,</p><p>New User registered in peptechcorp.com.</p><p>User Information is given below.</p>" +
                                    "<p>FullName is ##FirstName##&nbsp;##LastName##</p><p>UserName is ##UserName##</p><p>Password is ##Password##</p><p><strong>Want to use purchase orders on peptechcorp.com - ##POrder##</strong></p>	<p><a href=\"http://www.peptechcorp.com/Admin_Peptech/RegisteredUser.aspx\">Click <u><u>here</u></u></a> to check more Info.</p>";

                                adminmsg = adminmsg.Replace("##FirstName##", txtFName.Text);
                                adminmsg = adminmsg.Replace("##LastName##", txtLName.Text);
                                adminmsg = adminmsg.Replace("##UserName##", txtUsername.Text);
                                adminmsg = adminmsg.Replace("##Password##", customUtility.DecryptData(dsUser.Tables[0].Rows[0]["Password"].ToString()));
                                if (chkorderno.Checked)
                                    adminmsg = adminmsg.Replace("##POrder##", "Yes");
                                else
                                    adminmsg = adminmsg.Replace("##POrder##", "No");

                                msg = dsMail.Tables[0].Rows[0]["text"].ToString();
                                msg = msg.Replace("##FirstName##", txtFName.Text);
                                msg = msg.Replace("##LastName##", txtLName.Text);
                                msg = msg.Replace("##UserName##", txtUsername.Text);
                                msg = msg.Replace("##Password##", customUtility.DecryptData(dsUser.Tables[0].Rows[0]["Password"].ToString()));
                                if(chkorderno.Checked)
                                    msg = msg.Replace("##POrder##", "Yes");
                                else
                                    msg = msg.Replace("##POrder##", "No");
                                customUtility.SendMailHtmlFromat("service@peptechcorp.com", txtEmail.Text, subject, msg);
                                customUtility.SendMailHtmlFromat("service@peptechcorp.com", "service@peptechcorp.com", "New User in Peptechcorp.com", adminmsg);
                            }
                        }
                        Response.Redirect("Thanks.aspx?fname=" + Server.UrlEncode(txtFName.Text.Trim()) + "&lname=" + Server.UrlEncode(txtLName.Text.Trim()));
                        if (Session["UserID"] != null)
                        {
                            Session["mainuserid"] = mainuserid;
                            Session["title"] = txtFName.Text.Replace("'", "''") + " " + txtLName.Text.Replace("'", "''");
                            if (Request.QueryString["requestpath"] != null && Request.QueryString["requestpath"].ToString() != "")
                                Response.Redirect(Request.QueryString["requestpath"].ToString());
                        }
                        else
                            Response.Redirect("Thanks.aspx?fname=" + Server.UrlEncode(txtFName.Text.Trim()) + "&lname=" + Server.UrlEncode(txtLName.Text.Trim()));
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = ex.Message;  //"There is some technical difficulty with the process , please try again";
                    }
                }
            }
        //}
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtRePassowrd.Text = "";
        txtFName.Text = "";
        txtLName.Text = "";
        txtCompany.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtStreet1.Text = "";
        txtCity.Text = "";
        txtZip.Text = "";
    }

    protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlState.SelectedItem.Text == "Others")
        {
           // txtOther.Visible = true;
        }
        else
        {
          //  txtOther.Visible = false;
        }
    }

    protected void ddlCountry_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue == "0")
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
        }
        else if (ddlCountry.SelectedValue == "254")
        {
            string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + ddlCountry.SelectedValue;
            DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
            if (dtState.Rows.Count > 0)
            {
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, new ListItem("Select State", "0"));
                ddlState.DataSource = dtState;
                ddlState.DataTextField = "state";
                ddlState.DataValueField = "id";
                ddlState.DataBind();
                txtOther.Visible = false;
            }
            ddlState.Visible = true;
        }
        else
        {
            ddlState.Visible = false;
            txtOther.Visible = true;
        }
    }
}
