using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using UserClass;

public partial class MenuControl2l : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ab = ConfigurationManager.AppSettings["websitepath1"].ToString();
        string main = "";
        string cat = "";
        string subcat = "";
        main = "<ul id='qm0' class='qmmc'>" +
               "<li><a href='" + ab + "default.aspx'><img border='0' onmouseout='javascript:removehome();' onmouseover='javascript:fillhome();' name='homepage' src='" + ab + "images/home.gif'/></a></li>" +
               "<li><a href='" + ab + "company.aspx'><img border='0' onmouseout='javascript:removecompany();' onmouseover='javascript:fillcompany();' name='company' src='" + ab + "images/company.gif'/></a></li>" +
               "<li><a href='" + ab + "Content.aspx?page=Services'><img border='0' onmouseout='javascript:removeservices();' onmouseover='javascript:fillservices();' name='services' src='" + ab + "images/services.gif'/></a></li>" +
               "<li><a class='qmparent' href='" + ab + "Categories.aspx'><img border='0' onmouseout='javascript:removeproducts();' onmouseover='javascript:fillproducts();' name='products' src='" + ab + "images/products.gif'/></a><ul id='qm1'>";
        DataTable dtcat = customUtility.GetTableData("select id,categoryname from " + customUtility.DBPrefix + "category where status=1 order by categoryname").Tables[0];
        if (dtcat.Rows.Count > 0)
        {
            for (int i = 0; i < dtcat.Rows.Count; i++)
            {
                DataTable dtsubcat = customUtility.GetTableData("select id,categoryid,subcategoryname from " + customUtility.DBPrefix + "subcategory where categoryid=" + dtcat.Rows[i]["id"] + " and status=1 order by subcategoryname").Tables[0];
                if (dtsubcat.Rows.Count > 0)
                {
                    main += "<li ><a class='qmparent' href='" + ab + "subcategories.aspx?pid=" + dtcat.Rows[i]["id"] + "'>" + dtcat.Rows[i]["categoryname"].ToString() + "</a><ul>";
                    for (int j = 0; j < dtsubcat.Rows.Count; j++)
                    {
                        main += "<li><a href='" + ab + "ProductLists.aspx?id=" + dtsubcat.Rows[j]["id"] + "'>" + dtsubcat.Rows[j]["subcategoryname"].ToString() + "</a></li>";
                    }
                    main += "</ul></li>";
                }
                else
                {
                    main += "<li><a href='" + ab + "subcategories.aspx?pid=" + dtcat.Rows[i]["id"] + "'>" + dtcat.Rows[i]["categoryname"].ToString() + "</a></li>";
                }
            }
        }
        main += "</ul></li>" +
               "<li><a href='" + ab + "operations.aspx'><img border='0' onmouseout='javascript:removeoperations();' onmouseover='javascript:filloperations();' name='operations' src='" + ab + "images/operations.gif'/></a></li>" +
               "<li><a href='" + ab + "Content.aspx?page=Case+Studies'><img border='0' onmouseout='javascript:removestudies();' onmouseover='javascript:fillstudies();' name='studies' src='" + ab + "images/case-studies.gif'/></a></li>" +
               "</ul>";
        lblul.Text = HttpUtility.HtmlDecode(main);
    }
}
