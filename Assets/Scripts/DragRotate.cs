using UnityEngine;
using System.Collections;

public class DragRotate : MonoBehaviour {
    
    public float dragSpeed = 0.1f;
    public GameObject VCJoystick;
    public GameObject VCButton;
    private float zoomPinch;
    
    
    void Start () {
        zoomPinch = Camera.main.GetComponent<FollowCamera>()._distance;
        Screen.showCursor = false;
    }
    
    void Update () {
        #if UNITY_EDITOR || !UNITY_IPHONE
        if (Input.GetAxis("Mouse ScrollWheel") != 0)  {
            zoomPinch = Mathf.Clamp(zoomPinch + Input.GetAxis("Mouse ScrollWheel")*5f,3f,40f);
            Camera.main.GetComponent<FollowCamera>()._distance = Mathf.Clamp(zoomPinch * 2f / 3f, 5f, 26f) ;
            Camera.main.GetComponent<FollowCamera>()._height = Mathf.Clamp(zoomPinch, 3f, 40f) ;
        }
        transform.Rotate (0, dragSpeed * Time.deltaTime * Input.GetAxis("Mouse X")*5f, 0);
        transform.Rotate (0, dragSpeed * Time.deltaTime * Input.GetAxis("JoystickRightLeft")*70f,0);
        #endif
    }
    
    void OnEnable() {
        
        //Hide Cursor
        Screen.showCursor = false;
        
        Gesture.onDraggingStartE += OnDraggingStart;
        #if UNITY_IPHONE && !UNITY_EDITOR
        Gesture.onDraggingE += OnDragging;
        #endif
        Gesture.onPinchE += OnPinch;
        
    }
    
    void OnDisable() {
        #if UNITY_IPHONE && !UNITY_EDITOR
        Gesture.onDraggingE -= OnDragging;
        #endif
        Gesture.onDraggingStartE -= OnDraggingStart;
        Gesture.onPinchE -= OnPinch;
    }
    
    
    void OnDraggingStart(DragInfo dragInfo) {
        // transform.Rotate(transform.up,dragSpeed * dragInfo.delta.x);
    }
    
    void OnPinch(PinchInfo pinchInfo) {
        
        if((VCJoystick.GetComponent<VCAnalogJoystickGuiTexture>().Dragging == false && VCButton.GetComponent<VCButtonGuiTexture>().Pressed == false))
        {
            //set zoomPinch
            zoomPinch += pinchInfo.magnitude * (0.05f * Mathf.Clamp(Mathf.Log(zoomPinch),0.01f,1.0f));
            zoomPinch = Mathf.Clamp(zoomPinch,1f,40f);

            Camera.main.GetComponent<FollowCamera>()._distance = Mathf.Clamp(zoomPinch * 2f / 3f, 5f, 26f) ;
            Camera.main.GetComponent<FollowCamera>()._height = Mathf.Clamp(zoomPinch, 3f, 40f) ;
        }
    }

#if UNITY_IPHONE && !UNITY_EDITOR
    void OnDragging(DragInfo dragInfo) {
        if((VCJoystick.GetComponent<VCAnalogJoystickGuiTexture>().Dragging == false && VCButton.GetComponent<VCButtonGuiTexture>().Pressed == false) || dragInfo.index == 1)
        {    
            transform.Rotate(0, dragSpeed * dragInfo.delta.x * Time.deltaTime, 0);
        }
        else
        {
            Debug.Log ("It's Dragging!");
        }
    }
#endif
}
