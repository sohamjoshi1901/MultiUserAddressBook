<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="BloodGroupList.aspx.cs" Inherits="AdminPanel_BloodGroup_BloodGroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <h1 style="text-align: center">Blood Group List</h1>
    <br />
    <div class="container">
    <asp:Label runat="server" ID="lblMessage" EnableViewState="false" CssClass="alert-danger" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
    
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-info btn-sm" Style="float: right" />
    <asp:GridView AutoGenerateColumns="false" ID="gvBloodGroup" runat="server" CssClass="table table-hover" OnRowCommand="gvBloodGroup_RowCommand">
        <Columns>
            
            <asp:BoundField DataField="BloodGroupName" HeaderText="Blood Group Type" />
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("BloodGroupID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:HyperLink runat="server" ID="hlEdit" CssClass="btn btn-light btn-sm" Text="Edit" NavigateUrl='<%# "~/AdminPanel/BloodGroup/BloodGroupAddEdit.aspx?BloodGroupID="+ Eval("BloodGroupID").ToString() %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    </div>

</asp:Content>

