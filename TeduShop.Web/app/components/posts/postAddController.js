/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('postAddController', postAddController);

    postAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function postAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.post = {
            //CreatedDate: new Date().toLocaleString('en-US', { timeZone: 'Asia/Jakarta' }),
            Status: true
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.AddPost = AddPost;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.post.Alias = commonService.getSeoTitle($scope.post.Name);
        }

        function AddPost() {
            $scope.post.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/post/create', $scope.post,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('posts');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadPostCategory() {
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

        loadPostCategory();
    }

})(angular.module('tedushop.posts'));