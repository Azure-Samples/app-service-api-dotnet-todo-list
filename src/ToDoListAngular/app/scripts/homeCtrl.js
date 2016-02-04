//
// Use the following code for running without authentication.
//
'use strict';
angular.module('todoApp')
.controller('homeCtrl', ['$scope','$location', function ($scope, $location) {
    $scope.isActive = function (viewLocation) {        
        return viewLocation === $location.path();
    };
}]);


//
// Use the following code for running with authentication.
//
//'use strict';
//angular.module('todoApp')
//.controller('homeCtrl', ['$scope', 'adalAuthenticationService','$location', function ($scope, adalService, $location) {
//    $scope.login = function () {
//        adalService.login();
//    };
//    $scope.logout = function () {
//        adalService.logOut();
//    };
//    $scope.isActive = function (viewLocation) {        
//        return viewLocation === $location.path();
//    };
//}]);
