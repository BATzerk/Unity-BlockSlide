﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour {
	// References!
	[SerializeField] public GameObject imageLine;

	[SerializeField] public GameObject backgroundTileSprite;

	[SerializeField] public GameObject gate;
	[SerializeField] public GameObject gateButton;
	[SerializeField] public GameObject gem;
	[SerializeField] public GameObject ground;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject toggleGround;


	// Instance
	static private ResourcesHandler instance;
	static public ResourcesHandler Instance { get { return instance; } }

	// Awake
	private void Awake () {
		// There can only be one (instance)!
		if (instance == null) {
			instance = this;
		}
		else {
			GameObject.Destroy (this);
		}
	}
}
