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
    socket.broadcast.emit('activePlayers', {id: thisPlayerID, x: player.position.x, y: player.position.y, z: player.position.z} );
    
    for(var playerID in Players){
        if(playerID != thisPlayerID){
            socket.emit('activePlayers', {id: Players[playerID].id, x: Players[playerID].position.x, y: Players[playerID].position.y, z: Players[playerID].position.z} );
        }
    }

    socket.on('updateLogin', function(data){
        player.baseUsername = data.name;
        console.log(player.baseUsername);
    });
    
    socket.on('updatePosition', function(data){
        player.position.x = data.pos.x;
        player.position.y = data.pos.y;
        player.position.z = data.pos.z;
        socket.broadcast.emit('updatePosition', {id: player.id, x: player.position.x, y: player.position.y, z: player.position.z});
        socket.emit('updatePosition', {id: player.id, x: player.position.x, y: player.position.y, z: player.position.z});
    });

    socket.on('disconnect', function(){
        console.log('client disconected :c');
        socket.broadcast.emit('disconnected', {id: thisPlayerID});
        delete Players[thisPlayerID];
    });
});