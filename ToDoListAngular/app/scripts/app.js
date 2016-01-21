//
// Use the following code for running without authentication.
//
'use strict';
angular.module('todoApp', ['ngRoute'])
.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider.when("/Home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html",
    }).when("/TodoList", {
        controller: "todoListCtrl",
        templateUrl: "/App/Views/TodoList.html",
    }).when("/UserData", {
        controller: "userDataCtrl",
        templateUrl: "/App/Views/UserData.html",
    }).otherwise({ redirectTo: "/Home" });
}]);


//
// Use the following code for running with authentication.
//
//'use strict';
//angular.module('todoApp', ['ngRoute', 'AdalAngular'])
//.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

//    $routeProvider.when("/Home", {
//        controller: "homeCtrl",
//        templateUrl: "/App/Views/Home.html",
//    }).when("/TodoList", {
//        controller: "todoListCtrl",
//        templateUrl: "/App/Views/TodoList.html",
//        requireADLogin: true,
//    }).when("/UserData", {
//        controller: "userDataCtrl",
//        templateUrl: "/App/Views/UserData.html",
//    }).otherwise({ redirectTo: "/Home" });

//    var endpoints = {
//        "https://todolistapi0118.azurewebsites.net/": "e68ca76d-1922-4f40-8872-86e05cc7450c"
//    };

//    adalProvider.init(
//        {
//            instance: 'https://login.microsoftonline.com/',
//            tenant: 'tomdykstra.onmicrosoft.com',
//            //clientId: '3271c206-5b87-4547-aea8-04177743474b',
//            clientId: 'e68ca76d-1922-4f40-8872-86e05cc7450c',
//            extraQueryParameter: 'nux=1',
//            endpoints: endpoints
//            //cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.
//        },
//        $httpProvider
//        );

//}]);

