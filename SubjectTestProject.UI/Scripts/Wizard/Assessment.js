angular.module('wizard').controller('assessmentController', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
    $http.get('/api/Subjects/GetWizard?code=' + $routeParams.Id + '&what=' + true).success(function (data) {
        $scope.subject = data;
        $scope.unitsMaster = angular.copy($scope.subject);
        $scope.assessments = data.Assessments;
    });
    $scope.checked_units = [];
    $scope.assessment = { Units: [] };
    $scope.master = angular.copy($scope.assessment);
    $scope.addAssessment = function () {
        for (var i = 0; i < $scope.subject.Units.length; i++) {
            if ($scope.subject.Units[i].checked) {
                var unit = {
                    Id: $scope.subject.Units[i].Id,
                    Name: $scope.subject.Units[i].Name,
                    Code: $scope.subject.Units[i].Code
                }
                $scope.assessment.Units.push(unit);
            };
        };
        var tada = $scope.assessment;
        tada.SubjectId = $scope.subject.Id;

        $http.post('/api/Assessment/', tada).success(function (data) {
            tada.Id = data.Id;
            $scope.assessments.push(tada);
            $scope.assessment = angular.copy($scope.master);
            $scope.subject = angular.copy($scope.unitsMaster);
        });
    };
    $scope.removeAssessment = function (asset) {
        $http.delete('/api/Assessment/DeleteSubjectAssessment?id=' + asset.Id + '&SubjectId=' + $scope.subject.Id).success(function () {
            for (var i = 0 ; i < $scope.assessments.length; i++) {
                if ($scope.assessments[i].code == asset.code) {
                    $scope.assessments.splice(i, 1);
                }
            }
        });
    }
}]);