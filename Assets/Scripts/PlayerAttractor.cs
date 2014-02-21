using UnityEngine;
using System.Collections;


public class PlayerAttractor : Attractor { // extends the basic Attractor.cs
    
    
    public float moveSpeed = 8f; // move speed
    public float jumpSpeed = 15f; // vertical jump initial speed

    
    public override void Update() {
        
        // jump code - jump to wall or simple jump  
        //if (jumping) return;  // abort Update while jumping to a wall

           Ray ray;
           RaycastHit hit;
        if ( Input.GetButtonDown("Jump")){ // jump pressed:
            if (isGrounded){    
                 rigidbody.velocity += jumpSpeed * objectNormal;    
            }  
        }
        
        #if UNITY_IPHONE 
        VCButtonBase jumpbtn = VCButtonBase.GetInstance("Jump");
        
        if ( jumpbtn.PressBeganThisFrame == true){ // jump pressed:
            ray = new Ray(transform.position, transform.forward);

            if (isGrounded){
                
                 rigidbody.velocity += jumpSpeed * objectNormal;
            }  
        }
        #endif
        
        
        // update surface normal and isGrounded:
        ray = new Ray(transform.position, -objectNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit) ){ // use it to update objectNormal and isGrounded  
            
    
            // again we check if the object we are standing on isAChild or is the parent itself
            if (hit.collider.tag != "PlayerBlock" && (hit.collider.transform == transform.parent.transform || isChildOfThisParent(hit) == true ))
            {
                if (hit.distance <= groundDist + deltaGround ) 
                {
                    isGrounded = true;
                    surfaceNormal = CheckNormal(hit); //hit.normal;
                }
                else
                {
                    isGrounded = false;                    
                    surfaceNormal = CheckNormal(hit); //hit.normal;
                 }    
            }
            else
                {
                    isGrounded = false;
                    // assume usual ground normal to avoid "falling forever"
                    // -> center point of parent (planet) 
                    
                    surfaceNormal = transform.position - transform.parent.transform.position;
                    surfaceNormal = surfaceNormal.normalized;
                 }    
        }
        else 
        {
            isGrounded = false;
            // slightly adjusted, this allows to "find back" to the planet, if we fail to ray it
            // TODO: this enables to object to "orbit"  - limit rigidbody velocity! 
            //         and find a better solution: "wind resistance" could help. Reducing velocity over time.
            if (this.transform.parent != null) 
                {
                surfaceNormal = transform.position - transform.parent.transform.position;
                surfaceNormal = surfaceNormal.normalized;
                }
            else
                {
                    surfaceNormal = Vector3.up;
                }
        }
        objectNormal = Vector3.Lerp(objectNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
        // find forward direction with new objectNormal
        Vector3 myForward = Vector3.Cross(transform.right, objectNormal);
        // align character to the new objectNormal while keeping the forward direction
        Quaternion targetRot = Quaternion.LookRotation(myForward, objectNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);
        
        
        // Movement
        Vector3 MoveN = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (MoveN.sqrMagnitude > 1) MoveN = MoveN.normalized;
        transform.Translate(MoveN * Time.deltaTime * moveSpeed);
        
        #if UNITY_IPHONE 
        VCAnalogJoystickBase stickMove = VCAnalogJoystickBase.GetInstance("stickMove");
            if (stickMove != null)
            {    
                transform.Translate( moveSpeed * Time.deltaTime * stickMove.AxisX, 0.0f ,moveSpeed * Time.deltaTime * stickMove.AxisY);
            }
        #endif

        
        
        
        
    }
}


