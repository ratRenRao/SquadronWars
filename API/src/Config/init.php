<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:52 PM
 *
 * will be used to initialize slim framework on server.
 */


/**
 * Define Directory Constants
 */
define("basedir", realpath(__DIR__.DIRECTORY_SEPARATOR.'..'.DIRECTORY_SEPARATOR.'..'.DIRECTORY_SEPARATOR));
define("srcpath", realpath(basedir.DIRECTORY_SEPARATOR.'src'));
define("commonpath", srcpath.DIRECTORY_SEPARATOR.'Common');

/**
 *
 * Initialize Framework
 */
require_once basedir.DIRECTORY_SEPARATOR.'vendor'.DIRECTORY_SEPARATOR.'autoload.php';
\Slim\Slim::registerAutoloader();




/**
 * start app
 */
require_once srcpath.DIRECTORY_SEPARATOR.'app.php';
