using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public float maxXpos, spawnTime;
	public GameObject enemy;
	public GameObject[] enemies;
	public float timeCnt;


	void Awake()
	{
		spawnTime = 0.8f;
		timeCnt = 1;
	}
	// Use this for initialization
	void Start () 
	{
		//SpawnEnemy ();
		//InvokeRepeating ("SpawnEnemy", 0.2f, spawnTime);
		InvokeRepeating ("PickEnemyType", 0.2f, spawnTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCnt = timeCnt +1;
		PickSpawnRate ();
	}

	void SpawnEnemy()
	{
		Instantiate (enemy, new Vector3 (Random.Range (-maxXpos, maxXpos),
			transform.position.y, transform.position.z), Quaternion.identity);
	}

	public void StopSpawning()
	{
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("PickEnemyType");
	}

	public void PickEnemyType()
	{
		float rand = Random.Range (1f, 100);

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
	}

	public void PickSpawnRate()
	{
		if(timeCnt > 1000f)
		{
			spawnTime = 0.4f;
		}
		if(timeCnt >= 1500f)
		{
			spawnTime = 0.2f;
		}
	}
}
