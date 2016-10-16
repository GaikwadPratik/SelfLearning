'use strict';
var JavascriptUtilsNS = (function (nameSpace) {
    let getFuncFromString = function (func) {
        // if already a function, return
        if (typeof func === 'function') return func;
        // if string, try to find function or method of object (of "obj.func" format)
        if (typeof func === 'string') {
            if (!func.length) return null;
            var target = window;
            var func = func.split('.');
            while (func.length) {
                var ns = func.shift();
                if (typeof (target[ns]) === 'undefined') return null;
                target = target[ns];
            }
            if (typeof target === 'function') return target;
        }
        // return null if could not parse
        return null;
    }

    let executeFunctionByName = function (functionName) {
        var args = Array.prototype.slice.call(arguments).splice(1);
        var namespaces = functionName.split(".");
        var func = namespaces.pop();
        var mainNS = window;
        for (var i = 0; i < namespaces.length; ++i) {
            mainNS = mainNS[namespaces[i]];
        }
        if (typeof (mainNS[func]) === 'function')
            return mainNS[func].apply(mainNS, args);
        else
            return null;
    }

    return {
        getfunctionFromName: getFuncFromString,
        evaluateFucntionByName: executeFunctionByName
    }
})();