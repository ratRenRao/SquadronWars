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


use Common\DBStructure\MySQL;
use \Symfony\Component\HttpFoundation\Response;

$app = new \Silex\Application();


$app->get('/',function() use($app){

    require_once commonpath.DIRECTORY_SEPARATOR.'html'.DIRECTORY_SEPARATOR.'index.html';
    return '';

});

/**
 * API end points to implement all of our database calls.
 */
$app->get('/api', function() use($app) {
    return 'Welcome to the API ';
});

$app->post('/api/auth', function() use($app) {
    $mysql = new MySQL();
    //$mysql->authenticateUser();
    //TODO: DBStructure API call -- return JSON encoded messages

});

$app->post('/api/getuser', function() use($app) {
    //TODO: Get User API call -- return JSON encoded messages

    if("ERROR CHECK HERE")
    {
        return $app->json($errormessageinarray,$errorcode);
    }

    return $app->json($arrayhere);
});




$app->run();