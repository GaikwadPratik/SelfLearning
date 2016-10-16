/// <reference path="../../Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="../../Scripts/typings/socket.io-client/socket.io-client.d.ts" />
var socket = io();
$('#send-message-btn').on('click', function () {
    var message = $('#message-box').val();
    socket.emit('chat', message);
    $('#messages').append($('<p>').text(message));
    $('#message-box').val('');
    return false;
});
socket.on('chat', function (message) {
    $('#messages').append($('<p>').text(message));
});
