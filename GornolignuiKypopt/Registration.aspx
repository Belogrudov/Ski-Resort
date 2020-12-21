<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="GornolignuiKypopt.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Регистрация</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="reg">
            <div class="container-fluid">
                <h1>Регистрация</h1>
                <hr />
                <div>
                    <h2>Фамилия</h2>
                    <asp:TextBox ID="tbFamiliya" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ErrorMes" ErrorMessage="Введите фамилию" Display="Dynamic" ControlToValidate="tbFamiliya"></asp:RequiredFieldValidator>
                    <h2>Имя</h2>
                    <asp:TextBox ID="tbName" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ErrorMes" ErrorMessage="Введите имя" Display="Dynamic" ControlToValidate="tbName"></asp:RequiredFieldValidator>
                    <h2>Отчество</h2>
                    <asp:TextBox ID="tbOtchestvo" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ErrorMes" ErrorMessage="Введите отчество" Display="Dynamic" ControlToValidate="tbOtchestvo"></asp:RequiredFieldValidator>
                    <h2>Логин</h2>
                    <asp:TextBox ID="tbLogin" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblLogin" runat="server" Text="Логин уже занят" Visible="false" CssClass="ErrorMes"></asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="ErrorMes" ErrorMessage="Введите логин" Display="Dynamic" ControlToValidate="tbLogin"></asp:RequiredFieldValidator>
                    <h2>Пароль</h2>
                    <asp:TextBox ID="tbPassword" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="ErrorMes" ErrorMessage="Введите пароль" Display="Dynamic" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>
                    <h2>Подтверждение пароля</h2>
                    <asp:TextBox ID="tbPasswordConfirm" runat="server" CssClass="tbRegistration"></asp:TextBox>
                    <br />
                    <asp:Button ID="btEnter" runat="server" Text="ВОЙТИ" CssClass="btEnter" OnClick="btEnter_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
