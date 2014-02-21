using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour 
{
    /*
     * Class members
     */
    public Transform _target;
    public float _distance = 10.0f;
    public float _height = 1.0f;
    public float _damping = 5.0f;
    public float _rotationDamping = 10.6f;
    
    public bool _enabled = true;
    
    public GameObject _flightTarget;
    
    void OnEnable() {
        if (_enabled == false) StartCoroutine(TrackCamera(_flightTarget));
    }
    
    void OnDisable() {
        if (_enabled == false) StopCoroutine("TrackCamera");
    }
    
    /*
     *  Update class function
     */
    private void FixedUpdate()
    {
        if (_enabled == true)
        {
            // Calculate and set camera position
            Vector3 desiredPosition = this._target.TransformPoint(0, this._height, -this._distance);
            //Vector3 dampVec = transform.position - desiredPosition;
            this.transform.position = Vector3.Slerp(this.transform.position, desiredPosition, Time.deltaTime * this._damping);
            
            // Calculate and set camera rotation
            Quaternion desiredRotation = Quaternion.LookRotation(this._target.position - this.transform.position, this._target.up);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, desiredRotation, Time.deltaTime * this._rotationDamping);
            
        }    
    }
    
    public IEnumerator TrackCamera(GameObject Gotarget) {
        _enabled = false;
        while (_enabled == false)
        {
            
            
            Vector3 aimVec = _target.transform.position - Gotarget.transform.position;
            Vector3 desiredAimPos = _target.transform.position + aimVec.normalized *  Mathf.Sqrt(Mathf.Pow(_distance,2f) + Mathf.Pow (_height, 2));
            desiredAimPos.y += _height / 10f;
            
            Quaternion desiredAimRotation = Quaternion.LookRotation(Gotarget.transform.position - desiredAimPos, _target.transform.up);
                       desiredAimRotation = Quaternion.LookRotation(_target.transform.position - this.transform.position);
                
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, desiredAimRotation, Time.deltaTime * this._rotationDamping);
            
            this.transform.position = Vector3.Slerp(this.transform.position, desiredAimPos, Time.deltaTime * this._damping);
            //this.transform.position = desiredAimPos;
            
            /*if (this.transform.position == desiredAimPos) {
            Debug.Log ("Broke! the while!");
                break;
            }*/
            yield return new WaitForFixedUpdate();
        }
        //yield return new WaitForFixedUpdate();
    }
    
}
