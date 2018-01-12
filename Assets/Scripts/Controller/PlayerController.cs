using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;
    public GameObject neighborObject;
    public GameObject heldResource;

    private Rigidbody2D rb;

	void Start () {
        ControllerInputManager.GetInstance().OnADown += OnAPress;

        rb = GetComponent<Rigidbody2D>();
        neighborObject = null;
        heldResource = null;
	}
	
	void FixedUpdate () {

        float y = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
        float x = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
        rb.velocity = new Vector2(x * m_speed, y * m_speed);

        if (Input.GetKeyDown(KeyCode.Space) && neighborObject)
        {
            if (neighborObject.tag == "RR") mineRR(neighborObject);
            else if (neighborObject.tag == "Refiner") neighborObject.GetComponent<Refiner>().Refine(heldResource);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        // NOTE: this might cause issues when player is colliding with two raw resources!
        neighborObject = coll.gameObject;
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        neighborObject = null;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        coll.transform.parent.transform.parent = transform;
        heldResource = coll.transform.parent.gameObject;

        // destory the trigger
        Destroy(coll.transform.gameObject);
    }

    private void OnAPress()
    {
        Debug.Log("Player pressed A");
    }

    private void mineRR(GameObject rr)
    {
        rr.GetComponent<Resource>().OnMine();
    }
}
