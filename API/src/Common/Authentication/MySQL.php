<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:45 PM
 *
 * Will be used to connect to the mysql server
 */

namespace Common\Authentication;

use PDO;



//we will need to implement the interface on this class. waiting until we have our interface defined.
class MySQL implements IAuthentication
{
    /*
     * function to authenticate user to database. will need to be changed to match our system calls and return
     * needed information back from the database. some modification will need to be done.
     *
     */
    public function authenticateUser($username, $password)
    {
        //create database reference object
        $dbh='';
        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=DBNAMEHERE",'USERNAMEHERE','PASSWORDHERE');
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            echo $e->getMessage();
        }
        //will need to be changed to call the stored procedure
        $query ="Select username, password from users";
        $results = $dbh->query($query);
        //iterates over the result set from the query. may need to change based on using stored procedure. will test more.
        while($row = $results->fetch(PDO::FETCH_ASSOC))
        {
            if($row["username"]=== $username && $row["password"] === $password)
            {
                $results->closeCursor();
                echo 'Login Successful for '.$username;
                return;
            }
        }
        $results->closeCursor();
        echo 'Login Failed!';
    }

    //will be removed. used to verify that i am receiving the config file.
    public function test()
    {
        $test = file_get_contents(basedir.DIRECTORY_SEPARATOR.'devconfiginfo.json');
        return $test;
    }


}