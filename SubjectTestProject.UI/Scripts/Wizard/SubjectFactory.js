angular.module('wizard').factory('SubjectResource',['$resource', function($resource){
    return $resource('/api/Subjects/:id', { id: '@Id' }, {
        'update': { method: 'PUT' }
    });
}]);