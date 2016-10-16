'use strict'
if (window.jQuery) {
    var custValidationUtil = custValidationUtil || {};
    custValidationUtil = function () {

        /*
        TODO:: 
        Add styling to error message displayed.
        validation for file upload
                handle asp controls
        */

        let custValidatorsToExecute = [];
        let custValidatorsSummary = [];

        let enableCustomValidation = "enableCustomValidation".toLowerCase();
        let customGroupValidator = 'customGroupValidator'.toLowerCase();

        let formID = '';
        let isformValid = false;

        let nonEmptyMessage = "nonEmptyMessage".toLowerCase();
        let maxLength = "maxLength".toLowerCase();
        let minLength = "minLength".toLowerCase();
        let onlyNumeric = "onlyNumeric".toLowerCase();
        let customValidationFunction = "customValidationFunction".toLowerCase();
        let customValidationMessage = "customValidationMessage".toLowerCase();

        let addCustomValidator = function (id, isInGroup) {
            try {
                let control;
                if (!isInGroup)
                    control = $('#' + id);
                else
                    control = $('[name="' + id + '"]');

                if (control.length > 0) {
                    control.each(function (index, element) {
                        setCustomAttribute(element.id, enableCustomValidation, "true");
                    });
                    if (isInArray(custValidatorsToExecute, "id", id) === -1)
                        custValidatorsToExecute.push({ "id": id, "isInGroup": isInGroup });
                }
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let isKeyValuePairByValuesinArray = function (jsonArray, key, value) {
            let rtnIndex = -1;
            try {
                jsonArray.every(function (curObjec, index) {

                    if (curObjec[key] !== value)
                        rtnIndex = index;
                    return curObjec[key] === value;
                });
                return rtnIndex;
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let isInArray = function (jsonArray, key, value) {
            let rtnVal = -1;
            try {
                if (typeof (jsonArray) !== 'undefined' && jsonArray !== null && jsonArray.length > 0) {
                    if (typeof (key) !== 'undefined' && key !== null && key !== '') {
                        for (let index = 0, arrLen = jsonArray.length; index < arrLen; index++) {
                            if (jsonArray[index][key] === value) {
                                rtnVal = index;
                                break;
                            }
                        }
                    }
                    else {
                        for (let index = 0, arrLen = jsonArray.length; index < arrLen; index++) {
                            if (jsonArray[index] === value) {
                                rtnVal = index;
                                break;
                            }
                        }
                    }
                }
            }
            catch (exception) {
                console.log(exception);
            }
            return rtnVal;
        }

        let removeCustomValidator = function (id) {
            try {
                let indexOfItem = isInArray(custValidatorsToExecute, "id", id);
                if (indexOfItem !== -1)
                    custValidatorsToExecute.splice(indexOfItem, 1);
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let showValidationSummary = function () {

            try {
                let divValidationSummary = $("#validationSummary");
                divValidationSummary.html('');
                if (custValidatorsSummary.length > 0 && isformValid === false) {
                    var list = document.createElement('ul');

                    for (let index = 0, summryLen = custValidatorsSummary.length; index < summryLen; index++) {

                        var item = document.createElement('li');
                        item.appendChild(document.createTextNode(custValidatorsSummary[index].message));
                        list.appendChild(item);
                    }

                    $(validationSummary).html(list.innerHTML);
                }
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let evaluateAllValidation = function () {

            if (typeof (custValidatorsToExecute) === 'undefined'
                || custValidatorsToExecute === null)
                return;

            try {
                let enabledValidators = getAllEnabledValidators();
                if (typeof (enabledValidators) === 'undefined'
                    || enabledValidators === null
                    || enabledValidators.length === 0)
                    return;
                custValidatorsSummary = [];
                let formValidChecks = [];
                for (let index = 0, valLen = enabledValidators.length; index < valLen; index++) {
                    let isValid = doValidation(enabledValidators[index]);
                    formValidChecks.push(isValid);
                }

                let isAnyInvalid = isInArray(formValidChecks, '', false);
                if (isAnyInvalid === -1)
                    isformValid = true;
                else {
                    isformValid = false;
                }
                showValidationSummary();
            }
            catch (exception) {
                console.log(exception);
            }
        };

        let getCustomAttribute = function (id, attrName, isInGroup) {
            let rtnVal = '';
            try {
                let elements = [];

                if (typeof (isInGroup) !== 'undefined' && isInGroup !== null && isInGroup) {
                    elements = document.getElementsByName(id);
                }
                else {
                    elements.push(document.getElementById(id));
                }

                for (let index = 0; index < elements.length; index++) {

                    if (typeof (elements[index]) !== 'undefined' && elements[index] !== null) {
                        if (rtnVal === ''
                            && typeof (elements[index].dataset) !== 'undefined'
                            && elements[index].dataset !== null
                            && elements[index].dataset.hasOwnProperty(attrName)) {
                            rtnVal = elements[index].dataset[attrName];
                        }
                        else {
                            if (attrName.indexOf("data-") === -1)
                                attrName = 'data-' + attrName;
                            if (elements[index].hasAttribute(attrName)) {
                                rtnVal = elements[index].getAttribute(attrName);
                            }
                            else if (rtnVal === '' && elements[index].hasOwnProperty(attrName))
                                rtnVal = elements[index].attrName;
                        }
                    }
                }
            }
            catch (exception) {
                console.log(exception);
            }


            return rtnVal;
        }

        let setCustomAttribute = function (id, attrName, attrValue) {
            try {
                var element = document.getElementById(id);
                if (typeof (element) !== 'undefined' && element !== null) {
                    element.dataset[attrName] = attrValue;
                }
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let doEmtpyCheck = function (validationPair) {

            let rtnVal = true;
            try {
                let custEmptyMessage = getCustomAttribute(validationPair.id, nonEmptyMessage, validationPair.isInGroup);

                if (typeof (custEmptyMessage) !== 'undefined' && custEmptyMessage !== null) {
                    let isInGroup = validationPair.isInGroup;
                    if (!isInGroup) {
                        //TODO:: dropdown value check handling.
                        let control = $('#' + validationPair.id);
                        if (control.length > 0) {
                            let controlType = control.prop('type');
                            if (controlType === 'radio' || controlType === 'checkbox') {
                                if (!control.is(':not(checked)')) {
                                    rtnVal = false;
                                }
                            }
                            else if (control.val() === '')
                                rtnVal = false;
                        }
                    }
                    else {
                        let controls = $('[name="' + validationPair.id + '"]');
                        if (controls.length > 0) {
                            let selectedControls = $('[name="' + validationPair.id + '"]:checked');
                            if (selectedControls.length === 0)
                                rtnVal = false;
                            /*TODO:: if control type is other than checkbox or radio button then do validation for each control.
                            * make sure to add same control type check
                            */
                            //let bsameTyep = true;
                            //let tempType = '';
                            //controls.each(function (index, elment) {
                            //    let tempcontrolType = $(element).prop('type');
                            //    if (tempType === '')
                            //        tempType = tempcontrolType;

                            //});
                        }
                    }
                    if (!rtnVal) {
                        setCustomValidationMessage(validationPair.id, custEmptyMessage);
                    }
                    else {
                        removeCustomValidationMessage(validationPair.id, custEmptyMessage);
                    }
                }
            }
            catch (exception) {
                console.log(exception);
            }

            return rtnVal;
        }

        let executeFunctionByName = function (functinName, argsToPass) {
            
            let args = Array.prototype.slice.call(arguments);//.split(1);
            args = args.splice(1);
            let nameSpaces = functinName.split(".");
            let func = nameSpaces.pop();
            let mainNs = window;
            for (let index = 0, len = nameSpaces.length; index < len; index++)
                mainNs = mainNs[nameSpaces[index]];
            if (typeof (mainNs) === 'object' && typeof (mainNs[func]) === 'function')
                return mainNs[func].apply(mainNs, args);
            else
                return null;
        }

        let callCustomValidationFunction = function (validationPair) {

            let bRtnVal = true;
            try {
                let control = document.getElementById(validationPair.id);
                if (typeof (control) !== 'undefined' && control !== null) {
                    let functionName = getCustomAttribute(control.id, customValidationFunction, validationPair.isInGroup);
                    if (typeof (functionName) !== 'undefined' && functionName !== null && functionName !== '') {
                        
                        if (typeof (functionName) === 'string') {
                            let funcNameRex = new RegExp(/^function\s*[^\(]*\(\s*([^\)]*)\)^/m);
                            let arr = funcNameRex.exec(functionName);
                            bRtnVal = executeFunctionByName(functionName, control);                            
                        }
                        else if (functionName === 'function')
                            bRtnVal = functionName(control);

                        if (typeof (bRtnVal) === 'undefined' || bRtnVal === null || typeof (bRtnVal) !== 'boolean')
                            console.log('custom validation function did not return boolean value');
                    }
                }
            }
            catch (exception) {
                console.log(exception);
            }
            return bRtnVal;
        }

        let setCustomValidationMessage = function (id, message) {
            let isMessagePresentforControl = false;
            try {
                isMessagePresentforControl = custValidatorsSummary.some(function (value, index, array) {
                    return value.id === id && value.message === message;
                });

                if (!isMessagePresentforControl)
                    custValidatorsSummary.push({ "id": id, "message": message });
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let removeCustomValidationMessage = function (id, message) {
            try {
                let indexOfItem = -1;
                indexOfItem = isKeyValuePairByValuesinArray(custValidatorsSummary, id, message);
                custValidatorsSummary.splice(indexOfItem, 1);
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let doValidation = function (validationPair) {
            let rtnVal = false;
            try {
                let control = $('#' + validationPair.id);
                if (control.length > 0 && control.is(':visible')) {
                    let tobeValidated = getCustomAttribute(validationPair.id, enableCustomValidation, validationPair.isInGroup);

                    let bEvaluateCustomValidation = true;
                    let bEmtpy = true;
                    if (typeof (tobeValidated) !== 'undefined' && tobeValidated !== null && tobeValidated) {
                        bEmtpy = doEmtpyCheck(validationPair);
                        if (bEmtpy) {
                            bEvaluateCustomValidation = callCustomValidationFunction(validationPair);
                        }
                        rtnVal = bEmtpy && bEvaluateCustomValidation;
                    }
                }
                else
                    rtnVal = true;
            }
            catch (exception) {
                console.log(exception);
            }
            return rtnVal;
        };

        let getValidationValue = function (id) {
            let rtnVal = '';
            try {
                let control = $('#' + id);
                if (control.length > 0)
                    rtnVal = control.val();
            }
            catch (exception) {
                console.log(exception);
            }
        };

        let validateForm = function (formId) {
            try {
				custValidatorsToExecute = [];
                if (typeof (formId) !== 'undefined' && formId !== null && formId !== '')
                    formID = formId;
                evaluateAllValidation();
                return isformValid;
            }
            catch (exception) {
                console.log(exception);
            }
        };

        let findAllPageValidators = function (isEnabled) {
            try {
                $(document).find("[data-" + enableCustomValidation + "]").each(function (index, element) {

                    try {
                        addCustomValidator(element.id);
                    }
                    catch (exception) {
                        console.log(exception);
                    }
                });
            }
            catch (exception) {
                console.log(exception);
            }
        };

        let getAllEnabledValidators = function () {

            let rtnVal = [];
            try {
                let collectionElement = null;
                if (formID !== '')
                    collectionElement = $('#' + formID);
                else
                    collectionElement = $(document);
                collectionElement.find("[data-" + enableCustomValidation + "='true']").each(function (index, element) {
                    try {
                        let isGroup = getCustomAttribute(element.id, customGroupValidator);
                        if (isGroup === "true")
                            addCustomValidator(element.name, true);
                        else
                            addCustomValidator(element.id, false);
                    }
                    catch (exception) {
                        console.log(exception);
                    }
                });
                rtnVal = custValidatorsToExecute;
            }
            catch (exception) {
                console.log(exception);
            }
            return rtnVal;
        };

        let doInit = function () {
            try {
                if (typeof (custValidatorsToExecute) === 'undefined' || custValidatorsToExecute === null) {
                    custValidatorsToExecute = [];
                }

                if (typeof (custValidatorsSummary) === 'undefined' || custValidatorsSummary === null) {
                    custValidatorsSummary = [];
                }

                let divValidationSummary = $("#validationSummary");
                if (divValidationSummary.length === 0) {
                    let allForms = $('form');
                    divValidationSummary = $('<div id= "validationSummary"></div>');
                    $(allForms[0]).before(divValidationSummary);
                }
            }
            catch (exception) {
                console.log(exception);
            }
        }

        let getTemp = function () {
            //custValidatorsSummary.push("test");
            return custValidatorsToExecute;
        }

        return {
            doInit: doInit,
            enableCustomValidator: addCustomValidator,
            disableCustomValidator: removeCustomValidator,
            validateForm: validateForm,
            setErrorMessage: setCustomValidationMessage,
            getTemp: getTemp
        }
    }();
};
$(document).ready(function () {
    custValidationUtil.doInit();
});