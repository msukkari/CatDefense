using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneratorInteract : MonoBehaviour, IInteractable {

    public UnitGenerator m_ug;

    void Start () {
        m_ug.m_ugInteract = this;
        m_ug.m_interactArea.GetComponent<InteractArea>().m_linkedInteract = this;
    }
	
    public void Interact(GameObject heldResource)
    {
        Debug.Log("INTERACTING WITH UNIT GENERATOR");

        if (heldResource != null) // add resource
        {
            Resource r = heldResource.GetComponent<Resource>();
            if (!r)
            {
                Debug.Log("Non-resource given to unit generator");
                return;
            }

            if (r.m_type == Resource.Type.Metal) m_ug.m_curResources.x++;
            else if (r.m_type == Resource.Type.Oil) m_ug.m_curResources.y++;
            else if (r.m_type == Resource.Type.Rubber) m_ug.m_curResources.z++;

            Destroy(heldResource);
        }
        else // build unit
        {
            if (m_ug.m_recipeMap.ContainsKey(m_ug.m_curResources))
            {
                GameObject unit = Instantiate(m_ug.m_recipeMap[m_ug.m_curResources]);
                unit.transform.position = m_ug.m_spawnPoint.transform.position;

                m_ug.m_curResources = Vector3.zero;
            }
            else
            {
                Debug.Log("Current resources don't map to a unit");
            }
        }
    }
}
