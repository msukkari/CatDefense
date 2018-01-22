using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

	public Transform target;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (target != null)
		{
			this.transform.position = (Vector3)Vector2.Lerp ((Vector2)this.transform.position, (Vector2)target.position, Time.deltaTime * speed) + new Vector3(0,0,this.transform.position.z);
		}
	}
}
