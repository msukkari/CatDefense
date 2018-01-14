﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public bool hasResource;
    public GameObject curResource;

    public void OnSpawn(GameObject resource)
    {
        hasResource = true;
        curResource = resource;
    }

    public void OnCollected()
    {
        hasResource = false;
        curResource = null;
    }

	void Start () {
        hasResource = false;
	}
	
	void Update () {
		
	}
}