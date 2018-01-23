using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

	public Transform target;
	public float speed = 1;

	public bool followX = false;
	public bool followY = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (target != null)
		{
			Vector3 newPos = (Vector3)Vector2.Lerp ((Vector2)this.transform.position, (Vector2)target.position, Time.deltaTime * speed) + new Vector3(0,0,this.transform.position.z);
			if (!followX)
				newPos.x = this.transform.position.x;
			if (!followY)
				newPos.y = this.transform.position.y;

			this.transform.position = newPos;
		}
	}
}
