using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject rpg;
    public Camera cam;
    public int totalAmmo = 0;
    // Update is called once per frame
    void Update()
    {
	//if mouse clicked and there are no other projectiles in the game world, we can shoot
        if (Input.GetMouseButton(0) && totalAmmo > 0 && !GameObject.FindWithTag("Projectile")) {
	    totalAmmo--;
	    Shoot();
	}	
    }

    void FixedUpdate() {
		
    }
    void Shoot() {
	//create a rocket from position, in direction of Gun (located in camera)
	Instantiate(rpg, transform.position, transform.rotation);

    }
}
