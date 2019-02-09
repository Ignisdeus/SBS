using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour {

    public static int droneCount = 0 ; 
    [Range(0, 100)]
    public float speed = 20;
    public GameObject blasterBolt;
    public Transform[] muzzlePoint;
    
    public enum protocall {formation, seek, engage}

    
    public protocall state;
    int myPos = 0; 
    public bool[] formationSlots = {false, false};
    public string leader, drone;

    public void ResetFormation(){

        for ( int i = 0; i < formationSlots.Length; i ++){
            formationSlots[i] = false; 
        }
        if (role == 1)
        {
            BrakeFormation();
           
        }

        role = 2;

    }

    GameObject[] myFormation = new GameObject[2];
    int shipCount = 0;
    public int AddShip(GameObject x ){

        myFormation[shipCount] = x;
        shipCount++;

        return shipCount - 1; 



    }
    void Start(){
        //Seek();
        
        state = protocall.formation;

        gameObject.tag = drone;
        //myPos = ShipBehaviour.droneCount % 3; 
        ShipBehaviour.droneCount++; 

        Seek();
    }

    [Range(1,5)]
    public int distanceBetweenShips= 2;
    void Update(){
        
        switch(state){

            case protocall.engage:
                    if(canFire){
                        StartCoroutine(Attack());
                    }
                break;
            case protocall.formation:
                if(gameObject.tag == drone){
                    // if there is no leader check for a new leader 
                    if (myLeader == null)
                    {
                        //gameObject.tag = leader;
                        Seek();
                    }

                    if((myLeader != null)){
                        if (myPos == 1){
                            targetLocation = new Vector3(myLeader.transform.position.x + distanceBetweenShips, myLeader.transform.position.y, myLeader.transform.position.z - distanceBetweenShips); 
                        }else{
                            targetLocation = new Vector3(myLeader.transform.position.x - distanceBetweenShips, myLeader.transform.position.y, myLeader.transform.position.z - distanceBetweenShips);
                        }
                    }


                }


                break;
            case protocall.seek:

                break;
            default:
                break;
        }

        Movement();
    }


    public float lerpSpeed;
    Vector3 targetLocation;
    GameObject myLeader;
    void Seek(){

        if( gameObject.tag == leader){
            Debug.Log("now Loc");
            targetLocation = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), Random.Range(-25, 25));
        }else{

            GameObject[] leaderCheck = GameObject.FindGameObjectsWithTag(leader);

            for(int i =0; i < leaderCheck.Length; i ++){

                for(int j =0; j < 2; j ++){
                    if(leaderCheck[i].GetComponent<ShipBehaviour>().formationSlots[j] == false){
                        leaderCheck[i].GetComponent<ShipBehaviour>().formationSlots[j] = true;
                        myLeader = leaderCheck[i];
                        myPos = myLeader.GetComponent<ShipBehaviour>().AddShip(this.gameObject);
                        break;

                    }
                }

                if (myLeader != null){
                    break;
                }
            }

            if(myLeader == null){

                gameObject.tag = leader;
                role = 1;
            }


        }
    }
    int role = 2;

    Ray ray;
    RaycastHit hit;
    public Transform rayCastStartingPoint;
    [Range(1,50)]
    float rayRange = 25f;
    [Range(45, 45)]
    float rayRotation = 0f, rotationSpeed = 0.1f;

    void Movement(){
        Vector3 lookPos = targetLocation - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lerpSpeed);


        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float dist = Vector3.Distance(targetLocation, transform.position);
        switch (role){
            //leader movment changes 
            case 1:

                if (dist < 10){
                    Seek();

                }

                rayRotation += rotationSpeed * Time.deltaTime;
                rayCastStartingPoint.rotation = Quaternion.Euler(rayCastStartingPoint.rotation.x, rayRotation, rayCastStartingPoint.rotation.z);
                ray.origin = rayCastStartingPoint.transform.position;
                ray.direction = transform.TransformDirection(new Vector3(rayRotation, 0,1));


                if (rayRotation >= 0.25f){
                    rotationSpeed =  -(rotationSpeed);
                }else if(rayRotation <= -0.25f){
                    rotationSpeed = Mathf.Abs( rotationSpeed);
                }

                //rayCastStartingPoint.transform.rotation = Quaternion.Euler(0, rayRotation, 0 );
                Debug.DrawRay(ray.origin, ray.direction * rayRange, Color.white);
                if (Physics.Raycast(ray, out hit, rayRange)){
                    if(hit.collider.tag == "FleetOne"){

                        Engage();
                        targetLocation = hit.transform.position;

                    }

                }
                break;
            // drone changes in movement
            case 2:



                if (dist > distanceBetweenShips){
                    speed = 8;
                } else{
                    speed = 5;
                }

                //if(myLeader == null){
                   // Seek();
                //}
                if(dist > 20){

                    Seek();
                }

                break;
            // if there is a mistake this happens
            default:
                break;
        }
       
    }

    [Range(0, 5)]
    float fireRate= 0.5f;
    bool canFire = true;

    public void BrakeFormation(){


        for(int i =0; i < myFormation.Length; i ++){

            myFormation[i].GetComponent<ShipBehaviour>().LostLeader();
        }

    }

    public void LostLeader(){
        myLeader = null; 
    }
    void Engage(){

        Debug.Log("Engaged");
    }
    IEnumerator Attack(){

        canFire = false;
        yield return new WaitForSeconds(fireRate);

        for(int i = 0; i < muzzlePoint.Length; i ++){
            Instantiate(blasterBolt, muzzlePoint[i].transform.position, Quaternion.Euler(0,90,0));
        }
        canFire = true;
    }
}
