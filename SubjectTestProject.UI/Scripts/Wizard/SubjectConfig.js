angular.module('wizard').config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/App/Views/Start.html',
        controller: 'subjectController',
        controllerAs: 'subjectCtrl'
    }).when('/add', {
        templateUrl: '/App/Views/AddSubject.html',
        controller: 'addSubjectController',
        controllerAs: 'addSubjectCtrl'
    }).when('/wizard/:id', {
        templateUrl: '/Views/Subject/Wizard.html'
    }).when('/details/:id', {
        templateUrl: '/Views/Subject/Details.html'
    }).otherwise({
        redirectTo: '/'
    });
}]);