using UnityEngine;
using System.Collections;

public class SpawnOctagonField : MonoBehaviour {
	
	
	// Public
	public float octagonRadius = 10f;
	
	public GameObject octagonBlockerPrefab;
	public GameObject octagonBlockerParent;
	public Material octagonBlockerMaterial;
	
	public bool drawGizmosAndRecalc = false;
	
	
	// Private
	private Vector3[] octagonCorners = new Vector3[8];
	
	

	
	
	
	// Use this for initialization
	void Start () {
		
		octagonCorners = OctagonPoints(octagonRadius);
		
		
		//temp
		Vector3[] ocTemp = new Vector3[4];
		
		for(int i = 0; i < 4; i++)
		{
			ocTemp[i] = octagonCorners[i*2];
		}
		
		//QuadGenerate(ocTemp);
		
		StartCoroutine(SpawnObstacles(2f, 1, 6));
		
		
	}

	
	void OnDrawGizmos()
	{
		
		//Draw the Octagon for debug
		if (drawGizmosAndRecalc == true)
		{
			//recalculate
			octagonCorners = OctagonPoints(octagonRadius);
			
			for (int i  = 0; i < 8; i++)
			{
				if (i < 7) {
				Debug.DrawLine(octagonCorners[i], octagonCorners[i+1], Color.green);
				} else if (i == 7)
				{
				Debug.DrawLine(octagonCorners[i], octagonCorners[0], Color.green);	
				}
			}
		}
	}
	
	
	IEnumerator SpawnObstacles(float sleepTime, int obstacleID, int startPos = 0)
	{
	 //dev
	while( Time.timeSinceLevelLoad < Time.timeSinceLevelLoad +10f)
		{
		switch(obstacleID)
		{
			case 0:
				//Do Nothing
				break;
			
			case 1:
				//Standard
				
				GameObject OctagonBlockerParent = (GameObject)Instantiate(octagonBlockerParent);

				
				startPos = Random.Range(0,7);
				
				for(int i = 0; i < 4; i++)
				{
					GameObject OctagonBlockerIn = QuadGenerate( new Vector3[] { 
													  octagonCorners[(startPos + i*2) % 8], 
													  octagonCorners[(startPos + 1 + i*2) % 8],
													  octagonCorners[(startPos + 1 + i*2) % 8] + Vector3.down*300f,
													  octagonCorners[(startPos + i*2) % 8] + Vector3.down*300f
												} );
					OctagonBlockerIn.transform.parent = OctagonBlockerParent.transform;

					
				}
				Debug.Log ("Meshed Quad");
			
				yield return new WaitForSeconds ( 3.5f);
			
				break;
		
		}
		}
	}
	
	
	
/////////////////////////////////////////
////// OCTAGON
/////////////////////////////////////////
	
	//Create Octagon Points
	Vector3[] OctagonPoints(float ORadius)
	{
		Vector3[] OPoints = new Vector3[8];
		
		for (int i  = 0; i < 8; i++)
		{
			float alpha = Mathf.Deg2Rad *  360f / 8f * ( i );
			float Ox = ORadius * Mathf.Cos(alpha);
			float Oy = ORadius * Mathf.Sin(alpha);
			OPoints[i] = new Vector3(Ox, 0 ,Oy);
			//Debug.Log (OPoints[i]);
		}
		return OPoints;
	}
	
	//Generate Mesh
	GameObject QuadGenerate(Vector3[] passedPoints)
	{	
		
		GameObject OctagonBlockerInstance = (GameObject)Instantiate(octagonBlockerPrefab);

		Mesh mesh = new Mesh();

        mesh.name = "OctagonBlocker";

        mesh.Clear();
		
	     
	    mesh.vertices = passedPoints;	
		
		
		Vector2[] tmpUv = new Vector2[4];
		
       	tmpUv[0] = new Vector2(0,0);
		tmpUv[1] = new Vector2(1,0);
		tmpUv[2] = new Vector2(0,1);
		tmpUv[3] = new Vector2(1,1);
		
		mesh.uv = tmpUv;

 
		int[] tmpTriangles = new int[6];
        tmpTriangles[0] = 0;
        tmpTriangles[1] = 1;
        tmpTriangles[2] = 2;
       	tmpTriangles[3] = 0;
        tmpTriangles[4] = 2;
        tmpTriangles[5] = 3;
		
		mesh.triangles = tmpTriangles;
		
		
        mesh.RecalculateNormals();

        MeshFilter mf = (MeshFilter)OctagonBlockerInstance.gameObject.GetComponent(typeof(MeshFilter));

        MeshRenderer mr = (MeshRenderer)OctagonBlockerInstance.gameObject.GetComponent(typeof(MeshRenderer));
		
		MeshCollider mc = (MeshCollider)OctagonBlockerInstance.gameObject.GetComponent(typeof(MeshCollider));
		
        mf.mesh = mesh;
		
		//reset and set
		mc.sharedMesh = null;
   		mc.sharedMesh = mf.mesh;

        mr.renderer.material = octagonBlockerMaterial;
		
		iTween.FadeTo(OctagonBlockerInstance, 0.5f, 0.1f);
		
		GameObject OctagonBlocktmp = OctagonBlockerInstance;
		
		return OctagonBlocktmp;
	}
	

	
}
