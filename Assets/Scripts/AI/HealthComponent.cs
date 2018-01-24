using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour {


	public float maxHealth = 10.0f;

	public float m_health;
	public bool animate = false;
	public RectTransform HealthBarUI;


	public delegate void DeathEvent();
	public event DeathEvent OnDeath;

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

		Animator anim = this.GetComponent<Animator> ();
		if (anim != null && animate)
		{
			anim.SetFloat ("Health", m_health/maxHealth);	
		}

		if (HealthBarUI != null)
		{
			HealthBarUI.localScale = new Vector3 ((m_health / maxHealth), HealthBarUI.localScale.y, HealthBarUI.localScale.z);
		}
		if (m_health <= 0)
		{
			Kill ();
		} 
	}

	// Update is called once per frame
	void Update () {
		CheckHealth ();
	}

	public void Kill()
	{
		if (OnDeath != null)
		{
			OnDeath ();
		}
		Destroy (this.gameObject);
	}
}
