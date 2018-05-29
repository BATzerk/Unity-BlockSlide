using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	// Properties
	private List<LevelData> snapshots;
	// References
//	private 


	// ----------------------------------------------------------------
	//  Start / Destroy
	// ----------------------------------------------------------------
	private void Start () {
		// Take our first snapshot!
		snapshots = new List<LevelData>();
		AddSnapshot();

//		// Add event listeners!
//		GameManagers.Instance.EventManager.PlayerMoveEndEvent += OnPlayerMoveEnd;
	}
	private void OnDestroy() {
//		// Remove event listeners!
//		GameManagers.Instance.EventManager.PlayerMoveEndEvent -= OnPlayerMoveEnd;
	}


	private void AddSnapshot() {
		LevelData snapshot = new LevelData(this.gameObject);
		snapshots.Add(snapshot);
	}


	public void InitializeFromData(LevelData data) {

	}



}
