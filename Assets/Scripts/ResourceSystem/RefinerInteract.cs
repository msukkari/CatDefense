using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerInteract : MonoBehaviour, IInteractable {

    public Refiner m_refiner;

    void Start()
    {
        m_refiner.m_refinerInteract = this;
        m_refiner.m_interactArea.GetComponent<InteractArea>().m_linkedInteract = this;
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
			Debug.Log("INTERACTING WITH REFINER");

	        GameObject heldResource = player.GetComponent<PlayerController>().heldResource;
	        if(heldResource != null)
	        {
	            GameObject linked = heldResource.GetComponent<Resource>().linkedResource;
	            Destroy(heldResource);

	            StartCoroutine(RefineCoRoutine(linked));
	        }
		}
    }

    private IEnumerator RefineCoRoutine(GameObject linkedResource)
    {
        yield return new WaitForSeconds(m_refiner.m_refineTime);

        GameObject fr = Instantiate(linkedResource);
        fr.transform.position = m_refiner.m_dropPoint.transform.position;
    }
}
