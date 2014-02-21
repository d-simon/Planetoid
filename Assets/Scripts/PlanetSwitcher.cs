using UnityEngine;
using System.Collections;

public class PlanetSwitcher : MonoBehaviour {

    public GameObject targetPlanet;
    public GameObject targetPath;
    public GameObject CameraTargetGo;
    public float switchSpeed = 30f;
    public float switchRotationDamping = 4f;
    public float switchDamping = 1f;
    
    private Collider colliderObj;
    private float oldRotationDamping;
    private float oldDamping;
    
    void OnCompleted()
    {
            colliderObj.transform.parent = targetPlanet.transform;
            colliderObj.transform.GetComponent<Attractor>().enabled = true;
            collider.enabled = true;
            StartCoroutine(CompleteFade());
        
    }
    
    IEnumerator CompleteFade() {
        FollowCamera tmpCam = Camera.mainCamera.GetComponent<FollowCamera>();
            tmpCam._enabled = true;
            float tmpTime = 0f;
            while(tmpTime < 5f)
            {
                yield return new WaitForFixedUpdate();
                tmpTime += Time.deltaTime;
                tmpCam._rotationDamping = Mathf.SmoothStep(0f, oldRotationDamping, tmpTime / 5f);
                tmpCam._damping = Mathf.SmoothStep(0f, oldDamping, tmpTime / 5f);
            }
            
            
    }
    
    
    void OnStarting()
    {
        StartCoroutine (StartingFade());
    }
    
    IEnumerator StartingFade() {
        FollowCamera tmpCam = Camera.mainCamera.GetComponent<FollowCamera>();
            tmpCam._enabled = false;
            float tmpTime = 0f;
            while(tmpTime < 2f)
            {
                yield return new WaitForFixedUpdate();
                tmpTime += Time.deltaTime;
                tmpCam._rotationDamping = Mathf.SmoothStep(0f, switchRotationDamping, tmpTime / 2f);
                tmpCam._damping = Mathf.SmoothStep(0f, switchDamping, tmpTime / 2f);
            }
            
            
    }
    
    
    void OnTriggerEnter (Collider other)
    {
        colliderObj = other;
         if(other.transform.tag == "Player" && collider.isTrigger){    
        
        // Camera settings
        FollowCamera tmpCam = Camera.mainCamera.GetComponent<FollowCamera>();
        oldRotationDamping = tmpCam._rotationDamping;
        oldDamping = tmpCam._damping;    
        tmpCam._rotationDamping = switchRotationDamping;
        tmpCam._damping = switchDamping;
            
            
        //Free from Planet
        other.GetComponent<Attractor>().enabled = false;
        other.transform.parent = null;
        StartCoroutine(Camera.main.GetComponent<FollowCamera>().TrackCamera(CameraTargetGo));
            
        string pathName = targetPath.name;
        iTween.MoveTo(other.gameObject, iTween.Hash (
                                                        "path", iTweenPath.GetPath(pathName), 
                                                        "speed" , switchSpeed, 
                                                        "easetype", iTween.EaseType.linear,
                                                        "orienttopath", true,
                                                        "onstart", "OnStarting",
                                                        "onstarttarget", gameObject,
                                                        "oncomplete", "OnCompleted",
                                                        "oncompletetarget", gameObject
                
                                                    ));
        collider.enabled = false;    
                   
            }
                                                        
    }
}
