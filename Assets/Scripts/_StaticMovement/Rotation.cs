using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	
	public bool randomize = false;
	public float xRotation = 100f;
	public float yRotation = 100f;
	public float zRotation = 100f;
	
	private Vector3 axisRotation;

	
	// Use this for initialization
	void Start () {
		if (randomize)
		{
			xRotation = Random.Range(-xRotation, xRotation);
			yRotation = Random.Range(-yRotation, yRotation);
			zRotation = Random.Range(-zRotation, zRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 axisRotation = new Vector3(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime);
		this.transform.Rotate(axisRotation);
	}
}
