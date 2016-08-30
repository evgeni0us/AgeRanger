(function () {

    var app = angular.module('app');

    app.factory('ageRangerService', ['$http', function ($http, $log) {


        var people = [];
        var ageGroups = [];

        ////////////////////////////////
        // Loading People
        ////////////////////////////////
        var loadPeople = function () {
            var promise = $http.get('./api/ageranger/getallpeople');

            promise.then(
                function (payload) {

                    people.length = 0;

                    payload.data.forEach(function (person) {
                        people.push(person);
                    });

                },
                function (errorPayload) {
                    $log.error('failure loading people', errorPayload);

                });

            return promise;
        }

        var getPeople = function () {
            return people;
        };

        ////////////////////////////////
        // Loading AgeGroups
        ////////////////////////////////
        var loadAgeGroups = function () {
            var promise = $http.get('./api/ageranger/getallagegroups');

            promise.then(
                function (payload) {

                    ageGroups.length = 0;

                    payload.data.forEach(function (ageGroup) {
                        ageGroups.push(ageGroup);
                    });

                },
                function (errorPayload) {
                    $log.error('failure loading age groups', errorPayload);

                });

            return promise;
        }

        var getAgeGroups = function () {
            return ageGroups;
        };

        ////////////////////////////////
        // Add New Person
        ////////////////////////////////
        var addPerson = function (person) {

            var promise = $http.post('./api/ageranger/addperson', person);

            promise.then(
                function () {
                    loadPeople();
                },
                function (errorPayload) {
                    $log.error('failure adding new person', errorPayload);
                });

            return promise;
        };


        ////////////////////////////////
        // Update Person
        ////////////////////////////////
        var updatePerson = function (person) {

            var promise = $http.put('./api/ageranger/updateperson', person);

            promise.then(
                function () {
                    loadPeople();
                },
                function (errorPayload) {
                    $log.error('failure updating person', errorPayload);
                });

            return promise;
        };

        ////////////////////////////////
        // Delete Person
        ////////////////////////////////
        var deletePerson = function (person) {

            var promise = $http.delete('./api/ageranger/deleteperson/' + person.Id);

            promise.then(
                function () {
                    loadPeople();
                },
                function (errorPayload) {
                    $log.error('failure deleting person', errorPayload);
                });

            return promise;
        };


        return {
            loadPeople: loadPeople,
            getPeople: getPeople,
            loadAgeGroups: loadAgeGroups,
            getAgeGroups: getAgeGroups,
            addPerson: addPerson,
            updatePerson: updatePerson,
            deletePerson: deletePerson
        }

    }]);

})();