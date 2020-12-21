<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yvolnenie.aspx.cs" Inherits="GornolignuiKypopt.Yvolnenie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Увольнение сотрудника</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="sdsYvolnenie" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsSotrydniki" runat="server"></asp:SqlDataSource>
        <div class="container-fluid">
            <a href="Sotrydniki.aspx" style="font-size: 28px; font-weight: 500">Сотрудники</a>
            <br />
            <a href="Yvolnenie.aspx" style="font-size: 20px; font-weight: 500">Увольнение сотрудника</a>
            <div class="content container">
                <div class="row">
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="ddlSotrydniki" class="lblTitle">Сотрудник</label>
                            <asp:DropDownList ID="ddlSotrydniki" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbData_Yvolneniya" class="lblTitle">Дата увольнения</label>
                            <asp:TextBox ID="tbData_Yvolneniya" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Введите дату" CssClass="ErrorMes" ControlToValidate="tbData_Yvolneniya" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-1">
                        <div class="form-group">
                            <label for="tbPrichina_yvolneniya" class="lblTitle">Причина увольнения</label>
                            <asp:TextBox ID="tbPrichina_yvolneniya" runat="server" class="form-control"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Введите причину" CssClass="ErrorMes" ControlToValidate="tbPrichina_yvolneniya" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btInsert" runat="server" CssClass="formButtons" Text="Добавить" OnClick="btInsert_Click" />
                <br />
                <asp:Button ID="btUpdate" runat="server" CssClass="formButtons" Text="Изменить" OnClick="btUpdate_Click" />
                <br />
                <asp:Button ID="btDelete" runat="server" CssClass="formButtons" Text="Удалить" CausesValidation="false" OnClick="btDelete_Click" />
                <br />
            </div>
            <div class="searchPanel">
                <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
                <asp:Button ID="btSerch" runat="server" Text="Поиск" CssClass="searchButtons" CausesValidation="false" OnClick="btSerch_Click"></asp:Button>
                <asp:Button ID="btFilter" runat="server" Text="Фильтр" CssClass="searchButtons" CausesValidation="false" OnClick="btFilter_Click"></asp:Button>
                <asp:Button ID="btCancel" runat="server" Text="Отмена" CssClass="searchButtons" CausesValidation="false" OnClick="btCancel_Click"></asp:Button>
            </div>
            <asp:GridView ID="gvYvolnenie" runat="server" AllowSorting="true" CurrentSortDirection="ASC" Font-Size="14px" CssClass="gridView"
                AlternatingRowStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvYvolnenie_RowDataBound" OnSelectedIndexChanged="gvYvolnenie_SelectedIndexChanged" OnSorting="gvYvolnenie_Sorting">
                <Columns>
                    <asp:CommandField ShowSelectButton="true" SelectText="Выбрать" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
