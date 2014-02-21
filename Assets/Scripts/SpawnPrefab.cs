using UnityEngine;
using System.Collections;


public class SpawnPrefab : MonoBehaviour {

    
    public GameObject SpawningPrefab;
    
    public GameObject spawn () {
        GameObject instance = Instantiate (SpawningPrefab, transform.position, transform.rotation) as GameObject;
        return instance;
    }

    public GameObject spawn (Vector3 deltaPos) {
        Vector3 newPos = transform.TransformPoint(deltaPos);
        newPos = new Vector3(newPos.x / transform.localScale.x, newPos.y / transform.localScale.y, newPos.z / transform.localScale.z);
        GameObject instance = Instantiate (SpawningPrefab, transform.position + newPos, transform.rotation) as GameObject;
        return instance;
    }
}
