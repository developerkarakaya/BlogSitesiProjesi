var controllerName = "HomeArticleController";
var getAllArticleUrl = "/Article/getAllArticle";
var getOneArticleUrl = "/Article/getOneArticle"
var contactPostUrl = "/Article/postMail";
var app = angular.module('MyBlog', ['ui.bootstrap']);

app.controller(controllerName, function ($scope, $http) {
    $scope.filteredArticles = [];
    $scope.maxSize = 5;
    $scope.currentPage = 1;
    $scope.numPerPage = 10;

    $http.get("https://api.github.com/users/developerkarakaya/repos")
        .then(function (response) {
            $scope.repo = response.data;
        });

    

    $scope.getAllArticle = function () {
        document.getElementById('loader').style.display = "";
        var getAllArticlePost = $http({
            method: "POST",
            url: getAllArticleUrl,
            dataType: 'json',
            data: {}
        });
        getAllArticlePost.success(function (data, status) {
            if (data.IsSucceed = false) {
                ShowMessage(data.Message, 'e');
                document.getElementById('loader').style.display = "none";

            }
            else {
                $scope.getAllArticleList = data.Result;
                for (var i = 0; i < $scope.getAllArticleList.length; i++) {
                    $scope.getAllArticleList[i].ArticleDate = $scope.jsonJavaScriptTarihCevir(data.Result[i].ArticleDate);
                    $scope.getAllArticleList[i].Content = data.Result[i].Content.substring(1, 250);
                }

                $scope.$watch('currentPage + numPerPage', $scope.updateFilteredArticle);
                $scope.updateFilteredArticle();
                document.getElementById('loader').style.display = "none";


            }
        });
    }

    $scope.updateFilteredArticle = function () {
        var begin = (($scope.currentPage - 1) * $scope.numPerPage),
            end = begin + $scope.numPerPage;
        $scope.filteredArticles = $scope.getAllArticleList.slice(begin, end);

    }


    $scope.getOneArticle = function () {
        if (document.getElementById('loader') != null) {
            document.getElementById('loader').style.display = "";
        }


        var getOneArticle = $http({
            method: "POST",
            url: getOneArticleUrl,
            dataType: 'json',
            data: {}
        });
        getOneArticle.success(function (data, status) {
            $scope.Id = data.Id;
            $scope.ArticleDate = $scope.jsonJavaScriptTarihCevir(data.ArticleDate);
            $scope.ArticleImage = data.ArticleImage;
            $scope.ArticleViews = data.ArticleViews;
            $scope.Author = data.Author;
            $scope.Title = data.Title;
            $scope.SeoLink = data.SeoLink;
            $scope.Is_Active = data.Is_Active;
            $scope.Content = data.Content.substring(1, 250);
            $scope.categoryListesi = data.categoryList;
            $scope.commentCount = data.commentCount;
            document.getElementById('loader').style.display = "none";


        });

    }

    var aylar = ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"];
    $scope.jsonJavaScriptTarihCevir = function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt.getDate() + " " + aylar[dt.getMonth()] + " " + dt.getFullYear();
    }

    $scope.PostMail = function () {
        var contactForm = $('#iletisim');
        var contactFormData = contactForm.serializeArray();
        var toData = {};
        if ($('#isim').val() == "" || $('#email').val() == "") {
            ShowMessage('Lütfen gerekli alanları doldurunuz !', 'e');
        }
        else {
            $.each(contactFormData, function (i, v) {
                toData[v.name] = v.value;
            });
            var contactPost = $http({
                method: 'POST',
                url: contactPostUrl,
                data: toData,
                dataType: 'json'
            });
            contactPost.success(function (data, status) {
                if (data.IsSucced = false) {
                    ShowMessage(data.Message, 'e');
                }
                else {
                    document.getElementById('iletisim').reset();
                    ShowMessage("Mail gönderme lşlemi başarı ile gerçekleşmiştir. En kısa sürede geri dönüş sağlanacaktır.", 's');
                }
            });
        }
    }


    $scope.getOneArticle();
    $scope.getAllArticle();
});