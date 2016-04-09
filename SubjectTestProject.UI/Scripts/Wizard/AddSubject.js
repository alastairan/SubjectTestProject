angular.module('wizard')
    .controller('addSubjectController', [
        '$scope',
        '$http',
        function ($scope, $http) {
            $scope.subject = {Units:[]};
            $scope.subjects = [];
            $scope.searchMessage = null;
            $scope.searchCourse = function () {
                $scope.searchMessage = "Searching...";
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
                    $scope.searchMessage = null;
                    $scope.tab = 2;
                }, function Error(response) {
                    $scope.searchMessage = response.statusText;
                });
            };
            $scope.selectUnits = function () {
                angular.forEach($scope.course.Units, function (u) {
                    if (u.checked) {
                        u.checked = false;
                    } else {
                        u.available = false;
                    }
                    $scope.tab = 3;
                });
            };
            $scope.previous = function () {
                $scope.tab = 1;
            };
            $scope.goBack = function () {
                angular.forEach($scope.course.Units, function (u) {
                    if (u.checked) {
                        u.checked = false;
                    }
                    if (u.IsEssential) {
                        u.checked = true;
                    }
                    if (!u.available) {
                        u.available = true;
                    }
                });
                $scope.tab = 2;
            };
    }]);