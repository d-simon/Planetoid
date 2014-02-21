using UnityEngine;
using System.Collections;



[RequireComponent (typeof (AudioSource))]
public class SpawnScore : MonoBehaviour {
    
    
    // Logic
    public AudioClip sound1;
    public float SpeedModeVelocity = 3f;
    public GameObject[] SpeedModeDisabledGo;
    
    
    [System.NonSerialized]
    public TargetSettings targetSettings = null;
    [System.NonSerialized]
    public int flyScore = 0;
    private int scoreMultiplier = 1;
    private int currentMode = 0; //none
    
    
    // Styling
    public Font Helvetica;
    public Camera GUICamera;
    private GUIStyle scoreStyle = new GUIStyle();
    
    
    
    
    //public float localTimeScale = 1f;
    
    
    // Use this for initialization
    void Start () {
                
        // Score Style
        scoreStyle.normal.textColor = Color.white;
        scoreStyle.font = Helvetica;
        scoreStyle.fontSize = 100;
        scoreStyle.alignment = TextAnchor.LowerRight;
        
        // Set Sound
        Camera.main.audio.clip = sound1;
        
    }
    
    
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
    
    
    void OnGUI () { 
            GUI.Label(new Rect(Screen.width-125 - 200, Screen.height - 100, 200, 100), flyScore.ToString (), scoreStyle);
        
            if (GUICamera != null)
            {    
                GUICamera.depth = 11;
                GUICamera.rect = new Rect ((Screen.width - 125f) / Screen.width, 25f / Screen.height, 120f / Screen.width , 120f / Screen.height); //new Rect(0, 0, 100 / Screen.width, 100 / Screen.height);
        
            }
    }
    
/*    bool IsTarget(GameObject goCheck, GameObject[] goArray) 
    {
        for(int i = 0; i < goArray.Length; i++)
        {
             if (goArray[i] == goCheck) return true;
        }
        return false;
    }
*/
    
    //called when a multi-Tap event is detected
    void OnMultiTap(Tap tap){
        
        //do a raycast base on the position of the tap
        Ray ray = Camera.main.ScreenPointToRay(tap.pos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            
            if (hit.collider.gameObject.tag == "TapDestroy")
            {
                // Get the target settings
                targetSettings = hit.collider.gameObject.GetComponent<TargetSettings>();
                    
                DoSpawnLogic();
                
                //Destroy Children
                
                foreach (Transform tmpChild in hit.collider.transform)
                {

                    StartCoroutine( DestroyShrink(tmpChild) );
                }
                
                StartCoroutine( DestroyShrink(hit.collider.transform) );
                
                
            }
            else if (hit.collider.gameObject.tag == "TapDestroyMyParent")
            {
                
                // Get the target settings
                targetSettings = hit.collider.transform.parent.gameObject.GetComponent<TargetSettings>();
                
                DoSpawnLogic();
                
                foreach (Transform tmpChild in hit.collider.transform.parent.gameObject.transform)
                {
                    StartCoroutine( DestroyShrink(tmpChild) );
                }
                
                // This comes last to ensure hit.collider.gameObject is still available
                
                StartCoroutine( DestroyShrink(hit.collider.transform.parent.transform) );
                
            }
        }
    }
    
    void OnDragging(DragInfo dragInfo){
        
        //do a raycast base on the position of the tap
        Ray ray = Camera.main.ScreenPointToRay(dragInfo.pos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            
            if (hit.collider.gameObject.tag == "TapDestroy")
            {
                // Get the target settings
                hit.collider.gameObject.tag = "Untagged";
                
                targetSettings = hit.collider.gameObject.GetComponent<TargetSettings>();
                    
                DoSpawnLogic();
                
                //Destroy Children
                
                foreach (Transform tmpChild in hit.collider.transform)
                {
                    tmpChild.gameObject.tag = "Untagged";
                    StartCoroutine( DestroyShrink(tmpChild) );
                }
                
                StartCoroutine( DestroyShrink(hit.collider.transform) );
                
                
            }
            else if (hit.collider.gameObject.tag == "TapDestroyMyParent")
            {
                hit.collider.gameObject.tag = "Untagged";
                
                // Get the target settings
                targetSettings = hit.collider.transform.parent.gameObject.GetComponent<TargetSettings>();
                
                DoSpawnLogic();
                
                foreach (Transform tmpChild in hit.collider.transform.parent.gameObject.transform)
                {
                    tmpChild.gameObject.tag = "Untagged";
                    StartCoroutine( DestroyShrink(tmpChild) );
                    
                }
                
                // This comes last to ensure hit.collider.gameObject is still available
                
                StartCoroutine( DestroyShrink(hit.collider.transform.parent.transform) );
                
            }
        }
    }
    
