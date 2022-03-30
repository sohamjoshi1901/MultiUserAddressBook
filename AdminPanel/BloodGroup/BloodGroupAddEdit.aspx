<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="BloodGroupAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_BloodGroup_BloodGroupAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><br />
     <div class="row">
                <h2 style="text-align:center"><asp:Label runat="server" ID="lblPageHeaderText" /></h2>
            </div>
    <br />
    <div class="container">
            <asp:Label runat="server" ID="lblMessage" CssClass="alert-danger" EnableViewState="false" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
            <div class="row">
                <div class="col-md-2">
                   <span class="text-danger">*</span> Blood Group Name:
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtBloodGroupName" />
                     <asp:RequiredFieldValidator runat="server" ID="rfvBloodGroup" ErrorMessage="Enter BloodGroup Name" CssClass="text-danger" ValidationGroup="save" ControlToValidate="txtBloodGroupName" Display="Dynamic"/>
                </div>
            </div>

            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info"  Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
            <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />

        </div>
</asp:Content>

