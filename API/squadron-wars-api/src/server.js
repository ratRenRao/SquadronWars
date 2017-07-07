/**
 * Created by branric on 7/6/2017.
 */

var express = require('express'),
    app = express(),
    port = process.env.PORT || 3000,
    bodyParser = require('body-parser');
    swagger = require('swagger-node-express');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

swagger.setAppHandler(app);
swagger.configure("/swagger/v1/swagger.json");

let routes = require('./routes/api-routes');
routes(app);

app.listen(port);

console.log('SquadronWars RESTful API server started on: ' + port);
