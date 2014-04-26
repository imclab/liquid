using UnityEngine;
using System.Collections;

public class tMG_ConstructionYard : Building {
	
	// Use this for initialization
	new void Start () 
	{
		//Assign all the details
		AssignDetails (ItemDB.MGConstructionYard);
		
		//Tell the base class to start as well, must be done after AssignDetails
		base.Start();
		
		//Tell Idle animation to play
		animation.Play ("idle");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}