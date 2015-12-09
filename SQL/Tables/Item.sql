
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
