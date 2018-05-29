using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ToggleGround : Ground {
	// Components
	[SerializeField] private SpriteRenderer sr_fill=null;
	[SerializeField] private BoxCollider2D myCollider=null;
	// Properties
	[SerializeField] private bool startsOn=false;
	private bool isOn;
	private Color bodyColorOn, bodyColorOff;



	// ----------------------------------------------------------------
	//  Serialize
	// ----------------------------------------------------------------
	new public ToggleGroundData SerializeAsData() {
		ToggleGroundData data = new ToggleGroundData (pos, Size, startsOn, isOn);
		return data;
	}

	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	public void Initialize(Level myLevel, ToggleGroundData data) {
		BaseInitialize(myLevel, data);
		Size = data.size;
		SetStartsOn (data.startsOn);
		SetIsOn(data.isOn);
	}
	// ----------------------------------------------------------------
	//  Start / Destroy
	// ----------------------------------------------------------------
	private void Start () {
		SetIsOn(startsOn);

		// Add event listeners!
		GameManagers.Instance.EventManager.PlayerMoveEndEvent += OnPlayerMoveEnd;
	}
	private void OnDestroy() {
		// Remove event listeners!
		GameManagers.Instance.EventManager.PlayerMoveEndEvent -= OnPlayerMoveEnd;
	}

	private void SetStartsOn(bool startsOn) {
		this.startsOn = startsOn;
		bodyColorOn = startsOn ? new Color(3/255f, 170/255f, 204/255f) : new Color(217/255f, 74/255f, 136/255f);
		bodyColorOff = new Color(bodyColorOn.r,bodyColorOn.g,bodyColorOn.b, bodyColorOn.a*0.14f);
	}


	// ----------------------------------------------------------------
	//  Events
	// ----------------------------------------------------------------
	private void OnPlayerMoveEnd(Player player) {
		ToggleIsOn();
	}
//	private void OnTriggerExit2D(Collider2D otherCol) {
//		// Ground??
//		if (LayerMask.LayerToName(otherCol.gameObject.layer) == LayerNames.Player) {
//			myPlayer.OnFeetLeaveGround ();
//		}
//	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	private void ToggleIsOn() {
		SetIsOn (!isOn);
	}
	private void SetIsOn(bool _isOn) {
		isOn = _isOn;
		myCollider.isTrigger = !isOn;
		sr_fill.color = isOn ? bodyColorOn : bodyColorOff;
	}




}
