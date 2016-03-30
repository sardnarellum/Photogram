angular.module("PhotogramAdminApp", ["ngRoute", "blogServices"])

.filter('search', BlogFilter) // src: BlogController.js

.controller("MainCtrl", MainController)
.controller("DashCtrl", DashboardController)
.controller("BlogCtrl", BlogController) // src: BlogController.js

.config(["$routeProvider", "$locationProvider",
  function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", { 
            templateUrl: "Dashboard",
            controller: "DashCtrl"
        })
        .when("/blog", {
            templateUrl: "Blog",
            controller: "BlogCtrl as ctrl"
        });
    $locationProvider.html5Mode(true);
}]);