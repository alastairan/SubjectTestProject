angular.module('wizard').config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: '/App/Views/Start.html',
        controller: 'subjectController',
        controllerAs: 'subjectCtrl'
    }).when('/add', {
        templateUrl: '/App/Views/AddSubject.html',
        controller: 'addSubjectController',
        controllerAs: 'addSubjectCtrl'
    }).when('/wizard/:Id', {
        templateUrl: '/App/Views/Wizard.html',
        controller: 'assessmentController',
        controllerAs: 'assessmentCtrl'
    }).when('/details/:Id', {
        templateUrl: '/App/Views/Detail.html',
        controller: 'detailController',
        controllerAs: 'detailCtrl'
    }).otherwise({
        redirectTo: '/'
    });
}]);