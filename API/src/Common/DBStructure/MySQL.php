<?php
/**
 * Created by PhpStorm.
 * User: Clint Fowler
 * Date: 11/16/2015
 * Time: 11:45 PM
 *
 * Will be used to connect to the mysql server
 */

namespace Common\DBStructure;

use PDO;



//we will need to implement the interface on this class. waiting until we have our interface defined.
class MySQL implements IDBStructure
{
    /*
     * function to authenticate user to database. will need to be changed to match our system calls and return
     * needed information back from the database. some modification will need to be done.
     *
     */

    public function authenticateUser($username, $password)
    {
        $credentials = json_decode(file_get_contents(basedir.DIRECTORY_SEPARATOR.'devconfiginfo.json'));
        //create database reference object
        $dbh='';
        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo",$credentials->{"username"},$credentials->{"password"});
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            echo $e->getMessage();
        }

        $query = $dbh->prepare("CALL sp_AuthorizePlayer(?,?)");
        $query->bindParam(1,$username, PDO::PARAM_STR);
        $query->bindParam(2,$password, PDO::PARAM_STR);
        $query->execute();

        $results = $query->fetchAll(PDO::FETCH_ASSOC);
        //var_dump($results);
        //iterates over the result set from the query. may need to change based on using stored procedure. will test more.
        if(sizeof($results) > 0)
        {
            //generate a session id to track logged in time store into an array to return
            $results["SessionID"] = "test";
            $query->closeCursor();
            //echo 'Login Successful for '.$username;
            return $results;
        }
        $query->closeCursor();
        echo 'Login Failed!';
    }

    //will be removed. used to verify that i am receiving the config file.
    public function test()
    {

        return $test;
    }


}