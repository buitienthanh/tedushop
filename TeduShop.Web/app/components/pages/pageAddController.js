/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('pageAddController', pageAddController);

    pageAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function pageAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.page = {
            //CreatedDate: new Date().toLocaleString('en-US', { timeZone: 'Asia/Jakarta' }),
            Status: true
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.AddPage = AddPage;
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

        function AddPage() {
            apiService.post('/api/page/create', $scope.page,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }

})(angular.module('tedushop.pages'));