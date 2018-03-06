using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class Enemy : ScriptableObject {

	public new string name;
	public Stats stats;
}
