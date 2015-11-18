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
$app = new \Slim\Slim();

$app->notFound(function () {
    echo 'Dude you are lost...';
});

$app->get('/', function () {
    echo 'Welcome to root...';
});

//$app->get('/', function()
//{
//    echo "hello";
//   // require_once realpath(__DIR__.DIRECTORY_SEPARATOR.'Common'.DIRECTORY_SEPARATOR.'html'.DIRECTORY_SEPARATOR.'index.html');
//});

