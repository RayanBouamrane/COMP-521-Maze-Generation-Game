using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isOnEndPlatform = false;    

    //updates whether the player is on the other side of maze or not
    void OnCollisionEnter(Collision c) {
	if (c.collider.tag == "End")
	    isOnEndPlatform = true;
	else 
	    isOnEndPlatform = false;
	
    }
}