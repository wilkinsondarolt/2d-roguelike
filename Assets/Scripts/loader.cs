﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour {
    public GameObject gameManager;

	// Use this for initialization
	void Awake () {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
		
	}
}
