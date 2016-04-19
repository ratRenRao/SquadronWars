using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;

public class SelectCharacterModel : MonoBehaviour {

    public Text characterId;
    public int id;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCharacterId()
    {
        characterId.text = id.ToString();
    }
}
