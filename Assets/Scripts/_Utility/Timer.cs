using UnityEngine;
using System.Collections;
 
public class Timer : MonoBehaviour 
{
 
// Attach this to a GUIText to make a frames/second indicator.
//
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
//
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.
 
public  float updateInterval = 0.1F;
 
private float accum   = 0; // FPS accumulated over the interval
private int   frames  = 0; // Frames drawn over the interval
private float timeleft; // Left time for current interval
private int theMinutes;
private int theSeconds;
private int theHundredths;
public bool _enabled = true;
 
void Start()
{
    if( !guiText )
    {
        Debug.Log("UtilityFramesPerSecond needs a GUIText component!");
        enabled = false;
        return;
    }
    timeleft = updateInterval;  
}
 
void Update()
{
	
    timeleft -= Time.deltaTime;
    accum += Time.timeScale/Time.deltaTime;
    ++frames;
 
    // Interval ended - update GUI text and start new interval
    if( timeleft <= 0.0 )
    {
	if (_enabled == true)
	{
	      
		int theTime = (int) (Time.timeSinceLevelLoad / Time.timeScale * 100);
		
		theMinutes = (int) theTime / 60 / 100;
		theSeconds = (int) theTime / 100 % 60;	
		theHundredths = theTime % 100;
	}
	string format = System.String.Format("{0:D}'' {1:D}' {2:D}", theMinutes, theSeconds, theHundredths);
	guiText.text = format; 
 
	//	DebugConsole.Log(format,level);
        timeleft = updateInterval;
        accum = 0.0F;
        frames = 0;
    }
}
}