using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {

	public new string name;
	public Stats stats;
	public Skill[] skillList;
	public Soundpack soundpack;
	public RuntimeAnimatorController animator;
}