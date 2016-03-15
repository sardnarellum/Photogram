var PhotogramAdminApp = angular.module("PhotogramAdminApp", ["ngRoute"]);

PhotogramAdminApp.controller("DashboardController", DashboardController);


var configFunction = function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "Dashboard",
            controller: "DashboardController"
        })
        .when("/blog", {
            templateUrl: "Blog",
            controller: BlogController
        });
    $locationProvider.html5Mode(true);
};

configFunction.$inject = ["$routeProvider", "$locationProvider"];

PhotogramAdminApp.config(configFunction);