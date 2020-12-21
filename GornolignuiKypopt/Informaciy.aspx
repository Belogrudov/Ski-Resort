<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informaciy.aspx.cs" Inherits="GornolignuiKypopt.Informaciy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <link rel="stylesheet" href="../Content/Site.css" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Информация</h1>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto">
                <h4>Функции </h4>
                <p>
                    Программный продукт разрабатывался для организации Горнолыжный курорт, он автоматизирует процессы, внутри подсистем Отдел кадров, аренда лыжного инвентаря и обслуживание трасс. 
                    <br />
                    Основные функции программного продукта: добавление, изменение и обновление данных, поиск, сортировка и фильтрация данных, также доступны функции авторизации и регистрации.
                </p>
            </div>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto">
                <h4>Установка программы</h4>
                <p>Для установки программного продукта необходимо перейти по ссылке <a href="#">Ссылка на GitHub</a> и скачать репозиторий.</p> <%--Ссылка на git--%>
            </div>
            <br />
            <div class="text-block" style="margin-left: auto; margin-right: auto; background-color: #dae9f2; padding: 5px">
                <h4>Галерея</h4>
                <br />
                <%--Фото--%>
                <div class="row">
                    <div class="column">
                        <img src="img/Arenda.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Authorization.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Kategorii.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Registration.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Sotrydniki.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Tovari.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>

                    <div class="column">
                        <img src="img/Yvolnenie.PNG" style="width: 100%" onclick="myFunction(this);">
                    </div>
                </div>
                <div class="container-img">
                    <span onclick="this.parentElement.style.display='none'" class="closebtn" style="color: black">×</span>
                    <img id="expandedImg" style="width: 100%">
                </div>
            </div>
        </div>
    </form>
    <script>
        function myFunction(imgs) {
            var expandImg = document.getElementById("expandedImg");
            expandImg.src = imgs.src;
            expandImg.parentElement.style.display = "block";
        }
    </script>
</body>
</html>
