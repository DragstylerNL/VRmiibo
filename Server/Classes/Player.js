var shortID = require('shortid');

module.exports = class Player {
    constructor(){
        this.baseUsername = '';
        this.baseUsernameTag = '#0000';
        this.username = '';
        this.id = shortID.generate();
    }
}