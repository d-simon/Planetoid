using UnityEngine;
using System.Collections;

public class CenterTo : MonoBehaviour {


    public GameObject centerTo;
    private Vector3 centerNormal;
    /*public float XRot = 0;
    public float YRot = 0;
    public float ZRot = 0;*/
    
    
    void Update() {
    
        if (centerTo != null) 
        {
            centerNormal = this.transform.position - centerTo.transform.position;
        }
        else if (this.transform.parent != null)
        {
            centerNormal = this.transform.position - this.transform.parent.transform.position;
        }
        
        
        // find forward direction 
        Vector3 myForward = Vector3.Cross(transform.right, centerNormal);
        // align character to the centerNormal while keeping the forward direction
        Quaternion targetRot = Quaternion.LookRotation(myForward, centerNormal);
        transform.rotation = targetRot;
        
        
    
    }
    void OnDrawGizmos() {
    
        if (centerTo != null) 
        {
            centerNormal = this.transform.position - centerTo.transform.position;
        }
        else if (this.transform.parent != null)
        {
            centerNormal = this.transform.position - this.transform.parent.transform.position;
        }
        
        
        // find forward direction 
        Vector3 myForward = Vector3.Cross(transform.right, centerNormal);
        // align character to the centerNormal while keeping the forward direction
       
        if ( centerNormal != Vector3.zero ) 
        {
            Quaternion targetRot = Quaternion.LookRotation(myForward, centerNormal);
                transform.rotation = targetRot;
        }
    
        
    
    }
}
