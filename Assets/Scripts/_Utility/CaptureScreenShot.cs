using UnityEngine;
using System.Collections;

public class CaptureScreenShot : MonoBehaviour {
    
    public int Size = 5;
    public string Name = "Screenshot.png";
    public KeyCode Key = KeyCode.P;
    
    void Update() {
        if (Input.GetKeyDown(Key)) 
        {
            CaptureSingle(Name, Size);
        }
    }
    
    
    public static void CaptureSingle(string _name, int _size)
    {
        Debug.Log ("Captured Screenshot: \"" + _name + "\" !");
        Application.CaptureScreenshot(_name, _size);
    }
}
