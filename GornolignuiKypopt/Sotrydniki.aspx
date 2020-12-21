<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sotrydniki.aspx.cs" Inherits="GornolignuiKypopt.Sotrydniki" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Сотрудники</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsSotrydniki" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsRole" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="Sotrydniki.aspx" style="font-size: 28px; font-weight: 500">Сотрудники</a>
            <br />
            <a href="Yvolnenie.aspx" style="font-size: 20px; font-weight: 500">Увольнение сотрудника</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbFamiliya" class="lblTitle">Фамилия</label>
                            <asp:TextBox ID="tbFamiliya" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите фамилию" CssClass="ErrorMes" ControlToValidate="tbFamiliya" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbName" class="lblTitle">Имя</label>
                            <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Введите имя" CssClass="ErrorMes" ControlToValidate="tbName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbOtchestvo" class="lblTitle">Отчество</label>
                            <asp:TextBox ID="tbOtchestvo" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Введите отчество" CssClass="ErrorMes" ControlToValidate="tbOtchestvo" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">   
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbLogin" class="lblTitle">Логин</label>
                            <asp:TextBox ID="tbLogin" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Введите логин" CssClass="ErrorMes" ControlToValidate="tbLogin" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMes" Text="Логин уже занят" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPassword" class="lblTitle">Пароль</label>
                            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Введите пароль" CssClass="ErrorMes" ControlToValidate="tbPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="row"> 
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlRole" class="lblTitle">Права доступа</label>
                            <asp:DropDownList ID="ddlRole" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" Width="115px" />
                <br />
                <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" Width="116px" />
                <br />
                <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" CausesValidation="false" OnClick="btDelete_Click" Width="116px" />
                <br />
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvSotrydniki" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvSotrydniki_RowDataBound" OnSelectedIndexChanged="gvSotrydniki_SelectedIndexChanged" OnSorting="gvSotrydniki_Sorting" Width="1088px">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
