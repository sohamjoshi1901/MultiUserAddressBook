<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <h2 style="text-align: center">
            <asp:Label runat="server" ID="lblPageHeaderText" /></h2>
    </div>
    <br />
    <div class="container">
        <asp:Label runat="server" ID="lblMessage" CssClass="alert-danger" EnableViewState="false" />
        <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
        <div class="row">
            <div class="col-md-2">
                 <span class="text-danger">*</span>Country Name:
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCountryName" />
                <asp:RequiredFieldValidator runat="server" ID="rfvCountryName" ValidationGroup="save" ErrorMessage="Enter Country Name" CssClass="text-danger" ControlToValidate="txtCountryName" Display="Dynamic"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span class="text-danger">*</span>Country Code:
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCountryCode" />
                <asp:RequiredFieldValidator runat="server" ID="rfvCountryCode" ValidationGroup="save" ErrorMessage="Enter Contry Code" CssClass="text-danger" ControlToValidate="txtCountryCode" Display="Dynamic"/>
            </div>
        </div>

        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
        <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />
    </div>
</asp:Content>

