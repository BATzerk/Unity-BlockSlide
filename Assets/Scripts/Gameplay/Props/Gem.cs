using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : BoardObject {
	// Components
	[SerializeField] private CircleCollider2D myCollider;
	[SerializeField] private ParticleSystem ps_collectedBurst;
	[SerializeField] private SpriteRenderer sr_body;
	// Properties
	private bool isCollected = false;


	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public GemData SerializeAsData() {
		GemData data = new GemData (pos, isCollected);
		return data;
	}

	// ----------------------------------------------------------------
	//  Start
	// ----------------------------------------------------------------
	private void Start () {
		// Size me right!
		float diameter = UnitSize * 0.6f;
		GameUtils.SizeSpriteRenderer(sr_body, diameter,diameter);
		myCollider.radius = diameter*0.5f;
	}
	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, GemData data) {
		BaseInitialize(myLevel, data);

		SetIsCollected(data.isCollected);
	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void SetIsCollected(bool isCollected) {
		this.isCollected = isCollected;
		myCollider.enabled = isCollected;
		sr_body.enabled = isCollected;
	}



	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void OnTriggerEnter2D(Collider2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Player) {
			GetCollected();
		}
	}
	private void GetCollected() {
		ps_collectedBurst.Emit(20);
		SetIsCollected(true);
	}


}
