/// <reference path="Scripts/typings/tedious/tedious.d.ts" />
var express = require('express');
var routes = require('./routes/index');
var user = require('./routes/user');
var http = require('http');
var path = require('path');
var tdsSql = require('tedious');
var io = require('socket.io');
var app = express();
// all environments
app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.json());
app.use(express.urlencoded());
app.use(express.methodOverride());
app.use(app.router);
var stylus = require('stylus');
app.use(stylus.middleware(path.join(__dirname, 'public')));
app.use(express.static(path.join(__dirname, 'public')));
// development only
if ('development' == app.get('env')) {
    app.use(express.errorHandler());
}
app.get('/', routes.index);
app.get('/users', user.list);
var sqlConfig = {
    userName: 'pratikdb',
    password: 'pratikdb',
    server: 'DELL',
    options: {
        instanceName: 'MSSQLSERVER',
        database: 'Test',
        debug: {
            packet: false,
            payload: false,
            token: false,
            data: false
        },
    }
};
var sqlRequestTypes = tdsSql.TYPES;
var sqlConnection = new tdsSql.Connection(sqlConfig);
var server = http.createServer(app);
var ioServer = io(server);
function executeSelectStatement() {
    var sqlRquest = new tdsSql.Request('select * from chatdetails', function (err, rowCount, rows) {
        if (err)
            console.log(err);
        else
            console.log(rowCount + ' rows');
        //sqlConnection.close();
    });
    sqlRquest.on('done', function (rowCount, more) {
        console.log(rowCount + ' rows returned');
    });
    sqlConnection.execSql(sqlRquest);
    //request = new Request("SELECT c.CustomerID, c.CompanyName,COUNT(soh.SalesOrderID) AS OrderCount FROM SalesLT.Customer AS c LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID GROUP BY c.CustomerID, c.CompanyName ORDER BY OrderCount DESC;", function (err) {
    //    if (err) {
    //        console.log(err);
    //    }
    //});
    //var result = "";
    //request.on('row', function (columns) {
    //    columns.forEach(function (column) {
    //        if (column.value === null) {
    //            console.log('NULL');
    //        } else {
    //            result += column.value + " ";
    //        }
    //    });
    //    console.log(result);
    //    result = "";
    //});
    //request.on('done', function (rowCount, more) {
    //    console.log(rowCount + ' rows returned');
    //});
    //connection.execSql(request);
}
function executeInsertStatement() {
    var sqlRequest = new tdsSql.Request('insert into chatdetails (RoomId,Username,Chattext,Created) values (@RoomId,@Username,@Chattext,@Created)', function (err, rowCount, rows) {
        if (err)
            console.log(err);
        else
            console.log(rowCount + ' rows inserted');
    });
    sqlRequest.addParameter('RoomId', sqlRequestTypes.NVarChar, '2');
    sqlRequest.addParameter('Username', sqlRequestTypes.NVarChar, 'Test2');
    sqlRequest.addParameter('Chattext', sqlRequestTypes.NVarChar, 'Test Chatedit');
    sqlRequest.addParameter('Created', sqlRequestTypes.Date, new Date(2016, 3, 12));
    sqlConnection.execSql(sqlRequest);
    //request = new Request("INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate) OUTPUT INSERTED.ProductID VALUES (@Name, @Number, @Cost, @Price, CURRENT_TIMESTAMP);", function (err) {
    //    if (err) {
    //        console.log(err);
    //    }
    //});
    //request.addParameter('Name', TYPES.NVarChar, 'SQL Server Express 2014');
    //request.addParameter('Number', TYPES.NVarChar, 'SQLEXPRESS2014');
    //request.addParameter('Cost', TYPES.Int, 11);
    //request.addParameter('Price', TYPES.Int, 11);
    //request.on('row', function (columns) {
    //    columns.forEach(function (column) {
    //        if (column.value === null) {
    //            console.log('NULL');
    //        } else {
    //            console.log("Product id of inserted item is " + column.value);
    //        }
    //    });
    //});
    //connection.execSql(request);
}
function connectToSql() {
    sqlConnection.on('connect', function (err) {
        // If no error, then good to go...
        if (err) {
            console.log(err);
        }
        else {
            console.log('connected to SQL!');
        }
    });
}
connectToSql();
server.listen(app.get('port'), function () {
    console.log('Express server listening on port ' + app.get('port'));
});
ioServer.on('connection', function (socket) {
    console.log('a user connected');
    executeSelectStatement();
    socket.on('disconnect', function () {
        console.log('user disconnected');
    });
    socket.on('chat', function (msg) {
        executeInsertStatement();
        //socket.emit('chat', msg);
        socket.broadcast.emit('chat', msg);
    });
});
