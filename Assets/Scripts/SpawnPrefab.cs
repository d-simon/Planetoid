using UnityEngine;
using System.Collections;


public class SpawnPrefab : MonoBehaviour {

	
	private GameObject _SpawningPrefab;
	
	public void spawn () {
		if (_SpawningPrefab != null) {
			Instantiate (_SpawningPrefab, this.transform.position, Quaternion.identity);
		}
	}


	public GameObject SpawningPrefab {
		set; get;
	}



}
