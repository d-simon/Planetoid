using UnityEngine;
using System.Collections;

public class Hologram : MonoBehaviour {


    void OnEnable () {
        
        iTween.FadeTo(this.gameObject, 0.3f, 1.0f);
        iTween.RotateBy(this.gameObject, iTween.Hash(
                                                    "y", 1.0f,
                                                    "time", 10.0f,
                                                    "looptype", iTween.LoopType.loop,
                                                    "easetype", iTween.EaseType.linear
                                                ));

    }
    

}
