﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(new Vector3(0.0f, 0.0f, 3.0f * Time.deltaTime));
        this.transform.Translate(new Vector3(3.0f * Time.deltaTime, 0.0f, 0.0f));
    }
}