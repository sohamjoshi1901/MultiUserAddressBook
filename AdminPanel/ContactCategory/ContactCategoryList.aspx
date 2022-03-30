<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="AddressBook_AdminPanel_ContactCategory_ContactCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <br />
    <h1 style="text-align: center">Contact Category List</h1>
    <br />
    <div class="container">
    <asp:Label runat="server" ID="lblMessage" EnableViewState="false" CssClass="alert-danger" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-info btn-sm" Style="float: right" />
           <asp:GridView AutoGenerateColumns="false" ID="gvContactCategory" runat="server" CssClass="table table-bordered" OnRowCommand="gvContactCategory_RowCommand">
               <Columns>
                   
                   <asp:BoundField DataField="ContactCategoryName" HeaderText="Contact Type" />
                   <asp:TemplateField HeaderText="Delete">
                       <ItemTemplate>
                           <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-sm btn-danger" CommandName="DeleteRecord" CommandArgument='<%#Eval("ContactCategoryID") %>'/>
                            </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Edit">
                       <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hlEdit" CssClass="btn btn-light btn-sm" Text="Edit" NavigateUrl='<%# "~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx?ContactCategoryID="+ Eval("ContactCategoryID").ToString() %>'/>
                       </ItemTemplate>
                   </asp:TemplateField>

                  
               </Columns>

           </asp:GridView>
        </div>


</asp:Content>

