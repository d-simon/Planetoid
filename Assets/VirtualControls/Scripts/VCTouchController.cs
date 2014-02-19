//----------------------------------------
// Virtual Controls Suite for Unity
// © 2012 Bit By Bit Studios, LLC
// Author: sean@bitbybitstudios.com
// Use of this software means you accept the license agreement.  
// Please don't redistribute without permission :)
//---------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// VCTouchController tracks all active touches in the scene and is used by each
/// Virtual Control in the suite.
/// </summary>
public class VCTouchController : MonoBehaviour 
{
	public static VCTouchController Instance { get; private set; }
	
	public static void ResetInstance()
	{
		Instance = null;
	}
	
	#region inspector variables
	/// <summary>
	/// In Unity Android and iOS builds, Input.multiTouchEnabled is set to this value.
	/// Set this to true if you want to track more than 1 touch at a time.  Not all 
	/// devices support multitouch.
	/// </summary>
	public bool multitouch = true;
	
	/// <summary>
	/// On some Android devices, multiple independent touches are not tracked, and are instead
	/// emulated as independent.  This often causes the position values of multiple touches to be
	/// averaged together, which can cause erroneous touch behavior with a multi touch game interface.
	/// Setting this to true will cause any touch's deltaPosition change to be ignored if Unity reports
	/// that a touch traveled farther than a specified distance in one frame.
	/// </summary>
	public bool ignoreMultitouchErrorMovement = false;
	
	/// <summary>
	/// When ignoreMultitouchErrorMovement is true, all touches that have moved a square magnitude 
	/// of pixels greater than this specified value will be ignored.
	/// </summary>
	public float multiTouchErrorSqrMagnitudeMax = 1000.0f;
	#endregion
	
	[HideInInspector]
	/// <summary>
	/// All touches tracked by VCTouchController.  They may be in any state, including Inactive.
	/// </summary>
	public List<VCTouchWrapper> touches;
	
	// cache the active touches list when it's requested in case it is 
	// requested multiple times per frame
	private List<VCTouchWrapper> _activeTouchesCache;
	
	
#if UNITY_EDITOR
	private const int kMaxTouches = 6; // extra touch for mouse emulation
#else
	private const int kMaxTouches = 5; // Unity doesn't support more than 5 touches.
#endif
	
	void Awake () 
	{
		this.useGUILayout = false;
		
		if (Instance != null)
		{
			VCUtils.DestroyWithError(gameObject, "Only one VCTouchController can be in a scene!  Destroying the gameObject with this component.");
			return;
		}
		Instance = this;
		
		touches = new List<VCTouchWrapper>();
		// add a TouchWrapper for each touch we will track.  We create
		// and reuse a pool instead of instantiating new touches during execution.
		for (int i = 0; i < kMaxTouches; i++)
		{
			touches.Add(new VCTouchWrapper());
		}
	}
	
	void Update () 
	{

		Input.multiTouchEnabled = multitouch;
		
		// update old touches
		foreach (Touch t in Input.touches)
		{
			VCTouchWrapper tw = touches.FirstOrDefault(x => x.fingerId == t.fingerId);
			if (tw == null)
			{
				// this is a new touch, add it to our active list
				VCTouchWrapper availableTw = touches.FirstOrDefault(x => x.fingerId == -1);
				if (availableTw == null)
					Debug.LogWarning("Cannot find and available TouchWrapper to assign Touch info from!  Consider increase kMaxTouches.");
				else
					availableTw.Set(t);
			}
			else
			// touch we're already tracking, update it.
			{
				tw.visited = true;
				
#if UNITY_ANDROID
				// test for error movement
				if (ignoreMultitouchErrorMovement && Input.touchCount > 1)
				{
					Vector2 d = tw.position - t.position;
					if (d.sqrMagnitude > multiTouchErrorSqrMagnitudeMax)
					{
						continue;
					}
				}
#endif
				tw.deltaPosition = t.position - tw.position;
				tw.position = t.position;
				tw.phase = t.phase;
					
			}
		}
		
#if UNITY_EDITOR
		// use mouse input to emulate a touch.
		if (Input.GetMouseButtonDown(0))
		{
			touches[kMaxTouches-1].phase = TouchPhase.Began;
			touches[kMaxTouches-1].fingerId = 1;
			touches[kMaxTouches-1].deltaPosition.x = 0.0f;
			touches[kMaxTouches-1].deltaPosition.y = 0.0f;
			touches[kMaxTouches-1].position.x = Input.mousePosition.x;
			touches[kMaxTouches-1].position.y = Input.mousePosition.y;
			touches[kMaxTouches-1].visited = true;
		}
		else if (Input.GetMouseButton(0))
		{
			touches[kMaxTouches-1].phase = TouchPhase.Moved;
			touches[kMaxTouches-1].deltaPosition.x = Input.mousePosition.x - touches[kMaxTouches-1].position.x;
			touches[kMaxTouches-1].deltaPosition.y = Input.mousePosition.y - touches[kMaxTouches-1].position.y;
			touches[kMaxTouches-1].position.x = Input.mousePosition.x;
			touches[kMaxTouches-1].position.y = Input.mousePosition.y;
			touches[kMaxTouches-1].visited = true;
		}
#endif
		
		// reset any previously active, but no longer active touch
		foreach (VCTouchWrapper tw in touches)
		{
			if (!tw.visited)
			{
				tw.Reset();
			}
			else
			{
				tw.visited = false; // ready to cull next frame
			}
		}
		
		_activeTouchesCache = null;
	}
	
