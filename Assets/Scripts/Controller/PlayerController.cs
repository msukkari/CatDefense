using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;

    private Rigidbody2D rb;
    private GameObject curRawResource;

	// Use this for initialization
	void Start () {
        ControllerInputManager.GetInstance().OnADown += OnAPress;

        rb = GetComponent<Rigidbody2D>();
        curRawResource = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float y = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
        float x = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
        rb.velocity = new Vector2(x * m_speed, y * m_speed);

        if (Input.GetKeyDown(KeyCode.Space) && curRawResource) mineRR(curRawResource);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        // NOTE: this might cause issues when player is colliding with two raw resources!
        if (coll.gameObject.tag == "RR") curRawResource = coll.gameObject;
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "RR") curRawResource = null;
    }

    private void OnAPress()
    {
        Debug.Log("Player pressed A");
    }

    private void mineRR(GameObject rr)
    {

    }
}
