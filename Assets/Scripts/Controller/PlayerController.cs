using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;

	// Use this for initialization
	void Start () {
        ControllerInputManager.GetInstance().OnADown += OnAPress;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) Debug.Log("Player pressed space");
    }

    private void OnAPress()
    {
        Debug.Log("Player pressed A");
    }
}
