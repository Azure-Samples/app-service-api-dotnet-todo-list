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
//        "https://{middle tier API app name}.azurewebsites.net/": "{client ID of Azure AD app}"
//    };

//    adalProvider.init(
//        {
//            instance: 'https://login.microsoftonline.com/',
//            tenant: '{your tenant, e.g.: contoso.onmicrosoft.com}',
//            clientId: '{client ID of Azure AD app}',
//            extraQueryParameter: 'nux=1',
//            endpoints: endpoints
//            //cacheLocation: 'localStorage', // enable this for IE, as sessionStorage does not work for localhost.
//        },
//        $httpProvider
//        );

//}]);

