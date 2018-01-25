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

public partial class menucontrol : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string main = "";
        main = "<ul id='qm0' class='qmmc'>" +
               "<li><a href='../default.aspx'><img border='0' onmouseout='javascript:removehome();' onmouseover='javascript:fillhome();' name='homepage' src='images/home.gif'/></a></li>" +
               "<li><a href='../company.aspx'><img border='0' onmouseout='javascript:removecompany();' onmouseover='javascript:fillcompany();' name='company' src='images/company.gif'/></a></li>" +
               "<li><a href='../Contents.aspx?page=Services'><img border='0' onmouseout='javascript:removeservices();' onmouseover='javascript:fillservices();' name='services' src='images/services.gif'/></a></li>" +
               "<li><a class='qmparent' href='../Categories.aspx'><img border='0' onmouseout='javascript:removeproducts();' onmouseover='javascript:fillproducts();' name='products' src='images/products.gif'/></a></li>" +
               "<li><a href='../operations.aspx'><img border='0' onmouseout='javascript:removeoperations();' onmouseover='javascript:filloperations();' name='operations' src='images/operations.gif'/></a></li>" +
               "<li><a href='../Contents.aspx?page=Case+Studies'><img border='0' onmouseout='javascript:removestudies();' onmouseover='javascript:fillstudies();' name='studies' src='images/case-studies.gif'/></a></li>" +
               "</ul>";
        lblul.Text = HttpUtility.HtmlDecode(main);
    }
}