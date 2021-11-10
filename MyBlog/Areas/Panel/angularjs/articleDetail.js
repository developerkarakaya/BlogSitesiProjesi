
var reload = false;
var controllerName = "articleDetailController";
var articleGetDetailUrl = "/panel/article/detailJsonData";
var getCategoriesUrl = "/panel/article/getCategories";
var getArticleCategoriesUrl = "/panel/article/getArticleCategories";
var getTagsUrl = "/panel/article/getTags";
var postArticleUrl = "/panel/article/PostArticle";
var postArticleImageUrl = "/panel/article/PostArticleImage";
var app = angular.module('myblogAdmin', ['ui.bootstrap']);
app.controller(controllerName, function ($scope, $http) {
    $scope.ImageView = false;
    $scope.ShowMessage = function (message, type) {
        if (type == "s") {
            swal({
                title: 'İşlem Başarılı',
                text: message,
                type: 'success',
                showCancelButtonClass: 'btn-success',
                confirmButtonText: 'Tamam'
            });
        }
        else {
            swal({
                title: 'İşlem Başarısız',
                text: message,
                type: 'error',
                showCancelButtonClass: 'btn-danger',
                confirmButtonText: 'Tamam'
            });
        }
    }
    $scope.articleDetail = function () {
        $('#loader').show();
        var id = $('#ArticleId').val();
        var getDetail = $http({
            method: "POST",
            url: articleGetDetailUrl,
            dataType: 'json',
            data: { Id: id }
        });
        getDetail.success(function (data, status) {
            if (data.IsSucceed == false) {
                $('#loader').hide();
            }
            else {
                $('#loader').hide();
                $scope.Id = data.Result.Id;
                $scope.ArticleDate = jsonJavaScriptTarihCevir(data.Result.ArticleDate);
                $scope.ArticleImage = data.Result.ArticleImage;
             
                if (data.Result.ArticleImage != null) {
                    $scope.ImageView = true;
                }
                $scope.ArticleViews = data.Result.ArticleViews;
                $scope.Author = data.Result.Author;
                $scope.Title = data.Result.Title;
                $scope.Is_Active = data.Result.Is_Active;
                setTimeout(function () {
                    CKEDITOR.instances["Content"].setData(data.Result.Content);
                }, 500);
            }
        });
        getDetail.error(function (data, status) {
            $('#loader').hide();
            ShowMessage('Makale detayı çekilirken bir hata oluştu', 'e');
        });
    }

    $scope.articleCategories = function () {
        var id = $('#ArticleId').val();
        var getArticleCategories = $http({
            method: "POST",
            url: getArticleCategoriesUrl,
            dataType: 'json',
            data: { Id: id }
        });
        getArticleCategories.success(function (data, status) {
            if (data.IsSucceed == false) {
            }
            else {
                $scope.articleCategories = data.Result;
                console.log(data.Result);
            }
        });


    }
    $scope.getCategories = function () {
        var getCategories = $http({
            method: "POST",
            url: getCategoriesUrl,
            dataType: 'json',
            data: {}
        });
        getCategories.success(function (data, status) {
            if (data.IsSucceed == false) {
            }
            else {
                $scope.categories = data.Result;
            }

        });


    }


    $scope.getTags = function () {
        var getCategories = $http({
            method: "POST",
            url: getTagsUrl,
            dataType: 'json',
            data: {}
        });
        getCategories.success(function (data, status) {
            if (data.IsSucceed == false) {
            }
            else {
                $scope.tags = data.Result;
            }

        });


    }

    $scope.postArticle = function () {

        var articleId = $('#ArticleId').val();
        var articlePostForm = $('#articleForm');
        var fileInput = articlePostForm.find("input[type=file]");
        var categoryValues = $('#category-data').val();
        var tagValues = $('#tag-data').val();
        var formData = articlePostForm.serializeArray();
        var gidenVeri = {};
        $.each(formData, function (i, v) {
            gidenVeri[v.name] = v.value;
        });
        gidenVeri.Id = articleId;
        gidenVeri.Content = CKEDITOR.instances['Content'].getData();
        gidenVeri.Is_Active = true;
        gidenVeri.ArticleDate = new Date();
        var articlePost = $http({
            method: "POST",
            traditional: true,
            url: postArticleUrl,
            dataType: 'json',
            data: { article: gidenVeri, categoryValues: categoryValues, tagValues: tagValues },
            headers: { "Content-Type": "application/json" }
        });
        articlePost.success(function (data, status) {
            if (data.IsSucceed == false) {
                ShowMessage(data.Message, 'e'); 
            }
            else {
                    var datainput = new FormData();
                    if (fileInput.length) {
                        files = fileInput[0].files;
                        datainput.append("photoFile", files[0]);
                        datainput.append("id", data.Result.Id);
                }
                    $.ajax({
                        type: "POST",
                        url: postArticleImageUrl,
                        contentType: false,
                        processData: false,
                        data: datainput
                    }).then(function () {
                        document.getElementById('articleForm').reset();
                        CKEDITOR.instances['Content'].setData('');
                        window.location = "/panel/article/index";
                            ShowMessage(data.Message, 's');
                    });

            }
        });
}

    $scope.getTags();
    $scope.articleDetail();
    $scope.getCategories();
    $scope.articleCategories();
});
