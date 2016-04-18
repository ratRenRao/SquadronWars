using UnityEngine;
using System.Collections;

public class AbilityList : MonoBehaviour {

    public GameObject ArmorBreak;
    public GameObject Bash;
    public GameObject Bio;
    public GameObject Cure;
    public GameObject DoubleAttack;
    public GameObject FlameStrike;
    public GameObject Fire;
    public GameObject Focus;
    public GameObject Ice;
    public GameObject ScorchedEarth;
    public GameObject MeteorShower;
    public GameObject UltimateSummon;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowAbility(string ability)
    {
        if(ability.ToLower() == "armorbreak")
        {
            ArmorBreak.SetActive(true);
        }
        if (ability.ToLower() == "bash")
        {
            Bash.SetActive(true);
        }
        if (ability.ToLower() == "bio")
        {
            Bio.SetActive(true);
        }
        if (ability.ToLower() == "cure")
        {
            Cure.SetActive(true);
        }
        if (ability.ToLower() == "doubleattack")
        {
            DoubleAttack.SetActive(true);
        }
        if (ability.ToLower() == "flamestrike")
        {
            FlameStrike.SetActive(true);
        }
        if (ability.ToLower() == "fire")
        {
            Fire.SetActive(true);
        }
        if (ability.ToLower() == "focus")
        {
            Focus.SetActive(true);
        }
        if (ability.ToLower() == "ice")
        {
            Ice.SetActive(true);
        }
        if (ability.ToLower() == "scorchedearth")
        {
            ScorchedEarth.SetActive(true);
        }
        if (ability.ToLower() == "meteorshower")
        {
            MeteorShower.SetActive(true);
        }
        if (ability.ToLower() == "ultimatesummon")
        {
            UltimateSummon.SetActive(true);
        }
    }

    public void HideAbilities()
    {
            ArmorBreak.SetActive(false);
            Bash.SetActive(false);
            Bio.SetActive(false);
            Cure.SetActive(false);
            DoubleAttack.SetActive(false);
            FlameStrike.SetActive(false);
            Fire.SetActive(false);
            Focus.SetActive(false);
            Ice.SetActive(false);
            ScorchedEarth.SetActive(false);
            MeteorShower.SetActive(false);
            UltimateSummon.SetActive(false);        
    }

    public void ShowAllAbilities()
    {
        ArmorBreak.SetActive(true);
        Bash.SetActive(true);
        Bio.SetActive(true);
        Cure.SetActive(true);
        DoubleAttack.SetActive(true);
        FlameStrike.SetActive(true);
        Fire.SetActive(true);
        Focus.SetActive(true);
        Ice.SetActive(true);
        ScorchedEarth.SetActive(true);
    }
    public void ShowSuperAbilities()
    {
        MeteorShower.SetActive(true);
        UltimateSummon.SetActive(true);
    }
}
