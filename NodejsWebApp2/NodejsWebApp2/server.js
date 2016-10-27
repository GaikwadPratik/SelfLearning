let express = require('express')
let app = express();
let http = require('http').Server(app);
let path = require('path');
let chokidar = require('chokidar');
let io = require('socket.io')(http);

let port = process.env.port || 1337;

let clientPath = path.join(__dirname, 'client');

app.use(express.static(clientPath));

app.get('/', function (req, res) {
    res.sendFile(path.join(clientPath, 'HTML1.html'));
});

http.listen(port, function () {
    console.log(`listning on ${port}`);
});

let watcher = chokidar.watch(path.join(clientPath, 'images'),
    { ignored: /^\./, persistent: true }
);

io.on('connection', function (socket) {
    executeWatcher(socket);
    socket.on('imageAddServer', function (data) {
        console.log(`${data.imagePath} has been processed at client`);
    });
    socket.on('disconnect', function () {
        console.log('Client disconnected');
    });

});

let executeWatcher = function (socket) {
    watcher
        .on('add', function (path) {
            console.log('File', path, 'has been added');
            socket.emit('imageAddClient', { imagePath: path });
        })
        .on('change', function (path) {
            console.log('File', path, 'has been changed');
            socket.emit('imageAddClient', { imagePath: path });
        })
        .on('unlink', function (path) {
            console.log('File', path, 'has been removed');
            socket.emit('imageAddClient', { imagePath: path });
        })
        .on('error', function (error) {
            console.error('Error happened', error);
        });
}
