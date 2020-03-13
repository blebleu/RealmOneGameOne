using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroCountDisplay : MonoBehaviour
{
    
    public int heroesCount;
    public bool isDefeated;
    public Text heroesCountDisplay;



    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
        heroesCountDisplay.text = heroesCount.ToString();
        
        if(Input.GetMouseButtonDown(1) && !isDefeated && heroesCount > 0){

            isDefeated = true;
            heroesCount--;
            isDefeated = false;


        }
    }
}
