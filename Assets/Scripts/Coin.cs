using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    private GameObject tmpPlayer;

    void OnEnable () {
        
        tmpPlayer = GameObject.Find("Player");
        
        iTween.FadeTo(this.gameObject, 0.3f, 1.0f);
        
        iTween.MoveAdd(this.gameObject, iTween.Hash(
                                                        
            "y", 0.2f,
            "time", Random.Range(0.8f,1.2f),
            "looptype", iTween.LoopType.pingPong,
            "easetype", iTween.EaseType.easeInOutQuad
            
        ));    
        
    }
    
    
    public void Destruct() {
        
        iTween.Stop(this.gameObject);
        this.gameObject.tag = null;
        
        // -> performance too low //iTween.ValueTo(this.gameObject, iTween.Hash("time", 0.3f, "from", this.gameObject.GetComponent<Light>().intensity ,  "to", 0f, "onUpdate", "changeLightIntensity"));
        
        StartCoroutine( DestroyWithDelay(this.gameObject, 0.45f));
    }
     
    
    IEnumerator DestroyWithDelay(GameObject gO, float t)
    {
        Vector3 startingPosition = gO.transform.localPosition;
        float elapsedTime = 0f;
           while (elapsedTime < t) {
            
               elapsedTime += Time.deltaTime; // <- move elapsedTime increment here
            
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
