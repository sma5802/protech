<html>

<head>
<title> Search by structure </title>
<script type="text/javascript" language="javascript" src="jsme/jsme.nocache.js"></script>
<script type="text/javascript" language="javascript" src='jsme/jquery-2.0.3.min.js'></script> 

<script type="text/javascript" language="javascript">
    //this function will be called after the JavaScriptApplet code has been loaded.
    var IDList = ''; 
    function jsmeOnLoad() {
        document.ChemMol_Structure = new JSApplet.JSME("ChemMol_Structure", "600px", "340px", { "options": "noquery,nohydrogens,polarnitro,nocanonize,oldlook" });
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
                url: "http://chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/server/server.php",
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
                    myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/3.gif\" border=0></a>";
                    myr += "<a onclick=\"gotopage(" + presults.totalpages + ")\">";
                    myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/4.gif\" border=0></a>";

                    myr += "</div>";

                    myr += "<table width=\"100%\" border=\"1\" cellspacing=\"0\" class=\"prod-list-table\" cellpadding=\"0\">	";


                    $.each(presults.result_list, function (icount, compounds) {
                        if (icount % 2)
                            linecolor = 'gray'
                        else
                            linecolor = 'white'

                        myr += "<tr>"
                        myr += "  <td class=\"" + linecolor + "\" rowspan=\"2\" valign=\"middle\" align=\"center\" style=\"vertical-align:middle;\">";
                        myr += "<a href=\"product.aspx?CNO=/" + compounds.Catalog_No + "/\">";
                        myr += "<img src=\"ProductImage/" + compounds.Catalog_No + ".png\"  /></a></td>";
                        myr += "  <td class=\"" + linecolor + "\" colspan=\"3\" valign=\"top\" align=\"left\">";
                        myr += "<a href=\"product.aspx?CNO=/" + compounds.Catalog_No + "/\"> " + compounds.CName + "   </a></td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Part # " + compounds.Catalog_No + "  </td>";
                        myr += " </tr>";
                        myr += "<tr>"
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">MDL # " + compounds.MDL_NUMBER + "</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">CAS # <br/>" + compounds.CAS;
                        myr += "	</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Formula " + compounds.Formula + "</td>";
                        myr += "  <td class=\"" + linecolor + "\" valign=\"top\" align=\"left\">Mol. Wt. " + compounds.MolWeight + "</td>";
                        myr += " </tr>";
                        //  myr += "<td class=\"ari-tbl-col-0\"><img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/cmshowimage.php?id=" + compounds.mol_id + "\" /> </td>";
                    });

                    myr += "</table>";

                    $('#myresults').html(myr)
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
            url: "http://chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/server/server.php",
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
                myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/1.gif\" border=0></a>";

                myr += "<a onclick=\"gotopage(prepage)\">";
                myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/2.gif\" border=0></a>";

                myr += "<a onclick=\"gotopage(nextpage)\">";
                myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/3.gif\" border=0></a>";


                myr += "<a onclick=\"gotopage(" + presults.totalpages + ")\">";
                myr += "<img src=\"http://www.chemmol.com/chemmol/suppliers/peptechcorp/buildingblocks/images/4.gif\" border=0></a>";

                myr += "</div>";

                myr += "<table width=\"100%\" border=\"1\" cellspacing=\"0\" class=\"prod-list-table\" cellpadding=\"0\">	";


                $.each(presults.result_list, function (icount, compounds) {
                    if (icount % 2)
                        linecolor = 'gray'
                    else
                        linecolor = 'white'

                    myr += "<tr>"
                    myr += "  <td class=\"" + linecolor + "\" rowspan=\"2\" valign=\"middle\" align=\"center\" style=\"vertical-align:middle;\">";
                    myr += "<a href=\"product.aspx?CNO=/" + compounds.Catalog_No + "/\">";
                    myr += "<img src=\"ProductImage/" + compounds.Catalog_No + ".png\"  /></a></td>";
                    myr += "  <td class=\"" + linecolor + "\" colspan=\"3\" valign=\"top\" align=\"left\">";
                    myr += "<a href=\"product.aspx?CNO=/" + compounds.Catalog_No + "/\"> " + compounds.CName + "   </a></td>";
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
</script>
</head>
<body>
<div id="ChemMol_Structure"></div>
<div align='center'>
<form name="ChemMol_SS_Form" method="post" action="#" >
    <input type="hidden" name="smiles"/>
    <input type="hidden" name="jme"/>
    <input type="hidden" name="mol"/>
    <input type="hidden" name="rinfo"/>
    <input type="hidden" name="exacts" value="n"/>    

    <input class="btn btn-primary" type="button" value="Exact Search" onclick="submitChemMolClientForm('y')"  />      
    <input class="btn btn-primary" type="button" value="Substructure Search" onclick="submitChemMolClientForm('n')" />
</form>
</div> 
<div id='myresults' style='display:none'>
</div>
</body>
</html>