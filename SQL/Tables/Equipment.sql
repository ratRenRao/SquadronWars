
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