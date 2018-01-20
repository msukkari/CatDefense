using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {


	public float maxHealth = 10.0f;

	public float m_health;

	public void Start()
	{
		m_health = maxHealth;
	}
	public void Damage(float amount)
	{
		m_health = Mathf.Clamp ( m_health - amount, 0,maxHealth);
		//Apply damage visual
		CheckHealth ();
	}

	public void Heal(float amount)
	{
		m_health = Mathf.Clamp ( m_health + amount, 0,maxHealth);
		//Apply heal visual
		CheckHealth ();
	}

	private void CheckHealth(){
		if (m_health <= 0)
		{
			Destroy (this.gameObject);
		} 
	}

	// Update is called once per frame
	void Update () {
		
	}
}
