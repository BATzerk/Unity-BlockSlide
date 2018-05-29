using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ground : BoardObject {


	// Getters / Setters
	private Vector2 GetSize() {
		return this.transform.localScale; //GetComponent<BoxCollider2D>().size;
	}
	private void SetSize(Vector2 size) {
		this.transform.localScale = new Vector3(size.x, size.y, 1);
	}



	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public GroundData SerializeAsData() {
		GroundData data = new GroundData (pos, GetSize());
		return data;
	}


	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, GroundData data) {
		BaseInitialize(myLevel, data);

		SetSize (data.size);
	}

}
