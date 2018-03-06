using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public int currentCorridor;
	public int currentWall;
	public int currentRoom;
	public int nextRoom;


	public Transform heroesT;
	Vector3 screenCenter;


	public Camera cam;


	public static GameManager instance = null;

	// Use this for initialization
	void Start () {
		if (instance == null) instance = this;

		else if (instance != this) Destroy(gameObject);  

		DontDestroyOnLoad(gameObject);

		heroesT = GameObject.Find ("Heroes").transform;
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHeroesPosition ();
	}

	void UpdateHeroesPosition() {
		screenCenter = cam.ScreenToWorldPoint (new Vector3 (Screen.width/2, Screen.height/2, 0));

		RaycastHit hit;
		if (Physics.Raycast (screenCenter, Vector3.forward * 25f, out hit, 25f)) {
			string[] hitWallName = hit.transform.name.Split (new string[] { "_" }, System.StringSplitOptions.None);
			currentWall = int.Parse (hitWallName [1]);
			print (currentWall);
		}

		Debug.DrawRay (screenCenter, Vector3.forward * 25f);
	}
}
