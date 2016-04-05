angular.module("PhotogramAdminApp",
    ["ngRoute", "blogServices", "tagServices", "ngTagsInput", 'ui.bootstrap', "textAngular"])

.filter('search', BlogFilter) // src: BlogController.js

.controller("MainCtrl", MainController)
.controller("DashCtrl", DashboardController)
.controller("BlogCtrl", BlogController) // src: BlogController.js
.controller("BlogPostCtrl", BlogPostController)

.config(["$routeProvider", "$locationProvider",
  function ($routeProvider, $locationProvider) {
      $routeProvider
          .when("/dashboard", {
              templateUrl: "../Templates/Dashboard",
              controller: "DashCtrl"
          })
          .when("/blog", {
              templateUrl: "../Templates/Blog",
              controller: "BlogCtrl as ctrl"
          })
          .when("/blog/:postId", {
              templateUrl: "../Templates/BlogPost",
              controller: "BlogPostCtrl as ctrl"
          })
          .otherwise({
            redirectTo: "/dashboard"
          });
      $locationProvider.html5Mode(true);
  }]);