using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boyed_Behaviour : MonoBehaviour {

	
	void Start () {
		
	}

    public Vector3  target, volocity, force;
    float maxSpeed = 5f, mass =1 ;
    public float banking = 0.1f;
    public Path path; 
    void Update () {


        force = CalculateBwehaviour();
        Vector3 accelaration = force / mass;
        volocity += accelaration * Time.deltaTime;
        transform.position += volocity * Time.deltaTime;

        if (volocity.magnitude > float.Epsilon)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (accelaration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + volocity, tempUp);
            transform.position += volocity * Time.deltaTime;


        }
       

    }
    GameObject targetGameobject;
    public bool seekEnabled = false, arriveEnabled = false, fleeEnabled = false, followPathEnabled = true; 
    Vector3 CalculateBwehaviour(){
        // make a default vector
        Vector3 force = Vector3.zero;
        // if i have a follow target
        if (targetGameobject != null){
            target = targetGameobject.transform.position;  
        }
        if(seekEnabled){
            force += Seek(target);
        }

        if(followPathEnabled){

            force += FollowPath();

        } 
        return force; 
        }
    Vector3 nextWayPoint; 
    Vector3 FollowPath(){
        nextWayPoint = path.NextWayPoint();

        if (!path.looped && path.IsLast()){

            return Vector3.zero; 
        } else{

            if(Vector3.Distance(transform.position, nextWayPoint) < 3){

                path.AdvanceToNext(); 
            }
            return Seek(nextWayPoint);
        }


    } 
    Vector3 Seek(Vector3 x){


        Vector3 desired = x - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - volocity; 
    }
}
