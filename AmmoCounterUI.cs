using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterUI : MonoBehaviour
{

    public GameObject cameraObject;
    public Gun gun;
    public Text t;

    //This class displays total ammo a player has at any time;
    void Start()
    {
	cameraObject = GameObject.FindWithTag("MainCamera");
        gun = cameraObject.GetComponent<Gun>();
    }

    void Update()
    {
        t.text = "Ammo Count: " + gun.totalAmmo.ToString();
    }
}
