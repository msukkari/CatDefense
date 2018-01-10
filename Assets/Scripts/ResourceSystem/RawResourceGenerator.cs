using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawResourceGenerator : MonoBehaviour {

    public List<GameObject> spawnPointList;
    public List<GameObject> prefabList;

    // RR refers to raw resource
    private List<GameObject> RRList;
    private int numSpawnedRRs;
    private float counter;
    

	// Use this for initialization
	void Start () {
        RRList = new List<GameObject>();
        numSpawnedRRs = 0;
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(numSpawnedRRs < spawnPointList.Count)
        {
            if (counter >= 1.0f)
            {
                GameObject resource = Instantiate(prefabList[Random.Range(0, 3)]);

                SpawnPoint sp = spawnPointList[Random.Range(0, spawnPointList.Count)].GetComponent<SpawnPoint>();
                while(sp.hasResource) sp = spawnPointList[Random.Range(0, spawnPointList.Count)].GetComponent<SpawnPoint>();

                resource.transform.position = sp.transform.position;
                sp.hasResource = true;
                sp.curResource = resource;
                
                RRList.Add(resource);
                numSpawnedRRs++;
                counter = 0;
            }
            else counter += Time.deltaTime;
        }
	}
}
