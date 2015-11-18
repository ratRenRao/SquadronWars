<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:55 PM
 *
 * will use this class to create our website endpoints and logic needed when reached
 * all API endpoints should return json encoded objects
 */

//Directory constants


use Common\Authentication\MySQL;

$app = new \Silex\Application();



$app->get('/',function() use($app){

    require_once commonpath.DIRECTORY_SEPARATOR.'html'.DIRECTORY_SEPARATOR.'index.html';
    return '';

});

/**
 * API end points to implement all of our database calls.
 */
$app->get('/api', function() use($app) {
    echo basedir;
    echo srcpath;
    echo commonpath;
    $mysql = new MySQL();
    $mysql->test();
    return 'Welcome to the API ';
});

$app->post('/api/auth', function() use($app) {
    //TODO: Authentication API call -- return JSON encoded messages
});

$app->post('/api/getuser', function() use($app) {
    //TODO: Get User API call -- return JSON encoded messages
});

$app->post('/api/getuser', function() use($app) {
    //TODO: Get User API call -- return JSON encoded messages
});

$app->run();