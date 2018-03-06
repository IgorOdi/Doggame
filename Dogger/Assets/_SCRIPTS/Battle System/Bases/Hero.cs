using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName ="Heroes")]
public class Hero : ScriptableObject {

	public new string name;
	public Stats stats;
}
