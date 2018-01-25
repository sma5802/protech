<%@ Page Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" Inherits="Categories" Codebehind="Categories.aspx.cs" %>
<%@ Register TagPrefix="uc2" TagName="prdList" Src="~/mnuProduct.ascx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript" src="jsme/jsme.nocache.js"></script>
<script type="text/javascript" language="javascript" src='jsme/jquery-2.0.3.min.js'></script> 
<script type="text/javascript" language="javascript">
    //this function will be called after the JavaScriptApplet code has been loaded.
    var IDList = '';
    function jsmeOnLoad() {
        document.ChemMol_Structure = new JSApplet.JSME("ChemMol_Structure", "600px", "340px", { "options": "noquery,nohydrogens,polarnitro,nocanonize,oldlook" });
        var jme = getCookie("jme");
        var mol = getCookie("mol");
        var smiles = getCookie("smiles");
        var exact = getCookie("exact");
        if (jme != null && jme != "") {
            document.ChemMol_Structure.readMolecule(jme);
        }
        if (mol != null && mol != "") {
            document.ChemMol_Structure.readMolFile(mol);
        }
        var drawing = document.ChemMol_Structure.smiles();
        if (drawing != null && drawing != "" && exact != null && exact != "") {
            submitChemMolClientForm(exact);
        }
    }
    function submitChemMolClientForm(exacts) {
        var mol = document.ChemMol_Structure.molFile();
        var smiles = document.ChemMol_Structure.smiles();
        var jme = document.ChemMol_Structure.jmeFile();
        if (smiles.length < 1) {
            alert("No molecule!");
        }
        else {
            $.ajax({
                type: "post",
                url: "https://chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/server/server.php",
                data: { a: 'n', jme: jme, mol: mol, smiles: smiles, ifexact: exacts },
                success: function (result) {
                    $('#myresults').show()
                    document.getElementById('myresults').scrollIntoView();
                    //$('#myresults').ScrollTo();

                    var all_results = JSON.parse(result);
                    IDList = all_results.listid;
                    //alert(IDList);
                    var presults = all_results.first_page;

                    if (presults.totalcompounds < 1)
                        alert("No structures found!")

                    var myr = " <div align=left> Total " + presults.totalcompounds + " product(s) found 1  / " + presults.totalpages + " page(s)</div> ";

                    // pagenation :
                    myr += "<div align=right>";
                    myr += "<a onclick=\"gotopage(2)\">";
                    myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/3.gif\" border=0></a>";
                    myr += "<a onclick=\"gotopage(" + presults.totalpages + ")\">";
                    myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/4.gif\" border=0></a>";

                    myr += "</div>";

                    myr += "<table width=\"100%\" border=\"1\" cellspacing=\"0\" class=\"prod-list-table\" cellpadding=\"0\">	";


                    $.each(presults.result_list, function (icount, compounds) {
                        if (icount % 2)
                            linecolor = 'gray'
                        else
                            linecolor = 'white'

                        myr += "<tr>"
                        myr += "  <td class=\"" + linecolor + "\" rowspan=\"2\" valign=\"middle\" align=\"center\" style=\"vertical-align:middle;\">";
                        myr += "<a href=\"product.aspx?CNO=" + compounds.Catalog_No + "\">";
                        myr += "<img class=\"pi\" src=\"ProductImage/" + compounds.Catalog_No + "-160.png\"  /></a></td>";
                        myr += "  <td class=\"" + linecolor + "\" colspan=\"3\" valign=\"top\" align=\"left\">";
                        myr += "<a  class=\"pa\" href=\"product.aspx?CNO=" + compounds.Catalog_No + "\"> " + compounds.CName + "   </a></td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Part # " + compounds.Catalog_No + "  </td>";
                        myr += " </tr>";
                        myr += "<tr>"
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">MDL # " + compounds.MDL_NUMBER + "</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">CAS # <br/>" + compounds.CAS;
                        myr += "	</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Formula " + compounds.Formula + "</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Mol. Wt. " + compounds.MolWeight + "</td>";
                        myr += " </tr>";
                        //  myr += "<td class=\"ari-tbl-col-0\"><img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/cmshowimage.php?id=" + compounds.mol_id + "\" /> </td>";
                    });

                    myr += "</table>";

                    $('#myresults').html(myr)
                    setCookie("jme", jme, 1);
                    setCookie("mol", mol, 1);
                    setCookie("smiles", smiles, 1);
                    setCookie("exact", exacts, 1);
                },

                error: function (jqXHR, status) {
                    alert('error')
                }
            });
        }
    }

    function gotopage(pageno) {
        $.ajax({
            type: "post",
            url: "https://chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/server/server.php",
            data: { a: 'o', p: pageno, ids: IDList },
            dataType: "json",
            success: function (result) {
                $('#myresults').show()

                var presults = result;

                if (presults.totalcompounds < 1)
                    alert("No structures found!")

                var myr = " <div align=left> Total " + presults.totalcompounds + " product(s) found." + pageno + " / " + presults.totalpages + " page(s)</div> <br/>";

                // pagenation : 
                if (pageno > 1) {
                    prepage = pageno - 1;
                }
                else {
                    prepage = pageno;
                }

                if (pageno < presults.totalpages) {
                    nextpage = pageno + 1;
                }
                else {
                    nextpage = pageno;
                }

                myr += "<div align=right>";
                myr += "<a onclick=\"gotopage(1)\">";
                myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/1.gif\" border=0></a>";

                myr += "<a onclick=\"gotopage(prepage)\">";
                myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/2.gif\" border=0></a>";

                myr += "<a onclick=\"gotopage(nextpage)\">";
                myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/3.gif\" border=0></a>";


                myr += "<a onclick=\"gotopage(" + presults.totalpages + ")\">";
                myr += "<img src=\"https://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/4.gif\" border=0></a>";

                myr += "</div>";

                myr += "<table width=\"100%\" border=\"1\" cellspacing=\"0\" class=\"prod-list-table\" cellpadding=\"0\">	";


                $.each(presults.result_list, function (icount, compounds) {
                    if (icount % 2)
                        linecolor = 'gray'
                    else
                        linecolor = 'white'

                    myr += "<tr>"
                    myr += "  <td class=\"" + linecolor + "\" rowspan=\"2\" valign=\"middle\" align=\"center\" style=\"vertical-align:middle;\">";
                    myr += "<a href=\"product.aspx?CNO=" + compounds.Catalog_No + "\">";
                    myr += "<img  class=\"pi\" src=\"ProductImage/" + compounds.Catalog_No + "-160.png\"  /></a></td>";
                    myr += "  <td class=\"" + linecolor + "\" colspan=\"3\" valign=\"top\" align=\"left\">";
                    myr += "<a class=\"pa\" href=\"product.aspx?CNO=" + compounds.Catalog_No + "\"> " + compounds.CName + "   </a></td>";
                    myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Part # " + compounds.Catalog_No + "  </td>";
                    myr += " </tr>";
                    myr += "<tr>"
                    myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">MDL # " + compounds.MDL_NUMBER + "</td>";
                    myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">CAS #" + compounds.CAS;
                    myr += "	</td>";
                    myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Formula </td>";
                    myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Mol. Wt. </td>";
                    myr += " </tr>";
                });

                myr += "</table>";
                $('#myresults').html(myr)
            },

            error: function (jqXHR, status) {
                alert('error')
            }
        });
    }
    function getCookie(c_name) {
        var c_value = document.cookie;
        var c_start = c_value.indexOf(" " + c_name + "=");
        if (c_start == -1) {
            c_start = c_value.indexOf(c_name + "=");
        }
        if (c_start == -1) {
            c_value = null;
        }
        else {
            c_start = c_value.indexOf("=", c_start) + 1;
            var c_end = c_value.indexOf(";", c_start);
            if (c_end == -1) {
                c_end = c_value.length;
            }
            c_value = unescape(c_value.substring(c_start, c_end));
        }
        return c_value;
    }

    function setCookie(c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    } 		
