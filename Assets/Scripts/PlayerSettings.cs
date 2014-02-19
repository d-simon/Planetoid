using UnityEngine;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

	public GameObject _CheckPoint;
	public bool StartAtCheckPoint = true;
	public int _CoinCount = 0;
	
	
	void Start() {
		if (StartAtCheckPoint == true && _CheckPoint != null) 
		{
			this.gameObject.GetComponent<PlayerTeleport>().PlayerDoTeleport(this.gameObject);
			
		}
	}
}
