using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selected))]
public class Unit : RTSObject, IOrderable {
	
	//Member Variables
	protected bool m_IsMoveable = true;
	
	protected bool m_IsDeployable = false;
	protected bool m_IsAttackable = true;
	protected bool m_IsInteractable = false;
	
	protected IGUIManager guiManager {
		get;
		private set;
	}
	
	protected ISelectedManager selectedManager {
		get;
		private set;
	}
	
	protected void Start() {
		guiManager = ManagerResolver.Resolve<IGUIManager>();
		selectedManager = ManagerResolver.Resolve<ISelectedManager>();
		
		m_IsDeployable = this is IDeployable;
		m_IsAttackable = this is IAttackable;
		m_IsInteractable = this is IInteractable;
	}
	
	protected void Update() {
		if (renderer.isVisible && guiManager.Dragging) {
			if (guiManager.IsWithin (transform.position)) {
				selectedManager.AddObject(this);
			}
			else {
				selectedManager.DeselectObject (this);				
			}
		}
	}
		
	public override void SetSelected () {
		if (!GetComponent<Selected>().IsSelected) {
			GetComponent<Selected>().SetSelected ();
		}
	}

	public override void SetDeselected () {
		GetComponent<Selected>().SetDeselected ();
	}

	public override void AssignToGroup (int groupNumber) {
		GetComponent<Selected>().AssignGroupNumber (groupNumber);
	}

	public override void RemoveFromGroup () {
		GetComponent<Selected>().RemoveGroupNumber ();
	}
	
	public override void ChangeTeams(int team) {
		switch (team) {
		case Const.TEAM_A:
			break;
			
		case Const.TEAM_B:
			break;
		}
	}

	public bool IsDeployable () {
		return this is IDeployable;
	}

	public bool IsAttackable () {
		return m_IsAttackable;
	}

	public bool IsMoveable () {
		return m_IsMoveable;
	}

	public bool IsInteractable () {
		return m_IsInteractable;
	}

	public void GiveOrder (Order order) {
		switch (order.OrderType) {
		
		case Const.ORDER_STOP:
			if (IsMoveable()) {
				if (IsDeployable ()) {
					CancelDeploy ();
				}
				GetComponent<Movement>().Stop ();
			}
			break;

		case Const.ORDER_MOVE_TO:
			if (IsMoveable()) {
				if (IsDeployable ()) {
					CancelDeploy ();
				}
				GetComponent<Movement>().MoveTo (order.OrderLocation);
			}
			break;
			
		case Const.ORDER_DEPLOY:
			GetComponent<Movement>().Stop ();
			((IDeployable)this).Deploy();
			break;
		}
	}

	public bool ShouldInteract (HoverOver hoveringOver) {
		switch (hoveringOver) {
		case HoverOver.Land:
			return m_IsMoveable;
			
		case HoverOver.EnemyBuilding:
		case HoverOver.EnemyUnit:
			return m_IsAttackable;
			
		case HoverOver.FriendlyUnit:
			return m_IsDeployable && ManagerResolver.Resolve<IUIManager>().IsCurrentUnit (this);
			
		default:
			return false;
		}
	}
	
	private void CancelDeploy() {
		((IDeployable)this).StopDeploy ();
	}
	
	void OnDestroy() {
		//Remove object from selected manager
		selectedManager.DeselectObject(this);
	}
}
