angular.module('app').config(['$routeProvider', function ($routeProvider) {



    $routeProvider.when('/', {
        controller: 'app.controllers.views.people',
        templateUrl: './app/views/people.html'
    });

    $routeProvider.when('/person', {
        controller: 'app.controllers.views.person',
        templateUrl: './app/views/person.html'
    });

    
    $routeProvider.otherwise({ redirectTo: '/' });

}
]);