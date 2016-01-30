CREATE TABLE `AbilityPreReq` (
  `abilityId` int(11) NOT NULL,
  `prereqAbilityId` int(11) NOT NULL,
  `prereqAbilityLevel` int(11) DEFAULT '1',
  PRIMARY KEY (`abilityId`,`prereqAbilityId`),
  KEY `FK_AbilityPreReq_Ability2_idx` (`prereqAbilityId`),
  CONSTRAINT `FK_AbilityPreReq_Ability2` FOREIGN KEY (`prereqAbilityId`) REFERENCES `Ability` (`abilityId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_AbilityPreReq_Ability1` FOREIGN KEY (`abilityId`) REFERENCES `Ability` (`abilityId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
