/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('contactDetailAddController', contactDetailAddController);

    contactDetailAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function contactDetailAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.contactDetail = {
            //CreatedDate: new Date().toLocaleString('en-US', { timeZone: 'Asia/Jakarta' }),
            Status: true
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.AddContactDetail = AddContactDetail;

        function AddContactDetail() {
            apiService.post('/api/contactdetail/create', $scope.contactDetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('contact_details');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

    }

})(angular.module('tedushop.contact_details'));