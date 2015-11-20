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