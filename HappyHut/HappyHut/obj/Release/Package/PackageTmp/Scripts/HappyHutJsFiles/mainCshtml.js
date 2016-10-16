var cityTypeAheadController = function ($scope, serverCallMakerFactory) {
    $scope.selectedCity = '';
    $scope.selectedArea = '';
    $scope.selectedService = '';
    $scope.disableAreas = true;
    $scope.disableServices = true;
    $scope.disableBtnBook = true;

    var cityId = 0;
    var areaId = 0;
    var serviceId = 0;

    $scope.cities = jsonCities;

    $scope.onCitySelect = function ($item, $model, $label) {

        $scope.selectedArea = '';
        $scope.selectedService = '';

        $scope.disableAreas = true;
        $scope.disableServices = true;
        $scope.disableBtnBook = true;

        var httpURL = getAreasInCityURL;
        var httpparams = { strCityId: $item.id };
        var httpMethod = 'GET';

        serverCallMakerFactory
            .callServer(httpMethod, httpURL, httpparams)
            .success(function (data, status) {
                if (status === 200) {
                    $scope.areasInCity = data;
                    $scope.disableAreas = false;
                    cityId = $item.id;
                }
            })
            .error(function (data, status) {
                //TODO: Use nice alert.
                alert('Server request failed with status ' + status + ' while getting area in the ' + $item.Name + ' city.');
            });
    }

    $scope.onAreaSelect = function ($item, $model, $label) {
        $scope.selectedService = '';
        $scope.disableBtnBook = true;

        var httpURL = getServicesInAreaURL;
        var httpparams = { strAreaId: $item.id };
        var httpMethod = 'GET';

        serverCallMakerFactory
            .callServer(httpMethod, httpURL, httpparams)
            .success(function (data, status) {
                if (status === 200) {
                    $scope.servicesInArea = data;
                    $scope.disableServices = false;
                    areaId = $item.id;
                }
            })
            .error(function (data, status) {
                //TODO: Use nice alert.
                alert('Server request failed with status ' + status + ' while getting services in the ' + $item.Name + ' area.');
            });
    }

    $scope.onServiceSelect = function ($item, $model, $label) {
        serviceId = $item.serviceID;
    }

    $scope.test = function () {
        $('#hdnCityAreaService').val(cityId + ';' + areaId + ';' + serviceId);
    }
}