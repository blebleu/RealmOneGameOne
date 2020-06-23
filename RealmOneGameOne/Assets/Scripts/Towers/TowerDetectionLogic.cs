using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetectionLogic : MonoBehaviour
{
    public GameObject parentTowerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == GameController.ENEMY_TAG)
    //    {
    //        if (parentTowerObject.TryGetComponent<TowerBehavior>(out TowerBehavior towerBehavior)){
    //            towerBehavior.enemiesInRange.Add(other.gameObject);
    //            //Debug.Log(other.name + " has ENTERED the radius of" + gameObject.name);
    //        }

    //    }
    //}

    //void OnTriggerExit(Collider other) {
    //    if (other.tag == GameController.ENEMY_TAG) {
    //        if (parentTowerObject.TryGetComponent<TowerBehavior>(out TowerBehavior towerBehavior))
    //        {
    //            //Debug.Log(other.name + " has EXITED the radius of" + gameObject.name);
    //            towerBehavior.enemiesInRange.Remove(other.gameObject);
    //        }
    //    }

    //}
}
