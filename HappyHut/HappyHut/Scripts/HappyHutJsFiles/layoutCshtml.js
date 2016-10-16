var HappyHutApp = angular.module('HappyHutApp', ['ui.bootstrap', 'ngAnimate']);

if (typeof (cityTypeAheadController) === 'function')
    HappyHutApp.controller('cityTypeAheadController', ['$scope', 'serverCallMakerFactory', 'focusFactory', cityTypeAheadController]);
if (typeof (enterDataController) === 'function')
    HappyHutApp.controller('enterDataController', ['$scope', enterDataController]);
HappyHutApp.factory('serverCallMakerFactory', ['$http', serverCallMakerFactory]);
HappyHutApp.factory('focusFactory', ['$timeout', '$window', focusFactory]);
HappyHutApp.directive('focusMe', ['focusFactory', focusMe]);

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

function focusFactory($timeout, $window) {
    var id = '';
    var focusFactory = {};

    focusFactory.focus = function (id) {
        // timeout makes sure that it is invoked after any other event has been triggered.
        // e.g. click events that need to run before the focus or
        // inputs elements that are in a disabled state but are enabled when those events
        // are triggered.
        $timeout(function () {
            var element = $window.document.getElementById(id);
            if (element)
                element.focus();
        });
    };
    return focusFactory;
}

function focusMe(focusFactory) {
    return function (scope, elem, attr) {
        elem.on(attr.eventFocus, function () {
            focusFactory.focus(attr.eventFocusId);
        });

        // Removes bound events in the element itself
        // when the scope is destroyed
        scope.$on('$destroy', function () {
            elem.off(attr.eventFocus);
        });
    };
}