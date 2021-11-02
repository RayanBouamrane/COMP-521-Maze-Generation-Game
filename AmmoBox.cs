using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject cameraObject;
    public Gun gun;
    public GameObject ammoBox;

    //Search world for object references
    void Start()
    {
	cameraObject = GameObject.FindWithTag("MainCamera");
        gun = cameraObject.GetComponent<Gun>();
    }

    //On collision with player, ammo box is destroyed and player ammo count increases
    void OnCollisionEnter(Collision c) {
	if (c.collider.tag == "Player") {
	    Destroy(ammoBox);
	    gun.totalAmmo++;
	}
    }
}
