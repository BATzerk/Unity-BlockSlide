﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {
	// Properties
//	public GateChannelData[] gateChannelDatas;
	public List<PropData> propDatas;
//	private PlayerData playerData;
//	public List<GateData> gateDatas;
//	public List<GateButtonData> gatebuttonDatas;
//	public List<ToggleGroundData> toggleGroundDatas;

	public LevelData() {
		propDatas = new List<PropData>();
//		gateChannelDatas = new GateChannelData[]
	}
//	public LevelData(Level l) {
//		propDatas = new List<PropData>();
//
//		if (l.player != null) { propDatas.Add(l.player.SerializeAsData()); }
//		foreach (Ground p in l.grounds) { propDatas.Add(p.SerializeAsData()); }
//	}
}
