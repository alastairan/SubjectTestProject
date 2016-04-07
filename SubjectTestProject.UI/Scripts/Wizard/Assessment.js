angular.module('wizard').controller('assessmentController', ['$scope', 'SubjectResource', function ($scope, SubjectResource) {
    $scope.subject = SubjectResource.get({ id: 1 });
    $scope.assessments = [];
    $scope.units = [
        { id: 1, name: 'ICTPRG527', checked: false },
        { id: 2, name: 'ICTPRG224', checked: false },
        { id: 3, name: 'ICTPGR334', checked: false }];
    $scope.checked_units = [];

    $scope.unitsMaster = angular.copy($scope.units);

    $scope.asset = {
        units: []
    };

    $scope.master = angular.copy($scope.asset);

    $scope.addAssessment = function () {
        var tada = $scope.asset
        $scope.addUnit(tada);
        $scope.assessments.push(tada);
        $scope.asset = angular.copy($scope.master);
        $scope.units = angular.copy($scope.unitsMaster);
    };
    $scope.addUnit = function (asset) {
        for (var i = 0; i < $scope.units.length; i++) {
            if ($scope.units[i].checked) {
                $scope.asset.units.push($scope.units[i].name);
            };
        };
    };
    $scope.isChecked = function (unit) {
        var match = false;
        for (var i = 0 ; i < $scope.checked_units.length; i++) {
            if ($scope.checked_units[i] == unit) {
                match = true;
            }
        }
        return match;
    };
    $scope.sync = function (bool, unit) {
        if (bool) {
            // add item
            $scope.checked_units.push(unit);
        } else {
            // remove item
            for (var i = 0 ; i < $scope.checked_units.length; i++) {
                if ($scope.checked_units[i] == unit) {
                    $scope.checked_units.splice(i, 1);
                }
            }
        }
    };
    $scope.removeAssessment = function (asset) {
        for (var i = 0 ; i < $scope.assessments.length; i++) {
            if ($scope.assessments[i].code == asset.code) {
                $scope.assessments.splice(i, 1);
            }
        }
    }
}]);