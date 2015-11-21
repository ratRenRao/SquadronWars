<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:44 PM
 *
 * Class will be used to declare Common DBStructure requirements for our server
 */

namespace Common\DBStructure;

interface IDBStructure
{
    /*
     * Functions used to interact with server
     *
     * We will need to define all functions we put into the MySQL file to create scalability and
     * loosely couple our code. this will allow for changes to structure later if needed
     *
     * */
    public function authenticateUser($username, $password);
}