using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

	public float damage = 2;
	public float speed = 5;
	public float lifeTime = 2;


	Coroutine killRoutine;
	private GameObject target;
	public void Fire(HealthComponent target){
		this.GetComponent<Rigidbody2D> ().velocity = ((Vector2)(target.transform.position - this.transform.position)).normalized * speed;
		this.target = target.gameObject;
		killRoutine = StartCoroutine (Kill (lifeTime));
	}

	private IEnumerator Kill(float time){
		yield return new WaitForSeconds (time);
		Destroy (this.gameObject);
	}

	public void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject == target.gameObject)
		{
			c.GetComponent<HealthComponent> ().Damage (damage);
			StopCoroutine (killRoutine);
			Destroy (this.gameObject);
		}
	}

}
