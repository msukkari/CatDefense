using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AbAttack {

	public Bullet bullet;
	public float fireRate;

	private float lastFire = 0;
	public override void Execute (HealthComponent target)
	{
		if ((Time.time - lastFire) >= fireRate)
		{
			lastFire = Time.time;
			Bullet b = Instantiate (bullet, this.transform.position, this.transform.rotation);
			b.Fire (target);
		}
	}
}
