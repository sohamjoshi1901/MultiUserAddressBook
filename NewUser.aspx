<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="NewUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/AddressBook/Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/AddressBook/Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="~/AddressBook/Content/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>
                --------------------------Sign Up page---------------------------   
            </h1>
    <div>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
  <div class="form-row">
    <div class="form-group col-md-6">
      <label for="inputEmail4">User Name:</label>
      <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control"></asp:TextBox>

    </div>
    <div class="form-group col-md-6">
      <label for="inputPassword4">Password:</label>
     <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"></asp:TextBox>
    </div>
  </div>
  <div class="form-group col-md-6">
      <label for="inputEmail4">Display Name:</label>
      <asp:TextBox runat="server" ID="txtDisplayName" CssClass="form-control"></asp:TextBox>
    </div>
  </div>
  <div class="form-group">
    <label for="inputAddress2">Address:</label>
     <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control"></asp:TextBox>
  </div>
   <div class="form-group col-md-6">
      <label >Mobile Number:</label>
      <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control"></asp:TextBox>
    </div>
 
            <asp:Button ID="btnSubmit" Text="submit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnGoBack" Text="Go back" runat="server" CssClass="btn btn-light" OnClick="btnGoBack_Click" />
    </div>
            
    </form>
</body>
</html>
