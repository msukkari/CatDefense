using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


enum AIState {
	FindMain,
	ChaseMain,
	AttackMain,
	FindSecond,
	ChaseSecond, 
	AttackSecond,
}
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class UnitAIBehaviour : MonoBehaviour {

	#region AI Properties

	public float speed = 5.0f;

	//How much the unit will try to keep chasing something that interupted it vs trying to get to its main target
	[Range(0,10)]
	public int TargetPriority = 5;

	//LayerMask of the maintarget unit is tracking, it will try to get to the unit unless we get interupted, in which case we'll attack the unit that interupted us
	public LayerMask MainTarget;
	//Range from which unit can attack main target
	public float MainAttackRange = 1.0f;
	public AbAttack MainAttack;

	//Layer of units which we will attack when we detect in our radius
	public LayerMask InteruptTargets;
	//The radius in unit will detect other things to attack
	public float InteruptRange = 2.0f;
	//The distance from which it can attack
	public float InteruptAttackRange = 1.0f;
	public AbAttack OtherAttack;

	public bool flipOnBack = false;
	public bool flipXOnBack = false;
	public bool freezeRotationVertical = false;
	#endregion

	#region AI
	private AIState m_state = AIState.FindMain;

	private HealthComponent m_currentTarget;
	private Rigidbody2D m_rb;
	#endregion

	HealthComponent m_healthMan;

	// Use this for initialization
	void Start () {
		m_healthMan = this.GetComponent<HealthComponent> ();
		m_rb = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateState ();
		ExecuteState ();
	}

	private void UpdateState()
	{
		Collider2D col = Physics2D.OverlapCircle ((Vector2)this.transform.position, InteruptRange, InteruptTargets);

		switch (m_state)
		{
		case AIState.FindMain:
			{
				//Only get interupted if you were looking for a main target.
				if (col != null)
				{
					m_state = AIState.FindSecond;
				} else if (m_currentTarget != null && ObjectInMask (m_currentTarget.gameObject, MainTarget))
				{
					m_state = AIState.ChaseMain;
				} else
				{
					//Should you try to find a secondary target? ATM we just stay put doing nothing
				}
				break;
			}
		case AIState.ChaseMain:
			{
				//Only get interupted if you were looking for a main target.
				if (col != null)
				{
					m_state = AIState.FindSecond;
				} else if (m_currentTarget == null)
				{
					m_state = AIState.FindMain;
				}
				else if(Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) <= MainAttackRange)
				{
					m_state = AIState.AttackMain;
				}
				break;
			}
		case AIState.AttackMain:
			{
				//Only get interupted if you were looking for a main target.
				if (col!=null)
				{
					m_state = AIState.FindSecond;
				}
				else if (m_currentTarget == null)
				{
					m_state = AIState.FindMain;
				}
				else if (Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) > MainAttackRange)
				{
					//Target moved out of position, start chasing again
					m_state = AIState.ChaseMain;
				}
				break;
			}
		case AIState.FindSecond:
			{
				if (m_currentTarget != null && ObjectInMask (m_currentTarget.gameObject, InteruptTargets))
				{
					m_state = AIState.ChaseSecond;
				}
				break;
			}
		case AIState.ChaseSecond:
			{
  
                if (m_currentTarget == null)
				{
					m_state = AIState.FindMain;
				}
                else if (col != null && col.gameObject != m_currentTarget.gameObject)
                {
                    m_state = AIState.FindSecond;
                }
                else if ( Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) <= InteruptAttackRange)
				{
					m_state = AIState.AttackSecond;
				} 
				break;
			}
		case AIState.AttackSecond:
			{
				if (m_currentTarget == null)
				{
					m_state = AIState.FindMain;
				} else if (Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) > InteruptAttackRange)
				{
					//Target moved out of position, start chasing again
					m_state = AIState.ChaseSecond;
				}
				break;
			}
		}
	}

	private void ExecuteState()
	{
		switch (m_state)
		{
		case AIState.FindMain:
			{
				m_currentTarget = FindClosestTarget (MainTarget);
				m_rb.velocity = Vector2.zero;
				break;
			}
		case AIState.ChaseMain:
		case AIState.ChaseSecond:
			{
				Vector2 newVelocity =((Vector2)(m_currentTarget.transform.position - this.transform.position)).normalized * speed;

				if (m_rb.velocity != newVelocity)
				{
					m_rb.velocity = newVelocity;


					//Update orientation
					if (newVelocity != Vector2.zero)
					{
						float angle = 0;
						if (flipOnBack && newVelocity.x < 0)
						{
							angle = Mathf.Atan2 (-1.0f*newVelocity.y, -1.0f*newVelocity.x) * Mathf.Rad2Deg;
						} else
						{
							angle = Mathf.Atan2 (newVelocity.y,newVelocity.x) * Mathf.Rad2Deg;
						}

						m_rb.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
					}
				}

				break;
			}
		case AIState.AttackMain:
			{
				m_rb.velocity = Vector2.zero;
				if(MainAttack!=null)
					MainAttack.Execute(m_currentTarget);
				return;
				break;
			}
		case AIState.FindSecond:
			{
				m_currentTarget = FindClosestTarget (InteruptTargets);
				m_rb.velocity = Vector2.zero;
				break;
			}
		case AIState.AttackSecond:
			{
				m_rb.velocity = Vector2.zero;
				//Do Attack Second
				if(OtherAttack!=null)
					OtherAttack.Execute(m_currentTarget);

				return;
				break;
			}
		}


		Animator anim = this.GetComponent<Animator> ();
		if (anim != null)
		{
			if (m_rb.velocity.magnitude != 0)
			{
				anim.SetBool ("Right", false);
				anim.SetBool ("Left", false);
				anim.SetBool ("Up", false);
				anim.SetBool ("Down", false);

				float rightDir = Vector2.Dot (m_rb.velocity.normalized, Vector2.right);
				if (rightDir > 0)
				{
					anim.SetBool ("Right", true);
					anim.SetBool ("Left", false);
				} else
				{
					anim.SetBool ("Right", false);
					anim.SetBool ("Left", true);
				}

				float upDir = Vector2.Dot (m_rb.velocity.normalized, Vector2.up);
				if (upDir > 0.5f)
				{
					if(freezeRotationVertical)
						this.transform.rotation = Quaternion.identity;
					
					anim.SetBool ("Right", false);
					anim.SetBool ("Left", false);
					anim.SetBool ("Up", true);
					anim.SetBool ("Down", false);

				} else if (upDir < -0.5f)
				{
					if(freezeRotationVertical)
						this.transform.rotation = Quaternion.identity;
					
					anim.SetBool ("Right", false);
					anim.SetBool ("Left", false);
					anim.SetBool ("Up", false);
					anim.SetBool ("Down", true);
				} else
				{
					anim.SetBool ("Up", false);
					anim.SetBool ("Down", false);
				}
			} else
			{
				anim.SetBool ("Right", false);
				anim.SetBool ("Left", false);
				anim.SetBool ("Up", false);
				anim.SetBool ("Down", true);
			}
		}

	}

	public static bool ObjectInMask(GameObject ob, LayerMask l)
	{
		return l == (l | (1 << ob.layer));
	}

	private HealthComponent FindClosestTarget(LayerMask targetLayer)
	{
		HealthComponent[] targets = GameObject.FindObjectsOfType<HealthComponent> ();
		targets = targets.Where(x => ObjectInMask(x.gameObject,targetLayer)).ToArray();

		HealthComponent res = null;
		float dist = float.MaxValue;
		foreach (HealthComponent h in targets)
		{

			float compDist = Vector2.Distance ((Vector2)h.transform.position, (Vector2)this.transform.position);
			if (h != this.m_healthMan && dist > compDist)
			{
				dist = compDist;
				res = h;
			}
		}

		return res;
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ObjectInMask(collision.gameObject, MainTarget) && collision.gameObject.GetComponent<HealthComponent>()!=null)
        {
            m_state = AIState.AttackMain;
            m_currentTarget = collision.gameObject.GetComponent<HealthComponent>();
        }else if (ObjectInMask(collision.gameObject, InteruptTargets) && collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            m_state = AIState.AttackSecond;
            m_currentTarget = collision.gameObject.GetComponent<HealthComponent>();
        }
    }

    void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (this.transform.position, MainAttackRange);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (this.transform.position, InteruptRange);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (this.transform.position, InteruptAttackRange);
	}
}
