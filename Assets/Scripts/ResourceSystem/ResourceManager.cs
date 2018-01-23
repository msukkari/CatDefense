using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public GameObject spawnPointParent;
    public List<GameObject> prefabList;
    public int numSpawnedRRs;

    // RR refers to raw resource
    private List<GameObject> RRList;
    private List<GameObject> spawnPointList;
    private float counter;

    public int spawnDelay;


    void Start()
    {
        RRList = new List<GameObject>();
        spawnPointList = new List<GameObject>();

        if (spawnPointParent)
        {
            foreach (Transform child in spawnPointParent.transform) spawnPointList.Add(child.gameObject);
        }
        else Debug.Log("SPAWN POINT PARENT NOT ASSIGNED TO RESOURCE MANAGER");

        numSpawnedRRs = 0;
        counter = 0;
    }

    void Update()
    {
        if (numSpawnedRRs < spawnPointList.Count)
        {
            if (counter >= spawnDelay)
            {

                SpawnPoint sp = spawnPointList[Random.Range(0, spawnPointList.Count)].GetComponent<SpawnPoint>();
                if(!sp.hasResource)
                {
                    GameObject resource = Instantiate(prefabList[Random.Range(0, 3)]);
                    resource.transform.position = sp.transform.position;
                    resource.GetComponent<Resource>().m_spawnPoint = sp;
                    sp.hasResource = true;
                    sp.curResource = resource;

                    RRList.Add(resource);
                    numSpawnedRRs++;
                    counter = 0;
                }
            }
            else counter += Time.deltaTime;
        }
    }
}
