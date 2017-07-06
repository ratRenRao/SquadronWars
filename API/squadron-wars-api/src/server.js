/**
 * Created by branric on 7/6/2017.
 */

var express = require('express'),
    app = express(),
    port = process.env.PORT || 3000;

app.listen(port);

console.log('SquadronWars RESTful API server started on: ' + port);