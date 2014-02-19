using UnityEngine;
using System.Collections;

public class Attractor : MonoBehaviour {
	
	
	public float lerpSpeed = 10f; // smoothing speed
	public float gravity = 30f; // gravity acceleration

	public float deltaGround = 0.6f; // character is grounded up to this distance
	public float velocityMax = 20f;
	//public float jumpRange = 2f;
	
	
	[System.NonSerialized] public bool isGrounded;
	[System.NonSerialized] public Vector3 surfaceNormal; // current surface normal
	[System.NonSerialized] public Vector3 objectNormal; // character normal
	[System.NonSerialized] public float groundDist; // distance from character position to ground
	//[HideInInspector] public bool jumping = false; // flag jumping
	
	void Start () { // Initialization
	
		objectNormal = transform.up; // normal starts as character up direction 
	    rigidbody.freezeRotation = true; // disable physics rotation
	    
		// distance from transform.position to ground
		// groundDist = collider.bounds.extents.y + collider.center.y; 
		// TODO: Couldn't get collider.center to work in C#!
	   	groundDist = collider.bounds.extents.y;
		
	}
	
	void FixedUpdate(){
		// apply constant weight force according to character normal:
	    rigidbody.AddForce(-gravity*rigidbody.mass*objectNormal);
		
		
		// Limit velocity according to velocityMax and thus prevent excessive accumulation of force or orbiting
		// In terms of realism this is not the most elegant solution - TODO: prevent the aforementioned issues more realistically
		float vMagnitude = rigidbody.velocity.magnitude;
	    if(velocityMax < vMagnitude || vMagnitude < -velocityMax)
	    {
			Vector3 normalizedV = Vector3.Normalize(rigidbody.velocity);
			rigidbody.velocity = normalizedV * velocityMax;
		}

	}
		
	
	public bool isChildOfThisParent(RaycastHit tHit) // This checks if supplied hit.collider is a child of the parent		
	{
		Transform[] Children = transform.parent.gameObject.GetComponentsInChildren<Transform>();
		for (int index = 0; index < Children.Length; index++)  
		{
			
			if( Children[index].gameObject == tHit.collider.gameObject)
			{
				return true;
			}
		}
		return false;
	}
	
	public Vector3 CheckNormal(RaycastHit nHit) {
	
	Vector3 returnNormal = nHit.normal;
		
		if (nHit.collider.tag == "PlattformTopOnly")
		{
			returnNormal = nHit.collider.transform.up;
		}
		else if (nHit.collider.tag == "PlanetCenterDrag")
		{
			returnNormal = this.transform.position - this.transform.parent.transform.position;
		}
		else if (nHit.collider.tag == "PlanetCenterOnly")
		{
			returnNormal = this.transform.position - this.transform.parent.transform.position;
			returnNormal = returnNormal.normalized;

		}
		
		return returnNormal;
	}
	
	
	
	public virtual void Update () {
		
	    // update surface normal and isGrounded:
	   	Ray ray;
	   	RaycastHit hit;
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
			        // assume usual ground normal to avoid "falling forever"
			        // surfaceNormal = Vector3.up; 
			        
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
		
	}
	
	
/*
	protected IEnumerator JumpToWall(Vector3 point, Vector3 normal){
	    // jump to wall 
	    jumping = true; 
	    rigidbody.isKinematic = true; // disable physics while jumping
	    Vector3 orgPos = transform.position;
	    Quaternion orgRot = transform.rotation;
	    Vector3 dstPos = point + normal * (groundDist + 0.5f); 
	    Vector3 myForward = Vector3.Cross(transform.right, normal);
	    Quaternion dstRot = Quaternion.LookRotation(myForward, normal);
	    for (float t = 0.0f ; t < 0.6f; )
		{
	        t += Time.deltaTime;
	        transform.position = Vector3.Lerp(orgPos, dstPos, t);
	        transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
	        yield return 0; // return here next frame
	    }
	    objectNormal = normal;
	    rigidbody.isKinematic = false; 
	    jumping = false;
	}
*/
}






