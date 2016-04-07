angular.module('wizard')
    .controller('addSubjectController', [
        '$scope',
        '$http',
        function ($scope, $http) {
            $scope.subject = {};
            $scope.subjects = [];
            $scope.course = [];
            $scope.searchCourse = function () {
                $scope.course = $http.get('/api/Course/Search?code=' + $scope.code);
            };
        }]);