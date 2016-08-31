///<reference path="~/Scripts/jasmine/lib/jasmine-core/jasmine.js"/>
///<reference path="~/Scripts/angularjs/angular.js"/>
///<reference path="~/Scripts/angular-mocks/angular-mocks.js"/>
///<reference path="~/Scripts/angular-route/angular-route.js"/>
///<reference path="~/Scripts/angular-bootstrap/ui-bootstrap.js"/>

///<reference path="~/App/app.js"/>
///<reference path="~/App/app.routes.js"/>
///<reference path="~/App/Views/people.js"/>
///<reference path="~/App/Views/person.js"/>
///<reference path="~/App/Services/ageRangerService.js"/>

describe('Controller: People', function () {
    var scope, ageRangerService, $location;

    beforeEach(function () {
        var mockAgeRangerService = {};
        module('app', function ($provide) {
            $provide.value('ageRangerService', mockAgeRangerService);
        });

        inject(function ($q) {
            mockAgeRangerService.data = [
                {
                    "Id": "0",
                    "FirstName": "William",
                    "LastName": "Blake",
                    "Age": "76",
                    "AgeGroup": "OldMan"

                },
                {
                    "Id": "1",
                    "FirstName": "Lewis",
                    "LastName": "Carroll",
                    "Age": "45",
                    "AgeGroup": "Teenager"

                },
                {
                    "Id": "2",
                    "FirstName": "Edgar",
                    "LastName": "Poe",
                    "Age": "12",
                    "AgeGroup": "Child"

                }
            ];

            mockAgeRangerService.loadPeople = function () {
                var defer = $q.defer();

                defer.resolve(this.data);

                return defer.promise;
            };

            mockAgeRangerService.getPeople = function () {

                return this.data;
            };
        });
    });

    beforeEach(inject(function ($controller, $rootScope, _$location_, _ageRangerService_) {
        scope = $rootScope.$new();
        $location = _$location_;
        ageRangerService = _ageRangerService_;

        $controller('ListLibrariesCtrl', { $scope: scope, $location: $location, ageRangerService: ageRangerService });

        scope.$digest();
    }));

    it('should contain all the libraries at startup', function () {
        expect(scope.libraries).toEqual([
          {
              id: 0,
              name: 'Angular'
          },
          {
              id: 1,
              name: 'Ember'
          },
          {
              id: 2,
              name: 'Backbone'
          },
          {
              id: 3,
              name: 'React'
          }
        ]);
    });

   
});