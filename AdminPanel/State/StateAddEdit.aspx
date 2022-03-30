<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_State_StateAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
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
                   <span class="text-danger">*</span> Select Country:
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="btn btn-secondary dropdown-toggle"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator runat="server" ID="rfvCountry" ValidationGroup="save" ErrorMessage="Select Country" CssClass="text-danger" ControlToValidate="ddlCountry" InitialValue="-1" Display="Dynamic"/>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                   <span class="text-danger">*</span> Enter State:
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtStateName" CssClass="form-control" />
                     <asp:RequiredFieldValidator runat="server" ID="rfvStateName" ValidationGroup="save" ErrorMessage="Enter State Name" CssClass="text-danger" ControlToValidate="txtStateName" Display="Dynamic"/>
                </div>

            </div>


            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info" Text="Save" ValidationGroup="save" OnClick="btnSave_Click" />
            <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />

        </div>
    </div>
</asp:Content>

