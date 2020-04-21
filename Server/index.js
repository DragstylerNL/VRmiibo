var io = require('socket.io')(process.env.PORT || 52300);

// custom classes
var Player = require('./Classes/Player.js');

console.log('Server has started!');

var Players = [];

io.on('connection', function(socket) {
    console.log('client connection made ^^');
    
    var player = new Player();
    var thisPlayerID = player.id;
    
    Players[thisPlayerID] = player;
    socket.emit('register', {id: thisPlayerID} );

    socket.on('updateLogin', function(data){
        player.baseUsername = data.name;
        console.log(player.baseUsername);
    });

    socket.on('disconnect', function(){
        console.log('client disconected :c');
        delete Players[thisPlayerID];
    });
});