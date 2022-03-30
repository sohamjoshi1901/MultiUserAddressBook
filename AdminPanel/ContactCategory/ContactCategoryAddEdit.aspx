<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_ContactCategory_ContactCategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
            <div class="row">
                 <h2 style="text-align:center"><asp:Label runat="server" ID="lblPageHeaderText" /></h2>
            </div>
    <div class="container">
             <asp:Label runat="server" ID="lblMessage" CssClass="alert-danger" EnableViewState="false" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
            <div class="row">
                <div class="col-md-3">
                   <span class="text-danger">*</span> Contect Category Name:
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCCName" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvContactCategory" ValidationGroup="save" ErrorMessage="Enter Contact Category Name" CssClass="text-danger" ControlToValidate="txtCCName" Display="Dynamic"/>
                </div>
            </div>
        

            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info" Text="Save" ValidationGroup="save" OnClick="btnSave_Click" />
            <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />

        </div>
</asp:Content>

