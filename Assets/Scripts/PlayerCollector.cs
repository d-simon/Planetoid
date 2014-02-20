using UnityEngine;
using System.Collections;

public class PlayerCollector : MonoBehaviour {

	public bool collectCoins = true;
	private PlayerSettings _playerSettings;

	public void Start () {
		_playerSettings = this.GetComponent<PlayerSettings>();
	}

	public void OnTriggerEnter(Collider collisionObj) {
		
		if (collectCoins == true){
			if( collisionObj.gameObject.tag == "Coin")
			{
				collisionObj.GetComponent<Coin>().Destruct();
				_playerSettings._CoinCount++;
			}
//			else if (collisionObj.gameObject.tag == "AudioVisCoin")
//			{
//				collisionObj.GetComponent<AudioVisCoin>().Destruct();
//				_playerSettings._CoinCount++;
//			}
//			else if (collisionObj.gameObject.tag == "AudioVisCoinChild")
//			{
//				collisionObj.transform.parent.GetComponent<AudioVisCoin>().Destruct();
//			}
		}
	}
}
