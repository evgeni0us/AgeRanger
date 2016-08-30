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

describe("AgeRanger service", function () {
    debugger;
    beforeEach(module("app"));

    var $q, service, $httpBackend, $scope, payloadData;

    beforeEach(inject(function (_ageRangerService_, _$q_, _$httpBackend_, _$rootScope_) {
        $q = _$q_;
        service = _ageRangerService_;
        $httpBackend = _$httpBackend_;
        $scope = _$rootScope_.$new();

        payloadData = [
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
    }));

    //afterEach(function () {
    //    $httpBackend.verifyNoOutstandingExpectation();
    //    $httpBackend.verifyNoOutstandingRequest();
    //});

    describe("Method loadAllPeople", function () {
        var people = [];
        
        it("should GET a list of people from web api controller", inject(function () {
            $httpBackend.whenGET('./api/ageranger/getallpeople').respond(payloadData);

            service.loadPeople().then(
                function () {
                    people = service.getPeople();

                    $httpBackend.flush();
                    expect(people.length).toBe(3);
                });
        }));
    });


    describe("Method addPerson", function () {
        var newPerson = {
            FirstName: "Eduardo",
            LastName: "Rodriges",
            Age: "45"
        };

        it("should POST a person object to web api controller", inject(function () {
            $httpBackend.whenPOST('./api/ageranger/addperson').respond();
            $httpBackend.whenGET('./api/ageranger/getallpeople').respond(payloadData);

            service.addPerson(newPerson);
            $httpBackend.flush();

            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        }));
    });

    describe("Method updatePerson", function () {
        var modifiedPerson = {
            FirstName: "Eduardo",
            LastName: "Rodriges",
            Age: "45"
        };

        it("should PUT a person object to web api controller", inject(function () {
            $httpBackend.whenPUT('./api/ageranger/updateperson').respond();
            $httpBackend.whenGET('./api/ageranger/getallpeople').respond(payloadData);

            service.updatePerson(modifiedPerson);
            $httpBackend.flush();

            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        }));
    });

    describe("Method deletePerson", function () {
        var personToBeDeleted = {
            Id: 123,
            FirstName: "Eduardo",
            LastName: "Rodriges",
            Age: "45"
        };

        it("should DELETE a person object to web api controller", inject(function () {
            $httpBackend.whenDELETE('./api/ageranger/deleteperson/' + personToBeDeleted.Id).respond();
            $httpBackend.whenGET('./api/ageranger/getallpeople').respond(payloadData);

            service.deletePerson(personToBeDeleted);
            $httpBackend.flush();

            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        }));
    });

});