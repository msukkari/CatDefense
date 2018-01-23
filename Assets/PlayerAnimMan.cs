using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimMan : MonoBehaviour {

    Rigidbody2D m_rb;
    Animator m_anim;
	// Use this for initialization
	void Start () {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        m_anim.SetFloat("WalkX", m_rb.velocity.normalized.x);
        m_anim.SetFloat("WalkY", m_rb.velocity.normalized.y);

    }
}
