using UnityEngine;
using System.Collections;

public class PlayerCollector : MonoBehaviour {
	
	public bool collectCoins = true;	
	
	void OnTriggerEnter(Collider collisionObj) {
		
		if (collectCoins == true){
			if( collisionObj.gameObject.tag == "Coin")
			{
				collisionObj.GetComponent<Coin>().Destruct();
				this.gameObject.GetComponent<PlayerSettings>()._CoinCount++;
			}
			else if (collisionObj.gameObject.tag == "AudioVisCoin")
			{
				collisionObj.GetComponent<AudioVisCoin>().Destruct();
			}
			else if (collisionObj.gameObject.tag == "AudioVisCoinChild")
			{
				collisionObj.transform.parent.GetComponent<AudioVisCoin>().Destruct();
			}
		}
	}
}
