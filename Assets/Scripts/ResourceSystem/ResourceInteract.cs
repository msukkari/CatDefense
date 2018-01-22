using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInteract : MonoBehaviour, IInteractable {

    public Resource m_r;

    void Start()
    {
        m_r.m_rrInteractable = this;
        m_r.m_interactArea.GetComponent<InteractArea>().m_linkedInteract = this;
    }

	public void onTriggerEnter()
	{

	}

	public void onTriggerExit()
	{

	}

	public void Interact(GameObject player, Button button)
    {
		if(button == Button.A)
		{
	        PlayerController p = player.GetComponent<PlayerController>();

	        p.heldResource = m_r.gameObject;
	        m_r.gameObject.transform.parent = player.transform;
		}
    }
}
