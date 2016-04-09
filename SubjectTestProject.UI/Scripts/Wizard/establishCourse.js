angular.module('wizard').directive('establishCourse', function () {
    return {
        templateUrl: '/App/Views/AddSubject.html',
        controller: function ($scope) {
            $scope.selectUnits = function () {
                angular.forEach($scope.course.Units, function (u) {
                    if (!u.checked) {
                        u.available = false;
                    }
                });
            };
            $scope.previous = function () {
                $scope.tab = 1;
            }
        }
    }
});