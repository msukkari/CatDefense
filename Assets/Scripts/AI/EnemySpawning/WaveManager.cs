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
	public HealthComponent enemy;
	public float count;
	public float timeBetween;
}

public class WaveManager : MonoBehaviour {

	public List<Wave> waves;
	int waveCount = -1;
	private int enemiesLeft = 0;
	private bool waveActive = false;
	// Use this for initialization
	void Start () {
		NextWave ();
	}

	void NextWave(){
		waveCount = Mathf.Clamp (waveCount + 1, 0, waves.Count);
		StartCoroutine(SpawnWave ());
	}

	IEnumerator SpawnWave()
	{
		//Call this when you want to spawn the current wave
		if (waveCount < waves.Count)
		{
			//For each spawnpoint, we make an enumerator which spawns its wave
			foreach (SpawnPoints p in waves[waveCount].SpawnPoints)
			{
				StartCoroutine (SpawnPointWave (p));
				yield return new WaitForEndOfFrame ();
			}
		}

		waveActive = false;
	}

	IEnumerator SpawnPointWave(SpawnPoints p){
		foreach (Spawn s in p.enemiesToSpawn)
		{
			for (int i = 0; i < s.count; i++)
			{
				HealthComponent spawned = Instantiate (s.enemy, p.spawnPoint.transform.position, p.spawnPoint.transform.rotation);
				enemiesLeft++;
				spawned.OnDeath += EnemyKilled;
				yield return new WaitForSeconds (s.timeBetween);
			}
		}
	}

	void EnemyKilled(){
		enemiesLeft--;
		if (enemiesLeft <= 0)
		{
			if (waveCount >= waves.Count)
			{
				//Game Done!
			} else
			{
				waveActive = true;
				NextWave ();
			}

		}
	}
}
