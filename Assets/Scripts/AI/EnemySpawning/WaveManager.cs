using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave{
	public List<SpawnPoints> SpawnPoints;
}

[System.Serializable]
public struct SpawnPoints
{
	public GameObject spawnPoint;
	public List<Spawn> enemiesToSpawn;
}

[System.Serializable]
public struct Spawn
{
	public GameObject enemy;
	public float count;
	public float timeBetween;
}

public class WaveManager : MonoBehaviour {

	public List<Wave> waves;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