    IEnumerator DestroyShrink (Transform tmpT, float t = 1f)
    {
        
        MonoBehaviour[] behavs = tmpT.GetComponents< MonoBehaviour >();
        foreach(MonoBehaviour c in behavs)
        {
          c.enabled = false;
        }
        
        Hashtable tmpScaleSettings = new Hashtable();
            
        tmpScaleSettings.Add ( "scale" , Vector3.zero );
        tmpScaleSettings.Add ( "easetype" , iTween.EaseType.easeOutExpo );
        tmpScaleSettings.Add ( "ignoretimescale" , true );
        tmpScaleSettings.Add ( "time" ,  t );
        
        iTween.ScaleTo(tmpT.gameObject, tmpScaleSettings);
        
        yield return new WaitForSeconds(t*Time.timeScale);
        
        if (tmpT != null) Destroy(tmpT.gameObject);
        
    }
    
    
    
    void DoSpawnLogic ()
    {
        if ( targetSettings != null)     
        {
            
            // Set score
            UpdateScore ();
        
            // Trigger Mode
            DoTriggerMode ();
        }
    }
    
    void UpdateScore ()
    {
        
        // Update score with target ScoreModifier
        int tmpScoreMod = targetSettings.scoreModifier;
        
        if (currentMode == 1) //targetSettings.Modes.SpeedMode
        {
            if (tmpScoreMod <= 0) tmpScoreMod = 0;
        }
        
        if ( flyScore + tmpScoreMod >= 0)
        {
            if (tmpScoreMod >= 0)  tmpScoreMod *= scoreMultiplier;
                
            flyScore += tmpScoreMod;
        }
        else
        {
            flyScore = 0;
        }    
        
    }
    
    
    void DoTriggerMode ()
    {
        //Get triggermode
        int tmpTriggerMode = (int)targetSettings.TriggerMode;
        
        
        if (currentMode != tmpTriggerMode) switch(tmpTriggerMode)
        {
            case 0:
                    // Do Nothing
                    break;
            
            case 1: 
                    //SpeedMode
                    StartCoroutine(SpeedMode());
                    break;
        }
        
    }
    
    void SetTimeScale (float t)
    {
        Time.timeScale = t;
    }
    
    void SetAudioPitch (float t)
    {
        Camera.main.audio.pitch = t;
    }
    
    IEnumerator ActiveDelay(GameObject ToBeDisabled, float t, bool state)
        {
            yield return new WaitForSeconds(t*Time.timeScale);
            ToBeDisabled.SetActive(state);
        }
    
    ////////// MODES

    
    /*
     *     Color randomColor() {
        Color tmpC = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
        return tmpC;
    }
    // Flash in Color
    IEnumerator FlashColor (GameObject tmpGo, float t)
    {
        Color originalColor = tmpGo.renderer.material.color;
            
        iTween.ColorTo(tmpGo, iTween.Hash( "color" , randomColor() , "time", 1f, "ignoretimescale" , true ) );
        
        iTween.ColorTo(tmpGo, iTween.Hash( "color" , randomColor() , "time", 1f, "ignoretimescale" , true, "looptype" , iTween.LoopType.loop, "delay" , 1f) );
        
        
        yield return new WaitForSeconds(t);
        
        iTween.Stop(tmpGo, "ColorTo");
        
        tmpGo.renderer.material.color = originalColor;
        
    }
    
    */
    
