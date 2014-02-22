using UnityEngine;
using System.Collections.Generic;
 
/*
 * http://forum.unity3d.com/threads/8814-Wireframe-3D/page3 
 * 
 * ---> http://forum.unity3d.com/threads/8814-Wireframe-3D?s=2c43d58f16555f57d668e842381a715d&p=1102362&viewfull=1#post1102362
 * 
 * In Unity 4.0 this should probably be done with  GL.wireframe on a separate layer (and camera)
 * 
 */
public class WireFrameEffect : MonoBehaviour
{
    Mesh LastMesh;
    Material LastMaterial;
	public Color shaderColor;
	string lineshader;

    public void OnEnable()
    {
		var colorStr = System.String.Format("({0},{1},{2},{3})", shaderColor.r, shaderColor.g, shaderColor.b, shaderColor.a);
		lineshader = "Shader \"Unlit/Color\" { Properties { _Color(\"Color\", Color) = " + colorStr +  "   } SubShader {  Lighting Off Color[_Color] Pass {} } }";

        var mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        var renderer = gameObject.GetComponent<MeshRenderer>();
        LastMaterial = renderer.material;
        LastMesh = mesh;
        var vertices = mesh.vertices;
        var triangles = mesh.triangles;
        var lines = new Vector3[triangles.Length];
        int[] indexBuffer;
        var GeneratedMesh = new Mesh();
 
        for (var t = 0; t < triangles.Length; t++)
        {
            lines[t] = (vertices[triangles[t]]);
        }
 
        GeneratedMesh.vertices = lines;
        GeneratedMesh.name = "Generated Wireframe";
 
        var LinesLength = lines.Length;
        indexBuffer = new int[LinesLength];
        var uvs = new Vector2[LinesLength];
        var normals = new Vector3[LinesLength];
 
        for (var m = 0; m < LinesLength; m++)
        {
            indexBuffer[m] = m;
            uvs[m] = new Vector2(0.0f, 1.0f); // sets a fake UV (FAST)
            normals[m] = new Vector3(1, 1, 1);// sets a fake normal
        }
 
        GeneratedMesh.uv = uvs;
        GeneratedMesh.normals = normals;
        GeneratedMesh.SetIndices(indexBuffer, MeshTopology.LineStrip, 0);
        gameObject.GetComponent<MeshFilter>().mesh = GeneratedMesh;
        Material tempmaterial = new Material(lineshader);
        renderer.material = tempmaterial;
    }
 
    void OnDisable()
    {
        gameObject.GetComponent<MeshFilter>().mesh = LastMesh;
        gameObject.GetComponent<MeshRenderer>().material = LastMaterial;
    }
}