<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:44 PM
 *
 * Class will be used to declare Common Authentication requirements for our server
 */

namespace Common\Authentication;

interface IAuthentication
{
    /*
     * Function used to authenticate user
     *
     * we will need to modify this to match how we want to authenticate users to our system
     *
     * */
    public function authenticateUser($username, $password);
}