angular.module('wizard').directive('searchCourse', function () {
    return {
        templateUrl: '/App/Views/AddSubject.html',
        controller: function ($scope) {
            $scope.searchCourse = function () {
                $http.get('/api/Course/Search?code=' + $scope.code).then(function Success(response) {
                    $scope.course = response.data;
                    angular.forEach($scope.course.Units, function (u) {
                        if (u.IsEssential) {
                            u.checked = true;
                        } else {
                            u.checked = false;
                        }
                        u.available = true;
                    });
                    $scope.tab === 2;
                }, function Error(response) {
                    $scope.course = response.statusText;
                });
            };
        }
    }
});