var shortID = require('shortid');
var Vector3 = require('./Vector3.js');


module.exports = class Player {
    constructor(){
        this.baseUsername = '';
        this.baseUsernameTag = '#0000';
        this.username = '';
        this.id = shortID.generate();
        this.position = new Vector3();
    }
}