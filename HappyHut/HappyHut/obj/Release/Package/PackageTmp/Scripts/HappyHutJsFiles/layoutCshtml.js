var HappyHutApp = angular.module('HappyHutApp', ['ui.bootstrap', 'ngAnimate']);

if (typeof (cityTypeAheadController) === 'function')
    HappyHutApp.controller('cityTypeAheadController', ['$scope', 'serverCallMakerFactory', cityTypeAheadController]);
if (typeof (pestControlController) === 'function')
    HappyHutApp.controller('pestControlController', ['$scope', pestControlController]);
HappyHutApp.factory('serverCallMakerFactory', ['$http', serverCallMakerFactory]);

function serverCallMakerFactory($http) {
    var httpURL = '';
    var httpparams;
    var httpMethod = '';
    var serverCallMakerFactory = {};

    serverCallMakerFactory.callServer = function (method, url, params) {
        httpMethod = method;
        httpURL = url;
        httpparams = params;
        return $http({ method: httpMethod, url: httpURL, params: httpparams });
    };

    return serverCallMakerFactory;
}