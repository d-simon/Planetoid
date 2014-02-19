using UnityEngine;
using System.Collections;
using System.Text;
using System.Security;

/*
 * This script served as a highscore and reload script at the exhibition on 20.12.2012 in the Propädeutikum ZHdK.
 */

public class ReloadLevelOnTriggerHS : MonoBehaviour {
	
	public int showGui = 0;
	public string secretKey = "65438";
	public string PostScoreUrl = "http://planetoid.davidsimon.ch/hs/postScore.php?";
	public string GetHighscoreUrl = "http://planetoid.davidsimon.ch/hs/getHighscore.php";

	private string sname = "Anonym";
	private int time = 0;
	private int coins = 0;
	private int score = 0;
	private string WindowTitel = "Planetoid Highcores";
	private string Score = "";

	public GUISkin Skin;
	public float windowWidth = 380;
	private float windowHeight = 300;
	public Rect windowRect;
	public Rect popupRect;
	
	public int maxNameLength = 20;
	public int getLimitScore = 15;
	
	
	
	void Start () {
		windowRect = new Rect (120, 40, 300, 300);
			
		popupRect = new Rect (Screen.width / 2 - 250 , Screen.height /  2 - 150 , 500, 300);
		
		StartCoroutine("GetScore");
		
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		Debug.Log ("There are " + coins.Length + " Coins loaded in the scene.");
	}
	
	void Update () {
		if (Input.GetButtonDown("JoystickStart") == true)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
				
		if (showGui == 2) {
			windowRect = new Rect (Screen.width / 2 -(windowWidth / 2), 40, windowWidth, Screen.height - 50);
			windowHeight = Screen.height - 50;
		}
	}
	
	void OnGUI()
	{
		if (showGui == 1) popupRect = GUI.Window(Screen.width / 2 - 220, popupRect, DoMyPopUp, " ");	
		if (showGui == 2) windowRect = GUI.Window(0, windowRect, DoMyWindow, WindowTitel);
		
	}
	
	void OnTriggerEnter (Collider other) {
		GameObject.Find("Player").GetComponent<Attractor>().enabled = false;
		GameObject.Find ("Timer").GetComponent<Timer>()._enabled = false;
		StartCoroutine(DelayReset(120f));
	}
	
	
	IEnumerator GetScore()
	{
		Score = "";
			
    	WindowTitel = "Loading";
		
		WWWForm form = new WWWForm();
		form.AddField("limit",getLimitScore);
		
    	WWW www = new WWW(GetHighscoreUrl,form);
    	yield return www;
		
		if(www.text == "") 
    	{
			print("There was an error getting the high score: " + www.error);
			WindowTitel = "There was an error getting the high score";
    	}
		else 
		{
			WindowTitel = "Planetoid Highcores";
       		Score = www.text;
		}
	}
	
	public IEnumerator PostScore(string sname, int time, int coins, int score)
	{
		string _name = sname;
		int _time = time;
		int _coins = coins;
		int _score = score;
		
		string hash = Md5Sum(_name + _score + secretKey).ToLower();
		
		WWWForm form = new WWWForm();
		form.AddField("name",_name);
		form.AddField("time",_time);
		form.AddField("coins",_coins);
		form.AddField("score",_score);
		form.AddField("hash",hash);
		
		WWW www = new WWW(PostScoreUrl,form);
		WindowTitel = "Planetoid Highcores";
		yield return www;
		
    	if(www.text == "done") 
    	{
       		StartCoroutine("GetScore");
    	}
		else 
		{
			print("There was an error posting the high score: " + www.error);
			WindowTitel = "There was an error posting the high score";
		}
	}
	
	void DoMyWindow(int windowID) 
	{
      GUI.skin = Skin;
		
    	GUI.Label (new Rect (windowWidth / 2 - windowWidth / 2, 30, windowWidth, 500), Score);
    	
		//Restart Button
    	if (GUI.Button(new Rect(windowWidth / 2 - 75, 400, 150,80),"Restart"))
    	{
			Application.LoadLevel(Application.loadedLevel);
			iTween.Stop ();
    	}         
		
		//Refresh Buttton
		/*if (GUI.Button(new Rect(Screen.width / 2 + 10 + 125 + 45, Screen.height /2 + 10, 90, 40),"Restart"))
    	{
			StartCoroutine("GetScore");
    	}*/   
		
    }
	
    void DoMyPopUp(int windowID) 
	{
      GUI.skin = Skin;
		
    	GUI.Label (new Rect (windowWidth / 2 - windowWidth / 2, 30, windowWidth, windowHeight), " ");
        	
			GUI.Label(new Rect (100, 40, 400, 40), "Name:  ");
			sname = GUI.TextField (new Rect (200, 50, 200, 40), sname,  maxNameLength);
	    	GUI.Label(new Rect (100, 80, 400, 40), "Score:  " + score);
	    	GUI.Label (new Rect (100, 120, 400, 40), "Time:   " + (float) time / 100 +  " s");
			GUI.Label(new Rect (100, 160, 400, 40), "Coins:  " + coins);

				
	    	if (GUI.Button(new Rect(10 + 125,  240, 200, 40), "Submit Score"))
	    	{
				StartCoroutine(PostScore(sname, time, coins, score));
	       		sname = "";
				time = 0;
				coins = 0;
				score = 0;
				showGui = 2;
	    	}   
		
    }
	
	public string Md5Sum(string input)
	{
    	System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
    	byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    	byte[] hash = md5.ComputeHash(inputBytes);
 
    	StringBuilder sb = new StringBuilder();
    	for (int i = 0; i < hash.Length; i++)
    	{	
    	    sb.Append(hash[i].ToString("X2"));
    	}
    	return sb.ToString();
	}
	

	IEnumerator DelayReset(float t)
	{
		string _name = "Anonym";
		sname = _name;
					
		int _time = (int) (Time.timeSinceLevelLoad / Time.timeScale * 100);
		time = _time;
		int _coins = GameObject.Find ("Player").GetComponent<PlayerSettings>()._CoinCount;
		coins = _coins;
		int _score = (int) (Mathf.Pow (_coins, 1.2f) / (Time.timeSinceLevelLoad / Time.timeScale) * 10000);
		score = _score;
					
		StartCoroutine("GetScore");
		
		float tmpT = 0;
		while(tmpT < t) 
		{
			showGui = 1;
			Screen.showCursor = true;
			yield return new WaitForSeconds(1f);
			tmpT += Time.deltaTime;
			if (showGui == 2) break;
			
		}
		
		showGui = 2;		
	}
	
}




