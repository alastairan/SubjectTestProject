angular.module('wizard')
    .controller('subjectController', [
        '$scope',
        'SubjectResource',
        function ($scope, SubjectResource) {
    $scope.subject = {};
    $scope.subjects = SubjectResource.query();
    $scope.units = [
        { id: 1, code: 'ICTPRG527', name: 'Unit No.1', checked: false },
        { id: 2, code: 'ICTPRG224', name: 'Unit No.2', checked: false },
        { id: 3, code: 'ICTPGR334', name: 'Unit No.3', checked: false },
        { id: 4, code: 'ICTPGO504', name: 'Unit No.4', checked: false },
        { id: 5, code: 'ICTPRG527', name: 'Unit No.5', checked: false },
        { id: 6, code: 'ICTPRG527', name: 'Unit No.6', checked: false },
        { id: 7, code: 'ICTPRG527', name: 'Unit No.7', checked: false },
        { id: 8, code: 'ICTPRG527', name: 'Unit No.8', checked: false },
        { id: 9, code: 'ICTPRG527', name: 'Unit No.9', checked: false },
        { id: 10, code: 'ICTPRG527', name: 'Unit No.10', checked: false },
    ];
    $scope.checked_units = [];

    $scope.unitsMaster = angular.copy($scope.units);

    $scope.master = angular.copy($scope.subject);

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