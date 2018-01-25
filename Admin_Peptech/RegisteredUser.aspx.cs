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

public partial class Admin_Peptech_RegisteredUser : System.Web.UI.Page
{
    private CheckBox cb;
    private HiddenField hddid;
    private DataSet ds;
    private int hddval;
    private string hidAllValue = "";
    private int rowcnt = 0;
    private int rowindex;
    private int order;
    private int id;
    private string cat;
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = customUtility.GetTableData("select * from " + customUtility.DBPrefix + "userregis order by DateOFMember").Tables[0];
        //if (dt.Rows.Count > 0)
        //{
        //    grdListProperty.DataSource = dt;
        //    grdListProperty.DataBind();
        //}
    }
    protected void grdListProperty_databound(object sender, EventArgs e)
    {
        if (grdListProperty.Rows.Count > 0)
        {
            rowcnt = int.Parse(grdListProperty.Rows.Count.ToString());
            Label lblpage = (Label)grdListProperty.BottomPagerRow.FindControl("lblpage");
            DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
            lblpage.Text = "Page : " + Convert.ToString(grdListProperty.PageIndex + 1) + " of " + grdListProperty.PageCount.ToString();
            
            
            ddlpage.SelectedValue = grdListProperty.PageSize.ToString();
        }

    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdListProperty.PageIndex = e.NewPageIndex;
        grdListProperty.DataBind();
    }
    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower() == "del")
        {
            bool del = customUtility.ExecuteNonQuery("delete " + customUtility.DBPrefix + "memberlist where id=" + e.CommandArgument.ToString());
            if (del.ToString().ToLower().Equals("true"))
            {
                lblmessage.Visible = true;
                lblmessage.Text = "User(s) Deleted Successfully";
                grdListProperty.DataBind();
            }
        }
    }
    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            Label lblname14 = (Label)e.Row.FindControl("lblname14");
            CheckBox statusCheck =(CheckBox)e.Row.FindControl("statusCheck"); 
            //if (dv["PurchaseOrderNo"].ToString().ToLower().Equals("true"))
            //    lblname14.Text = "Yes";
            //else
            //{
            //    lblname14.Text = "No";
            //}
            //Label lblname34 = (Label)e.Row.FindControl("lblname34");
            //if (dv["status"].ToString().ToLower().Equals("true"))
            //    lblname34.Text = "Yes";
            //else
            //    lblname34.Text = "No";
        }
    }
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
        grdListProperty.PageSize = int.Parse(ddlpage.SelectedValue);
        grdListProperty.DataBind();
    }

    //protected void active_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
    //    {
    //        cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
    //        hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

    //        hddval = Convert.ToInt32(hddid.Value);

    //        if (cb.Checked)
    //        {
    //            hidAllValue = hidAllValue + hddval + ",";
    //        }
    //    }
    //    try
    //    {
    //        hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
    //        setActive(hidAllValue, 1);
    //        lblmessage.Text = "User(s) Activated successfully.";

    //        lblmessage.Visible = true;
    //    }
    //    catch (Exception e1)
    //    {
    //        lblmessage.Text = "Atleast one option must be checked";
    //        lblmessage.Visible = true;
    //    }

    //}
    //Deactivating ListProperty
    //protected void deactive_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
    //    {
    //        cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
    //        hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

    //        hddval = Convert.ToInt32(hddid.Value);

    //        if (cb.Checked)
    //        {
    //            hidAllValue = hidAllValue + hddval + ",";
    //        }
    //    }
    //    try
    //    {
    //        hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
    //        setActive(hidAllValue, 0);
    //        lblmessage.Text = "User(s) Deactivated successfully.";
    //        lblmessage.Visible = true;
    //    }
    //    catch (Exception e1)
    //    {
    //        lblmessage.Text = "Atleast one option must be checked";
    //        lblmessage.Visible = true;
    //    }
    //}

    //protected void setActive(string val, int status_value)
    //{
    //    customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Memberlist set status = " + status_value + " where id in (" + val + ")");
    //    grdListProperty.DataBind();
    //}
    protected void btnAPO_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            setPO(hidAllValue, 1);
            lblmessage.Text = "Purchase Order Activated successfully for selected User(s).";

            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }
    }
    protected void btnDPO_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= grdListProperty.Rows.Count - 1; i++)
        {
            cb = (CheckBox)grdListProperty.Rows[i].FindControl("statusCheck");
            hddid = (HiddenField)grdListProperty.Rows[i].FindControl("checkid");

            hddval = Convert.ToInt32(hddid.Value);

            if (cb.Checked)
            {
                hidAllValue = hidAllValue + hddval + ",";
            }
        }
        try
        {
            hidAllValue = hidAllValue.Substring(0, hidAllValue.Length - 1);
            setPO(hidAllValue, 0);
            lblmessage.Text = "Purchase Order Deactivated successfully for selected User(s).";

            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }
    }
    protected void setPO(string val, int status_value)
    {
        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Memberlist set purchaseOrderNo = " + status_value + " where id in (" + val + ")");
        grdListProperty.DataBind();
    }
}
