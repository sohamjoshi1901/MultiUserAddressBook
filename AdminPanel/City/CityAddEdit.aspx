<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_City_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <h2 style="text-align:center">
                <asp:Label runat="server" ID="lblHeaderText" /></h2>
        </div>
        <asp:Label runat="server" ID="lblMessage" CssClass="alert-danger" EnableViewState="false" />
        <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
        <div class="row">
            <div class="col-md-2">
                <span class="text-danger">*</span>Select Country:
            </div>
            <div class="col-md-3">
                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="btn btn-secondary dropdown-toggle" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" ID="rfvCountry" ErrorMessage="Select Country" CssClass="text-danger" ControlToValidate="ddlCountry" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span class="text-danger">*</span>Select State:
            </div>
            <div class="col-md-3">
                <asp:DropDownList runat="server" ID="ddlState" CssClass="btn btn-secondary dropdown-toggle" AutoPostBack="true"></asp:DropDownList><br />
                <asp:RequiredFieldValidator runat="server" ID="rfvState" ErrorMessage="Select State" CssClass="text-danger" ControlToValidate="ddlState" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
               <span class="text-danger">*</span> Enter City:
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ID="rfvCityName" ErrorMessage="Enter City Name" CssClass="text-danger" ControlToValidate="txtCityName" ValidationGroup="save" Display="Dynamic"/>
            </div>

        </div>
        <div class="row">
            <div class="col-md-2">
                Enter Pin Code:
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtPinCode" CssClass="form-control" />
            </div>

        </div>
        <div class="row">
            <div class="col-md-2">
                Enter STD code:
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtSTDCode" CssClass="form-control" />
            </div>

        </div>



        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
        <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />

    </div>
</asp:Content>

