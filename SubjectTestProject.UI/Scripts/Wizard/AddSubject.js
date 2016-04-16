angular.module('wizard')
    .controller('addSubjectController', [
        '$scope',
        '$http',
        function ($scope, $http) {
            $scope.Subject = {Units:[]};
            $scope.subjects = [];
            $scope.subjectDelivery = {};
            $scope.courseDelivery = {};
            $scope.DeliveryMode = {};
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
            $scope.createCourseDelivery = function () {
                $http.post('/api/CourseDelivery/', $scope.courseDelivery).success(function (data) {
                    $scope.courseDeliveryId = data.Id;
                });
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
            $scope.addSubect = function () {
                angular.forEach($scope.course.Units, function (u) {
                    if (u.checked) {
                        u.checked = false;
                        u.available = false;
                        $scope.Unit = {
                            Id: u.Id,
                            Name: u.Name,
                            Code: u.Code
                        }
                        $scope.Subject.Units.push($scope.Unit);
                        $scope.Unit = {};
                    }
                });
                $scope.Subject.CourseId = $scope.course.Id;
                $scope.Subject.CourseDeliveryId = $scope.courseDeliveryId;
                $http.post('/api/SubjectDelivery/', $scope.Subject).success(function (data) {
                    $scope.subjects.push(data);
                });
                $scope.Subject = { Units: [] };
            };
            $scope.removeSubject = function (data) {
                $scope.AddUnit = [];
                angular.forEach(data.Units, function (d) {
                    $scope.AddUnit.push(d.Id);
                });
                angular.forEach($scope.course.Units, function (u) {
                    if (!u.available) {
                        for (var i = 0; i < data.Units.length; i++) {
                            if (data.Units[i].Id == u.Id) {
                                u.available = true;
                            }
                        }
                    }
                });
                $http.delete('/api/SubjectDelivery/' + data.Id).success(function () {
                    for (var i = 0 ; i < $scope.subjects.length; i++) {
                        if ($scope.subjects[i].Id == data.Id) {
                            $scope.subjects.splice(i, 1);
                        }
                    }
                });
            }
    }]);