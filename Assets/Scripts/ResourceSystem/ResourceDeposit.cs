using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceDeposit : MonoBehaviour {

    public GameObject m_playerObject;

    private Player m_player;

    public GameObject m_interactArea;
    public IInteractable m_ugInteract;

    // Use this for initialization
    void Start () {
        m_player = m_playerObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		SceneManager.LoadScene ("menu");
		//Gameover
	}
}
