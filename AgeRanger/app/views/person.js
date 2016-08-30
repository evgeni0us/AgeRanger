(function () {
    var app = angular.module('app');

    var controllerId = 'app.controllers.views.person';
    app.controller(controllerId,
        function ($scope, ageRangerService, person, $uibModalInstance, newPerson) {

            $scope.person = jQuery.extend(true, {}, person);;

            if (newPerson) {
                $scope.title = "New Person";
            } else {
                $scope.title = "Edit Person";
            }

            $scope.submit = function () {
                var msg = 'Are you sure you want to proceed?';

                bootbox.confirm({
                    message: msg,
                    callback: function (result) {
                        if (result) {

                            if (newPerson) {
                                var promise = ageRangerService.addPerson($scope.person);

                                promise.then(
                                    function (payload) {
                                        
                                    },
                                    function (errorPayload) {
                                        bootbox.alert(errorPayload.data);
                                    });;

                            } else {
                                var promise = ageRangerService.updatePerson($scope.person);

                                promise.then(
                                    function (payload) {

                                    },
                                    function (errorPayload) {
                                        bootbox.alert(errorPayload.data);
                                    });;
                            }

                           
                            $uibModalInstance.close($scope.person);
                            

                            return;
                        } else {
                            return;
                        }
                    }
                });

            };


            $scope.close = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    );
})();
