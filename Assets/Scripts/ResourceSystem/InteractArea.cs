using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Button {A, X, Y, B};
public interface IInteractable
{
	void onTriggerEnter();
	void onTriggerExit();
    void Interact(GameObject player, Button button);
}

public class InteractArea : MonoBehaviour {

    public IInteractable m_linkedInteract;
}