    // Init SpeedMode
    IEnumerator SpeedMode()
    {
        //Set CurrentMode
        
        currentMode = 1;  //targetSettings.Modes.SpeedMode
        
        //Increase Multiplier
        scoreMultiplier += 3;
        
        foreach (GameObject ToDisable in SpeedModeDisabledGo)
        {
            
            foreach (Transform ToDisableChild in ToDisable.transform)
            {        
                StartCoroutine(DestroyShrink(ToDisableChild, 0.5f));
            }
            
            StartCoroutine( ActiveDelay(ToDisable, 0.6f, false));
            
        }
            
        
        iTween.ValueTo(this.gameObject, iTween.Hash ( 
                                                        "easetype", iTween.EaseType.easeInOutQuad,
                                                        "time", 3f,
                                                        "from", Time.timeScale,
                                                        "to", SpeedModeVelocity,
                                                        "ignoretimescale", true,
                                                        "onupdate", "SetTimeScale"
                                                    ));
        
        iTween.ValueTo(this.gameObject, iTween.Hash ( 
                                                        "easetype", iTween.EaseType.easeInOutQuad,
                                                        "time", 3f,
                                                        "from", Camera.main.audio.pitch,
                                                        "to", 1f + SpeedModeVelocity / 20f,
                                                        "ignoretimescale", true,
                                                        "onupdate", "SetAudioPitch"
                                                    ));
        
        // Set initial ColorFade (for which the Fading time has to be slow)
        foreach (GameObject GoToColor in  GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if(GoToColor.name == "SphereRed(Clone)" || GoToColor.name == "YellowSpike(Clone)")
            {
                if ( GoToColor.GetComponent<TargetSettings>() )
                    {
                        GoToColor.GetComponent<TargetSettings>().FlashInColors (true, 0.8f, 3f);
            
                    }
            }
        }
        
        // Iterate to set ColorFade to newly created objects (for which Fading time has to be "instant")
        float StartPoint = Time.realtimeSinceStartup; 
        
        yield return new WaitForSeconds(0.2f * Time.timeScale);
        while ( Time.realtimeSinceStartup < StartPoint + 8f)
        {
            foreach (GameObject GoToColor in  GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if(GoToColor.name == "SphereRed(Clone)" || GoToColor.name == "YellowSpike(Clone)")
                {
                    if ( GoToColor.GetComponent<TargetSettings>() )
                    {
                        GoToColor.GetComponent<TargetSettings>().FlashInColors (true, 0.8f, 0.2f);
            
                    }
                }
                else if(GoToColor.name == "SphereBlack(Clone)")
                {
                    iTween.FadeTo(GoToColor, 0.8f, 0.5f);
                }
            }
            yield return new WaitForSeconds(0.2f * Time.timeScale);
        }
        
        
        // wait 12 / 1.5 = 8 seconds
        //yield return new WaitForSeconds(10f * Time.timeScale);

    
        
        iTween.ValueTo(this.gameObject, iTween.Hash ( 
                                                        "easetype", iTween.EaseType.easeInOutQuad,
                                                        "time", 3f,
                                                        "from", Time.timeScale,
                                                        "to", 1f,
                                                        "ignoretimescale", true,
                                                        "onupdate", "SetTimeScale"
                                                    ));
        
        iTween.ValueTo(this.gameObject, iTween.Hash ( 
                                                        "easetype", iTween.EaseType.easeInOutQuad,
                                                        "time", 3f,
                                                        "from", Camera.main.audio.pitch,
                                                        "to", 1f,
                                                        "ignoretimescale", true,
                                                        "onupdate", "SetAudioPitch"
                                                    ));
        
        foreach (GameObject GoToColor in  GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if(GoToColor.name == "SphereRed(Clone)" || GoToColor.name == "YellowSpike(Clone)")
            {
                if ( GoToColor.GetComponent<TargetSettings>() )
                {
                    GoToColor.GetComponent<TargetSettings>().FlashInColors (false,0.2f);
        
                }
            }
            else if(GoToColor.name == "SphereBlack(Clone)")
            {
                iTween.FadeTo(GoToColor,1f,1f);
            }
        }
        
        foreach (GameObject ToDisable in SpeedModeDisabledGo)
        {
            iTween.FadeTo(ToDisable.gameObject, 1f, 1f);            
            StartCoroutine( ActiveDelay(ToDisable, 3f, true));
            
        }
        
            
        // Decrease ScoreMultiplier gradually 
        yield return new WaitForSeconds(1f);
        scoreMultiplier -= 1;
        yield return new WaitForSeconds(1f);
        scoreMultiplier -= 1;
        yield return new WaitForSeconds(1f);
        scoreMultiplier -= 1;
        currentMode = 0; // targetSettings.Modes.None;

    }
}
