using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneMaster : MonoBehaviour {

    private void Start(){

        InvokeRepeating("Spawn", 1f,2f);
        
    }

    public GameObject drone;
    public GameObject[] spawnPoints; 
    int count = 1;
    int maxBugs = 9; 
    void Spawn(){
         
        if(GameMaster.bugCount < maxBugs)
        {
            int i = (int)Random.Range(0, spawnPoints.Length);
            GameObject x =  Instantiate(drone, spawnPoints[i].transform.position, Quaternion.identity);
            x.GetComponent<Health>().SpawnerObject(spawnPoints[i]);
            GameMaster.bugCount++; 
        }
    }
}
