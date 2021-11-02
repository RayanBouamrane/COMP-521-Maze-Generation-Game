using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{

    public Text t;

    // display instructions for 5 seconds
    IEnumerator Start()
    {
        
	yield return new WaitForSeconds(5f);
	Destroy(t);

    }

}
