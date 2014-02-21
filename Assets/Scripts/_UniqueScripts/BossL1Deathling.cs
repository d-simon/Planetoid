using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AiMotor))]
[RequireComponent (typeof (SpawnPrefab))]
public class BossL1Deathling : MonoBehaviour {

    
    [System.NonSerialized] bool _isEngaged = false; // is engaged
    public AiMotor AiMotor;

    public void Start() {
        
        AiMotor = this.GetComponent<AiMotor>();

        engage();
        
    }
    
    public void engage () {
        if (!_isEngaged)
        {
            _isEngaged = true;
            StartCoroutine(IsEngaged());
        }
    }
    
    // This runs as long as the Boss is engaged and calls Reset() afterwards
    private IEnumerator IsEngaged () {
    

        // The Loop
        while(_isEngaged)
        {
            idle();
            yield return new WaitForEndOfFrame();    
        }
        
        // Clean Up
        reset ();
        
    }



    private void reset () {

    }

    private void idle () {





        //AiMotor.currentMovement = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
        // Vector3 tmpDist = Player.transform.position - this.transform.position;
        // if (tmpDist.sqrMagnitude > 100)
        // {
        //     tmpDist = tmpDist.normalized * AiMotor.moveSpeed;
        //     tmpDist = AiMotor.transform.InverseTransformDirection(tmpDist);
        //     AiMotor.currentMovement = Vector3.Slerp(AiMotor.currentMovement, tmpDist, Time.deltaTime*1f);
        // }
        // else
        // {
        //     AiMotor.currentMovement = Vector3.Slerp(AiMotor.currentMovement, Vector3.zero, Time.deltaTime * 8f);
        // }
        //yield return new WaitForSeconds(1f);
        //AiMotor.Jump();
    }

    
}
