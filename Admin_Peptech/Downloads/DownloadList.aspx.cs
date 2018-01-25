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

public partial class Admin_Peptech_Downloads_DownloadList : System.Web.UI.Page
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
        if (Request.QueryString["Upd"] != null)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Downloads Updated Successfully";
        }
        else if (Request.QueryString["add"] != null)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Downloads Added Successfully";
        }
        if (!IsPostBack)
        {
            // Bind();
        }

    }

    protected void active_Click(object sender, EventArgs e)
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
            setActive(hidAllValue, 1);
            lblmessage.Text = "Activate successfully.";

            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }

    }
    //Deactivating ListProperty
    protected void deactive_Click(object sender, EventArgs e)
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
            setActive(hidAllValue, 0);
            lblmessage.Text = "Deactivate successfully.";
            lblmessage.Visible = true;
        }
        catch (Exception e1)
        {
            lblmessage.Text = "Atleast one option must be checked";
            lblmessage.Visible = true;
        }

    }

    protected void setActive(string val, int status_value)
    {
        customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "Downloads set status = " + status_value + " where id in (" + val + ")");
        grdListProperty.DataBind();
    }

    protected void grdListProperty_OnRowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            Image img = (Image)e.Row.FindControl("ImageStatus");
            if (dv["status"].ToString().ToLower().Equals("true"))
            {
                img.ImageUrl = "../../images/status-on.gif";
            }
            else
            {
                img.ImageUrl = "../../images/status-off.gif";
            }
            LinkButton lbDownload = (LinkButton)e.Row.FindControl("lbDownload");

            string physicalPath = "";
            string imagepath = "~/downloads/" + dv["downloadfile"].ToString();
            int n1 = imagepath.LastIndexOf("/");
            string folderPath = "";
            string filename = "";
            if (n1 > 0)
            {
                folderPath = imagepath.Substring(0, n1);
                filename = imagepath.Substring(n1 + 1);
            }
            physicalPath = HttpContext.Current.Server.MapPath(folderPath);
            if (File.Exists(physicalPath + "\\" + filename))
            {
                lbDownload.Enabled = true;
            }
            else
            {
                lbDownload.Enabled = false;
                lbDownload.Attributes.Add("onclick", "javascript:alert('File not available');");
            }
        }
    }

    //Paging

    protected void grdListProperty_databound(object sender, EventArgs e)
    {
        if (grdListProperty.Rows.Count > 0)
        {
            rowcnt = int.Parse(grdListProperty.Rows.Count.ToString());
            Label lblpage = (Label)grdListProperty.BottomPagerRow.FindControl("lblpage");
            DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
            lblpage.Text = "Page : " + Convert.ToString(grdListProperty.PageIndex + 1);
            ddlpage.SelectedValue = grdListProperty.PageSize.ToString();
        }
    }
    protected void ddlpage_selected(object sender, EventArgs e)
    {
        DropDownList ddlpage = (DropDownList)grdListProperty.BottomPagerRow.FindControl("ddlpage");
        grdListProperty.PageSize = int.Parse(ddlpage.SelectedValue);
        grdListProperty.DataBind();
    }

    protected void grdListProperty_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin_Peptech/Downloads/Adddownload.aspx?add=1");
    }
    protected void grdListProperty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // Response.Write(e.NewPageIndex.ToString());

        if (e.NewPageIndex > 0)
        {
            grdListProperty.PageIndex = e.NewPageIndex;
            grdListProperty.DataBind();

        }
    }




    protected void grdListProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower() == "delete")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Record deleted successfully";
            grdListProperty.DataBind();
            //Bind();  
        }
        if (e.CommandName == "download")
        {
            Response.ContentType = "File";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + e.CommandArgument);
            Response.WriteFile(Server.MapPath("~/Downloads/" + e.CommandArgument));
        }
    }

    protected void imgUP_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton thisbutton = (ImageButton)sender;
        GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
        HiddenField hiddenorder = (HiddenField)(thisrow.FindControl("HiddenFieldOrder"));
        HiddenField hidID = (HiddenField)(thisrow.FindControl("hidID"));
        string thisorder = hiddenorder.Value;
        string thispart = thisrow.Cells[2].Text;
        Int32 thistype = Convert.ToInt32(hidID.Value);

        GridView thisgrid = (GridView)thisrow.Parent.Parent;
        int maxrows = thisgrid.Rows.Count;
        int row = thisrow.RowIndex;
        int previousrownumber = row - 1;
        if (previousrownumber >= 0)
        {
            GridViewRow previousrow = thisgrid.Rows[previousrownumber];
            hiddenorder = (HiddenField)(previousrow.FindControl("HiddenFieldOrder"));
            HiddenField previousid = (HiddenField)(previousrow.FindControl("hidiD"));
            string nextorder = hiddenorder.Value;
            Int32 nexttype = Convert.ToInt32(previousid.Value);
            string nextpart = previousrow.Cells[2].Text;
            if (thisorder != "")
            {
                try
                {
                    customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "downloads set preference=" + nextorder + " where id=" + thistype);
                    customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "downloads set preference=" + thisorder + " where id=" + nexttype);
                    grdListProperty.DataBind();
                }
                catch
                {
                }
            }
        }
    }
    protected void imgDown_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton thisbutton = (ImageButton)sender;
        GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
        HiddenField hiddenorder = (HiddenField)(thisrow.FindControl("HiddenFieldOrder"));
        HiddenField hidID = (HiddenField)(thisrow.FindControl("hidiD"));
        string thisorder = hiddenorder.Value;
        string thispart = thisrow.Cells[2].Text;
        Int32 thistype = Convert.ToInt32(hidID.Value);

        GridView thisgrid = (GridView)thisrow.Parent.Parent;
        int maxrows = thisgrid.Rows.Count;
        int row = thisrow.RowIndex;
        int previousrownumber = row + 1;

        if (previousrownumber < maxrows)
        {
            GridViewRow previousrow = thisgrid.Rows[previousrownumber];
            hiddenorder = (HiddenField)(previousrow.FindControl("HiddenFieldOrder"));
            HiddenField previousid = (HiddenField)(previousrow.FindControl("hidID"));
            string nextorder = hiddenorder.Value;
            Int32 nexttype = Convert.ToInt32(previousid.Value);
            string nextpart = previousrow.Cells[2].Text;
            if (thisorder != "")
            {
                try
                {
                    customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "downloads set preference=" + nextorder + " where id=" + thistype);
                    customUtility.ExecuteNonQuery("update " + customUtility.DBPrefix + "downloads set preference=" + thisorder + " where id=" + nexttype);
                    grdListProperty.DataBind();
                }
                catch
                {
                }
            }
        }
    }
}
