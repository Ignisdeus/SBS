using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {
    [Range(0,10)]
    public float timeToWait;
    private void Start(){

        Destroy(gameObject, timeToWait);
    }
}
