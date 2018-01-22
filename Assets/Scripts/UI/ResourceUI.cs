using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour {

	public Text metal;
	public Text oil;
	public Text rubber;

	public Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		metal.text = player.metal.ToString();
		oil.text = player.oil.ToString();
		rubber.text = player.rubber.ToString();
	}
}
