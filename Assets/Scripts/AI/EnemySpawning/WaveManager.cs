using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Wave{
	public float WaveDelay;
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
	public float timeStartWait ;
	public HealthComponent enemy;
	public float count;
	public float timeBetween;
}

public class WaveManager : MonoBehaviour {

	public string next_level;

	public List<Wave> waves;
	int waveCount = -1;
	public int enemiesLeft = 0;
	private bool waveActive = false;
	private bool waveDone = true;
	// Use this for initialization
	void Start () {
		ControllerInputManager.GetInstance ().OnStartDown += PressNext;
		//NextWave ();
	}

	public void PressNext()
	{
		if (waveDone)
		{
			if (waveCount >= (waves.Count-1))
			{
				
				if (!string.IsNullOrEmpty(next_level))
				{
					SceneManager.LoadScene (next_level);
				} else
				{
					SceneManager.LoadScene ("menu");
				}
			} else
			{
				NextWave ();
			}
		}

	}

	public void NextWave(){
		if (enemiesLeft <= 0)
		{
			waveActive = true;
			waveCount = Mathf.Clamp (waveCount + 1, 0, waves.Count);
			StartCoroutine(SpawnWave ());
		}
	}

	IEnumerator SpawnWave()
	{
		waveDone = false;
		//Call this when you want to spawn the current wave
		if (waveCount < waves.Count)
		{
			yield return new WaitForSeconds (waves [waveCount].WaveDelay);
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
			yield return new WaitForSeconds (s.timeStartWait);

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
			waveDone = true;
			if (waveCount >= (waves.Count-1))
			{
				//Game Done!
			} else
			{
				//NextWave ();
			}

		}
	}
}
