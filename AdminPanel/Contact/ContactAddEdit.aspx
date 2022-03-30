<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AddressBook_AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"><br />

            <div class="row">
                <h2 style="text-align:center"><asp:Label runat="server" ID="lblPageHeaderText" /></h2>
            </div>


    <asp:Label runat="server" ID="lblMessage" CssClass="alert-danger" EnableViewState="false" />
    <asp:Label runat="server" ID="lblSuccess" EnableViewState="false" CssClass="alert-success" />
    <div style="padding-left:20px">
    <div class="row">
        <div class="col-md-2">
            <span class="text-danger">*</span>Person  Name:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtPeresonName" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPersonName" ErrorMessage="Enter Person Name" CssClass="text-danger" ControlToValidate="txtPeresonName" Display="Dynamic" ValidationGroup="save"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Address:
        </div>

        <div class="col-md-8">
            <asp:TextBox Rows="3" runat="server" CssClass="form-control" ID="txtAddress" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <span class="text-danger">*</span>Country:
        </div>

        <div class="col-md-3">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlCountry" CssClass="btn btn-secondary dropdown-toggle" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" /><br />
       <asp:RequiredFieldValidator runat="server" ID="rfvCountry" ErrorMessage="Select Country" CssClass="text-danger" ControlToValidate="ddlCountry" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
             </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <span class="text-danger">*</span>State:
        </div>

        <div class="col-md-3">
            <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" /><br />
            <asp:RequiredFieldValidator runat="server" ID="rfvState" ErrorMessage="Select State" CssClass="text-danger" ControlToValidate="ddlState" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
             <span class="text-danger">*</span>City:
        </div>

        <div class="col-md-3">
            <asp:DropDownList runat="server" ID="ddlCity" AutoPostBack="True" CssClass="btn btn-secondary dropdown-toggle" /><br />
            <asp:RequiredFieldValidator runat="server" ID="rfvCity" ErrorMessage="Select City" CssClass="text-danger" ControlToValidate="ddlCity" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
        </div>
    </div>



    <div class="row">
        <div class="col-md-2">
            PinCode:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtPincode" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
             <span class="text-danger">*</span>Mobile Number:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtMobileNo" />
            <asp:RequiredFieldValidator runat="server" ID="rfvMobileNo" ErrorMessage="Enter Mobile Number" CssClass="text-danger" ControlToValidate="txtMobileNo" Display="Dynamic" ValidationGroup="save"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Phone Number
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtPhoneNo" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Email:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Gender:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtGender" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Birth Date:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtBirthDate" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Profession:
        </div>

        <div class="col-md-3">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtProfession" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <span class="text-danger">*</span>Blood Group:
        </div>

        <div class="col-md-3">
            <asp:DropDownList runat="server" ID="ddlBloodGroup" CssClass="btn btn-secondary dropdown-toggle" /><br />
            <asp:RequiredFieldValidator runat="server" ID="rfvBloodGroup" ErrorMessage="Select Blood Group" CssClass="text-danger" ControlToValidate="ddlBloodGroup" InitialValue="-1" Display="Dynamic" ValidationGroup="save"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <span class="text-danger">*</span>Contact Category:
        </div>

        <div class="col-md-3">
            <asp:CheckBoxList runat="server" ID="ddlContactCategory" RepeatDirection="Horizontal" CssClass="btn btn-secondary dropdown-toggle" RepeatLayout="Flow" /><br />
            <%--<asp:RequiredFieldValidator runat="server" ID="rfvContactCategory" ErrorMessage="Select Contact Category" CssClass="text-danger" ControlToValidate="ddlContactCategory" InitialValue="-1" Display="Dynamic"/>--%>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span class="text-danger">*</span>Select Photo:
            </div>
            <div class="col-md-3">
                <asp:FileUpload ID="fuPhoto" runat="server" />
            </div>
            <div>
                <asp:Image runat="server" ID="imgOldPhoto" EnableViewState="false" Height="70" Width="70" />
            </div>
        </div>
    </div>

    

    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-info" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
    <asp:Button runat="server" ID="btncancel" CssClass="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" />
        </div>
</asp:Content>

