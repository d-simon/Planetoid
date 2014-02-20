using UnityEngine;
using System.Collections;


public class SpawnPrefab : MonoBehaviour {

	
	public GameObject SpawningPrefab;
	
	public void spawn () {
		if (SpawningPrefab != null) {
			Instantiate (SpawningPrefab, this.transform.position, this.transform.rotation);
		}
	}
}
