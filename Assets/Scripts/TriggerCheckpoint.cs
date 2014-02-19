using UnityEngine;
using System.Collections;

public class TriggerCheckpoint : MonoBehaviour {
	
	public GameObject thisCheckPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerSettings>()._CheckPoint = thisCheckPoint;
		}
	}
}
