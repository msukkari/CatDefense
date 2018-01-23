using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public bool hasResource;
    public GameObject curResource;

    public void OnSpawn(GameObject resource)
    {
        hasResource = true;
        curResource = resource;
    }

    public void OnCollected()
    {
        StartCoroutine(spawnDelay());
    }

	void Start () {
        hasResource = false;
	}
	
	void Update () {
		
	}

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(5.0f);
        hasResource = false;
        curResource = null;
    }

}
