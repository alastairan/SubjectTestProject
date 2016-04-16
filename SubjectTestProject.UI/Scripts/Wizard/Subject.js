angular.module('wizard')
    .controller('subjectController', [
        '$scope',
        '$http',
        function ($scope, $http) {
            $scope.subject = {};
            $http.get('/api/Subjects/').success(function (data) {
                $scope.subjects = data;
            });
            $scope.removeSubject = function (data) {
                $http.delete('/api/SubjectDelivery/' + data.Id).success(function () {
                    for (var i = 0 ; i < $scope.subjects.length; i++) {
                        if ($scope.subjects[i].Id == data.Id) {
                            $scope.subjects.splice(i, 1);
                        }
                    }
                });
            };
}]);