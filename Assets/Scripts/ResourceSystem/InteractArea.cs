using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject go);
}

public class InteractArea : MonoBehaviour {

    public IInteractable m_linkedInteract;
}
