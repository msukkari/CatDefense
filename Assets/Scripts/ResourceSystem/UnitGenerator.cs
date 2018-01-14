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

    public GameObject m_interactArea;
    public IInteractable m_ugInteract;

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
}
