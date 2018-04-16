/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('postEditController', postEditController);

    postEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function postEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.post = {
            Status: true
        }
        
        $scope.UpdatePost = UpdatePost;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }
        function loadPostDetail() {
            apiService.get('/api/post/getbyid/' + $stateParams.id, null, function (result) {
                $scope.post = result.data;
                $scope.moreImages = JSON.parse($scope.post.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePost() {
            $scope.post.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/post/update', $scope.post,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('posts');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadPostCategories() {
            apiService.get('/api/postcategory/getallparents', null, function (result) {
                $scope.postCategories = result.data;
            }, function () {
                console.log('Cannot get list postCategories');
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.post.Image = fileUrl;
                })
            }
            finder.popup();
        }
        
        loadPostCategories();
        loadPostDetail();
    }

})(angular.module('tedushop.posts'));