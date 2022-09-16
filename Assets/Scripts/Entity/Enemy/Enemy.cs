using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	[System.Serializable]
	public enum SpecializedType {
		Unarmored,
		Armor,
		Shield
	}
	public SpecializedType armorType;


}

