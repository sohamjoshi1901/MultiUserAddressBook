<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="MultiUserAddressBook_AdminPanel_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="~/AddressBook/Content/js/bootstrap.min.js"></script>

</head>
<body>

    <form id="form" runat="server">
    <div class="container ">
        <div class="row " >
            <div class="col-md-6">
                <h1>Login Page</h1>
            </div>
        </div>
        <asp:Label ID="lblMessage" CssClass="text-danger" runat="server" EnableViewState="false" />
    <div class="row">
        <div class="col-md-2">
            User Name:
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" />
        </div>
    </div>
         <div class="row">
        <div class="col-md-2">
            Password:
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"  />
            
            <div>Click here to see password:<span id="toggle_pwd" class="fa fa-fw fa-eye field_icon"></span></div>
            
    <script type="text/javascript">
        $(function () {
            $("#toggle_pwd").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var type = $(this).hasClass("fa-eye-slash") ? "text" : "password";
                $("#txtPassword").attr("type", type);
            });
        });
    </script>
        </div>
    </div>
        <div class="row" style="padding-left:330px">
        <div class="col-md-6">
            <asp:HyperLink runat="server" ID="hlNewUser" Text="Enter For New User" NavigateUrl="~/NewUser.aspx" />
            <asp:Button runat="server" ID="btnLogIn" Text="Log in" CssClass="btn btn-info" OnClick="btnLogIn_Click"  />
          
        </div>
    </div>
        </div>
    </form>
</body>
</html>
