using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.GameClasses;

namespace Assets.Scripts {
    public class AbilityButton : MonoBehaviour {

        public GameObject mainPanel;
        public GameObject abilityPanel;
        public GameObject gameController;
        public Button button;
        public string ability;
        public string abilityType;
        // Use this for initialization
        void Start() {
            button.onClick.AddListener(() => {
                Debug.Log("Click Event Called");
                mainPanel.SetActive(true);
                abilityPanel.SetActive(false);
                if (abilityType == "cast")
                {
                    gameController.GetComponent<GameController>().CastAbility("SpellPrefabs/" + ability);
                }
                else {
                    gameController.GetComponent<GameController>().AttackAbility("SpellPrefabs/" + ability);
                }
            });
        }

        // Update is called once per frame
        void Update() {

        }

    }
}