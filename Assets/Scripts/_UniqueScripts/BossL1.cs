using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AiMotor))]
[RequireComponent (typeof (SpawnPrefab))]
public class BossL1 : MonoBehaviour {
	
	
	[System.NonSerialized] bool _isEngaged = false; // is engaged
	private AiMotor AiMotor;
	private SpawnPrefab SpawnPrefab;
	private GameObject Player;
	public GameObject Deathling;

	public void Start() {
		
		AiMotor = this.GetComponent<AiMotor>();
		Player = GameObject.FindGameObjectWithTag("Player");
		SpawnPrefab = this.GetComponent<SpawnPrefab>();
		SpawnPrefab.SpawningPrefab = Deathling;


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
		float _isCooling = 10.0f; // is cooling until next Attack

		// The Loop
		while(_isEngaged)
		{
			_isCooling -= Time.deltaTime;
			
			
			// Attack Phase
			if(_isCooling < 0.0f) {

				this.transform.Rotate (Vector3.Slerp(Vector3.zero, new Vector3(0.0f, 20.0f, 0.0f),_isCooling/-6.0f));

				// Attack Phase End
				if (_isCooling < -8.0f) {
					_isCooling = 10.0f;
					AiMotor.Jump();
					spawnDeathlings();
				}
				
				// Idle Phase
			} else { 
				idle();
			}
			
			yield return new WaitForEndOfFrame();	
		}
		
		// Clean Up
		reset ();
		
	}



	private void reset () {

	}

	private void idle () {
		//AiMotor.currentMovement = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
		Vector3 tmpDist = Player.transform.position - this.transform.position;
		if (tmpDist.sqrMagnitude > 100)
		{
			tmpDist = tmpDist.normalized * AiMotor.moveSpeed;
			tmpDist = AiMotor.transform.InverseTransformDirection(tmpDist);
			AiMotor.currentMovement = Vector3.Slerp(AiMotor.currentMovement,tmpDist,Time.deltaTime*1f);
		}
		else
		{
			AiMotor.currentMovement = Vector3.Slerp(AiMotor.currentMovement,Vector3.zero,Time.deltaTime*8f);
		}
		//yield return new WaitForSeconds(1f);
		//AiMotor.Jump();
	}

	private void spawnDeathlings () {
		SpawnPrefab.spawn();
		SpawnPrefab.spawn();
		SpawnPrefab.spawn();
		SpawnPrefab.spawn();
		SpawnPrefab.spawn();
		SpawnPrefab.spawn();
		Debug.Log("Spawned Deathlings!");
	}

	
}
