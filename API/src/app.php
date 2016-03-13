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
        $returnObject = $mysql->createCharacter($createRequest);
    }

    if(sizeof($returnObject) > 0)
    {
        return json_encode($returnObject);
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
        $gameObject = json_decode($_POST['GameObject']);
        $returnObject = $mysql->createPlayer($gameObject);
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
            $response = $mysql->updateCharacter($gameObject);
        }
       // $response = $mysql->updateCharacter($verifyPlayer["playerId"], $gameObject->{"character"});
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

$app->post('/api/StartGame', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();

    //verify post request contains username and password.
    if(isset($_POST['GameObject'])) {
        $gameObject = json_decode($_POST['GameObject']);
        $verifyPlayer = $mysql->authenticateUser($gameObject->{"username"}, $gameObject->{"password"});
        if (sizeof($verifyPlayer) > 0) {
            $returnObject = $mysql->startGame($verifyPlayer["playerId"]);
            return json_encode($returnObject);
        }
    }
    return new Response("Failed",401);
});

$app->post('/api/CheckGameInfo', function() use($app)
{
    //create database object to be used.
    $mysql = new MySQL();

    //verify post request contains username and password.
    if(isset($_POST['GameObject'])) {
        $gameObject = json_decode($_POST['GameObject']);
        $returnObject = $mysql->checkGame($gameObject->{"gameID"});
        return json_encode($returnObject);
    }
    return new Response("Failed",401);
});

$app->post('/api/PlaceCharacters', function() use($app)
{
    $returncode =  200;
    $mysql = new MySQL();
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        //TODO: call sql for inserting player characters
    }
    if($returncode == 200)
    {
        return new Response("Updated Game",200);

    }
    elseif($returncode == 500)
    {
        return new Response("Server Error",500);
    }
    else
    {
        return new Response("Failed",401);

    }

});

$app->post('/api/UpdateGameInfo', function() use($app)
{
    $returncode = 401;
    $mysql = new MySQL();
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $returncode = $mysql->updateGame($gameObject);
    }
    if($returncode == 200)
    {
        return new Response("Updated Game",200);

    }
    elseif($returncode == 500)
    {
        return new Response("Server Error",500);
    }
    else
    {
        return new Response("Failed",401);

    }
});

/*
 * Test Method
 *
$app->post('/test', function() use($app){
    $mysql = new MySQL();
//    $test = $mysql->startGame(17);
    $test = $mysql->checkGame(14);
    var_dump($test);
    if(isset($_POST['GameObject']))
    {
        $gameObject = json_decode($_POST['GameObject']);
        $testing = $mysql->updateGame($gameObject);
        var_dump($testing);
    }

//    $game = json_encode($game);

//    $testing = json_decode($game);
//    var_dump($testing);
//    $test = $mysql->updateGame();

    return '';
});
/**/

$app->run();