var controllerName = "categoryController";
var categoryDeletedUrl = "/panel/article/categoryDeleted";
var getCategoriesUrl = "/panel/article/getCategories";
var app = angular.module('myblogAdmin', ['ui.bootstrap']);
app.controller(controllerName, function ($scope, $http) {

    $scope.getCategories = function () {
        var getCategories = $http({
            method: "POST",
            url: getCategoriesUrl,
            dataType: 'json',
            data: {}
        });
        getCategories.success(function (data, status) {
            if (data.IsSucceed == false) {
                ShowMessage(data.Message, 'e');
            }
            else {
                $scope.categories = data.Result;
            }

        });


    }

    $scope.categoryDelete = function (id) {
        var categoryId = id;
        var categoryDeleted = $http({
            method: 'POST',
            url: categoryDeletedUrl,
            dataType: 'json',
            data: { id: categoryId }
        });

        categoryDeleted.success(function (data, status) {
            if (data.IsSucceed == false) {
                ShowMessage('İşlem başarısız oldu.', 'e');
            }
            else  {
                ShowMessage("naber gurbet işlem başarılı", 's');
                $scope.getCategories();
            }



        });

    }

    $scope.getCategories();
});