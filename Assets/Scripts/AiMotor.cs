using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Attractor))]
public class AiMotor : MonoBehaviour {
    
    public float moveSpeed = 8f; // move speed
    public float jumpSpeed = 15f; // vertical jump initial speed

    private Attractor attractor;
    [System.NonSerialized] public Vector3 currentMovement = Vector3.zero;
    
    
    void OnEnable () {
        
        attractor = this.gameObject.GetComponent<Attractor>();
    
    }
    
    void Update () {
        
        Vector3 MoveN = new Vector3 (currentMovement.x, 0,currentMovement.z);
        if (MoveN.sqrMagnitude > 1) MoveN = MoveN.normalized;
        transform.Translate(MoveN * Time.deltaTime * moveSpeed);   
    }
    
    
    public void Jump () {
    
        if (attractor.isGrounded)
        {
            rigidbody.velocity += jumpSpeed * attractor.objectNormal;
        }    
    }
}
