using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour {

	public RectTransform OilIcon;
	public RectTransform RubberIcon;
	public RectTransform MetalIcon;


	public LinkedList<RectTransform> currentRecipe = new LinkedList<RectTransform>();
	public RectTransform holder;

	public float widthElement = 50;
	public float iconWidth = 40;
	public float padding = 10;

	private Vector3 initialPosHolder;
	// Use this for initialization
	void Start () {
		initialPosHolder = holder.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddElement(int i){
		RectTransform el;
		switch (i)
		{
		case 0:
			el = Instantiate (MetalIcon);
			break;
		case 1:
			el = Instantiate (OilIcon);
			break;
		case 2:
		default:
			el = Instantiate (RubberIcon);
			break;

		}

		el.transform.SetParent(holder.transform);
		el.position = Vector3.zero;
		currentRecipe.AddLast (el);
		UpdateDisplay ();
	}

	public void UpdateDisplay()
	{
		int i = 0;
		foreach (RectTransform rt in currentRecipe)
		{
			rt.localPosition = new Vector3 (i * widthElement + padding, 0, 0);
			Debug.Log (new Vector3 (i * widthElement + padding, 0, 0));
			i++;
		}

		holder.transform.position = initialPosHolder + new Vector3(-0.5f*(i*iconWidth+(i-1)*padding),0,0);
	}

	public void ClearRecipe()
	{
		foreach (RectTransform rt in currentRecipe)
		{
			Destroy (rt.gameObject);
		}
		currentRecipe.Clear ();
	}
}
