using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateChannel {
	// Properties
	private int channelID;
	// References
	private Level myLevel;

	public Color Color {
		get {
			switch (channelID) {
				case 0: return new ColorHSB(332/360f, 1.0f, 0.53f).ToColor();
				case 1: return new ColorHSB(297/360f, 1.0f, 0.53f).ToColor();
				case 2: return new ColorHSB(254/360f, 1.0f, 0.53f).ToColor();
				case 3: return new ColorHSB(211/360f, 1.0f, 0.53f).ToColor();
				case 4: return new ColorHSB(183/360f, 1.0f, 0.53f).ToColor();
				default: return Color.red;
			}
		}
	}
	private bool AreAllMyButtonsPressed() {
		foreach (GateButton button in myLevel.gateButtons) {
			if (button==null || button.ChannelID!=channelID) { continue; }
			if (!button.IsPressed) { return false; }
		}
		return true;
	}


	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public GateChannel(Level myLevel, int channelID) {
		this.myLevel = myLevel;
		this.channelID = channelID;
	}

	public void Reset() {
		bool areAllButtonsPressed = AreAllMyButtonsPressed();
		foreach (Gate gate in myLevel.gates) {
			if (gate==null || gate.ChannelID!=channelID) { continue; }
			gate.SetIsOn(!areAllButtonsPressed);
		}
	}


	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	public void OnButtonPressed() {
		bool areAllButtonsPressed = AreAllMyButtonsPressed();
		if (areAllButtonsPressed) {
			foreach (Gate gate in myLevel.gates) {
				if (gate==null || gate.ChannelID!=channelID) { continue; }
				gate.UnlockMe();
			}
		}
	}



}
