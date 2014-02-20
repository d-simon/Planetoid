using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AiMotor))]
[RequireComponent (typeof (SpawnPrefab))]
public class BossL1 : MonoBehaviour {

	
	public float idlePhaseTime = 10f;
	public float spawnPhaseTime = 8f;
	public float spawnPhaseSpinSpeed = 20.0f;
	[Range(0f,1f)]
	public float spawnPhaseSpawnTimeTreshold = 0.75f;
	public GameObject Deathling;
	
	[System.NonSerialized] bool _isEngaged = false; // is engaged
	private AiMotor AiMotor;
	private SpawnPrefab SpawnPrefab;
	private GameObject Player;

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
		float _isCooling = idlePhaseTime; // is cooling until next Attack
		float _spawnTimeTreshold = spawnPhaseSpawnTimeTreshold * -spawnPhaseTime;
		bool _hasSpawnedDeathlings = false;

		// The Loop
		while(_isEngaged)
		{
			_isCooling -= Time.deltaTime;
			
			
			// Spawn Phase
			if(_isCooling < 0.0f) {

				// Start spinning
				float _slerpValue = (_isCooling > _spawnTimeTreshold) ? Mathf.Abs(_isCooling / _spawnTimeTreshold) : 1.0f - Mathf.Abs((_isCooling - _spawnTimeTreshold) / (-spawnPhaseTime - _spawnTimeTreshold)); // normalize from 0->-8 to 0->1->0
				this.transform.Rotate (Vector3.Slerp(Vector3.zero, new Vector3(0.0f, spawnPhaseSpinSpeed, 0.0f), _slerpValue));

				// Spawn Deathlings
				if (_hasSpawnedDeathlings == false && _isCooling < _spawnTimeTreshold) {
					spawnDeathlings();
					_hasSpawnedDeathlings = true;
				}

				// Attack Phase End
				if (_isCooling < -spawnPhaseTime) {
					_isCooling = idlePhaseTime;
					_hasSpawnedDeathlings = false;
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
