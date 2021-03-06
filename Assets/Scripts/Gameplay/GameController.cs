﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	// Properties
	private bool isPaused = false;
	private bool debug_isSlowMo = false;
	// Components
	[SerializeField] private GameObject go_structure; // the physical level layout
	// References
	[SerializeField] private GameCameraController cameraController;
	[SerializeField] private Level level;
	[SerializeField] private UndoController undoController;

	// Getters / Setters
	public Level Level { get { return level; } }
	private InputController inputController { get { return InputController.Instance; } }



	// ----------------------------------------------------------------
	//  Start
	// ----------------------------------------------------------------
	private void Start () {
		// Start us off in this version by telling the Level to look at itself, and set its references from what's in it.
		level.FindAllMyPropReferences();
		Debug_SerializeAndReloadLevel();
	}


	// ----------------------------------------------------------------
	//  Doers - Loading Level
	// ----------------------------------------------------------------
	private void ReloadScene () { OpenScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); }
	private void OpenScene (string sceneName) { StartCoroutine (OpenSceneCoroutine (sceneName)); }
	private IEnumerator OpenSceneCoroutine (string sceneName) {
//		// First frame: Blur it up.
//		cameraController.DarkenScreenForSceneTransition ();
//		yield return null;

		// Second frame: Load up that business.
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);
		yield return null;
	}

	private void Debug_SerializeAndReloadLevel() {
		LevelData levelData = level.SerializeAsData();
		InitializeLevelFromData(levelData);
		undoController.Reset();
	}
	public void InitializeLevelFromData(LevelData levelData) {
		level.InitializeFromData(levelData);
		cameraController.Reset();
	}


	// ----------------------------------------------------------------
	//  Doers - Gameplay
	// ----------------------------------------------------------------
	private void TogglePause () {
		isPaused = !isPaused;
		UpdateTimeScale ();
	}
	private void UpdateTimeScale () {
		if (isPaused) { Time.timeScale = 0; }
		else if (debug_isSlowMo) { Time.timeScale = 0.2f; }
		else { Time.timeScale = 1; }
	}



	// ----------------------------------------------------------------
	//  Update
	// ----------------------------------------------------------------
	private void Update () {
		RegisterButtonInput ();
	}

	private void RegisterButtonInput () {
		bool isKey_alt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
		bool isKey_control = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
		bool isKey_shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

		// Game Flow
		if (Input.anyKeyDown) {
//			if (gameState==GameStates.PostGame && Time.unscaledTime>unscaledTimeSinceGameEnded+2f) { // 2 second delay to start again.
//				ReloadScene();
//			}
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause();
		}
		else if (Input.GetKeyDown(KeyCode.Z)) {
			undoController.Undo();
		}
		else if (Input.GetKeyDown(KeyCode.X)) {
			undoController.Redo();
		}

		// ~~~~ DEBUG ~~~~
		if (Input.GetKeyDown(KeyCode.Return)) {
			ReloadScene();
			return;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			Debug_SerializeAndReloadLevel();
			return;
		}
		else if (Input.GetKeyDown(KeyCode.T)) {
			debug_isSlowMo = !debug_isSlowMo;
			UpdateTimeScale();
		}

			
		// ALT + ___
		if (isKey_alt) {
			
		}
		// CONTROL + ___
		if (isKey_control) {
		}
		// SHIFT + ___
		if (isKey_shift) {
		}
	}







}