	/// <summary>
	/// Gets a List of touches which are currently Active.
	/// </summary>
	public List<VCTouchWrapper> ActiveTouches
	{
		get
		{
			if (_activeTouchesCache == null)
			{
				_activeTouchesCache = touches.Where(x => x.Active).ToList();
			}
			
			return _activeTouchesCache;
		}
	}
	
	/// <summary>
	/// Gets the VCTouchWrapper with the specified fingerId.
	/// </summary>
	public VCTouchWrapper GetTouch(int fingerId)
	{
		return touches.FirstOrDefault(x => x.fingerId == fingerId);
	}
}

/// <summary>
/// A simplified wrapper for Unity's Touch class that is also capable of emulating
/// a touch from Mouse input.
/// </summary>
public class VCTouchWrapper
{
	/// <summary>
	/// ID of the finger that owns this touch.
	/// </summary>
	public int fingerId;
	
	/// <summary>
	/// Unity TouchPhase of this touch.
	/// </summary>
	public TouchPhase phase;
	
	/// <summary>
	/// The position in screen (pixel) coordinates.
	/// </summary>
	public Vector2 position;
	
	/// <summary>
	/// The change in position since last frame in screen (pixel) coordinates.
	/// </summary>
	public Vector2 deltaPosition;
	
	/// <summary>
	/// Whether or not this VCTouchWrapper has been updated this frame.
	/// </summary>
	public bool visited;
	
	/// <summary>
	/// True when this touch is simulated and not an actual touch.
	/// </summary>
	public bool debugTouch;
	
	public VCTouchWrapper()
	{
		visited = false;
		position = new Vector2();
		deltaPosition = new Vector2();
		phase = TouchPhase.Canceled;
		fingerId = -1;
	}
			
	public VCTouchWrapper(Vector2 position)
	{
		visited = true;
		this.position = position;
		deltaPosition = new Vector2();
		fingerId = 0;
		phase = TouchPhase.Began;
	}
	
	public VCTouchWrapper(Touch touch)
	{
		Set(touch);
	}
					
	public void Reset()
	{
		visited = false;
		position = Vector2.zero;
		deltaPosition = Vector2.zero;
		phase = TouchPhase.Ended;
		fingerId = -1;
		debugTouch = false;
	}
	
	/// <summary>
	/// Returns true if the Touch is in any of the following TouchPhases:
	/// TouchPhase.Began, TouchPhase.Moved, TouchPhase.Stationary.
	/// </value>
	public bool Active
	{
		get { return (phase == TouchPhase.Began || phase == TouchPhase.Moved || phase == TouchPhase.Stationary); }
	}
	
	/// <summary>
	/// Sets VCTouchWrapper members based on a supplied Unity Touch.
	/// </summary>
	public void Set(Touch touch)
	{
		visited = true;
		position = touch.position;
		deltaPosition = touch.deltaPosition;
		phase = touch.phase;
		fingerId = touch.fingerId;
	}
						
}
