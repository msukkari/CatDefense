using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDepositInteract : MonoBehaviour, IInteractable {

    public ResourceDeposit m_rd;

	void Start () {
        m_rd.m_ugInteract = this;
        m_rd.m_interactArea.GetComponent<InteractArea>().m_linkedInteract = this;
    }
	
	public void Interact(GameObject player)
    {
        GameObject heldResource = player.GetComponent<PlayerController>().heldResource;
        Player p = player.GetComponent<Player>();

        if(heldResource)
        {
            Resource resource = heldResource.GetComponent<Resource>();
            if(resource)
            {
                if (resource.m_type == Resource.Type.Metal) p.metal++;
                else if(resource.m_type == Resource.Type.Oil) p.oil++;
                else if (resource.m_type == Resource.Type.Rubber) p.rubber++;
            }

            Destroy(heldResource);
        }
    }
}
