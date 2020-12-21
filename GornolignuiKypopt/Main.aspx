<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GornolignuiKypopt.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Главная страница</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsTovari" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <div style="text-align: right;">
                <asp:Label ID="lblUserName" runat="server" Font-Size="24"></asp:Label>
                <br />
                <asp:Button ID="btEnter" runat="server" CssClass="searchButtons" Text="Авторизоваться" Visible="false" OnClick="btEnter_Click" Width="150px" />
                <br />
                <a href="OtpravitPismo.aspx">Отправить письмо</a>
                <br />
                <a href="Informaciy.aspx">Информация</a>
            </div>
            <div class="logo">
            </div>
            <div class="content container text-center">
                <h1>Товары</h1>
                <div class="searchPanel">
                    <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                    <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                    <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
                    <asp:Button ID="Button1" runat="server" Text="ВОЙТИ" CssClass="btEnter" Style="margin-bottom: 30px" OnClick="btEnter_Click" />
                    
                    <br />
                </div>
                <asp:GridView ID="gvTovari" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                    AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvTovari_RowDataBound" OnSorting="gvTovari_Sorting" OnSelectedIndexChanged="gvTovari_SelectedIndexChanged" Width="751px">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
