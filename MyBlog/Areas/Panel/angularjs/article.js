
var reload = false;
var controllerName = "articleController";
var articleGetDetailUrl = "/panel/article/detailJsonData";
var articleGetUrl = "/panel/article/getArticle";
var articleDeleteUrl = "/panel/article/articleDelete";
var articleActiveInPassiveUrl = "/panel/article/articleActiveInPassive";
var app = angular.module('myblogAdmin', ['ui.bootstrap']); 
app.controller(controllerName, function ($scope, $http) {
    $scope.filteredArticle = [];
    $scope.maxSize = 5;
    $scope.currentPage = 1;
    $scope.numPerPage = 10;
   
    $scope.articleGet = function () {
        $('#loader').show();
        var post = $http({
            method: "POST",
            url: articleGetUrl,
            dataType: 'json',
            data: {},
            headers: { "Content-Type": "application/json" }
        });
        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                ShowMessage(data.Message, "e");
                $('#loader').hide();
            }
            else {
                $scope.articleList = data.Result;
                for (var i = 0; i < $scope.articleList.length; i++) {
                    $scope.articleList[i].ArticleDate = jsonJavaScriptTarihCevir(data.Result[i].ArticleDate);
                }
                $scope.$watch('currentPage + numPerPage', $scope.updateFilteredArticle);
                $scope.updateFilteredArticle();
                $('#loader').hide();
            }
        });
        post.error(function (data, status) {
            ShowMessage(data.Message, "e");
            $('#loader').hide();
        });
    }

    $scope.updateFilteredArticle = function () {
        var begin = (($scope.currentPage - 1) * $scope.numPerPage),
            end = begin + $scope.numPerPage;
        $scope.filteredArticles = $scope.articleList.slice(begin, end);
    }

    $scope.articleActiveInPassive = function (articleId) {
        var deleteArticle = $http(
            {
                method: 'POST',
                url: articleActiveInPassiveUrl,
                dataType: 'json',
                data: { articleId: articleId },
                headers: { "Content-Type": "application/json" }
            }
        );
        deleteArticle.success(function (data, status) {
            if (data.IsSucceed == false) {
                ShowMessage(data.Message, 'e');
            }
            else {
                $scope.articleGet();
                if (data.Message == "True") {
                    ShowMessage("Makale aktifleştirme işlemi başarılı !", 's');
                }
                else {
                    ShowMessage("Makale pasifleştirme işlemi başarılı !", 's');
                }
            }
        });
    }
  

    $scope.articleDelete = function (articleId) {
        swal({
            title: 'Silme İşlemi',
            text: "Seçilen veriyi silmek istediğinize emin misiniz ?",
            type: 'warning',
            showCancelButton: true,
            showCancelButtonClass: 'btn-warning',
            confirmButtonColor: "#d43f3a",
            confirmButtonText: "Evet, silinsin!",
            cancelButtonText: "Hayır, vazgeç!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfrim) {
                if (isConfrim) {
                    var deleteArticle = $http(
                        {
                            method: 'POST',
                            url: articleDeleteUrl,
                            dataType: 'json',
                            data: { articleId: articleId },
                            headers: { "Content-Type": "application/json" }
                        }
                    );
                    deleteArticle.success(function (data, status) {
                        if (data.IsSucceed == false) {
                            ShowMessage(data.Message, 'e');
                        }
                        else {
                            $scope.articleGet();
                            ShowMessage(data.Message, 's');
                        }
                    });
                }
                else {
                    swal({
                        title: "İptal",
                        text: "<strong>Silme İşlemi İptal Edildi.</strong>",
                        type: "error",
                        html: true,
                        timer: 3000
                    });
                }
            } );
            
    }

    $scope.articleGet();

});
