using UnityEngine;
using System.Collections;

public class TargetSettings : MonoBehaviour {

	public int scoreModifier = 1;
	public Color rootColor;
	
	// Modes
	public enum Modes 
	{
		None = 0,
		SpeedMode = 1,
		Bonusx2 = 2,
	}
	// standard = None
	public Modes TriggerMode = Modes.None;
	
	
	void Start()
	{
		if (this.gameObject.renderer != null) rootColor = this.gameObject.renderer.material.color;
	}
	
	Color randomColor() {
				Color tmpC = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
				return tmpC;
	}
	
	
	
	public void FlashInColors (bool active, float flashSpeed, float fadeTime = 0f)
	{
		// if FadeTime is standard value: make it equal to the speed " flashSpeed" (also, conveniently : FadeTime should not be zero anyways)
		if (fadeTime == 0f) fadeTime =  flashSpeed;
		
		
		if (active == true && iTween.Count(this.gameObject, "ColorTo")  == 0)
		{
			
	
			rootColor = this.gameObject.renderer.material.color;
				
			iTween.ColorTo(this.gameObject, iTween.Hash( "color" , randomColor() , "time", fadeTime, "ignoretimescale" , true ) );
			
			iTween.ColorTo(this.gameObject, iTween.Hash( "color" , randomColor() , "time", flashSpeed, "ignoretimescale" , true, "looptype" , iTween.LoopType.pingPong, "delay" , flashSpeed) );
		
		}
		else if (active == false)
		{
			
			iTween.Stop(this.gameObject, "ColorTo");
			
			iTween.ColorTo(this.gameObject, iTween.Hash( "color" , rootColor , "time", fadeTime, "ignoretimescale" , true ) );
		
		}
	}
}
