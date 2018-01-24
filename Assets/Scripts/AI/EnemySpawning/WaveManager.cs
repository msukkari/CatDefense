using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Wave{
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
	public int waveCount;
	public int enemiesLeft = 0;

    public GameObject pressStart; // press start to go to next level text

    private bool allWavesDone = false;

    private int[] waveDelayList = { 3, 3, 5, 5, 5 };

	void Start () {
        ControllerInputManager.GetInstance ().OnStartDown += PressNext;

        pressStart.SetActive(false);

		for (int i = 0; i < waves.Count; i++) waves[i].WaveDelay = waveDelayList[i>=waveDelayList.Length?waveDelayList.Length-1:i];
        NextWave ();

	}

    private void OnDestroy()
    {
        ControllerInputManager.GetInstance().OnStartDown -= PressNext;
    }

    public void PressNext()
	{
        if(allWavesDone)
        {
            if (!string.IsNullOrEmpty(next_level))
            {
                SceneManager.LoadScene(next_level);
            }
            else
            {
                SceneManager.LoadScene("menu");
            }
        }
	}

	public void NextWave(){
		if (enemiesLeft <= 0)
		{
			waveCount = Mathf.Clamp (waveCount + 1, 0, waves.Count);
			StartCoroutine(SpawnWave ());
		}
	}

	IEnumerator SpawnWave()
	{
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
			if (waveCount >= (waves.Count-1))
			{
                //Game Done!
                allWavesDone = true;
                pressStart.SetActive(true);
            } else
			{
				NextWave ();
			}

		}
	}
}
