-- -----------------------------------------------------
-- procedure sp_GetEquipment
-- -----------------------------------------------------


CREATE  PROCEDURE sp_GetEquipment()
BEGIN	
    
    
select e.equipmentId , e.statId,  e.slot , s.str, s.int, s.agi, wis, vit, dex, hitPoints, dmg, abilityPoints, speed, defense, magicDefense, magicAttack, hitRate, critRate, dodgeRate
from 
(select * from dbo.Equipment) e
left join
(select * from dbo.Stats) s
on e.statId = s.StatId;

    
    
    
    
END