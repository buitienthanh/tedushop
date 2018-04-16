﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tedushop.footers', ['tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('footers', {
            url: "/footers",
            parent: 'base',
            templateUrl: "/app/components/footers/footerListView.html",
            controller: "footerListController"
        }).state('footer_add', {
            url: "/footer_add",
            parent: 'base',
            templateUrl: "/app/components/footers/footerAddView.html",
            controller: "footerAddController"
        }).state('footer_edit', {
            url: "/footer_edit/:id",
            parent: 'base',
            templateUrl: "/app/components/footers/footerEditView.html",
            controller: "footerEditController"
        });
    }
})();