using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BoardPos {
	int _col;
	int _row;
	int _sideFacing; // 0 top, 1 right, 2 bottom, 3 left

	public int col { get { return _col; } }
	public int row { get { return _row; } }
	public int sideFacing {
		get { return _sideFacing; }
		set {
			_sideFacing = value;
			if (_sideFacing<0) { _sideFacing += 4; }
			if (_sideFacing>=4) { _sideFacing -= 4; }
		}
	}

	public BoardPos (int Col,int Row, int SideFacing) {
		this._col = Col;
		this._row = Row;
		this._sideFacing = SideFacing;
	}

	public override bool Equals(object o) { return base.Equals (o); } // NOTE: Just added these to appease compiler warnings. I don't suggest their usage (because idk what they even do).
	public override int GetHashCode() { return base.GetHashCode(); } // NOTE: Just added these to appease compiler warnings. I don't suggest their usage (because idk what they even do).

	public static bool operator == (BoardPos b1, BoardPos b2) {
		return b1.Equals(b2);
	}
	public static bool operator != (BoardPos b1, BoardPos b2) {
		return !b1.Equals(b2);
	}

}
