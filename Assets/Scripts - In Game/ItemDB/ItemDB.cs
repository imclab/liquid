using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ItemDB {
	
	private static GameObject m_SmallExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_MediumExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_LargeExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_GiantExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	
	private static List<Item> AllItems = new List<Item>();

	// Examples
	// Vehicle
	public static Item MGStandardTank = new Item {
		ID = 0,
		TypeIdentifier = Const.TYPE_Vehicle,
		TeamIdentifier = Const.TEAM_A,
		Name = "Tank",
		Health = 100.0f,
		Armour = 3.0f,
		Damage = 30.0f,
		Speed = 10.0f,
		RotationSpeed = 80.0f,
		Acceleration = 2.0f,
		Explosion = m_SmallExplosion,
		// Remember to change the resources to reflect new directory
		Prefab = Resources.Load ("Models/MG/Units/Vehicles/Standard Tank/StandardTank", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/MG/Units/Vehicles/Standard Tank/StandardTank", typeof(Texture2D)) as Texture2D,
		SortOrder = 0,
		RequiredBuildings = new int[] { 40 },
		Cost = 700,
		BuildTime = 10.0f,
	};
	
	// Building
	
	public static Item MGConstructionYard = new Item {
		ID = 0,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_A,
		Name = "Construction Yard",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		// Remember to change the resources to reflect new directory
		Prefab = Resources.Load ("Models/MG/Buildings/Construction Yard/test", typeof(GameObject)) as GameObject,
		//ItemImage = Resources.Load ("", typeof(Texture2D)) as Texture2D,
		SortOrder = 100,
		RequiredBuildings = new int[] { 7, 6, 100 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(tMG_ConstructionYard),
	};

	// Support
	public static Item MGSupportGun = new Item {
		ID = 30,
		TypeIdentifier = Const.TYPE_Support,
		TeamIdentifier = Const.TEAM_A,
		Name = "Rifle Turret",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		// Remember to change the resources to reflect new directory
		Prefab = Resources.Load ("Models/MG/Buildings/Support/Gun/GunReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/MG/Buildings/Support/Gun/Gun", typeof(Texture2D)) as Texture2D,
		SortOrder = 1,
		RequiredBuildings = new int[] { 1 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(tMG_SupportGun),
	};
	
	public static void Initialise() {
		InitialiseItem (MGStandardTank);
		InitialiseItem (MGConstructionYard);
		InitialiseItem (MGSupportGun);
	}
	
	private static void InitialiseItem(Item item) {
		item.Initialise ();
		AllItems.Add (item);
	}
	
	public static List<Item> GetAvailableItems(int ID, List<Building> CurrentBuildings) {
		List<Item> valueToReturn = AllItems.FindAll(x => {
			if (x.RequiredBuildings.Length == 1) {
				if (x.RequiredBuildings[0] == ID) {
					return true;
				}
			}
			else {
				bool otherBuildingsPresent = true;
				//Does this item require the added building ID?
				if (x.RequiredBuildings.Contains (ID)) {
					//If so do we have the other required ID's?
					foreach (int id in x.RequiredBuildings) {
						if (id != ID && CurrentBuildings.FirstOrDefault(building => building.ID == id) == null) {
							otherBuildingsPresent = false;
							break;
						}
					}
				}
				else {
					otherBuildingsPresent = false;
				}
				
				return otherBuildingsPresent;
			}
			return false;
		});
		return valueToReturn;
	}
}
