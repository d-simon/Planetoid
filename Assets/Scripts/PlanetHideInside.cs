using UnityEngine;
using System.Collections;

public class PlanetHideInside : MonoBehaviour {

    public bool _enabled = true;
    
    void OnTriggerEnter (Collider other) {
        if(other.tag == "Player")
        {
            if(_enabled == true) 
            {    
                //iTween.FadeTo(this.gameObject, 0.0f, 0.5f);    
                transform.GetComponent<MeshRenderer>().enabled = false;
                foreach( Transform child in transform )
                {
                    child.gameObject.SetActive( false );
                } 
                _enabled = false;
            }
            
        }
    }
    void OnTriggerExit (Collider other) {
        if(other.tag == "Player")
        {
            if(_enabled == false) 
            {    
                iTween.FadeTo(this.gameObject, 1.0f, 0.5f);    
                //transform.GetComponent<MeshRenderer>().enabled = true;
                
                foreach( Transform child in transform )
                {
                    child.gameObject.SetActive( true );
                } 
                _enabled = true;
            }
            
        }
    }
}

