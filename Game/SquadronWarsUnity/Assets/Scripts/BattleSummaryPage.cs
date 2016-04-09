using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        if (gold < maxGold)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                gold += 1;
            goldAmount.text = gold.ToString();
        }
    }
}
