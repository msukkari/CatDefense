using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public enum Type { Alpha, Beta, Gamma };
    public enum State { Blocked, Free }; // Resource is blocked when it is in a machine or is in a raw resource block

    public Type m_type;
    public State m_state;

    public Resource(Type type, State state)
    {
        m_type = type;
        m_state = state;
    }

}
