using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	// Properties
	public GateChannel[] gateChannels;
	private List<LevelData> snapshots;
	// Components
	public  Player player;
	private List<Gem> gems;
	public  List<Gate> gates;
	private List<Ground> grounds;
	public  List<GateButton> gateButtons;
	private List<ToggleGround> toggleGrounds;

	// Getters
	private ResourcesHandler resourcesHandler { get { return ResourcesHandler.Instance; } }



	// ----------------------------------------------------------------
	//  Start / Destroy
	// ----------------------------------------------------------------
	private void Start () {
//		ClearPropLists();

//		// Add event listeners!
//		GameManagers.Instance.EventManager.PlayerMoveEndEvent += OnPlayerMoveEnd;
	}
	private void OnDestroy() {
//		// Remove event listeners!
//		GameManagers.Instance.EventManager.PlayerMoveEndEvent -= OnPlayerMoveEnd;
	}
	private void DestroyAllChildren() {
		foreach (Transform child in this.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}


	// ----------------------------------------------------------------
	//  Prop Lists Functions
	// ----------------------------------------------------------------
	public LevelData SerializeAsData() {
		LevelData ld = new LevelData();

		if (player != null) { ld.propDatas.Add(player.SerializeAsData()); }
		foreach (Gem p in gems) { ld.propDatas.Add(p.SerializeAsData()); }
		foreach (Gate p in gates) { ld.propDatas.Add(p.SerializeAsData()); }
		foreach (GateButton p in gateButtons) { ld.propDatas.Add(p.SerializeAsData()); }
		foreach (Ground p in grounds) { ld.propDatas.Add(p.SerializeAsData()); }
		foreach (ToggleGround p in toggleGrounds) { ld.propDatas.Add(p.SerializeAsData()); }

		return ld;
	}
	private void ClearPropLists() {
		gateChannels = new GateChannel[9];
		for (int i=0; i<gateChannels.Length; i++) { gateChannels[i] = new GateChannel(this, i); }
		player = null;
		gems = new List<Gem>();
		gates = new List<Gate>();
		grounds = new List<Ground>();
		gateButtons = new List<GateButton>();
		toggleGrounds = new List<ToggleGround>();
	}
	public void FindAllMyPropReferences() {
		ClearPropLists();

		BoardObject[] allObjects = GetComponentsInChildren<BoardObject>(); // First, get ONE list of ALL BoardObjects.
		// Now, go through the big list one by one, adding to each smaller list.
		foreach (BoardObject bo in allObjects) { // NOTE: The order of these checks matters!! Classes extend each other. (E.g. if we ask if it's a Ground before ToggleGround, we'll miss the ToggleGround check.)
			if (bo is Player) { player = bo as Player; }
			else if (bo is Gem) { gems.Add(bo as Gem); }
			else if (bo is Gate) { gates.Add(bo as Gate); }
			else if (bo is GateButton) { gateButtons.Add(bo as GateButton); }
			else if (bo is ToggleGround) { toggleGrounds.Add(bo as ToggleGround); }
			else if (bo is Ground) { grounds.Add(bo as Ground); }
		}
	}
	private void AddPropFromData(PropData data) {
		if (data is GateData) {
			Gate prop = Instantiate(resourcesHandler.gate).GetComponent<Gate>();
			prop.Initialize(this, data as GateData);
			gates.Add(prop);
		}
		else if (data is GateButtonData) {
			GateButton prop = Instantiate(resourcesHandler.gateButton).GetComponent<GateButton>();
			prop.Initialize(this, data as GateButtonData);
			gateButtons.Add(prop);
		}
		else if (data is GemData) {
			Gem prop = Instantiate(resourcesHandler.gem).GetComponent<Gem>();
			prop.Initialize(this, data as GemData);
			gems.Add(prop);
		}
		else if (data is GroundData) {
			Ground prop = Instantiate(resourcesHandler.ground).GetComponent<Ground>();
			prop.Initialize(this, data as GroundData);
			grounds.Add(prop);
		}
		else if (data is PlayerData) {
			Player prop = Instantiate(resourcesHandler.player).GetComponent<Player>();
			prop.Initialize(this, data as PlayerData);
			player = prop;
		}
		else if (data is ToggleGroundData) {
			ToggleGround prop = Instantiate(resourcesHandler.toggleGround).GetComponent<ToggleGround>();
			prop.Initialize(this, data as ToggleGroundData);
			toggleGrounds.Add(prop);
		}
		else {
			Debug.LogError("Unrecognized PropData type: " + data);
		}
	}


	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void InitializeFromData(LevelData levelData) {
		// Clear out my sinuses.
		DestroyAllChildren();
		ClearPropLists();

		// Add everything one at a time!
		foreach (PropData propData in levelData.propDatas) {
			AddPropFromData(propData);
		}
		// Reset channels (this'll update their references)!
		for (int i=0; i<gateChannels.Length; i++) { gateChannels[i].Reset(); }

		// Take our first snapshot!
		snapshots = new List<LevelData>();
		AddSnapshot();
	}



	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void AddSnapshot() {
		LevelData snapshot = SerializeAsData();
		snapshots.Add(snapshot);
	}





}
