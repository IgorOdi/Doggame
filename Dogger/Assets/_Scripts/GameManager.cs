using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	public int currentCorridor;
	public List<Corridor> corridorList = new List<Corridor>();
	public RectTransform[] corridorsT;
	public List<RoomGenerator> roomList = new List<RoomGenerator>();
	public int currentWall;
	public int currentRoom;
	public int nextRoom;


	public Transform heroesT;
	Vector3 screenCenter;


	public Camera cam;


	public static GameManager instance = null;

	public string currentLocation;

	// Use this for initialization
	void OnEnable () {
		if (instance == null) instance = this;

		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		GameManager.instance.currentLocation = "";

	}
	
	// Update is called once per frame
	void Update () {
		UpdateHeroesPosition ();
	}

	void UpdateHeroesPosition() {

		cam = Camera.main;
		screenCenter = cam.ScreenToWorldPoint (new Vector3 (Screen.width/4, Screen.height/2, 0));

		RaycastHit hit;
		if (Physics.Raycast (screenCenter, Vector3.forward * 25f, out hit, 25f)) {
			
			if (hit.transform.name.Contains("Wall")) {

				string[] hitWallName = hit.transform.name.Split (new string[] { "_" }, System.StringSplitOptions.None);
				currentWall = int.Parse (hitWallName [1]);
				GameManager.instance.currentLocation = "Corridor";
				Movement.corridorToMove = hit.transform.parent;
			
			} else if (hit.transform.name.Contains("Room")) {
				
				GameManager.instance.currentLocation = "Room";

				for (int i = 0; i < corridorsT.Length; i++) {
					corridorsT [i].anchoredPosition = new Vector2 (0, corridorsT [i].anchoredPosition.y);
				}

			} else {
				GameManager.instance.currentLocation = "";
			}
		}

		Debug.DrawRay (screenCenter, Vector3.forward * 25f);
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void GoToRoom (Transform roomPosition) {
		GameManager.instance.heroesT.position = roomPosition.position;
	}

	public void GoToCorridor (Transform corridorPos) {
		GameManager.instance.heroesT.position = corridorPos.position;
	}
}
