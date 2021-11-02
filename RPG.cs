using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : MonoBehaviour
{
    public GameObject rpg;
    public Rigidbody rigidRPG;
    public GameManager gm;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
	gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rigidRPG = rpg.GetComponent<Rigidbody>();

	//if rocket doesn't collide, it is destroyed after 6 seconds

	Destroy(rpg, 6);
	GameObject p = GameObject.FindWithTag("Player");
	Collider pc = p.GetComponent<Collider>();
	Collider rpgc = rpg.GetComponent<Collider>();

	//avoids bug of player colliding with rocket
	Physics.IgnoreCollision(pc, rpgc);
	
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	//rocket is pushed in current direction continuously
        rigidRPG.AddForce(rpg.transform.forward * speed);
    }

    void OnCollisionEnter(Collision c) { 

	//if the rocket collides with a maze platform, destroy the platform and update variable
	if (c.collider.tag == "FloatingPlatform") {
	    Destroy(c.gameObject);
	    gm.platformHasBeenDestroyed = true;
	}
	//rocket destroyed if colliding with world (except the player)
	if(c.collider.tag != "Player") 
	    Destroy(rpg); 
	
    }
}
