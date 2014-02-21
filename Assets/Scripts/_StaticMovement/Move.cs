using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    
    public float dX = 1f;
    public float dY = 1f;
    public float dZ = 1f;
    
    public bool RandomizeXY = true;
    
    public float clampRadius = 0f; // 0 is infinite
    
    
    // Use this for initialization
    void Start () {
        if (RandomizeXY == true) {
            dX = Random.Range(-dX, dX);
            dY = Random.Range(-dY, dY);
        }
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        if (clampRadius != 0) {
            
            float tempX = this.gameObject.transform.position.x + dX;
            float tempY = this.gameObject.transform.position.y + dY;
            Vector2 clampedXY = Vector2.ClampMagnitude(new Vector2(tempX,tempY), clampRadius);
            
            
            
            // Apply
            this.gameObject.transform.position = new Vector3(clampedXY.x, clampedXY.y, this.gameObject.transform.position.z + dZ);
            
        
            // OnClamp: change Direction
            if ( tempX != clampedXY.x | tempY != clampedXY.y/*which is z*/ )
            {
                dX = -dX;
                dY = -dY;
            }
            
            
        }
        else 
        {
            this.gameObject.transform.position += new Vector3(dX,dY,dZ);
        }
    
    }
}
