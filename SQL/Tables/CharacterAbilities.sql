
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