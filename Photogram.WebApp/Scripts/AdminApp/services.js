var blogServices = angular.module('blogServices', ['ngResource']);

blogServices.factory('BlogPost', ['$resource',
    function ($resource) {
        return $resource('../api/blog/:id', {}, {
            query: { method: 'GET', params: {}, isArray: true },
            update: { method: 'PUT' }
        });
    }
]);

var tagServices = angular.module('tagServices', ['ngResource']);

blogServices.factory('Tag', ['$resource',
    function ($resource) {
        return $resource('../api/tag/', {}, {
            query: { method: 'GET', params: { searchTerm:'' }, isArray: true },
            update: { method: 'PUT' }
        });
    }
]);