using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public Vector3 reqs;
    public GameObject unit;
}

public class UnitGenerator : MonoBehaviour {

    // these are split up so they can be changed in the editor
    public List<Vector3> m_reqList;
    public List<GameObject> m_unitList;

    public GameObject m_spawnPoint;
    public Vector3 m_curResources; // [metal, oil, rubber]
    public Dictionary<Vector3, GameObject> m_recipeMap;

	// Use this for initialization
	void Start () {
        if (m_reqList.Count != m_unitList.Count) Debug.Log("Requirment list is not the same size as unit list!");

        // Build recipe map
        m_recipeMap = new Dictionary<Vector3, GameObject>();
        for(int i = 0; i < m_reqList.Count; i++)
        {
            m_recipeMap[m_reqList[i]] = m_unitList[i];
        }

        m_curResources = Vector3.zero;
	}

    public void OnAddResource(GameObject resource)
    {
        Resource r = resource.GetComponent<Resource>();
        if(!r)
        {
            Debug.Log("Non-resource given to unit generator");
            return;
        }

        if (r.m_type == Resource.Type.Metal) m_curResources.x++;
        else if (r.m_type == Resource.Type.Oil) m_curResources.y++;
        else if (r.m_type == Resource.Type.Rubber) m_curResources.z++;

        Destroy(resource);
    }

    public void OnBuildUnit()
    {
        if(m_recipeMap.ContainsKey(m_curResources))
        {
            GameObject unit = Instantiate(m_recipeMap[m_curResources]);
            unit.transform.position = m_spawnPoint.transform.position;

            m_curResources = Vector3.zero;
        }
        else
        {
            Debug.Log("Current resources don't map to a unit");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
