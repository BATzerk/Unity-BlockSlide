using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BoardObject {
	// Movement
	private const float MoveSpeed = 100f; // how fast we move.
	// Properties
	private bool isMoving;
	private int numMovesSinceGround;
	private int moveSide; // the SIDE we're moving in. Matches moveDir.
	private Vector2Int moveDir; // the DIRECTION we're moving in. Matches moveSide.
	private Vector2 size;
	// Components
	[SerializeField] private PlayerFeet myFeet=null;
	[SerializeField] private PlayerHat myHat=null;
	[SerializeField] private PlayerWhiskers myWhiskers=null;
	[SerializeField] private BoxCollider2D bodyCollider=null;
	[SerializeField] private Rigidbody2D myRigidbody=null;
	[SerializeField] private PlayerBody body=null;

	// Getters/Setters
	private Vector3 pos {
		get { return this.transform.localPosition; }
		set { this.transform.localPosition = value; }
	}
	private Vector2 vel {
		get { return myRigidbody.velocity; }
		set { myRigidbody.velocity = value; }
	}
	private bool CanMoveInDir(int side) { return CanMoveInDir(MathUtils.GetDir(side)); }
	private bool CanMoveInDir(Vector2Int dir) {
		if (isMoving) { return false; } // Can't move while moving.
		if (myWhiskers.IsTouchingGroundAtSide(dir)) { return false; } // Can't move in this direction.
		return true; // Ah, looks all good!
	}
	private Vector2 inputAxis { get { return InputController.Instance==null ? Vector2.zero : InputController.Instance.PlayerInput; } }

//	public bool IsDashing { get { return isMoving; } }
	public int MoveSide { get { return moveSide; } }
	public Vector2 Pos { get { return pos; } }
	public Vector2 Size { get { return size; } }


	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	public PlayerData SerializeAsData() {
		PlayerData data = new PlayerData (GetBoardPos());
		return data;
	}



	// ----------------------------------------------------------------
	//  Start
	// ----------------------------------------------------------------
	private void Start () {
		// Size me, queen!
		SetSize (new Vector2(3.9f,3.9f));//2.5f, 2.5f));

		ResetVel();
		SnapPosToGrid();
	}
	private void SetSize(Vector2 _size) {
		this.size = _size;
		body.SetSize(size);
		bodyCollider.size = size;
		myFeet.OnSetBodySize(size);
		myHat.OnSetBodySize(size);
	}


	// ----------------------------------------------------------------
	//  Resetting
	// ----------------------------------------------------------------
	public void ResetPos(Vector3 _pos) {
		pos = _pos;
		ResetVel();
	}
	private void ResetVel() {
		vel = Vector2.zero;
	}



	// ----------------------------------------------------------------
	//  Update
	// ----------------------------------------------------------------
	private void Update () {
		if (Time.timeScale == 0) { return; } // No time? No dice.

		AcceptDashInput();
	}
	private void AcceptDashInput() {
		// TEMP hardcoded
		if (Input.GetKeyDown(KeyCode.LeftArrow))  { TryToMove(Sides.L); }
		if (Input.GetKeyDown(KeyCode.RightArrow)) { TryToMove(Sides.R); }
		if (Input.GetKeyDown(KeyCode.DownArrow))  { TryToMove(Sides.B); }
		if (Input.GetKeyDown(KeyCode.UpArrow))    { TryToMove(Sides.T); }
	}

	private void FixedUpdate () {
		if (Time.timeScale == 0) { return; } // No time? No dice.


	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void TryToMove(int side) {
		// We're on the ground and NOT timed out of dashing! Go!
		if (CanMoveInDir(side)) {
			StartMove(side);
		}
	}
	private void StartMove(int side) {
		isMoving = true;
		moveSide = side;
		moveDir = MathUtils.GetDir(side);
		vel = moveDir.ToVector2() * MoveSpeed;
		body.OnStartMove();
		GameManagers.Instance.EventManager.OnPlayerDash(this);
	}
	private void EndMove() {
		isMoving = false;
		vel = Vector2.zero;
		SnapPosToGrid();
		GameManagers.Instance.EventManager.OnPlayerDashEnd(this);
//		if (myWhiskers.IsTouchingGround()) { // If I'm touching the ground at all, recharge my dash!
//			RechargeMoves();
//		}
		body.OnEndMove();
	}
	private void SnapPosToGrid() {
		float pu = GameProperties.UnitSize*0.5f;
		pos = new Vector3(Mathf.Round(pos.x/pu)*pu, Mathf.Round(pos.y/pu)*pu, pos.z);
	}

	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void OnTouchGround() {
		if (isMoving) {
			EndMove();
		}
	}


	private void OnCollisionEnter2D(Collision2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Ground) {
			OnTouchGround ();
		}
	}


}

