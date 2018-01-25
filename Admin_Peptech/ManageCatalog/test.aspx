<%@ Page Language="C#" MasterPageFile="~/Admin_Peptech/Admin.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Admin_Peptech_ManageCatalog_test"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True" OnSorting="GridView1_Sorting1">
        <PagerSettings Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="id" />
            <asp:BoundField DataField="catalogname" HeaderText="catalogname" SortExpression="catalogname" />
        </Columns>
    </asp:GridView>
</asp:Content>

