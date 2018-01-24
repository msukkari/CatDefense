using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_speed;
	public float m_boostRatio = 1.0f;

	private bool m_boostOn = false;

    public IInteractable m_curInteract;
    public GameObject heldResource;

    private Rigidbody2D rb;

	private ControllerInputManager m_controllerInstance;

	void Start () {
		m_controllerInstance = ControllerInputManager.GetInstance ();
		m_controllerInstance.OnADown += onADown;
		m_controllerInstance.OnXDown += onXDown;
		m_controllerInstance.OnYDown += onYDown;
		m_controllerInstance.OnBDown += onBDown;
		m_controllerInstance.OnLSChange += Move;

		m_controllerInstance.OnRBDown += BoostOn;
		m_controllerInstance.OnRBUp += BoostOff;

        rb = GetComponent<Rigidbody2D>();
        m_curInteract = null;
        heldResource = null;
	}

	void OnDestroy()
	{
		if (m_controllerInstance != null)
		{
			m_controllerInstance.OnADown -= onADown;
			m_controllerInstance.OnXDown -= onXDown;
			m_controllerInstance.OnYDown -= onYDown;
			m_controllerInstance.OnBDown -= onBDown;
			m_controllerInstance.OnLSChange -= Move;
		}
	}
	
	void FixedUpdate () {
        // This is temporary, allows player to be controlled with keyboard
		if (Input.GetKeyDown(KeyCode.Space)) onADown();
    }

	public void BoostOn()
	{
		m_boostOn = true;
	}

	public void BoostOff()
	{
		m_boostOn = false;
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
		if (m_boostOn)
		{
			rb.velocity = rb.velocity * m_boostRatio;
		}
    }

	public void onADown() {onButtonDown(Button.A);}
	public void onXDown() {onButtonDown(Button.X);}
	public void onYDown() {onButtonDown(Button.Y);}
	public void onBDown() {onButtonDown(Button.B);}

	public void onButtonDown(Button button)
    {
		if (m_curInteract != null) m_curInteract.Interact(gameObject, button);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        InteractArea ia = coll.gameObject.GetComponent<InteractArea>();

        if(ia == null)
        {
            Debug.Log("InteractArea not found!");
            return;
        }

        m_curInteract = coll.gameObject.GetComponent<InteractArea>().m_linkedInteract;
		if (m_curInteract != null)
		{
			m_curInteract.onTriggerEnter();
			Debug.Log("OnTriggerEnter2D: " + m_curInteract.ToString());
		}

    }

    public void OnTriggerExit2D(Collider2D coll)
    {
		if (m_curInteract != null)
		{
			m_curInteract.onTriggerExit();
			m_curInteract = null;
		}
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
