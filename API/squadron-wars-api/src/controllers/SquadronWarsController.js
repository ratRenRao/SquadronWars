/**
 * Created by branric on 7/6/2017.
 */

let mysql = require('mysql');
let con = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "Stonesoup_283028",
    database: "dbo"
});

exports.login = function(user, pass){
    con.connect(function (err) {
        if (err) throw err;
        con.query(`SELECT userName, password from Player where userName = ${user} and password = ${pass}`, function (err, result, fields) {
            if (err) throw err;
            if (result.value = null)
                return false;
            else
                return true;
        });
    });
};

