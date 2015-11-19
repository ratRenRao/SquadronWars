

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