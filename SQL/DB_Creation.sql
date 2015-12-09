SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `dbo` DEFAULT CHARACTER SET latin1 ;
USE `dbo` ;

-- -----------------------------------------------------
-- Table `dbo`.`Ability`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Ability` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Ability` (
  `abilityId` INT(11) NOT NULL AUTO_INCREMENT ,
  `statId` INT(11) NOT NULL ,
  `name` VARCHAR(45) NOT NULL DEFAULT '' ,
  `description` VARCHAR(100) NOT NULL DEFAULT '' ,
  PRIMARY KEY (`abilityId`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Levels`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Levels` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Levels` (
  `LevelID` INT(11) NOT NULL ,
  `LevelDescription` VARCHAR(45) NULL DEFAULT NULL ,
  PRIMARY KEY (`LevelID`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1
COMMENT = 'Levels for characters';


-- -----------------------------------------------------
-- Table `dbo`.`Player`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Player` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Player` (
  `playerId` INT(11) NOT NULL AUTO_INCREMENT ,
  `userName` VARCHAR(45) NULL DEFAULT NULL ,
  `password` VARCHAR(45) NULL DEFAULT NULL ,
  `firstName` VARCHAR(45) NULL DEFAULT NULL ,
  `lastName` VARCHAR(45) NULL DEFAULT NULL ,
  `email` VARCHAR(254) NULL DEFAULT NULL ,
  `lastlogin` DATETIME NULL DEFAULT NULL ,
  PRIMARY KEY (`playerId`) ,
  UNIQUE INDEX `username_UNIQUE` (`userName` ASC) )
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Squad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Squad` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Squad` (
  `SquadID` INT(11) NOT NULL AUTO_INCREMENT ,
  `playerId` INT(11) NOT NULL COMMENT 'FK to Player table' ,
  `Capasity` INT(11) NULL DEFAULT NULL COMMENT 'Amount of characters in the Squad' ,
  `SquadName` VARCHAR(45) NULL DEFAULT NULL COMMENT 'Name of Squad' ,
  PRIMARY KEY (`SquadID`) ,
  INDEX `FK_Squad_Player_idx` (`playerId` ASC) ,
  CONSTRAINT `FK_Squad_Player`
    FOREIGN KEY (`playerId` )
    REFERENCES `dbo`.`Player` (`playerId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Character`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Character` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Character` (
  `characterId` INT(11) NOT NULL ,
  `SquadID` INT(11) NOT NULL COMMENT 'One Squad has many Characters' ,
  `statId` INT(11) NOT NULL ,
  `LevelID` INT(11) NOT NULL ,
  `name` VARCHAR(45) NULL DEFAULT NULL ,
  `experience` INT(11) NULL DEFAULT NULL ,
  `helm` INT(11) NULL DEFAULT NULL ,
  `chest` INT(11) NULL DEFAULT NULL ,
  `gloves` INT(11) NULL DEFAULT NULL ,
  `pants` INT(11) NULL DEFAULT NULL ,
  `shoulders` INT(11) NULL DEFAULT NULL ,
  `boots` INT(11) NULL DEFAULT NULL ,
  `accessory1` INT(11) NULL DEFAULT NULL ,
  `accessory2` INT(11) NULL DEFAULT NULL ,
  `IsStandard` TINYINT(1) NULL ,
  PRIMARY KEY (`characterId`) ,
  INDEX `FK_Character_Level_idx` (`LevelID` ASC) ,
  INDEX `FK_Character_Squad_idx` (`SquadID` ASC) ,
  CONSTRAINT `FK_Character_Level`
    FOREIGN KEY (`LevelID` )
    REFERENCES `dbo`.`Levels` (`LevelID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Character_Squad`
    FOREIGN KEY (`SquadID` )
    REFERENCES `dbo`.`Squad` (`SquadID` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`CharacterAbilities`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`CharacterAbilities` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`CharacterAbilities` (
  `abilityId` INT(11) NOT NULL ,
  `characterId` INT(11) NOT NULL ,
  PRIMARY KEY (`abilityId`, `characterId`) ,
  INDEX `FK_CharacterAbilities_Character_idx` (`characterId` ASC) ,
  CONSTRAINT `FK_CharacterAbilities_Ability`
    FOREIGN KEY (`abilityId` )
    REFERENCES `dbo`.`Ability` (`abilityId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_CharacterAbilities_Character`
    FOREIGN KEY (`characterId` )
    REFERENCES `dbo`.`Character` (`characterId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Stats`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Stats` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Stats` (
  `statId` INT(11) NOT NULL AUTO_INCREMENT ,
  `str` INT(11) NULL DEFAULT '0' ,
  `int` INT(11) NULL DEFAULT '0' ,
  `agi` INT(11) NULL DEFAULT '0' ,
  `wis` INT(11) NULL DEFAULT '0' ,
  `vit` INT(11) NULL DEFAULT '0' ,
  `dex` INT(11) NULL DEFAULT '0' ,
  `hitPoints` INT(11) NULL DEFAULT '0' ,
  `dmg` INT(11) NULL DEFAULT '0' ,
  `abilityPoints` INT(11) NULL DEFAULT '0' ,
  `speed` INT(11) NULL DEFAULT '0' ,
  `defense` INT(11) NULL DEFAULT '0' ,
  `magicDefense` INT(11) NULL DEFAULT '0' ,
  `magicAttack` INT(11) NULL DEFAULT '0' ,
  `hitRate` INT(11) NULL DEFAULT '0' ,
  `critRate` INT(11) NULL DEFAULT '0' ,
  `dodgeRate` INT(11) NULL DEFAULT '0' ,
  PRIMARY KEY (`statId`) )
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Equipment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Equipment` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Equipment` (
  `equipmentId` INT(11) NOT NULL AUTO_INCREMENT ,
  `statId` INT(11) NOT NULL ,
  `slot` CHAR(1) NOT NULL ,
  PRIMARY KEY (`equipmentId`) ,
  INDEX `FK_Equipment_Stat_idx` (`statId` ASC) ,
  CONSTRAINT `FK_Equipment_Stat`
    FOREIGN KEY (`statId` )
    REFERENCES `dbo`.`Stats` (`statId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Item`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Item` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Item` (
  `itemId` INT(11) NOT NULL AUTO_INCREMENT ,
  `equipmentId` INT(11) NULL DEFAULT NULL ,
  `consumeId` INT(11) NULL DEFAULT NULL ,
  `name` VARCHAR(45) NOT NULL DEFAULT '' ,
  `description` VARCHAR(100) NOT NULL DEFAULT '' ,
  PRIMARY KEY (`itemId`) ,
  INDEX `FK_Item_Equipment_idx` (`equipmentId` ASC) ,
  CONSTRAINT `FK_Item_Equipment`
    FOREIGN KEY (`equipmentId` )
    REFERENCES `dbo`.`Equipment` (`equipmentId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `dbo`.`Inventory`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `dbo`.`Inventory` ;

CREATE  TABLE IF NOT EXISTS `dbo`.`Inventory` (
  `playerId` INT(11) NOT NULL ,
  `itemId` INT(11) NOT NULL ,
  `quantity` INT(11) NULL DEFAULT '1' ,
  PRIMARY KEY (`itemId`, `playerId`) ,
  INDEX `FK_Inventory_Player_idx` (`playerId` ASC) ,
  CONSTRAINT `FK_Inventory_Player`
    FOREIGN KEY (`playerId` )
    REFERENCES `dbo`.`Player` (`playerId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Inventory_Item`
    FOREIGN KEY (`itemId` )
    REFERENCES `dbo`.`Item` (`itemId` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;

USE `dbo` ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;