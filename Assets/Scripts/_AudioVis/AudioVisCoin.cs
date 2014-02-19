using UnityEngine;
using System.Collections;

public class AudioVisCoin : MonoBehaviour {

	private GameObject tmpPlayer;
	
	// Use this for initialization
	void OnStart()
	{
		
	}
	
	void OnEnable () {
		
		tmpPlayer = GameObject.Find("Player");
		
		/*iTween.FadeTo(this.gameObject, 0.3f, 1.0f);*/
		/*iTween.RotateBy(this.gameObject, iTween.Hash(
														
															"y", 1.0f,
															"time", 10.0f,
															"looptype", iTween.LoopType.loop,
															"easetype", iTween.EaseType.linear
														));
														*/
		/*iTween.MoveAdd(this.gameObject, iTween.Hash(
														
															"y", 0.2f,
															"time", Random.Range(0.8f,1.2f),
															"looptype", iTween.LoopType.pingPong,
															"easetype", iTween.EaseType.easeInOutQuad
														));	*/
		
	}
	
	
	public void Destruct() {
		//Debug.Log (tmpPlayer);

		iTween.Stop(this.gameObject);
		//iTween.MoveTo(this.gameObject ,iTween.Hash("position", tmpPlayer.transform.position, "time", 0.5f, "easetype", iTween.EaseType.easeInCubic));
		//iTween.ValueTo(this.gameObject, iTween.Hash("time", 0.3f, "from", this.gameObject.GetComponent<Light>().intensity ,  "to", 0f, "onUpdate", "changeLightIntensity"));
		
		
		
		//transform.parent = null;
		StartCoroutine( DestroyWithDelay(this.gameObject, 0.45f));
	}
     
	
	IEnumerator DestroyWithDelay(GameObject gO, float t)
	{
		 Vector3 startingPosition = gO.transform.localPosition;
   		 //Quaternion targetRotation =  Quaternion.Euler ( new Vector3 ( 0.0f, 0.0f, 200.0f ) );
		float elapsedTime = 0f;
   		 while (elapsedTime < t) {
			
   		    elapsedTime += Time.deltaTime;
			
     		transform.localPosition = Vector3.Lerp (startingPosition, tmpPlayer.transform.localPosition, (elapsedTime / t)   );  
		   
			yield return new WaitForEndOfFrame ();
		 }
		Destroy (gO);
	}
	
   	void changeLightIntensity(float tmpIntensity){
        this.gameObject.GetComponent<Light>().intensity = tmpIntensity;
    }
	
	
	void OnDisable() {
	
		iTween.Stop(this.gameObject);
	}

}
