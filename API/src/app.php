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
$app = new \Silex\Application();



$app->get('/',function() use($app){
    return require_once realpath(DIRECTORY_SEPARATOR.'Common'.DIRECTORY_SEPARATOR.'html'.DIRECTORY_SEPARATOR.'index.hmtl');
});


$app->run();