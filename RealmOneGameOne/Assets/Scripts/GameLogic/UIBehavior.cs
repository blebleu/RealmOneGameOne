using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    public GameObject creditsLabel;
    public GameObject baseHealthLabel;
    // Start is called before the first frame update
    void Start()
    {
        baseHealthLabel.GetComponent<Text>().text = "Base Health: " + GameController.GetCurrentBaseHealth();
        GameController.baseHealthChangeEvent.AddListener(UpdateBaseHealthLabel);

        creditsLabel.GetComponent<Text>().text = "Credits: " + GameController.GetCurrentCredits();
        GameController.creditsChangeEvent.AddListener(UpdateCreditsLabel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCreditsLabel() {
        creditsLabel.GetComponent<Text>().text = "Credits: " + GameController.GetCurrentCredits();
    }
    void UpdateBaseHealthLabel() {
        baseHealthLabel.GetComponent<Text>().text = "Base Health: " + GameController.GetCurrentBaseHealth();
    }
}
