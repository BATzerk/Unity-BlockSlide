using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PropData {
	public BoardPos boardPos;
}

public class PlayerData : PropData {
	public PlayerData(BoardPos boardPos) {
		this.boardPos = boardPos;
	}
}
