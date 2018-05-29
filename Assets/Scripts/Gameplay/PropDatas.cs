using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PropData {
//	public BoardPos boardPos;
	public Vector2 pos; // NOTE: not super clean when boardPos exists (and it's implied that boardPos is to be used). Currently used for objects whose pivots are CENTERed (which is most).
}

public class GroundData : PropData {
//	public Color bodyColor;
	public Vector2 size;
	public GroundData(Vector2 pos, Vector2 size) {//, Color bodyColor) {
		this.pos = pos;
		this.size = size;
//		this.bodyColor = bodyColor;
	}
}
public class GateData : GroundData {
	public int channelID;
	public GateData(Vector2 pos, Vector2 size, int channelID) : base(pos, size) {
		this.channelID = channelID;
	}
}
public class GateButtonData : PropData {
	public bool isPressed;
	public int sideToPressMe;
	public int channelID;
	public GateButtonData(Vector2 pos, int sideToPressMe, int channelID, bool isPressed) {
		this.pos = pos;
		this.sideToPressMe = sideToPressMe;
		this.channelID = channelID;
		this.isPressed = isPressed;
	}
}

public class GemData : PropData {
	public bool isCollected=false;
	public GemData(Vector2 pos, bool isCollected) {
		this.pos = pos;
		this.isCollected = isCollected;
	}
}
public class PlayerData : PropData {
	public PlayerData(Vector2 pos) {//BoardPos boardPos) {
		this.pos = pos;
	}
}

public class ToggleGroundData : GroundData {
	public bool startsOn;
	public bool isOn;
	public ToggleGroundData(Vector2 pos, Vector2 size, bool startsOn, bool isOn) : base(pos, size) {
		this.startsOn = startsOn;
		this.isOn = isOn;
	}
}
