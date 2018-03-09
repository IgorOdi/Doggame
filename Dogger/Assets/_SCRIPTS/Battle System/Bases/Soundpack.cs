using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soundpack", menuName = "Soundpack")]
public class Soundpack : ScriptableObject {

	public AudioClip[] attackSound;
	public AudioClip[] screamSound;
	public AudioClip[] dieSound;
}