</script>

<style type="text/css">
.buttons 
{
    margin:10px 10px 10px 10px;
}
.gray
{
    background-color:lightgray;
}
.white
{
    background-color:White;
}

img.pi
{
    width:130px;
    height:130px;
}
</style>

<table width="1004" border="0" cellpadding="0" cellspacing="0" class="text">
  <tr>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-lbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
    <td align="left" valign="top"><table width="100%"  border="0" cellpadding="0" cellspacing="0" class="text">
          <tr align="left" valign="top">
            <td><table width="100%"  border="0" cellpadding="10" cellspacing="0" class="text">
                  <tr>
                    <td align="left" valign="top">
                    <div align="justify">
                    <br />
                        <div align="left">
                            <asp:HyperLink ID="hlcat" runat="server" CssClass="bluelink" Font-Bold="true" NavigateUrl="~/default.aspx" Text="Home"></asp:HyperLink>&nbsp;<b class="bluetext">>></b>&nbsp;<asp:Label ID="lblSubCat" Font-Bold="true" CssClass="bluetext" runat="server" Text="Product"></asp:Label>
                        </div>
                        <table width="100%"  border="0" cellpadding="5" cellspacing="1" bgcolor="#E1E1E1" class="text">
                          <tr>
                            <td align="left" valign="top" bgcolor="white" width="30%">
                                    <uc2:prdList id="prdList" runat="server" />                           
                            </td>
                            <td valign="top" bgcolor="white"><h3> Search by structure </h3>
                                <div id="ChemMol_Structure"></div>
                                <div align='center'>
                                    <form name="ChemMol_SS_Form" method="post" action="#" >
                                        <input type="hidden" name="smiles"/>
                                        <input type="hidden" name="jme"/>
                                        <input type="hidden" name="mol"/>
                                        <input type="hidden" name="rinfo"/>
                                        <input type="hidden" name="exacts" value="n"/>    
                                        <input class="buttons" type="button" value="Exact Search" onclick="submitChemMolClientForm('y')"  />      
                                        <input class="buttons" type="button" value="Substructure Search" onclick="submitChemMolClientForm('n')" />

                                    </form>
                                    </div> 
                                    <div id='myresults' style='display:none'>
                                </div>
                            </td>
                          </tr>
                        </table>
                        <br />
                    </div></td>
                  </tr>
              </table></td>
            <td width="5"><img src="images/space.gif" width="1" height="1" /></td>
          </tr>
        </table>
      </td>
    <td width="17" align="left" valign="top" style="background-image:url(images/main-rbg.gif) "><img src="images/space.gif" alt="space" width="1" height="1" /></td>
  </tr>
</table>
</asp:Content>

