var BlogController = ["$rootScope", "$scope", "$http", 'searchFilter', "BlogPost",
  function ($rootScope, $scope, $http, searchFilter, BlogPost) {
    $rootScope.viewTitle = "Blog";

    var self = this;
    self.posts = BlogPost.query();
    self.errorMsg = '';
    self.sortType = "modified";
    self.sortReverse = true;
    self.searchTerm = "";

    self.model = {};
    self.model.allChecked = false;
    self.filtered = searchFilter(self.posts);

    self.addPost = function () {
        var addable = new BlogPost();
        addable.title = self.newPostTitle;
        addable.$save(function (response) {
            self.newPostTitle = '';
            $scope.addPostForm.$setPristine();
            self.posts.push(response);
        });
    };

    self.selectPost = function () {
        // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
        for (var i = 0; i < self.posts.length; i++) {
            if (!self.posts[i].checked) {
                self.model.allChecked = false;
                return;
            }
        }

        //If not the check the "allItemsSelected" checkbox
        self.model.allChecked = true;
    };

    self.selectAll = function () {
        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < self.posts.length; ++i) {
            self.posts[i].checked = self.model.allChecked;
        }
    };

    self.checkedCount = function () {
        var c = 0;
        for (var i = 0; i < self.posts.length; ++i) {
            if (self.posts[i].checked) {
                ++c;
            }
        }

        return c;
    }

    self.deletePost = function () {
        var deletables = [];

        angular.forEach(self.posts, function (value, key) {
            if (value.checked) {
                this.push({ id: value.id });
            };
        }, deletables);

        angular.forEach(deletables, function (value, key) {
            BlogPost.delete({ "id": value.id }, function () {
                for (var i = 0; i < self.posts.length; ++i) {
                    if (self.posts[i].id == value.id) {
                        self.posts.splice(i, 1);
                        return;
                    }
                }
            });
        });
    }

    self.setVisibility = function (visible) {
        var targets = [];

        angular.forEach(self.posts, function (value, key) {
            if (value.checked) {
                this.push({
                    id: value.id,
                    visibility: visible,
                    title: value.title
                });
            }
        }, targets);

        angular.forEach(targets, function (value, key) {
            var post = BlogPost.get({ "id": value.id });
            post.title = value.title
            post.visible = value.visibility;
            post.$update({ "id": value.id }, function (res) {
                for (var i = 0; i < self.posts.length; ++i) {
                    if (self.posts[i].id == value.id) {
                        self.posts[i].visible = res.visible;
                        return;
                    }
                }
            });
        });
    }
  }];

var BlogFilter = function () {
    return function (posts, searchText) {
        var filtered = [];

        if (searchText != '') {
            for (var i = 0; i < posts.length; ++i) {
                if (posts[i].title.search(searchText) != -1) {
                    filtered.push(posts[i]);
                }
            }
        }
        else {
            filtered = posts;
        }

        //if (filtered.length == 0) {
        //    console.log("filtered is empty.");
        //}
        //else {
        //    var str = "filtered titles:"
        //    for (var i = 0; i < filtered.length; ++i) {
        //        str += " " + (i + 1) + ":\"" + filtered[i].title + "\" [" + filtered[i].checked + "]";
        //    }
        //    console.log(str);
        //}
        return filtered;
    }
};