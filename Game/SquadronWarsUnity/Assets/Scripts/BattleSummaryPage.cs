using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.GameClasses;

public class BattleSummaryPage : MonoBehaviour
{

    public Text goldAmount;
    public int gold = 0;
    public int maxGold = 100;
    

	void Start ()
    {
        goldAmount.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (0 < GlobalConstants.EarnedGold)
        {
            GlobalConstants.Player.gold += 1;
            goldAmount.text = GlobalConstants.Player.gold.ToString();
            GlobalConstants.EarnedGold -= 1;
        }
    }
}
