using UnityEngine;
using System.Collections;

public class MoveLoop : MonoBehaviour {
    
    public float Speed = 1f;
    public float moveTime = 0f;
    public bool move = true;
    public float dX = 0f;
    public float dY = 2f;
    public float dZ = 0f;
    public bool followPath = false;
    public iTweenPath Path;
    public iTween.EaseType pathEase;
    
    public bool rotate = false;
    public float rX = 0f;
    public float rY = 2f;
    public float rZ = 0f;
    public float rSpeed = 1f;
    public float rTime = 0f;
    public iTween.EaseType rotationEase = iTween.EaseType.linear;
    public iTween.LoopType rotationLoop = iTween.LoopType.loop;
    
    void OnEnable() {
        if (followPath == true && Path != null)
        {
            string NameOfPath = Path.pathName;
            if (moveTime == 0f)
            {
            iTween.MoveTo(this.gameObject, iTween.Hash (
                                                            "path", iTweenPath.GetPath(NameOfPath),
                                                            "speed" , Speed, 
                                                            "easetype", pathEase,
                                                            "looptype", iTween.LoopType.loop,
                                                            "movetopath", false
                                                        ));
            }
            else
            {
            iTween.MoveTo(this.gameObject, iTween.Hash (
                                                            "path", iTweenPath.GetPath(NameOfPath),
                                                            "time", moveTime, 
                                                            "easetype", pathEase,
                                                            "looptype", iTween.LoopType.loop,
                                                            "movetopath", false
                                                        ));
            }
        }
        else if (followPath == true && Path == null)
        {
            
            Debug.LogError("followPath set true, but no Path specified in " + this.gameObject.name);
            Debug.Break();
        }
        
            if(move == true && followPath == false)
            {
            iTween.MoveBy(this.gameObject, iTween.Hash (
                                                            "x", dX,
                                                            "y", dY,
                                                            "z", dZ,
                                                            "speed" , Speed, 
                                                            "easetype", iTween.EaseType.easeInOutQuad,
                                                            "looptype", iTween.LoopType.pingPong
                                                        ));
            }
            if(rotate == true)
            {
                if(rTime != 0f)
                {
                    iTween.RotateBy(this.gameObject, iTween.Hash (
                                                                    "x", rX,
                                                                    "y", rY,
                                                                    "z", rZ,
                                                                    "time", rTime, 
                                                                    "easetype", rotationEase,
                                                                    "looptype", rotationLoop
                                                                ));
                }
                else
                {
                    iTween.RotateBy(this.gameObject, iTween.Hash (
                                                                    "x", rX,
                                                                    "y", rY,
                                                                    "z", rZ,
                                                                    "speed" , rSpeed, 
                                                                    "easetype", rotationEase,
                                                                    "looptype", rotationLoop
                                                                ));
                }
            
            }    
        
    }
    void OnDisable() {
        iTween.Stop(this.gameObject);
    }
}
