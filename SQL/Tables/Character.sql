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
