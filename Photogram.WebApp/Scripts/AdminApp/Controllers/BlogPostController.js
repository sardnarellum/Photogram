var BlogPostController = ["$rootScope", "$scope", "$http", "$routeParams", "BlogPost", "Tag",
  function ($rootScope, $scope, $http, $routeParams, BlogPost, Tag) {
      var self = this;
      self.post = BlogPost.get({ id: $routeParams.postId }, function () {
          $rootScope.viewTitle = "BlogPost - " + self.post.title;
      });

      self.submit = function () {
          self.post.$update({ id: $routeParams.postId });
      }

      self.loadTags = function (query) {
          return Tag.query({ searchTerm: query }).$promise;
      }    
  }];
