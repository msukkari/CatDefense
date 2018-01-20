using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : AbAttack {

	public float range;
	public float damage;

	public override void Execute (HealthComponent t)
	{
		Collider2D[] hit = Physics2D.OverlapCircleAll ((Vector2)this.transform.position, range);
		foreach (Collider2D c in hit)
		{
			HealthComponent h = c.gameObject.GetComponent<HealthComponent> ();
			if (h != null && h == t)
			{
				h.Damage (damage);
			}
		}

		HealthComponent self = this.GetComponent<HealthComponent> ();
		if (self != null)
		{
			self.Kill ();
		} else
		{
			Destroy (this.gameObject);
		}

	}
}
