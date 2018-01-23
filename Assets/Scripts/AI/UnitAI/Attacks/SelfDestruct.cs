using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : AbAttack {

	public float range;
	public float damage;
    public bool DamageOnDeath = false;
    private bool self_destruct = false;
	public LayerMask layerTarget;

	public override void Execute (HealthComponent t)
	{

        self_destruct = true;
        Collider2D[] hit = Physics2D.OverlapCircleAll ((Vector2)this.transform.position, range);
		
		foreach (Collider2D c in hit)
		{
			HealthComponent h = c.gameObject.GetComponent<HealthComponent> ();
			if (h != null && UnitAIBehaviour.ObjectInMask(h.gameObject,layerTarget))
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

    public void OnDestroy()
    {
        if(DamageOnDeath && !self_destruct)
        {
            //When it dies, kill things around it as well
            Execute(null);
        }

    }
}
