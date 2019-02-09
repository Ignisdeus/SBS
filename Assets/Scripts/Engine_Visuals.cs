using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine_Visuals : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
    [Range(50, 150)]
    public float rotationSpeed = 50f;
    
	void Update () {



        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

	}
}
