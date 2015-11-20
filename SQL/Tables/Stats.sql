
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