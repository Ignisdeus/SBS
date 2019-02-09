using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameObject GM; 

    public static int bugCount = 0, humanCount = 0;
    void Awake () {

        GM = this.gameObject;
	}
    
}
