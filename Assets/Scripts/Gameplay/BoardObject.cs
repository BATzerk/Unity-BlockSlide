using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BoardObject : MonoBehaviour {
	// References
	protected Level myLevel;

	// Getters
	protected float UnitSize { get { return GameProperties.UnitSize; } }
	protected Vector3 pos {
		get { return this.transform.localPosition; }
		set { this.transform.localPosition = value; }
	}
	protected float rotation {
		get { return this.transform.localEulerAngles.z; }
		set { this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, value); }
	}


	public Vector2 Pos { get { return pos; } }
	public BoardPos GetBoardPos() {
		float pu = GameProperties.UnitSize;
		int col = Mathf.RoundToInt(pos.x/pu);
		int row = Mathf.RoundToInt(pos.y/pu);
		int sideFacing = Mathf.RoundToInt(rotation/90f);
		return new BoardPos(col,row, sideFacing);
	}

//	abstract public PropData SerializeAsData();


	// ----------------------------------------------------------------
	//  Initialize
	// ----------------------------------------------------------------
	protected void BaseInitialize(Level myLevel, PropData data) {
		this.myLevel = myLevel;

		// Parent jazz!
		this.transform.SetParent(myLevel.transform);
		this.transform.localPosition = Vector3.zero;
		this.transform.localScale = Vector3.one;
		this.transform.localEulerAngles = Vector3.zero;

		// Position!
//		SetPosFromBoardPos(data.boardPos);
		pos = data.pos;
	}


	// ----------------------------------------------------------------
	//  Doers
	// ----------------------------------------------------------------
	protected void SetPosFromBoardPos(BoardPos boardPos) {
		pos = new Vector3(boardPos.col*UnitSize, boardPos.row*UnitSize);
	}

}

