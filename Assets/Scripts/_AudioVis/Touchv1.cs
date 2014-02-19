using UnityEngine;
using System.Collections;


public class Touchv1 : MonoBehaviour {

	public bool hasPlayed = false;
	
	void OnEnable(){
		//these events are obsolete, replaced by onMultiTapE, but it's still usable
		//Gesture.onShortTapE += OnShortTap;
		//Gesture.onDoubleTapE += OnDoubleTap;
		//Gesture.onLongTapE += OnLongTap;
		
		Gesture.onMultiTapE += OnMultiTap;
		
		audio.Play();

	}
	
	void OnDisable(){
		//these events are obsolete, replaced by onMultiTapE, but it's still usable
		//Gesture.onShortTapE -= OnShortTap;
		//Gesture.onDoubleTapE -= OnDoubleTap;
		
		//Gesture.onLongTapE -= OnLongTap;
		Gesture.onMultiTapE -= OnMultiTap;
	}
	

	
	
	//called when a multi-Tap event is detected
	void OnMultiTap(Tap tap){
		
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap.pos);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if(hit.collider.gameObject.tag == "Sound" && audio.isPlaying == false && hasPlayed == false)
			{
				audio.Play();
				hasPlayed = true;
				Debug.Log ("Soundplayed");	
			}
		}
	}
	
	
	
/*	void OnLongTap(Tap tap){
		
		//do a raycast base on the position of the tap
		Ray ray = Camera.main.ScreenPointToRay(tap.pos);
		RaycastHit hit;	
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if(hit.collider.gameObject.tag == "Sound")
			{
				if(Application.loadedLevelName == "Spawn") Application.LoadLevel("Ring");
				if(Application.loadedLevelName == "Ring") Application.LoadLevel("Spawn");
			}
			
		}
	}
*/
}
