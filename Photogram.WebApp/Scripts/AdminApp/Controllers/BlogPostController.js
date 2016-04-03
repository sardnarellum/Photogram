var BlogPostController = ["$rootScope", "$scope", "$http", "$routeParams", "BlogPost", "Tag",
  function ($rootScope, $scope, $http, $routeParams, BlogPost, Tag) {
      var self = this;
      self.submitable = false;
      self.messages = [];
      self.post = BlogPost.get({ id: $routeParams.postId }, function () {
          self.submitable = true;
          $rootScope.viewTitle = "BlogPost - " + self.post.title;
      });

      self.submit = function () {
          self.submitable = false;
          self.post.$update({ id: $routeParams.postId }, function () {
              $scope.editPostForm.$setPristine();
              self.submitable = true;
          }, function (error) {
              self.submitable = true;
              if (error.status == 500) {
                  self.messages.push({
                      type: "danger",
                      text: "Something went wrong on the server, try later."
                  });
              } else {
                  self.messages.push({
                      type: "danger",
                      text: "Unknown error, try later."
                  });
              }
              console.log(self.messages);
          });
      }

      self.closeAlert = function (index) {
          self.messages.splice(index, 1);
      }

      self.loadTags = function (query) {
          return Tag.query({ searchTerm: query }).$promise;
      }    
  }];
