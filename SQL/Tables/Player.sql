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