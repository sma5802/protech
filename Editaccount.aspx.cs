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

public partial class Editaccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["mainuserid"] == null && Session["mainuserid"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["WebsitePath"].ToString() + "Login.aspx?requestpath=" + Request.Url.ToString());


        if (!IsPostBack)
        {
            string strsqluser = "select * from " + customUtility.DBPrefix + "MemberList where id =" + Session["mainuserid"].ToString();
            DataSet dsuser = customUtility.GetTableData(strsqluser);
            if (dsuser.Tables[0].Rows.Count > 0)
            {
                txtFName.Text = dsuser.Tables[0].Rows[0]["fname"].ToString();
                txtLName.Text = dsuser.Tables[0].Rows[0]["lname"].ToString();
                txtCompany.Text = dsuser.Tables[0].Rows[0]["company"].ToString();
                txtFax.Text = dsuser.Tables[0].Rows[0]["fax"].ToString();
                txtPhone.Text = dsuser.Tables[0].Rows[0]["phone"].ToString();
                txtZip.Text = dsuser.Tables[0].Rows[0]["BilZip"].ToString();
                txtStreet1.Text = dsuser.Tables[0].Rows[0]["BilStreet1"].ToString();
                txtStreet2.Text = dsuser.Tables[0].Rows[0]["BilStreet2"].ToString();
                txtEmail.Text = dsuser.Tables[0].Rows[0]["email"].ToString();
                txtCity.Text = dsuser.Tables[0].Rows[0]["BilCity"].ToString();
                ddlCountry.SelectedValue = dsuser.Tables[0].Rows[0]["BilCountry"].ToString();
                if (dsuser.Tables[0].Rows[0]["BilCountry"].ToString().Equals("254"))
                {
                    ddlCountry.DataBind();
                    ddlState.DataBind();
                    ddlState.SelectedValue = dsuser.Tables[0].Rows[0]["BilState"].ToString();

                    //string SqlStrState = "select * from " + customUtility.DBPrefix + "state where isactive=1 and countryid= " + dsuser.Tables[0].Rows[0]["BilCountry"].ToString();
                    //DataTable dtState = customUtility.GetTableData(SqlStrState).Tables[0];
                    //if (dtState.Rows.Count > 0)
                    //{
                    //    ddlState.Visible = true;
                    //    ddlState.Items.Clear();
                    //    ddlState.Items.Insert(0, new ListItem("Select State", "0"));
                    //    ddlState.DataSource = dtState;

                    //    ddlState.DataBind();
                    //    //txtOther.Visible = false;

                    //}
                    //ddlState.SelectedValue = dsuser.Tables[0].Rows[0]["BilState"].ToString();
                }
                else
                {
                    //ddlState.Visible = false;
                    //txtOther.Visible = true;
                    txtOther.Text = dsuser.Tables[0].Rows[0]["BilOther"].ToString().Replace("'", "''");
                }
            }













            //DataTable dtstate = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "state where isactive=1 order by id,state asc").Tables[0];
            //if (dtstate.Rows.Count > 0)
            //{
            //    ddlState.DataSource = dtstate;
            //    ddlState.DataTextField = "state";
            //    ddlState.DataValueField = "id";
            //    ddlState.DataBind();
            //    ddlState.Items.Insert(0, new ListItem("Select State", ""));
            //}
            ////txtOther.Visible = false;
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (Session["mainuserid"] != null)
        {
            if (customUtility.CheckDuplicateFieldValue(txtEmail.Text.Replace("'", "''").Trim(), "MemberList", "Email", customUtility.CompareType.text, " and id<>" + Session["mainuserid"].ToString()))
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Email ID already exist, please choose another email Id.";
            }
            else
            {
                try
                {
                    string SqlStr = "";
                    if (ddlCountry.SelectedValue == "254")
                    {
                        SqlStr = "update " + customUtility.DBPrefix + "memberlist set FName='" + txtFName.Text.Replace("'", "''") + "',LName='" + txtLName.Text.Replace("'", "''") + "',";
                        SqlStr += " Company='" + txtCompany.Text.Replace("'", "''") + "',Email='" + txtEmail.Text.Replace("'", "''") + "',Phone='" + txtPhone.Text.Replace("'", "''") + "',";
                        SqlStr += " Fax='" + txtFax.Text.Replace("'", "''") + "',BilStreet1='" + txtStreet1.Text.Replace("'", "''") + "',BilStreet2='" + txtStreet2.Text.Replace("'", "''") + "',";
                        SqlStr += " BilCity='" + txtCity.Text.Replace("'", "''") + "',BilState=" + ddlState.SelectedValue + ",bilother='',BilZip='" + txtZip.Text.Replace("'", "''") + "',";
                        SqlStr += " BilCountry='" + ddlCountry.SelectedValue + "' where id=" + Session["mainuserid"].ToString();
                        customUtility.ExecuteNonQuery(SqlStr);
                    }
                    else
                    {
                        SqlStr = "update " + customUtility.DBPrefix + "memberlist set FName='" + txtFName.Text.Replace("'", "''") + "',LName='" + txtLName.Text.Replace("'", "''") + "',";
                        SqlStr += " Company='" + txtCompany.Text.Replace("'", "''") + "',Email='" + txtEmail.Text.Replace("'", "''") + "',Phone='" + txtPhone.Text.Replace("'", "''") + "',";
                        SqlStr += " Fax='" + txtFax.Text.Replace("'", "''") + "',BilStreet1='" + txtStreet1.Text.Replace("'", "''") + "',BilStreet2='" + txtStreet2.Text.Replace("'", "''") + "',";
                        SqlStr += " BilCity='" + txtCity.Text.Replace("'", "''") + "',BilZip='" + txtZip.Text.Replace("'", "''") + "',";
                        SqlStr += " BilCountry='" + ddlCountry.SelectedValue + "',BilState=null,bilother='" + txtOther.Text.Replace("'", "''") + "' where id=" + Session["mainuserid"].ToString();
                        customUtility.ExecuteNonQuery(SqlStr);

                        Session["title"] = txtFName.Text + "&nbsp;" + txtLName.Text;
                        ((Label)Page.Master.FindControl("lblUser")).Text = Session["title"].ToString();
                    }
                    //lblmsg.Visible = true;
                    //lblmsg.Text = "Updated Successfully";
                    Response.Redirect("UserAccount.aspx?upd=1");
                }
                catch (Exception ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = ex.Message;  //"There is some technical difficulty with the process , please try again";
                }
            }
        }
    }

    protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlState.SelectedItem.Text == "Others")
        {
            txtOther.Visible = true;
        }
        else
        {
            txtOther.Visible = false;
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue == "254")
        {
            ddlState.Visible = true;
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, new ListItem("Select State", "0"));
            ddlState.DataBind();
            txtOther.Visible = false;
        }
        else
        {
            ddlState.Visible = false;
            txtOther.Visible = true;
        }
    }
}
