using UnityEngine;
using System.Collections;
using System;

public interface IUIManager {	
	bool IsShiftDown {
		get;
		set;
	}
	
	bool IsControlDown {
		get;
		set;
	}
	
	Mode CurrentMode {
		get;
	}
	
	bool IsCurrentUnit(RTSObject obj);
	
	void LeftButton_SingleClickDown(MouseEventArgs e);
	void LeftButton_DoubleClickDown(MouseEventArgs e);
	void LeftButton_SingleClickUp(MouseEventArgs e);
	
	void RightButton_SingleClick(MouseEventArgs e);
	void RightButton_DoubleClick(MouseEventArgs e);
		
	void MiddleButton_SingleClick(MouseEventArgs e);
	void MiddleButton_DoubleClick(MouseEventArgs e);
	
	void UserPlacingBuilding(Item item, Action callbackFunction);
	
	void SwitchMode(Mode mode);
}
