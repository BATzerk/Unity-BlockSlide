using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	// Properties
	private List<LevelData> snapshots;
	// Components
	public Player player;
	public List<Ground> grounds;

	// Getters
	private ResourcesHandler resourcesHandler { get { return ResourcesHandler.Instance; } }


	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public LevelData SerializeAsData() {
		return new LevelData(this);
	}


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
	private void ClearPropLists() {
		player = null;
		grounds = new List<Ground>();
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

		// Take our first snapshot!
		snapshots = new List<LevelData>();
		AddSnapshot();
	}
	private void AddPropFromData(PropData data) {
		if (data is GroundData) {
			Ground prop = Instantiate(resourcesHandler.ground).GetComponent<Ground>();
			prop.Initialize(this, data as GroundData);
			grounds.Add(prop);
		}
		else if (data is PlayerData) {
			Player prop = Instantiate(resourcesHandler.player).GetComponent<Player>();
			prop.Initialize(this, data as PlayerData);
			player = prop;
		}
	}
	public void FindAllMyPropReferences() {
		player = GetComponentInChildren<Player>();
		grounds = new List<Ground>(GetComponentsInChildren<Ground>());
	}



	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void AddSnapshot() {
		LevelData snapshot = SerializeAsData();
		snapshots.Add(snapshot);
	}





}
