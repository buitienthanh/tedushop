
/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerEditController', footerEditController);

    footerEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function footerEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.footer = {
            Status: true
        }

        $scope.UpdateFooter = UpdateFooter;
       
        function loadFooterDetail() {
            apiService.get('/api/footer/getbyid/' + $stateParams.id, null, function (result) {
                $scope.footer = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateFooter() {
            apiService.put('/api/footer/update', $scope.footer,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('footers');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
     

      
        loadFooterDetail();
    }

})(angular.module('tedushop.footers'));