using UnityEngine;
using System.Collections;

public class RandomizeBlockSpawn : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject[] objectToSpawn;
	public GameObject finalBlock;
	public float TimeRemaining;

	// Use this for initialization
	void Start () 
	{
		SpawnTimer();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TimeRemaining -= Time.deltaTime;

		if(TimeRemaining <= 0.1f)
		{
			Invoke("SpawnFinalBlock" ,0.09f);
		}

		if(TimeRemaining <= 0)
		{
			CancelInvoke();
		}
	}


	void Spawn()
	{
		for(int i = 0; i < 1; i++)
		{
			GameObject obj = objectToSpawn[Random.Range(0, objectToSpawn.Length)];
			Instantiate(obj, spawnPoint.transform.position, spawnPoint.rotation);
		}
		//Instantiate(finalBlock, spawnPoint.transform.position, spawnPoint.rotation);
	}

	void SpawnTimer()
	{
		InvokeRepeating("Spawn", 1f , 1f);
	}

	void SpawnFinalBlock()
	{
		Instantiate(finalBlock, spawnPoint.transform.position, spawnPoint.rotation);
	}
}
