using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour {

	public float speed;


    Vector3 rotationDirection;

    private void Start(){

        rotationDirection = new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));
        
    }
    void Update () {

        transform.Rotate(rotationDirection * speed * Time.deltaTime);

	}
}
