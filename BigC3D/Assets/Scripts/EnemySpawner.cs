using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public static EnemySpawner instance;

	public float maxXpos, spawnTime;
	public GameObject enemy;
	public GameObject[] enemies;
	public float timeCnt;
	public int count;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}

		spawnTime = 0f;
	}
		
	// Use this for initialization
	public void Start () 
	{
		
		//InvokeRepeating ("PickEnemyType", 0.2f, spawnTime);
		//SpawnEnemy ();
		//InvokeRepeating ("SpawnEnemy", 0.2f, spawnTime);

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (count);
		//timeCnt = timeCnt +1;
		//PickSpawnRate ();
		/*if(count > 10)
		{
			CancelInvoke ();
		}*/

		/*if(UIManager.instance.startWaveCountdown == true && UIManager.instance.gameOver == false)
		{
			spawnTime = 0.8f;
		}*/

		spawnTime -= Time.deltaTime;



	}

	public void StopSpawning()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("PickEnemyType");
	}

	public void StartWave()
	{
		InvokeRepeating ("PickEnemyType", 0.2f, spawnTime);
	}

	public void PickEnemyType()
	{
		//Control Waves of enemies here****************************************************///////////////
		/////////************************************************************/////////////////////////
		float rand = Random.Range (1f, 100);
		if (spawnTime <= 0)
		{
			if (UIManager.instance.waveCount >= 0 && UIManager.instance.waveCount <= 10) 
			{
				spawnTime = 1.3f;

				if (rand >= 0 && rand <= 23) {
					Instantiate (enemies [0], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 24 && rand <= 47) {
					Instantiate (enemies [1], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 48 && rand <= 70) {
					Instantiate (enemies [2], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 71 && rand <= 80) {
					Debug.Log ("fast1");
					Instantiate (enemies [0], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 81 && rand <= 90) {
					Debug.Log ("fast2");
					Instantiate (enemies [1], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 91 && rand <= 100) {
					Debug.Log ("fast3");
					Instantiate (enemies [2], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
			} 
			else if (UIManager.instance.waveCount >= 11 && UIManager.instance.waveCount <= 20) 
			{
				spawnTime = 1.2f;

				if (rand >= 0 && rand <= 21) {
					Instantiate (enemies [0], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 22 && rand <= 43) {
					Instantiate (enemies [1], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 44 && rand <= 65) {
					Instantiate (enemies [2], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 66 && rand <= 78) {
					Debug.Log ("fast1");
					Instantiate (enemies [0], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 79 && rand <= 90) {
					Debug.Log ("fast2");
					Instantiate (enemies [1], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
				if (rand >= 91 && rand <= 100) {
					Debug.Log ("fast3");
					Instantiate (enemies [2], new Vector3 (Random.Range (-maxXpos, maxXpos),
						transform.position.y, transform.position.z), Quaternion.identity);
				}
			} 
			else if (UIManager.instance.waveCount >= 21 && UIManager.instance.waveCount <= 30) 
			{
				spawnTime = 1.1f;
			}
			else if (UIManager.instance.waveCount >= 31 && UIManager.instance.waveCount <= 40) 
			{
				spawnTime = 1.0f;
			} 
			else if (UIManager.instance.waveCount >= 41 && UIManager.instance.waveCount <= 50) 
			{
				spawnTime = 0.9f;
			} 
			else if (UIManager.instance.waveCount >= 51) 
			{
				spawnTime = 0.8f;
			}
		}
	}

	/*public void PickSpawnRate()
	{
		if(timeCnt > 1000f)
		{
			spawnTime = 0.4f;
		}
		if(timeCnt >= 1500f)
		{
			spawnTime = 0.2f;
		}
	}*/

	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds (1.5f);
	}
}
/*
float rand = Random.Range (1f, 100);
if(spawnTime <= 0)
{
	if(rand >= 0 && rand <= 33)
	{
		Instantiate (enemies[0], new Vector3 (Random.Range (-maxXpos, maxXpos),
			transform.position.y, transform.position.z), Quaternion.identity);
	}
	if(rand >= 34 && rand <= 66)
	{
		Instantiate (enemies[1], new Vector3 (Random.Range (-maxXpos, maxXpos),
			transform.position.y, transform.position.z), Quaternion.identity);
	}
	if(rand >=67 && rand <= 100)
	{
		Instantiate (enemies[2], new Vector3 (Random.Range (-maxXpos, maxXpos),
			transform.position.y, transform.position.z), Quaternion.identity);
	}

	if(UIManager.instance.waveCount >= 0 && UIManager.instance.waveCount <= 10)
	{
		spawnTime = 1.5f;
	}
	else if(UIManager.instance.waveCount >= 11 && UIManager.instance.waveCount <= 20)
	{
		spawnTime = 1.4f;
	}
	else if(UIManager.instance.waveCount >= 21 && UIManager.instance.waveCount <= 30)
	{
		spawnTime = 1.3f;
	}
	else if(UIManager.instance.waveCount >= 31 && UIManager.instance.waveCount <= 40)
	{
		spawnTime = 1.2f;
	}
	else if(UIManager.instance.waveCount >= 41 && UIManager.instance.waveCount <= 50)
	{
		spawnTime = 1.1f;
	}
	else if(UIManager.instance.waveCount >= 51)
	{
		spawnTime = 1f;
	}
*/
/*if (rand >= 0 && rand <= 33) {
Instantiate (enemies [0], new Vector3 (Random.Range (-maxXpos, maxXpos),
	transform.position.y, transform.position.z), Quaternion.identity);
}
if (rand >= 34 && rand <= 66) {
	Instantiate (enemies [1], new Vector3 (Random.Range (-maxXpos, maxXpos),
		transform.position.y, transform.position.z), Quaternion.identity);
}
if (rand >= 67 && rand <= 100) {
	Instantiate (enemies [2], new Vector3 (Random.Range (-maxXpos, maxXpos),
		transform.position.y, transform.position.z), Quaternion.identity);
}*/