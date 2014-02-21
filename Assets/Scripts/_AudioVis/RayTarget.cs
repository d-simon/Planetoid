using UnityEngine;
//using UnityEditor;
using System.Collections;

public class RayTarget : MonoBehaviour {
    
    private RaycastHit hit;
    private Ray ray;
    private Vector3 Direction;
    private GameObject tmpGo;
    //private Camera camera;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        
        //float sx = Screen.width/2f;
        //float sy = Screen.height/2f;
        //camera = Camera.current;
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.Log(sx + "  " + sy);
        //Debug.Log(Camera.current.name);
    
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) == true)
        {
            hit.collider.gameObject.transform.localScale = new Vector3(0.005498669f * 1.2f,0.005498669f * 1.2f, 0.005498669f * 1.2f);
            tmpGo = hit.collider.gameObject;
            Debug.Log("hit!");
        }
        else {
            if(tmpGo != null) tmpGo.transform.localScale = new Vector3(0.005498669f,0.005498669f,0.005498669f);
        }
    
    }
}
