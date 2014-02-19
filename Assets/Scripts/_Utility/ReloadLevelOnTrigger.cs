using UnityEngine;
using System.Collections;
using System.Text;
using System.Security;


public class ReloadLevelOnTrigger : MonoBehaviour {

	
	
	void Start () {
	
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		Debug.Log ("There are " + coins.Length + " Coins loaded in the scene.");
	}
	
	void Update () {
	
		
		if (Input.GetButtonDown("JoystickStart") == true)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
				
	
	}

	
	void OnTriggerEnter (Collider other) {
		//GameObject.Find("Player").GetComponent<Attractor>().enabled = false;
		//GameObject.Find ("Timer").GetComponent<Timer>()._enabled = false;
		Application.LoadLevel(Application.loadedLevel);
	}
	
	
}




