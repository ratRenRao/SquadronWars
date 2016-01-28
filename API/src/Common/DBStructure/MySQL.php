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
    //will return status code for response message.
    public function updatePlayer($playerObject)
    {
        //create database reference object
        $dbh = '';
        $returnCode = 500;

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            //echo $e->getMessage();

            //return status code to be used in response message. 500 server error.
            return $returnCode;
        }

        //prepare SQL statemenet
        $query = $dbh->prepare("CALL sp_UpdatePlayer(?,?,?,?,?)");
        $query->bindParam(1, $playerObject->{"username"},  PDO::PARAM_STR);
        $query->bindParam(2, $playerObject->{"password"},  PDO::PARAM_STR);
        $query->bindParam(3, $playerObject->{"firstname"}, PDO::PARAM_STR);
        $query->bindParam(4, $playerObject->{"lastname"},  PDO::PARAM_STR);
        $query->bindParam(5, $playerObject->{"email"},     PDO::PARAM_STR);


        //execute procedure.
       $query->execute();

        if($query->rowCount() > 0)
        {
            //return status code to be used in response message. 200 successful.
            $returnCode = 200;
        }

        $query->closeCursor();

        return $returnCode;
    }

    public function updateCharacter($playerID, $characterObject)
    {
        //create database reference object
        $dbh = '';
        $returnCode = 500;

        //Try to connect to mysql service
        try
        {
            //created config file to hold user name and password that we will use to obscure and keep off of our repo.
            $dbh = new PDO("mysql:host=localhost:3306;dbname=dbo", dbuser, dbpass);
        }
        catch(PDOException $e)
        {
            //this will be ignored in production. do not want to echo back this error.
            //echo $e->getMessage();

            //return status code to be used in response message. 500 server error.
            return $returnCode;
        }

        $query = $dbh->prepare("CALL sp_UpdateCharacter(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
        $query->bindParam(1, $characterObject->{"characterId"}, PDO::PARAM_INT);
        $query->bindParam(2, $characterObject->{"statPoints"}, PDO::PARAM_INT);
        $query->bindParam(3, $characterObject->{"skillPoints"}, PDO::PARAM_INT);
        $query->bindParam(4, $characterObject->{"luck"}, PDO::PARAM_INT);
        $query->bindParam(5, $characterObject->{"LevelID"}, PDO::PARAM_INT);
        $query->bindParam(6, $characterObject->{"name"}, PDO::PARAM_STR);
        $query->bindParam(7, $characterObject->{"experience"}, PDO::PARAM_INT);
        $query->bindParam(8, $characterObject->{"helm"}, PDO::PARAM_INT);
        $query->bindParam(9, $characterObject->{"chest"}, PDO::PARAM_INT);
        $query->bindParam(10, $characterObject->{"gloves"}, PDO::PARAM_INT);
        $query->bindParam(11, $characterObject->{"pants"}, PDO::PARAM_INT);
        $query->bindParam(12, $characterObject->{"shoulders"}, PDO::PARAM_INT);
        $query->bindParam(13, $characterObject->{"boots"}, PDO::PARAM_INT);
        $query->bindParam(14, $characterObject->{"accessory1"}, PDO::PARAM_INT);
        $query->bindParam(15, $characterObject->{"accessory2"}, PDO::PARAM_INT);
        $query->bindParam(16, $characterObject->{"IsStandard"}, PDO::PARAM_INT);
        $query->bindParam(17, $characterObject->{"str"}, PDO::PARAM_INT);
        $query->bindParam(18, $characterObject->{"int"}, PDO::PARAM_INT);
        $query->bindParam(19, $characterObject->{"agi"}, PDO::PARAM_INT);
        $query->bindParam(20, $characterObject->{"wis"}, PDO::PARAM_INT);
        $query->bindParam(21, $characterObject->{"vit"}, PDO::PARAM_INT);
        $query->bindParam(22, $characterObject->{"dex"}, PDO::PARAM_INT);
        $query->bindParam(23, $characterObject->{"hitPoints"}, PDO::PARAM_INT);
        $query->bindParam(24, $characterObject->{"dmg"}, PDO::PARAM_INT);
        $query->bindParam(25, $characterObject->{"abilityPoints"}, PDO::PARAM_INT);
        $query->bindParam(26, $characterObject->{"speed"}, PDO::PARAM_INT);
        $query->bindParam(27, $characterObject->{"defense"}, PDO::PARAM_INT);
        $query->bindParam(28, $characterObject->{"magicDefense"}, PDO::PARAM_INT);
        $query->bindParam(29, $characterObject->{"magicAttack"}, PDO::PARAM_INT);
        $query->bindParam(30, $characterObject->{"hitRate"}, PDO::PARAM_INT);
        $query->bindParam(31, $characterObject->{"critRate"}, PDO::PARAM_INT);
        $query->bindParam(32, $characterObject->{"dodgeRate"}, PDO::PARAM_INT);


        //execute procedure.
        $query->execute();

        if($query->rowCount() > 0)
        {
            //return status code to be used in response message. 200 successful.
            $returnCode = 200;
        }

        $query->closeCursor();

        return $returnCode;
    }

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
            //echo $e->getMessage();
        }

        //Prepare SQL statement and bind the parameters for authenticating a username and password.
        $query = $dbh->prepare("CALL sp_AuthorizePlayer(?,?)");
        $query->bindParam(1, $username, PDO::PARAM_STR);
        $query->bindParam(2, $password, PDO::PARAM_STR);

        //Execute stored procedure with username and password passed in.
        $query->execute();

        //Turns the result from the stored procedure (if any) into an associative array.
        $results = $query->fetch(PDO::FETCH_ASSOC);

        if(!$results)
        {
            return;
        }

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