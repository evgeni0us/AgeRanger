(function () {
    var app = angular.module('app');

    var controllerId = 'app.controllers.views.people';
    app.controller(controllerId,
        function ($scope, $rootScope, $log, $uibModal, $timeout, ageRangerService, $sce) {

            // Loading all people from WebApi service
            var promise = ageRangerService.loadPeople();

            promise.then(
                function () {},
                function (errorPayload) {
                    bootbox.alert(errorPayload.data);

                });

            $scope.people = ageRangerService.getPeople();

            // Loading all age groups from WebApi service
            var promiseAgeGroups = ageRangerService.loadAgeGroups();

            promiseAgeGroups.then(
                function () {},
                function (errorPayload) {
                    bootbox.alert(errorPayload.data);

                });

            $scope.ageGroups = ageRangerService.getAgeGroups();


            $scope.filterText = '';

            var tempFilterText = '',
            filterTextTimeout;
            $scope.$watch('searchText',
                function(val) {
                    if (filterTextTimeout) $timeout.cancel(filterTextTimeout);

                    tempFilterText = val;
                    filterTextTimeout = $timeout(function() {
                            $scope.filterText = tempFilterText;
                        },
                        250); // delay 250 ms
                });



            $scope.openEditForm = function(person) {

                var modalInstance = $uibModal.open({
                    templateUrl: 'app/views/person.html',
                    controller: 'app.controllers.views.person',
                    size: 'md',
                    resolve: {
                        person: function() {
                            return person;
                        },
                        newPerson: function () {
                            return false;
                        }
                    }
                });

                modalInstance.result.then(function (request) {
                    // reload people
                    ageRangerService.loadPeople();
                },
                    function () {
                        // do nothing - user clicked Esc
                    });


            }

            $scope.openNewPersonForm = function () {

                var modalInstance = $uibModal.open({
                    templateUrl: 'app/views/person.html',
                    controller: 'app.controllers.views.person',
                    size: 'md',
                    resolve: {
                        person: function () {
                            return {
                                FirstName: "",
                                LastName: "",
                                Age: ""
                            };
                        },
                        newPerson: function () {
                            return true;
                        }
                    }
                });

                modalInstance.result.then(function (request) {
                    
                },
                function () {
                    // do nothing - user clicked Esc
                });
            }

            $scope.delete = function (person) {

                var msg = 'Are you sure you want to delete person ' + person.FirstName + ' ' + person.LastName + '?';

                bootbox.confirm({
                    message: msg,
                    callback: function (result) {
                        if (result) {
                            ageRangerService.deletePerson(person);

                            return;
                        } else {
                            return;
                        }
                    }
                });
            }

            $scope.highlight = function (text, search) {
                if (!search) {
                    return $sce.trustAsHtml(text);
                }
                return $sce.trustAsHtml(text.replace(new RegExp(search, 'gi'), '<span class="highlightedText">$&</span>'));
            };

            $scope.search = function (row) {
                return (angular.lowercase(row.FirstName).indexOf(angular.lowercase($scope.filterText) || '') !== -1 ||
                        angular.lowercase(row.LastName).indexOf(angular.lowercase($scope.filterText) || '') !== -1);
            };
        }
    );
})();
