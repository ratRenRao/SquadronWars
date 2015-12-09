using UnityEngine;
using SquadronWars2;
using UnityEngine.UI;
using System.Collections;
using System;

public class SampleButton : MonoBehaviour
{

    public Button button;
    public Character character;
    public Text nameLabel;

    public void BuildCharacterScreen()
    {
        GameObject menuManager = GameObject.FindGameObjectWithTag("MenuManager");
        GameObject statsManager = GameObject.FindGameObjectWithTag("CharacterStats");
        SampleButton button = gameObject.GetComponent<SampleButton>();
        MenuManager menu = menuManager.GetComponent<MenuManager>();
        CharacterScreen stats = statsManager.GetComponent<CharacterScreen>();
        SpriteRenderer sprite = GameObject.FindGameObjectWithTag("Character1").GetComponent<SpriteRenderer>();
        character.sprite = sprite.sprite;
        menu.squadScreenPanel.SetActive(false);
        menu.characterScreenPanel.SetActive(true);
        stats.characterSprite.sprite = character.sprite;
        stats.characterName.text = character.name;
        stats.strengthStat.text = character.stats.strength.ToString();
        stats.agilityStat.text = character.stats.agility.ToString();
        stats.intelligenceStat.text = character.stats.intelligence.ToString();
        stats.vitalityStat.text = character.stats.vitality.ToString();
        stats.dexterityStat.text = character.stats.dexterity.ToString();
        stats.wisdomStat.text = character.stats.wisdom.ToString();
        stats.luckStat.text = character.stats.luck.ToString();
        stats.hitPointsStat.text = character.stats.calculateHP().ToString();
        stats.manaStat.text = character.stats.calculateMP().ToString();
        stats.damageStat.text = character.stats.calculateDamage().ToString();
        stats.magicDamageStat.text = character.stats.calculateMagicDamage().ToString();
        stats.speedStat.text = character.stats.calculateSpeed().ToString();
        stats.defenseStat.text = character.stats.calculateDefense().ToString();
        stats.magicDefenseStat.text = character.stats.calculateMagicDefense().ToString();
        stats.hitRateStat.text = character.stats.calculateHitRate().ToString();
        stats.dodgeRateStat.text = character.stats.calculateDodgeRate().ToString();
        stats.criticalRateStat.text = character.stats.calculateCritRate().ToString();
        stats.levelStat.text = character.stats.level.ToString();
        int expToNextLevel = (int)Math.Floor(character.stats.level * 1.5 * 200);
        stats.experienceStat.text = string.Format("{0} / {1}", character.stats.experience.ToString(), expToNextLevel.ToString());
        double tempVal = ((double)character.stats.experience / expToNextLevel) * 100;
        int progBar = Convert.ToInt32(tempVal);
        stats.ProgressBar.value = progBar;
        
    }
}
