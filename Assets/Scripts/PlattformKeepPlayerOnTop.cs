using UnityEngine;
using System.Collections;

/** 
 * Helps keeping charactercontroller entities nicely on the platform
 * Needs a Collider set as trigger in the gameobject this script is added to
 * works best if collider is bit smaller as platform but extends quite a lot
 * (say .5m or so) above the platform, As the platform possibly already has
 * a normal collider the easiest way is to add a GameObject to the platform,
 * give it a trigger collider and add this script. The yOffset is the vertical 
 * offset the character should have above the platform (a good value to start
 * with is half the y value of the Collider size).
 * 
 * http://answers.unity3d.com/questions/8207/charactercontroller-falls-through-or-slips-off-mov.html
 * 
 * David Simon / 15.12.12: slightly edited
 */
public class PlattformKeepPlayerOnTop : MonoBehaviour {

    // helper struct to contain the transform of the player and the
    // vertical offset of the player (how high the center of the
    // charcontroller must be above the center of the platform)
    public struct Data {
    	public Data(Rigidbody ctrl, Transform t, float yOffset) {
    		this.ctrl = ctrl;
    		this.t = t;
    		this.yOffset = yOffset;
    	}
    	public Rigidbody ctrl; // the char controller
    	public Transform t; // transform of char
    	public float yOffset; // y offset of char above platform center
    };

    public float verticalOffset = 0.1f; // height above the center of object the char must be kept

    // store all playercontrollers currently on platform
    private Hashtable onPlatform = new Hashtable();

    // used to calculate horizontal movement
    private Vector3 lastPos;
	
	private Vector3 relLastPos;
	

    void OnTriggerEnter(Collider other) {
    	Rigidbody ctrl = other.GetComponent(typeof(Rigidbody)) as Rigidbody;

    	// make sure we only move objects that are rigidbodies or charactercontrollers.
    	// this to prevent we move elements of the level itself
    	if (ctrl == null) return;
		
    	Transform t = other.transform; // transform of character

    	// we calculate the yOffset from the character height and center
    	float yOffset = /*ctrl.height / 2f - ctrl.center.y*/ verticalOffset;

    	Data data = new Data(ctrl, t, yOffset);

    	// add it to table of characters on this platform
    	// we use the transform as key
    	onPlatform.Add(other.transform, data);
    }
	
/*	void OnTriggerStay(Collider other) {

		
		mPoint(other.transform.position);
		
		
		
		 // let's loop over all characters in the table
    	foreach (DictionaryEntry d in onPlatform) {
			
    		Data data = (Data) d.Value; // get the data
    		float charYVelocity = data.ctrl.velocity.y;
			
			relLastPos = relCurPos;
         relCurPos = this.transform.InverseTransfor
    		Vector3 relDelta = relCurPos - relLastPos;// check if char seems to be jumping
    		//if ((charYVelocity <= 0f) || (charYVelocity <= yVelocity)) {
    			Vector3 pos = data.t.position; // current charactercontroller position
    			//pos.y = y + data.yOffset; // adjust to new platform height
    			pos += delta; // adjust to horizontal movement
				
				Debug.Log ("delta = " + delta + relDelta);
    			data.t.position = pos; // and write it back!
    		//}
    	}
		
	}*/

    void OnTriggerExit(Collider other) {
    	// remove (if in table) the uncollided transform
    	onPlatform.Remove(other.transform);
		
    }

    void Start() {
    	lastPos = transform.position;
		
    }

    void FixedUpdate () {
    	Vector3 curPos = transform.position;
    	Vector3 delta = curPos - lastPos;
    	lastPos = curPos;

    	foreach (DictionaryEntry d in onPlatform) {
			
    			Data data = (Data) d.Value;
				Vector3 pos = data.t.position; 
    			pos += delta;
    			data.t.position = pos;
    	}
    }
}