/*
public class Player : MonoBehaviour {
	// Movement
	private const float MoveSpeed = 100f; // how fast we move.
//	public const int MaxMoves = 3; // How many times we can move until we have to touch the ground again.
//	private const float MoveDistance = GameProperties.UnitSize * 2; // we move 2 grid spaces.
	private readonly float MoveDuration = Mathf.Infinity;//No limit right now. DashDistance / DashForce;// / Physics2D.positionIterations;//QQQ 0.15f; // how long each dash lasts.
	// Properties
	private bool onGround;
	private bool isMoving;
//	private float timeWhenMoveEnd; // set to Time.time + DashDuration when we dash.
	private int numMovesSinceGround;
//	private Vector2 aimDir; // the direction we're facing. We dash in this direction.
//	private Vector2 dashDir; // the direction of the current dash. We keep going in this dir for the dash.
	private Vector2 size;
	// Components
	[SerializeField] private PlayerFeet myFeet=null;
	[SerializeField] private PlayerHat myHat=null;
	[SerializeField] private PlayerWhiskers myWhiskers=null;
	[SerializeField] private BoxCollider2D bodyCollider=null;
	[SerializeField] private Rigidbody2D myRigidbody=null;
	[SerializeField] private PlayerBody body=null;

	// Getters/Setters
	private Vector3 pos {
		get { return this.transform.localPosition; }
		set { this.transform.localPosition = value; }
	}
	private Vector2 vel {
		get { return myRigidbody.velocity; }
		set { myRigidbody.velocity = value; }
	}
//	private bool CanDash { get { return numDashesSinceGround<MaxDashes && Time.time>=timeWhenCanDash; } }
//	private bool CanMove() {
//	}
	private bool CanMoveInDir(int side) { return CanMoveInDir(MathUtils.GetDir(side)); }
	private bool CanMoveInDir(Vector2 dir) {
		if (isMoving) { return false; } // Can't move while moving.
//		if (numMovesSinceGround>=MaxMoves) { return false; } // Max dashes before we have to recharge.
		if (myWhiskers.IsTouchingGroundAtSide(dir)) { return false; } // Can't move in this direction.
		return true; // Ah, looks all good!
	}
	private bool DidDash { get { return numMovesSinceGround > 0; } }
	private Vector2 inputAxis { get { return InputController.Instance==null ? Vector2.zero : InputController.Instance.PlayerInput; } }

	public bool IsDashing { get { return isMoving; } }
	public int NumMovesSinceGround { get { return numMovesSinceGround; } }
//	public Vector2 DashDir { get { return dashDir; } }
	public Vector2 Pos { get { return pos; } }
	public Vector2 Size { get { return size; } }



	// ----------------------------------------------------------------
	//  Start
	// ----------------------------------------------------------------
	private void Start () {
		// Size me, queen!
		SetSize (new Vector2(3.9f,3.9f));//2.5f, 2.5f));

		ResetVel();
		SnapPosToGrid();
	}
	private void SetSize(Vector2 _size) {
		this.size = _size;
		body.SetSize(size);
		bodyCollider.size = size;
		myFeet.OnSetBodySize(size);
		myHat.OnSetBodySize(size);
	}


	// ----------------------------------------------------------------
	//  Resetting
	// ----------------------------------------------------------------
	public void ResetPos(Vector3 _pos) {
		pos = _pos;
		ResetVel();
	}
	private void ResetVel() {
		vel = Vector2.zero;
		aimDir = Vector2Int.R.ToVector2();
	}



	// ----------------------------------------------------------------
	//  Update
	// ----------------------------------------------------------------
	private void Update () {
		if (Time.timeScale == 0) { return; } // No time? No dice.

		AcceptDashInput();
	}
	private void AcceptDashInput() {
		// TEMP hardcoded
		if (Input.GetKeyDown(KeyCode.LeftArrow))  { TryToDash(Sides.L); }
		if (Input.GetKeyDown(KeyCode.RightArrow)) { TryToDash(Sides.R); }
		if (Input.GetKeyDown(KeyCode.DownArrow))  { TryToDash(Sides.B); }
		if (Input.GetKeyDown(KeyCode.UpArrow))    { TryToDash(Sides.T); }
	}

	private void FixedUpdate () {
		if (Time.timeScale == 0) { return; } // No time? No dice.

//		UpdateMove();
	}


//	private void UpdateMove() {
////		// End the Dash?DISABLED duration!
////		if (isDashing && Time.time>=timeWhenDashEnd) {
////			EndDash();
////		}
//	}

	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void StartMove(int side) { StartMove(MathUtils.GetDir(side)); }
	private void StartMove(Vector2 dir) {
		isMoving = true;
		vel = dir * MoveSpeed;
//		timeWhenCanDash = Time.time + DashCooldown;
//		timeWhenMoveEnd = Time.time + DashDuration;
		numMovesSinceGround ++;
		body.OnStartMove();
		GameManagers.Instance.EventManager.OnPlayerDash(this);
	}
	private void EndMove() {
		isMoving = false;
		vel = Vector2.zero;
		SnapPosToGrid();
		GameManagers.Instance.EventManager.OnPlayerDashEnd(this);
		if (myWhiskers.IsTouchingGround()) { // If I'm touching the ground at all, recharge my dash!
			RechargeMoves();
		}
		body.OnDashEnd();
	}
	private void SnapPosToGrid() {
		float pu = GameProperties.UnitSize*0.5f;
		pos = new Vector3(Mathf.Round(pos.x/pu)*pu, Mathf.Round(pos.y/pu)*pu, pos.z);
	}
	private void RechargeMoves() {
		numMovesSinceGround = 0;
		body.OnRechargeDash();
	}

	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void TryToDash(int side) {
		// We're on the ground and NOT timed out of dashing! Go!
		if (CanMoveInDir(side)) {
			StartMove(side);
		}
	}
	private void OnTouchGround() {
		onGround = true;
		if (isMoving) {
			EndMove();
		}
		RechargeMoves();
	}
	private void OnLeaveGround() {
		onGround = false;
	}
	/** We also do a constant check, which is way more reliable. * /
	private void OnTouchingGround() {
		onGround = true;
	}


	private void OnCollisionEnter2D(Collision2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Ground) {
			OnTouchGround ();
		}
	}
	private void OnCollisionExit2D(Collision2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Ground) {
			OnLeaveGround ();
		}
	}
	private void OnCollisionStay2D(Collision2D otherCol) {
		// Ground??
		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Ground) {
			OnTouchingGround ();
		}
	}


}
*/
