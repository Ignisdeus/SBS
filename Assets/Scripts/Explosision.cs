using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosision : MonoBehaviour {

    public float forceToAdd = 200f; 

    private void Start(){

       Rigidbody var = gameObject.AddComponent<Rigidbody>();
       var.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1))* forceToAdd);
       Destroy(gameObject,2f);
    }
}

