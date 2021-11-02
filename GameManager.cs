using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{

    public bool platformHasBeenDestroyed = false;

    bool gameOver = false;    
    public GameObject player;
    public Player p;
    public Rigidbody rb;
    public RigidbodyFirstPersonController rbfps;
    public GameObject camera;
    public Camera c;
    public Gun g;
    public Text t;


    //RigidbodyFirstPersonController is an imported asset
    //it is used to access the camera controls sensitivity, and set it to zero when the game ends
    void Start() {
	player = GameObject.FindWithTag("Player");
	rb = player.GetComponent<Rigidbody>();
	p = player.GetComponent<Player>();
	camera = GameObject.FindWithTag("MainCamera");
	c = camera.GetComponent<Camera>();
	g = c.GetComponent<Gun>();
	rbfps = p.GetComponent<RigidbodyFirstPersonController>();
    }

    void Update() {
	
	//player has a isOnEndPlatform variable updated every time
	//they contact a walking surface

	//if the player is on the end platform (has crossed maze) and has destroyed maze solution, win condition
	//if the player is not at the end, but destroyed solution, lose condition
	if (p.isOnEndPlatform && platformHasBeenDestroyed)
	    WinGame();
	else if (platformHasBeenDestroyed)
	    LoseGame();
	
	//if there are no more ammo boxes, and the player is out of ammo, lose condition
	if(g.totalAmmo <= 0 && !GameObject.FindWithTag("AmmoBox"))
    	    LoseGame();

	//if the player's y position is low, they have fallen off the platform, lose condition
	if (player.transform.position.y < -10)
	    LoseGame();
    }

    
    public void LoseGame() {
	if (!gameOver) {
	    gameOver = true;
	    t.text = "You Lose!";
	    EndGame();
	}
    }
    
    public void WinGame() {
	if (!gameOver) {
	    gameOver = true;
	    t.text = "You Win!";
	    EndGame();
	}
    }

    public void EndGame() {
	
	//player movement is restricted by setting rigidBody to kinematic
	//camera movement is disable by changing mouse sensitivity to 0
	rb.isKinematic = true;
	rbfps.mouseLook.XSensitivity = 0f;
	rbfps.mouseLook.YSensitivity = 0f;
    }
}
