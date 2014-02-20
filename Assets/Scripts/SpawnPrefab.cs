using UnityEngine;
using System.Collections;


public class SpawnPrefab : MonoBehaviour {

	
	public GameObject SpawningPrefab;
	
	public void spawn () {
		if (SpawningPrefab != null) {
			Instantiate (SpawningPrefab, transform.position, transform.rotation);
		}
	}

    public void spawn (Vector3 deltaPos) {
        if (SpawningPrefab != null) {
            Vector3 newPos = transform.TransformPoint(deltaPos);
            newPos = new Vector3(newPos.x / transform.localScale.x, newPos.y / transform.localScale.y, newPos.z / transform.localScale.z);
            Instantiate (SpawningPrefab, transform.position + newPos, transform.rotation);
        }

    }
}
