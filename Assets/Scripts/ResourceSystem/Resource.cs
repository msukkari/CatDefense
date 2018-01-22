using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public enum Type { RMetal, ROil, RRubber, Metal, Oil, Rubber };
    public enum State { Blocked, Free }; // Resource is blocked when it is in a machine or is in a raw resource block
    public GameObject linkedResource;
    public Type m_type;
    public State m_state;

    public GameObject m_interactArea;
    public IInteractable m_rrInteractable;

    public SpawnPoint m_spawnPoint;
    public ResourceManager m_resourceManager;

    public void Start()
    {
        m_resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    public Resource(Type type, State state)
    {
        m_type = type;
        m_state = state;
    }

    public void OnMine()
    {
        if(m_type == Type.Metal || m_type == Type.Oil || m_type == Type.Rubber || m_state == State.Free)
        {
            Debug.Log("ERROR: Attempting to mine a resource that isn't raw!");
            return;
        }
        else if(!linkedResource)
        {
            Debug.Log("ERROR: linked resource was not attached to resource");
            return;
        }


        GameObject fr = Instantiate(linkedResource);
        fr.transform.position = transform.position;
        Destroy(this.gameObject);
    }

}
