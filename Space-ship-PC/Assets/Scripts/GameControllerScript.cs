using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

	// Main Script that controls the game. Aka. Heart and Soul.

	public GameObject player;
	public GameObject EnemyBoss;
	public GameObject EnemyShip;
	public GameObject hazard;
	public GameObject FireUp;
	public GameObject Laser;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int lives;
	public int Round1;
	public int Round2;
	public int Round3;
	public int Round4;
	
	public static int score;
	public Text scoreText;
	
	public Image gameOverImage;
	public Text levelText;
	public Text Announce;
	public Text liveText;
	public int posDiff;
	
	private bool gameOver;
	private bool restart;
	private int level;
	private PlayerController plctr;
	
	
	void Start () {
		/*
			Give Default values to all private Variables.
		*/
		score = 0;						// Initial Score
		level=1;						// Initial Level
		levelText.text="Level: "+level;	 
		liveText.text="Lives: "+lives;
		UpdateScore();
		StartCoroutine(SpawnWaves());
		gameOver = false;
		restart = false;
		gameOverImage.enabled = false;
		Announce.text="";
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("Player");
		if (gameControllerObject != null) {
			plctr = gameControllerObject.GetComponent <PlayerController>();
		}
		if (plctr == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	void Update() {
		// Check if we need to restart the game.
		if (restart) {
			SceneManager.LoadScene("Title");
		}
	}
	IEnumerator SpawnWaves() {
		/*
			This function Spawns the differnt obstacles with respect to levels and checks for restart,
			Levels are divided into Rounds, defined as public variables
			Round1>> Asteroids
			Round2>> Laser Restriction with asteroids
			Round3>> Vertical Moving enemies
			Round4>> Horizontal Moving enemy boss 
		*/

		yield return new WaitForSeconds(startWait);

		//------------------------Round 1--------------------------
		
		while (level<=Round1) {
			plctr.fireRate=0.25f;
			if(level>=2){
				Instantiate(
					FireUp,
					new Vector3(Random.Range(plctr.boundary.Xmin,plctr.boundary.Xmax),1,Random.Range(plctr.boundary.Zmin,plctr.boundary.Zmax)),
					Quaternion.identity);
			}
			levelText.text="Level: "+level;	
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3

				(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
				if (gameOver) {
						if(lives==1){
						yield return new WaitForSeconds(2);
						restart = true;
						break;
					}
					else{
						
						--lives;
						// player.gameObject.SetActive(true);
						player.transform.position=new Vector3(0,0.91f,plctr.boundary.Zmin+2);
						player.SetActive(true);
						gameOver=false;
						liveText.text="Lives: "+ lives;
						continue;
					}
				}

			}
			++level;
			
			++hazardCount;
			yield return new WaitForSeconds(waveWait);
		}

		//----------------Round 2----------------------------------------
		
		Announce.text="Get Ready For Some Laser Action";
		yield return new WaitForSeconds(2);
		Announce.text="";
		
		if (gameOver) {
						if(lives==1){
						yield return new WaitForSeconds(2);
						restart = true;
						Update();
					}
					else{
						
						// player.gameObject.SetActive(true);
						player.transform.position=new Vector3(0,0.91f,level);
						player.SetActive(true);
						--lives;
						gameOver=false;
						liveText.text="Lives: "+ lives;
						
					}
				}

		
		while(level<=Round2 && restart==false){
			Instantiate(
				FireUp,
				new Vector3(Random.Range(plctr.boundary.Xmin,plctr.boundary.Xmax),1,Random.Range(plctr.boundary.Zmin,plctr.boundary.Zmax)),
				Quaternion.identity);
		
			Quaternion laserAngle=Quaternion.identity;
			laserAngle.Set(0.5f,-0.5f,0.5f,0.5f);
			
			Instantiate(Laser,new Vector3(0.0f,0f,level-2),laserAngle);
			
			plctr.fireRate=0.25f;
			
			levelText.text="Level: "+level;	
			//player position=level+1-2
			//zmin=level-1-2
			//laser=level-2
			if (player!=null){
				player.transform.position=new Vector3(player.transform.position.x,player.transform.position.y,level);

			}
			

			plctr.boundary.Zmin=level-2-2;
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3

				(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
				if (gameOver) {
						if(lives==1){
						yield return new WaitForSeconds(2);
						restart = true;
						break;
					}
					else{
						
						// player.gameObject.SetActive(true);
						player.transform.position=new Vector3(0,0.91f,level);
						player.SetActive(true);
						--lives;
						gameOver=false;
						liveText.text="Lives: "+ lives;
						continue;
					}
				}

			}
			
			++level;
			++hazardCount;
			yield return new WaitForSeconds(waveWait);
		}

		//------------------------Round 3----------------------------------
		
		Announce.text="Enemy Ships Are Here";
		yield return new WaitForSeconds(2);
		Announce.text="";
		
		if (gameOver) {
				if(lives==1){
				yield return new WaitForSeconds(2);
				restart = true;
				Update();
			}
			else{
				
				--lives;
				// player.gameObject.SetActive(true);
				player.transform.position=new Vector3(0,0.91f,plctr.boundary.Zmin+2);
				player.SetActive(true);
				gameOver=false;
				liveText.text="Lives: "+ lives;
				
			}
		}
		hazardCount=1;
		plctr.boundary.Zmin=0;
		while (level<=Round3 && restart==false) {
			plctr.fireRate=0.25f;
			if(level<=12){
				Instantiate(
					FireUp,
					new Vector3(Random.Range(plctr.boundary.Xmin,plctr.boundary.Xmax),1,Random.Range(plctr.boundary.Zmin,plctr.boundary.Zmax)),
					Quaternion.identity);
			}
			levelText.text="Level: "+level;	
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3

				(Random.Range(-spawnValues.x, spawnValues.x),0, spawnValues.z);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(EnemyShip, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
				if (gameOver) {
						if(lives==1){
						yield return new WaitForSeconds(2);
						restart = true;
						break;
					}
					else{
						
						// player.gameObject.SetActive(true);
						player.transform.position=new Vector3(0,0.91f,plctr.boundary.Zmin+2);
						player.SetActive(true);
						--lives;
						gameOver=false;
						liveText.text="Lives: "+ lives;
						continue;
					}
				}

			}
			++level;
			
			++hazardCount;
			yield return new WaitForSeconds(waveWait);
		}

		//------------------Round 4----------------------------------

		Announce.text="Boss is Here";
		yield return new WaitForSeconds(2);
		Announce.text="";
		
		if (gameOver) {
				if(lives==1){
				yield return new WaitForSeconds(2);
				restart = true;
				Update();
			}
			else{
				
				--lives;
				// player.gameObject.SetActive(true);
				player.transform.position=new Vector3(0,0.91f,plctr.boundary.Zmin+2);
				player.SetActive(true);
				gameOver=false;
				liveText.text="Lives: "+ lives;
				
			}
		}
		hazardCount=1;
		plctr.boundary.Zmin=0;
		float BossSpawn=0;
		while (level<=Round4 && restart==false) {
			plctr.fireRate=0.25f;
			
			Instantiate(
				FireUp,
				new Vector3(Random.Range(plctr.boundary.Xmin,plctr.boundary.Xmax),1,Random.Range(plctr.boundary.Zmin,plctr.boundary.Zmax)),
				Quaternion.identity);
			
			levelText.text="Level: "+level;	
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3

				(Random.Range(plctr.boundary.Xmin,plctr.boundary.Xmax),0, spawnValues.z-2-BossSpawn);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(EnemyBoss, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
				if (gameOver) {
						if(lives==1){
						yield return new WaitForSeconds(2);
						restart = true;
						break;
					}
					else{
						
						// player.gameObject.SetActive(true);
						player.transform.position=new Vector3(0,0.91f,plctr.boundary.Zmin+2);
						player.SetActive(true);
						--lives;
						gameOver=false;
						liveText.text="Lives: "+ lives;
						continue;
					}
				}

			}
			++level;
			BossSpawn+=2;
			// ++hazardCount;
			yield return new WaitForSeconds(waveWait);
		}
	
		
		
		if (gameOver) {
			yield return new WaitForSeconds(2);
			restart = true;
			Update();
		}
		if(restart==false){
			if(score>400){
				Announce.text="You Won!!";
			}
			else{
				Announce.text="Low Score :(";
			}
			yield return new WaitForSeconds(2);
			restart=true;
		}
			

		
	}
	public void AddScore(int newScoreValue) {	// To increase the score
		score += newScoreValue;	
		UpdateScore();
	}
	void UpdateScore() {						// To update the score on the Canvas
		scoreText.text = "Score: " + score;
	}
	public void GameOver()						// Initiate GameOver effects.
	{
		if(lives==1){
			gameOverImage.enabled = true;
		}
		gameOver = true;
		// Handheld.Vibrate();
		StartCoroutine(delay(2));
		Update();
		
	}
	IEnumerator delay(int n)					// Just to provide delay effect
    {
        yield return new WaitForSeconds(n);
    }
}
