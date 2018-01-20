using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;
    public IInteractable m_curInteract;
    public GameObject neighborObject;
    public GameObject heldResource;

    private Rigidbody2D rb;

	private ControllerInputManager m_controllerInstance;

	void Start () {
		m_controllerInstance = ControllerInputManager.GetInstance ();
		m_controllerInstance.OnADown += Pickup;
		m_controllerInstance.OnLSChange += Move;

        rb = GetComponent<Rigidbody2D>();
        m_curInteract = null;
        neighborObject = null;
        heldResource = null;
	}

	void OnDestroy()
	{
		if (m_controllerInstance != null)
		{
			m_controllerInstance.OnADown -= Pickup;
			m_controllerInstance.OnLSChange -= Move;
		}
	}
	
	void FixedUpdate () {
        // This is temporary, allows player to be controlled with keyboard
        if (Input.GetKeyDown(KeyCode.Space)) Pickup();
    }

    public void Move(float x, float y)
    {
		//Temporary so we can use keyboard input as well
		if (x == 0 && y == 0)
		{
	        y = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;
	        x = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
		}
        rb.velocity = new Vector2(x * m_speed, y * m_speed);
    }

    public void Pickup()
    {
        if (m_curInteract != null) m_curInteract.Interact(gameObject);

        /*
        if(neighborObject)
        {
            if (neighborObject.tag == "RR") mineRR(neighborObject);
            else if (neighborObject.tag == "Refiner") neighborObject.GetComponent<Refiner>().Refine(heldResource);
            else if (neighborObject.tag == "Generator")
            {
                UnitGenerator ug = neighborObject.GetComponent<UnitGenerator>();
                if (heldResource) ug.OnAddResource(heldResource);
                else ug.OnBuildUnit();
            }
        }
        */
    }

    /*
    public void OnCollisionEnter2D(Collision2D coll)
    {
        // NOTE: this might cause issues when player is colliding with two raw resources!
        neighborObject = coll.gameObject;
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        neighborObject = null;
    }
    */

    public void OnTriggerEnter2D(Collider2D coll)
    {
        InteractArea ia = coll.gameObject.GetComponent<InteractArea>();

        if(ia == null)
        {
            Debug.Log("InteractArea not found!");
            return;
        }

        m_curInteract = coll.gameObject.GetComponent<InteractArea>().m_linkedInteract;
        Debug.Log("OnTriggerEnter2D: " + m_curInteract.ToString());
        /*
        coll.transform.parent.transform.parent = transform;
        heldResource = coll.transform.parent.gameObject;

        // destory the trigger
        Destroy(coll.transform.gameObject);
        */
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        m_curInteract = null;
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
