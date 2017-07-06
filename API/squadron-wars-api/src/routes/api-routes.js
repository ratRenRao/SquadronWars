'use strict';

module.exports = function(app) {
    const controller = require('../controllers/SquadronWarsController');

    app.route('/login')
        .get(controller.login);
};