using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
    
    public float orbitSpeed = 20f;
    public float orbitAxisX = 0;
    public float orbitAxisY = 0;
    public float orbitAxisZ = 0;
    
    public GameObject orbitAround;

    void Update () {
        
         transform.RotateAround ( 
            
            orbitAround.transform.position, 
            orbitAround.transform.up + new Vector3(orbitAxisX,orbitAxisY,orbitAxisZ), 
            orbitSpeed* Time.deltaTime
            
        );
    }
}
