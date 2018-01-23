using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAoE : AbAttack {

    public LayerMask targets;
    public float range = 5;
    public float damage = 2;

    
    public override void Execute(HealthComponent target)
    {
        //Do Nothing TBH
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)this.transform.position, range);

        foreach (Collider2D c in hit)
        {
            HealthComponent h = c.gameObject.GetComponent<HealthComponent>();
            if (h != null && UnitAIBehaviour.ObjectInMask(h.gameObject, targets))
            {
                h.Damage(damage*Time.deltaTime);
            }
        }
    }
}
