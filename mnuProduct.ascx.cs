using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UserClass;

namespace Peptech
{
    public partial class mnuProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dtcat = customUtility.GetTableData("select id,categoryname from " + customUtility.DBPrefix + "category where status=1 order by categoryname").Tables[0];
                string main = "";
                main = "<div class='main'><ul>";
                
                if (dtcat.Rows.Count > 0)
                {
                    for (int i = 0; i < dtcat.Rows.Count; i++)
                    {
                        DataTable dtsubcat = customUtility.GetTableData("select id,categoryid,subcategoryname from " + customUtility.DBPrefix + "subcategory where categoryid=" + dtcat.Rows[i]["id"] + " and status=1 order by subcategoryname").Tables[0];
                        if (dtsubcat.Rows.Count > 0)
                        {
                            main += "<li><a href='subcategories.aspx?pid=" + dtcat.Rows[i]["id"] + "'>" + dtcat.Rows[i]["categoryname"].ToString() + "</a><div class='sub'><ul >";
                            for (int j = 0; j < dtsubcat.Rows.Count; j++)
                            {
                                main += "<li><a href='ProductLists.aspx?id=" + dtsubcat.Rows[j]["id"] + "'>" + dtsubcat.Rows[j]["subcategoryname"].ToString() + "</a></li>";
                            }
                            main += "</ul></div></li>";
                        }
                        else
                        {
                            main += "<li><a href='subcategories.aspx?pid=" + dtcat.Rows[i]["id"] + "'>" + dtcat.Rows[i]["categoryname"].ToString() + "</a></li>";
                        }
                    }
                }
                main += "</ul></div>";
                lblul.Text = HttpUtility.HtmlDecode(main);
            }
        }
    }
}