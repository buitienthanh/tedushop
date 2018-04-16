/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerAddController', footerAddController);

    footerAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function footerAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.footer = {}
        $scope.AddFooter = AddFooter;

        function AddFooter() {
            apiService.post('/api/footer/create', $scope.footer,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('footers');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    
    }

})(angular.module('tedushop.footers'));