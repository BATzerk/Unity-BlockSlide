using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PropData {
//	public BoardPos boardPos;
	public Vector2 pos; // NOTE: not super clean when boardPos exists (and it's implied that boardPos is to be used). Currently used for objects whose pivots are CENTERed (which is most).
}

public class GroundData : PropData {
	public Vector2 size;
	public GroundData(Vector2 pos, Vector2 size) {
//		this.boardPos = boardPos;
		this.pos = pos;
		this.size = size;
	}
}

public class PlayerData : PropData {
	public PlayerData(Vector2 pos) {//BoardPos boardPos) {
		this.pos = pos;
	}
}
