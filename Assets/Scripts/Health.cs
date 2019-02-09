using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health  = 100;


    public GameObject spawner;
    
    

    public bool littleShip;
    public void SpawnerObject(GameObject x){

        spawner = x;
        
    }

    private void Update(){

        if (health <= 0)
        {
            DestoryThisObject();
        }
    }
    public void HealthLoss(string x){

        switch (x){

            case "Blaster":
                health -= 25;
                break;

            case "SpaceObject":
                health -= 100;
                break;
            default:
                break;
        }

    }
    Vector3 myStartingPos;
    void ObjectReset(){
        if (littleShip)
        {
            GetComponent<ShipBehaviour>().ResetFormation();
            gameObject.tag = "drone";
            transform.position = spawner.transform.position;
            health = 100;
            
            //GetComponent<ShipBehaviour>().role = 2;
           
        }
        else {


        }

        }

    public GameObject explParts,expl;
    void DestoryThisObject(){
        ObjectReset();
        Instantiate(explParts, transform.position, Quaternion.identity);
        Instantiate(expl, transform.position, Quaternion.identity);

       
    }


}
