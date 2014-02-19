using UnityEngine;
using System.Collections;

// DOES NOT WORK! (or do anything)

public class WalkTowardsTouch: MonoBehaviour {
	
	
	public float turnSpeed = 90f; 
	
	public float moveSpeed = 6f; 
	
void OnEnable()
	{
		Gesture.onMultiTapE += OnMultiTap;
		Gesture.onDraggingE += OnDragging;
	}
	
	void OnDisable()
	{
		Gesture.onMultiTapE -= OnMultiTap;
		Gesture.onDraggingE -= OnDragging;
	}
	
	bool isChildOfThisParent(RaycastHit tHit) // This checks if supplied hit.collider is a child of the parent		
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

	
	void OnMultiTap(Tap tap){
		
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap.pos);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
				
			if (isChildOfThisParent(hit) == true)
			{
				//float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		       // float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		       // Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
				Vector3 targetDir = this.transform.position - hit.transform.position;
				targetDir.y = 0;
				Quaternion targetRot = Quaternion.LookRotation(targetDir);
		        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
				transform.Translate(targetDir * moveSpeed * Time.deltaTime );
						
				
				 //transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);	
			}
		}
	}
	
	void OnDragging(DragInfo dragInfo){
		
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(dragInfo.pos);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			Debug.Log("Rayed!");
			if (isChildOfThisParent(hit) == true)
			{
				//float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		       // float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		       // Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
				Vector3 targetDir = this.transform.position - hit.point;
				targetDir.y = 0;
				Quaternion targetRot = Quaternion.LookRotation(targetDir);
		        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
				transform.Translate(targetDir);
						
						
				
				 //transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);	
			}
		}
	}
}
