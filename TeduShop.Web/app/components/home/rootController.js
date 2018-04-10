(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
            loginService.reload();
        }
        $scope.authentication = authData.authenticationData;

        //authenticationService.validateRequest();
    }
})(angular.module('tedushop'));