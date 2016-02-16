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



$app->get('/',function() use($app)
{
    //loads landing page. under construction
    require_once commonpath.DIRECTORY_SEPARATOR.'html'.DIRECTORY_SEPARATOR.'index.html';
    return '';

});

/**
 * API end points to implement all of our database calls.
 */
$app->get('/api', function() use($app)
{
    return 'Welcome to the API ';
});

$app->post('/api/auth', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();

    //verify post request contains username and password.
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $returnobject["PlayerInfo"] = $mysql->authenticateUser($gameObject->{"username"}, $gameObject->{"password"});
//       $returnobject["PlayerInfo"] = $mysql->authenticateUser("test","testing123");


        if($returnobject["PlayerInfo"] != null)
        {
            //Return just character information.
            //$returnobject["Characters"] = $mysql->getCharacters($returnobject["PlayerInfo"]["playerId"]);
            $returnobject["PlayerDetails"] = $mysql->getPlayer($returnobject["PlayerInfo"]["playerId"]);
            return $app->json($returnobject);
        }
    }

    return new Response("Failed Authentication", 401);
});

$app->post('/api/CreateCharacter', function() use($app)
{
    $mysql = new MySQL();
    $returnCode = 401;

    $createRequest = null;

    if(isset($_POST['GameObject']))
    {
        $createRequest = json_decode($_POST['GameObject']);
    }

    if($returnCode == 200)
    {
        return new Response("Successful Create Player", 200);
    }
    else
    {
        return new Response("Failed",401);
    }
});

$app->post('/api/CreatePlayer', function() use($app)
{
    $mysql = new MySQL();

    $returnObject = null;

    if(isset($_POST['GameObject']))
    {
        $register = json_decode($_POST['GameObject']);
        $returnObject = $mysql->createPlayer($register);
    }

    if($returnObject != null)
    {
        return json_encode($returnObject);
    }

    return new Response("Failed",401);
});

$app->post('/api/CreateSquad', function() use($app)
{
    //TODO: Create Squad
    return new Response("Failed",401);
});

$app->post('/api/UpdateCharacter', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();
    $response = 401;

    //verify post request contains username and password.
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $verifyPlayer = $mysql->authenticateUser($gameObject->{"username"}, $gameObject->{"password"});
        if(sizeof($verifyPlayer) > 0)
        {
            $response = $mysql->updateCharacter($verifyPlayer["playerId"], $gameObject->{"character"});
        }
    }

    if($response == 200)
    {
        return new Response("Successful", 200);
    }
    else if($response == 500)
    {
        return new Response("Server Error", 500);
    }
    else
    {
        return new Response("Failed",401);
    }
});

$app->post('/api/UpdatePlayer', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();
    $response = 401;

    //verify post request contains username and password.

    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $verifyPlayer = $mysql->authenticateUser($gameObject->{"username"}, $gameObject->{"password"});
        if(sizeof($verifyPlayer) > 0)
        {
            $response = $mysql->updatePlayer($gameObject->{"player"});
        }
    }
    if($response == 200)
    {
        return new Response("Successful", 200);
    }
    else if($response == 500)
    {
        return new Response("Server Error", 500);
    }
    else
    {
        return new Response("Failed",401);
    }
});

$app->post('/api/UpdateSquad', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();

    //verify post request contains username and password.
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $verifyPlayer = $mysql->authenticateUser($gameObject->{"username"}, $gameObject->{"password"});
        if(sizeof($verifyPlayer) > 0)
        {
            //TODO: update squad
        }
    }

    return new Response("Failed",401);
});

$app->post('/api/UpdateCharAbility', function() use($app)
{
    //TODO: Update/Insert Character Ability
    return new Response("Failed",401);
});

$app->post('/api/StartGame', function() use($app)
{
    //TODO: Start Game
    return new Response("Failed",401);
});

$app->post('/api/GetGameInfo', function() use($app)
{
    //TODO: Check Game status
    return new Response("Failed",401);
});

/*
 * Test Method
 *
$app->get('/test', function() use($app){
    $mysql = new MySQL();
    $test["Characters"] = $mysql->getCharacters(9);
    var_dump($test);
    return '';
});
**/

$app->run();