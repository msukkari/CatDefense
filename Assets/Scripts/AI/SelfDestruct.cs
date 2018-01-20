using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : AbAttack {

	public float range;
	public float damage;

	public override void Execute ()
	{
		Collider2D[] hit = Physics2D.OverlapCircleAll ((Vector2)this.transform.position, range);
		foreach (Collider2D c in hit)
		{
			HealthComponent h = c.transform.root.GetComponentInChildren<HealthComponent> ();
			if (h != null)
			{
				h.Damage (damage);
			}
		}

		HealthComponent self = this.GetComponent<HealthComponent> ();
		if (self != null)
		{
			self.Damage (self.maxHealth);
		} else
		{
			Destroy (this.gameObject);
		}

	}
}
