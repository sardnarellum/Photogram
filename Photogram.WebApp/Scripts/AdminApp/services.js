var blogServices = angular.module('blogServices', ['ngResource']);

blogServices.factory('BlogPost', ['$resource',
    function ($resource) {
        return $resource('../api/blog/:id', {}, {
            query: { method: 'GET', params: {}, isArray: true },
            update: { method: 'PUT' }
        });
    }
]);