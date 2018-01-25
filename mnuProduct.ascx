<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mnuProduct.ascx.cs" Inherits="Peptech.mnuProduct" %>
<style type="text/css">
    .main ul 
    {
        list-style-type:none;
        line-height:200%;
    }

    .main li
    {
        display:block;
        position:relative;
        left:-30px;
     } 
    
    .sub 
    {
        display:none;
    }
    
    .main li:hover .sub
    {
        display:block;
    }
        
    .main a:link, .main a:visited
    {
        display:block;
        color:Gray;
        font-family:Arial;
        border-bottom-style:solid;
        border-bottom-width:1px;        
    } 
    
    .sub a:link, .main a:visited
    {
        border-bottom-style:none;   
    } 
    .main a:hover
    {
        background-color:#000033;
        color:White;
    }          

</style>
<asp:Label ID="lblul" runat="server" ></asp:Label> 