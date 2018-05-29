using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GateButton : BoardObject {
	// Components
	[SerializeField] private BoxCollider2D myCollider;
	[SerializeField] private SpriteRenderer sr_body;
	// References
	private Gate myGate;
	// Properties
	[SerializeField] private int channelID;
	private bool isPressed;
//	private int sideToPressMe;

	// Getters
	public bool IsPressed { get { return isPressed; } }
	public int ChannelID { get { return channelID; } }

	private Color bodyColor { get { return myChannel.Color; } }
	private GateChannel myChannel { get { return myLevel.gateChannels[channelID]; } }
	private int sideToPressMe() { return GetSideToPressMe(); } // Hacky messy. :P
	/** Kind of a weird function. We ONLY use my rotation to estimate what side button we are, based on the grid. */
	private int GetSideToPressMe() {
//		Vector2 pos = transform.localPosition;
//		float pu = GameProperties.UnitSize;
//		Vector2 snappedPos = new Vector3(Mathf.Round(pos.x/pu)*pu, Mathf.Round(pos.y/pu)*pu);
//		Vector2 diff = snappedPos - pos;
//		if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
//			if (diff.x < 0) { return Sides.L; }
//			else { return Sides.R; }
//		}
//		else {
//			if (diff.y < 0) { return Sides.B; }
//			else { return Sides.T; }
//		}
		int side = Mathf.RoundToInt(this.transform.localEulerAngles.z/90f);
		return side;
	}


	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public GateButtonData SerializeAsData() {
		GateButtonData data = new GateButtonData (pos, sideToPressMe(), channelID, isPressed);
		return data;
	}

	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, GateButtonData data) {
		BaseInitialize(myLevel, data);
		this.channelID = data.channelID;
		SetIsPressed(data.isPressed);

		rotation = data.sideToPressMe * 90;

		float scale = UnitSize * 1; // hacked in!
		this.transform.localScale = new Vector3(scale,scale,1);
	}
//	private void Start() {
//		sideToPressMe = GetSideToPressMe(); // Set this right away in case we wanna serialize me.
//
////		sr_body
//	}



	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
//	public void SetMyGate(Gate gate) {
//		this.myGate = gate;
//		bodyColor = myGate.BodyColor;
//		sr_body.color = bodyColor;
//	}
	private void SetIsPressed(bool _isPressed) {
		isPressed = _isPressed;
		if (isPressed) {
			sr_body.color = new Color(bodyColor.r,bodyColor.g,bodyColor.b, 0.1f);
		}
		else {
			sr_body.color = bodyColor;
		}
	}



	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void GetPressed() {
		SetIsPressed (true);
		myChannel.OnButtonPressed();
	}
	private void OnTriggerEnter2D(Collider2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Player) {
			OnPlayerTouchMe(otherCol.GetComponentInChildren<Player>());
		}
	}
	private void OnPlayerTouchMe(Player player) {
		if (player==null) { Debug.LogError("Uh-oh! Calling OnPlayerTouchMe with a null Player. Hmm."); return; }
		int playerMoveSide = player.MoveSide;

		if (playerMoveSide == sideToPressMe()) {
			GetPressed();
		}
	}


}
