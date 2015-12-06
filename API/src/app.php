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


    //Set our database scheme to be used here.
    $mysql = new MySQL();

    if(isset($_POST['username']) && isset($_POST['password']))
    {
        $returnobject = $mysql->authenticateUser($app->escape($_POST['username']),$app->escape($_POST['password']));
        if(sizeof($returnobject) > 0)
        {
            return $app->json($returnobject);
        }
    }

    return new Response("Failed Authentication",401);
});

$app->post('/api/getchars', function() use($app){

    $mysql = new MySQL();

    //verify that session id is valid and get character information. Call from authenticate?
    if(isset($_POST['PlayerID']) && isset($_POST['SessionID']))
    {
        $returnObject = $mysql->getCharacters($_POST['PlayerID']);
        return $app->json($returnObject);
    }

    return new Response("Invalid Session ID", 401);
});




$app->run();