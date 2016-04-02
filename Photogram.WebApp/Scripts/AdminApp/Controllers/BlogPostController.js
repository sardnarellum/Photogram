var BlogPostController = ["$rootScope", "$scope", "$http", "$routeParams", "BlogPost",
  function ($rootScope, $scope, $http, $routeParams, BlogPost) {
      var self = this;
      self.post = BlogPost.get({ id: $routeParams.postId }, function () {
          $rootScope.viewTitle = "BlogPost - " + self.post.title;
      });

      self.submit = function () {
          self.post.$update({ id: $routeParams.postId });
      }
    
  }];
