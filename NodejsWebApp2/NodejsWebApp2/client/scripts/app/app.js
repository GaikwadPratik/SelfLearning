var app = angular.module('imageLoaderApp', []);
app.controller('loadController', function ($scope, socket) {
    try {
        images = [];
        socket.on('imageAddClient', function (data) {
            let fileName = data.imagePath.substring(data.imagePath.lastIndexOf('\\') + 1);
            console.log(fileName);
            images.push({ name: fileName, src: fileName });
            socket.emit('imageAddServer', { imagePath: fileName });
        });

        $scope.images = images;
        //console.log($scope.images);
    }
    catch (exception) {
        console.log(`Error from loadController ${exception}`);
    }
});

app.factory('socket', function ($rootScope) {
    var socket = io.connect('http://localhost:1337');
    return {
        on: function (eventName, callBack) {
            socket.on(eventName, function () {
                let args = arguments;
                $rootScope.$apply(function () {
                    callBack.apply(socket, args);
                });
            });
        },
        emit: function (eventName, data, callBack) {
            socket.emit(eventName, data);
        }
    }
});

//var socket = io.connect('http://localhost:1337');
//socket.on('imageAddClient', function (data) {
//    console.log(data);
//    socket.emit('imageAddServer', { imagePath: data.imagePath });
//});
