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
				if (col!=null)
				{
					m_state = AIState.FindSecond;
				}
				else if (m_currentTarget == null || Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) <= MainAttackRange)
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
				else if (Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) > InteruptAttackRange)
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
				if (m_currentTarget == null || Vector2.Distance ((Vector2)this.transform.position, (Vector2)m_currentTarget.transform.position) <= InteruptAttackRange)
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
				m_rb.velocity = ((Vector2)(m_currentTarget.transform.position - this.transform.position)).normalized * speed;
				//Update orientation
				if (m_rb.velocity != Vector2.zero)
				{
					float angle = Mathf.Atan2 (m_rb.velocity.y, m_rb.velocity.x) * Mathf.Rad2Deg;
					m_rb.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
				}
				break;
			}
		case AIState.AttackMain:
			{
				//Do Attack Main
				if(MainAttack!=null)
					MainAttack.Execute();
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
					OtherAttack.Execute();
				break;
			}
		}
	}

	private bool ObjectInMask(GameObject ob, LayerMask l)
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
}
