using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public enum Type { RMetal, ROil, RRubber, Metal, Oil, Rubber };
    public enum State { Blocked, Free }; // Resource is blocked when it is in a machine or is in a raw resource block

    public Type m_type;
    public State m_state;

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

        //GameObject fr = 
    }

}
