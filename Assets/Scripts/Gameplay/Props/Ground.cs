using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ground : BoardObject {
//	// Components
//	[SerializeField] private BoxCollider2D myCollider;
//	[SerializeField] private SpriteRenderer bodySprite;


	// Getters / Setters
	protected Vector2 Size {
		get { return this.transform.localScale; } //myCollider.size;
		set { this.transform.localScale = new Vector3(value.x, value.y, 1); }
	}



	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public GroundData SerializeAsData() {
		GroundData data = new GroundData (pos, Size);
		return data;
	}

	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, GroundData data) {
		BaseInitialize(myLevel, data);
		Size = data.size;
	}

}
