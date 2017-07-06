'use strict';

module.exports = function(app) {
    var controller = requre('../controllers/SquadronWarsController');

    app.route('/login')
        .get(controller.login);
}