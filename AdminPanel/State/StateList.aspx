<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AddressBook_AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <br />
    <h1 style="text-align: center">State List</h1>
    <br />
    <div class="container">
    <asp:Label runat="server" ID="lblMessage" EnableViewState="false" CssClass="alert-danger" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
    <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-info btn-sm" Style="float: right" />
               <asp:GridView AutoGenerateColumns="false" ID="gvState" runat="server" CssClass="table table-bordered" OnRowCommand="gvState_RowCommand">
               <Columns>
                   
                   <asp:BoundField DataField="StateName" HeaderText="State" />
                   <asp:BoundField DataField="CountryName" HeaderText="Country" />
                   <asp:TemplateField HeaderText="Delete">
                       <ItemTemplate>
                           <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("StateID") %>'/>
                           </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                       <ItemTemplate>
                           <asp:HyperLink runat="server" ID="hlEdit" CssClass="btn btn-light btn-sm" Text="Edit" NavigateUrl='<%# "~/AdminPanel/State/StateAddEdit.aspx?StateID="+ Eval("StateID").ToString() %>'/>

                       </ItemTemplate>
                   </asp:TemplateField>
                  
               </Columns>

           </asp:GridView>
        </div>
</asp:Content>

