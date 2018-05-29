using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {
	// Properties
	public List<PropData> propDatas;
//	private PlayerData playerData;
//	public List<GateData> gateDatas;
//	public List<GateButtonData> gatebuttonDatas;
//	public List<ToggleGroundData> toggleGroundDatas;

	public LevelData(GameObject levelGO) {
		propDatas = new List<PropData>();

		Player player = levelGO.GetComponentInChildren<Player>();
		if (player != null) { propDatas.Add(player.SerializeAsData()); }

		Gate[] gates = levelGO.GetComponentInChildren<Gate>();
		if (gates.Length > 0) {
			foreach (Gate p in gates) { propDatas.Add(p.SerializeAsData()); }
		}

		GateButton[] gateButtons = levelGO.GetComponentInChildren<GateButton>();
		if (gateButtons.Length > 0) {
			foreach (GateButton p in gateButtons) { propDatas.Add(p.SerializeAsData()); }
		}

		ToggleGround[] toggleGrounds = levelGO.GetComponentInChildren<ToggleGround>();
		if (toggleGrounds.Length > 0) {
			foreach (ToggleGround p in toggleGrounds) { propDatas.Add(p.SerializeAsData()); }
		}
	}
}
