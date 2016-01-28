<?php
/**
 * Created by PhpStorm.
 * User: Clinton Fowler
 * Date: 11/25/2015
 * Time: 7:00 AM
 */

// Define Production Specific Configurations here

//create Username and password handle for production environment. will need to store file where it cannot be read.
$credentials = json_decode(file_get_contents(basedir.DIRECTORY_SEPARATOR.'devconfiginfo.json'));
define("dbuser", $credentials->{"username"});
define("dbpass", $credentials->{"password"});