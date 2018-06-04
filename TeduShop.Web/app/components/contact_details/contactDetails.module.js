/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tedushop.contact_details', ['tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('contact_details', {
            url: "/contact_details",
            parent: 'base',
            templateUrl: "/app/components/contact_details/contactDetailListView.html",
            controller: "contactDetailListController"
        }).state('contact_detail_add', {
            url: "/contact_detail_add",
            parent: 'base',
            templateUrl: "/app/components/contact_details/contactDetailAddView.html",
            controller: "contactDetailAddController"
        }).state('contact_detail_edit', {
            url: "/contact_detail_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/contact_details/contactDetailtEditView.html",
            controller: "contactDetailEditController"
        });
    }
})();