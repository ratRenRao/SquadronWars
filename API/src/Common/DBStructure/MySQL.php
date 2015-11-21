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
        //read username and password from config file. will create a different file name and reference that for production.
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

        //Prepare SQL statement and bind the parameters for authenticating a username and password.
        $query = $dbh->prepare("CALL sp_AuthorizePlayer(?,?)");
        $query->bindParam(1,$username, PDO::PARAM_STR);
        $query->bindParam(2,$password, PDO::PARAM_STR);

        //Execute stored procedure with username and password passed in.
        $query->execute();

        //Turns the result from the stored procedure (if any) into an associative array.
        $results = $query->fetchAll(PDO::FETCH_ASSOC);

        //done with database connection. Closing connection.
        $query->closeCursor();

        //if there are rows in result set from the query, then successful authentication.
        if(sizeof($results) > 0)
        {
            //generate a session id to track logged in time store into an array to return
            $results["SessionID"] = "test";

            //return Player information.
            return $results;
        }

        //failed authentication, we return nothing.
        return;
    }



}