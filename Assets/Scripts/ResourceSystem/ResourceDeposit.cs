using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceDeposit : MonoBehaviour {

    public GameObject m_playerObject;

    private Player m_player;

    public GameObject m_interactArea;
    public IInteractable m_ugInteract;

	private HealthComponent m_health;
    // Use this for initialization
    void Start () {
		m_health = this.GetComponent<HealthComponent> ();
		if (m_health != null)
		{
			m_health.OnDeath += Die;
		}
        m_player = m_playerObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Die()
	{
        Destroy(GameObject.Find("BackgroundMusic"));
		SceneManager.LoadScene ("menu");
	}

	void OnDestroy()
	{
		if (m_health != null)
		{
			m_health.OnDeath -= Die;
		}
	}

}
