using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    // make a list of Vector3's 
    public List<Vector3> wayPoints = new List<Vector3>();
    int next = 0;
    public bool looped = true; 
    
    
    void Start () {


        wayPoints.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i ++){

            wayPoints.Add(transform.GetChild(i).position);

        } 
	}
    public void OnDrawGizmos()
    {
        int count = looped ? (transform.childCount + 1) : transform.childCount;
        Gizmos.color = Color.cyan;
        for (int i = 1; i < count; i++)
        {
            Transform prev = transform.GetChild(i - 1);
            Transform next = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(prev.transform.position, next.transform.position);
            Gizmos.DrawSphere(prev.position, 1);
            Gizmos.DrawSphere(next.position, 1);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Vector3 NextWayPoint(){

        return wayPoints[next];

    }

    public void AdvanceToNext(){

        if(looped){
            next = (next + 1) % wayPoints.Count;
        }else{

            if(next != wayPoints.Count -1){
                next++; 
            }

        }

    } 

    public bool IsLast(){


        return next == wayPoints.Count - 1; 
    }
    }
