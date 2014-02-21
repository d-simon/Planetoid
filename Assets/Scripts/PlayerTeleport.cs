using UnityEngine;
using System.Collections;

public class PlayerTeleport : MonoBehaviour {
    
    public bool UseTrigger = true;
    public bool ToPlayerCheckpoint = false;
    public GameObject TeleLocation;
    public float teleSpeed = 0.5f;
    public bool SetNewParent;
    public GameObject NewParent;
    
    void OnTriggerEnter (Collider otherCol) {
        if (UseTrigger == true)
        {
            PlayerDoTeleport(otherCol.gameObject);
        }
    }
    
    public void PlayerDoTeleport(GameObject otherColGo)
    {
    
            if ( otherColGo.tag == "Player" )
            {    
                if (ToPlayerCheckpoint == true)
                {
                    otherColGo.transform.position = otherColGo.GetComponent<PlayerSettings>()._CheckPoint.transform.position;
                    otherColGo.transform.rotation = otherColGo.GetComponent<PlayerSettings>()._CheckPoint.transform.rotation;
                    otherColGo.rigidbody.velocity = Vector3.zero;
                    if(SetNewParent == true && NewParent != null) 
                    {
                         otherColGo.transform.parent = NewParent.transform;
                    }
                    else if (SetNewParent == true && NewParent == null)
                    {
                        otherColGo.transform.parent = otherColGo.GetComponent<PlayerSettings>()._CheckPoint.transform.parent;
                    }
                    StartCoroutine(DelayAttractor(otherColGo, teleSpeed));
            
                }
                else if (TeleLocation != null)
                {
                    otherColGo.transform.position = TeleLocation.transform.position;
                    otherColGo.transform.rotation = TeleLocation.transform.rotation;
                    otherColGo.rigidbody.velocity = Vector3.zero;
                    if(SetNewParent == true) 
                    {
                        if (NewParent != null) otherColGo.transform.parent = NewParent.transform;
                        else otherColGo.transform.parent = TeleLocation.transform.parent;
                    }
                    StartCoroutine(DelayAttractor(otherColGo, teleSpeed));
                }
            }
        
    }
    
    IEnumerator SlerpTo(GameObject FromGo, GameObject ToGo)
    {
            float dTime = 0f;
            while ( dTime < teleSpeed)
            {
            
                FromGo.transform.position = Vector3.Slerp(FromGo.transform.position, ToGo.transform.position, dTime/teleSpeed);
                FromGo.transform.rotation = Quaternion.Slerp(FromGo.transform.rotation, ToGo.transform.rotation, dTime/teleSpeed);
                yield return new WaitForEndOfFrame();
                dTime = Time.deltaTime;
            }
    }
    IEnumerator DelayAttractor(GameObject otherGo, float t)
    {
        yield return new WaitForSeconds(t);    
        otherGo.GetComponent<Attractor>().enabled = true;
    }
}
