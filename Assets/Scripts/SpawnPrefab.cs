using UnityEngine;
using System.Collections;


public class SpawnPrefab : MonoBehaviour {

	
	public GameObject SpawningPrefab;
	
	public void spawn () {
		Debug.Log (SpawningPrefab);
		if (SpawningPrefab != null) {
			Instantiate (SpawningPrefab, this.transform.position, Quaternion.identity);
		}
	}
}
