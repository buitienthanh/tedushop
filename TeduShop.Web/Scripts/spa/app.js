/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module('myModule', []);

myApp.controller("myController", myController);
myApp.directive("teduShopDirective", teduShopDirective);
myApp.service("Validator", Validator);
myController.$inject = ['$scope', 'Validator'];

function myController($scope, Validator) {
    $scope.checkNumber = function () {
        $scope.message = Validator.checkNumber($scope.num);
    }
    $scope.num = 1;
}

function Validator($window) {
    return {
        checkNumber: checkNumber
    }
    function checkNumber(input){
        if(input%2==0){
            return 'this is even';
        }
        else
            return 'this is odd';
    }
 
}

function teduShopDirective() {
    return {
        template:"<h1>this is my template</h1>"
    }
}