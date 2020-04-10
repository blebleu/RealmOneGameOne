using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class waveCount : MonoBehaviour
{

    public int wave;
    public Text waveDisplay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplay.text = wave.ToString();

        
        if(Input.GetMouseButtonDown(0)){

            wave++;

        }

    }
}
