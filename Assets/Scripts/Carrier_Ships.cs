using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier_Ships : MonoBehaviour {

	
	void Start () {
		
	}
    float theta = 0f;
    public float radius =100f; 
    public Vector3 centerPoint; 
	void Update () {

        float x = centerPoint.x + Mathf.Sin(theta) * radius;
        float z = centerPoint.z - Mathf.Cos(theta) * radius;

        transform.position = new Vector3(x, 0, z);

        theta += 0.001f;

        Vector3 lookPos = centerPoint - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);








    }
}
