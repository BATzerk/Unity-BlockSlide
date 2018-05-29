using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Gate : Ground {
	// Components
	[SerializeField] private BoxCollider2D myCollider;
	[SerializeField] private SpriteRenderer sr_body;
//	// References
//	private List<GateButton> myButtons;
	// Properties
	[SerializeField] private int channelID;

	// Getters
	public int ChannelID { get { return channelID; } }

	private Color bodyColor { get { return myChannel.Color; } }
	private GateChannel myChannel { get { return myLevel.gateChannels[channelID]; } }


//	// ----------------------------------------------------------------
//	//  Gizmos
//	// ----------------------------------------------------------------
//	private void OnDrawGizmos() {
//		if (myButtons==null) { return; }
////		Gizmos.color = Color.Lerp(Color.white, BodyColor, 0.5f);
//		foreach (GateButton button in myButtons) {
//			if (button==null) { continue; }
//			Gizmos.DrawLine(this.transform.position, button.transform.position);
//		}
//	}



	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	new public GateData SerializeAsData() {
		GateData data = new GateData (pos, Size, channelID);
		return data;
	}

	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, GateData data) {
		BaseInitialize(myLevel, data);
		Size = data.size;
		channelID = data.channelID;
	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	public void UnlockMe() {
		SetIsOn(false);
		//todo: some animation or something, I guess
	}
	public void SetIsOn(bool _isOn) {
		myCollider.enabled = _isOn;
		if (_isOn) {
			sr_body.color = bodyColor;
		}
		else {
			sr_body.color = new Color(bodyColor.r,bodyColor.g,bodyColor.b, 0.1f);
		}
	}


//	// ----------------------------------------------------------------
//	//  Events
//	// ----------------------------------------------------------------
//	public void OnButtonPressed() {
//		// Check if all buttons have been pressed!
//		if (AreAllMyButtonsPressed()) {
//			UnlockMe();
//		}
//	}




}
