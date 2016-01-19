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

        //create database reference object
        $dbh='';

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
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
        $results = $query->fetch(PDO::FETCH_ASSOC);

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

    public function getPlayer($playerID)
    {
        //create database reference object
        $dbh='';

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            echo $e->getMessage();
        }

        $query = $dbh->prepare("CALL sp_GetCharacters(?)");
        $query->bindParam(1, $playerID, PDO::PARAM_INT);
        $query->execute();
        $returnObject["Characters"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query = $dbh->prepare("CALL sp_GetCharacterAbilities(?)");
        $query->bindParam(1, $playerID, PDO::PARAM_INT);
        $query->execute();
        $returnObject["CharacterAbilities"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query = $dbh->prepare("CALL sp_GetInventory(?)");
        $query->bindParam(1, $playerID, PDO::PARAM_INT);
        $query->execute();
        $returnObject["Inventory"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query = $dbh->prepare("CALL sp_GetEquipment");
        $query->execute();
        $returnObject["Equipment"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query = $dbh->prepare("CALL sp_GetSquads(?)");
        $query->bindParam(1, $playerID, PDO::PARAM_INT);
        $query->execute();
        $returnObject["Squads"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query = $dbh->prepare("CALL sp_GetItems");
        $query->execute();
        $returnObject["Items"] = $query->fetchAll(PDO::FETCH_ASSOC);

        $query->closeCursor();

        return $returnObject;
    }

    public function getCharacters($playerID)
    {

        //create database reference object
        $dbh='';

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            echo $e->getMessage();
        }


        $query = $dbh->prepare("CALL sp_GetCharacters(?)");
        $query->bindParam(1, $playerID, PDO::PARAM_INT);
        $query->execute();
        $returnObject = $query->fetchAll(PDO::FETCH_ASSOC);


        //done with database connection. Closing connection.
        $query->closeCursor();

        return $returnObject;

    }

    public function createPlayer($email)
    {
        // TODO: Implement createPlayer() method.
        //create database reference object
        $dbh='';

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            echo $e->getMessage();
        }

    }


}