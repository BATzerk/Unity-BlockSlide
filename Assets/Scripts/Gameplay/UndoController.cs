using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoController : MonoBehaviour {
	// Properties
	private int currentSnapshotIndex=0;
	private List<LevelData> snapshots;
	// References
	[SerializeField] private GameController gameController;

	// Getters
	private Level level { get { return gameController.Level; } }


	// ----------------------------------------------------------------
	//  Start / Destroy
	// ----------------------------------------------------------------
	private void Start () {
		// Add event listeners!
		GameManagers.Instance.EventManager.PlayerMoveEndEvent += OnPlayerMoveEnd;
	}
	private void OnDestroy() {
		// Remove event listeners!
		GameManagers.Instance.EventManager.PlayerMoveEndEvent -= OnPlayerMoveEnd;
	}

	public void Reset() {
		// Take our first snapshot!
		snapshots = new List<LevelData>();
		AddSnapshot();
	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void AddSnapshot() {
		LevelData snapshot = level.SerializeAsData();
		snapshots.Add(snapshot);
		currentSnapshotIndex = snapshots.Count-1; // whenever we add a snapshot, always set this to the highest it can be.
	}
	public void Undo() {
		if (currentSnapshotIndex > 0) {
			currentSnapshotIndex --;
			LevelData snapshot = snapshots[currentSnapshotIndex];
			gameController.InitializeLevelFromData(snapshot);
		}
	}
	public void Redo() {
		if (currentSnapshotIndex < snapshots.Count-1) {
			currentSnapshotIndex ++;
			LevelData snapshot = snapshots[currentSnapshotIndex];
			gameController.InitializeLevelFromData(snapshot);
		}
	}


	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void OnPlayerMoveEnd(Player player) {
		// Overwrite any snapshots beyond our currentSnapshotIndex.
		int numToRemove = snapshots.Count - currentSnapshotIndex - 1;
		if (numToRemove > 0) {
			snapshots.RemoveRange (currentSnapshotIndex+1, numToRemove);
		}
		AddSnapshot();
	}




}


