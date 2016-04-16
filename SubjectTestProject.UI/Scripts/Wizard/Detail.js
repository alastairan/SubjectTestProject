angular.module('wizard')
    .controller('detailController', [
        '$scope',
        '$http',
        '$routeParams',
        function ($scope, $http, $routeParams) {
            $http.get('/api/Detail/' + $routeParams.Id).success(function (data) {
                $scope.subject = data;
            });
        }]);