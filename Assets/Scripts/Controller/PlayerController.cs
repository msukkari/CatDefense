using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 dis = new Vector2(Input.GetAxis("Horizontal") * m_speed, Input.GetAxis("Vertical") * m_speed);
        Vector2 dir = new Vector2(Input.GetAxis("RStickX"), Input.GetAxis("RStickY"));

        Debug.Log(dir);

        transform.Translate(dis);
        //transform.LookAt(new Vector3(dir.x, 0.0f, dir.y));
    }
}
