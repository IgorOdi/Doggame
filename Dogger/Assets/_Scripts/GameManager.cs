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


	public Transform[] roomPositions;
	public Transform[] corridorPositions;

	public Transform heroesT;
	public GameObject explroerHeroes;
	public GameObject battleHeroes;
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

	void Start () {
		roomPositions = GameObject.Find ("Room Positions").transform.GetComponentsInChildren<Transform> ();
		corridorPositions = GameObject.Find ("Corridor Positions").transform.GetComponentsInChildren<Transform> ();

		nextRoom = 2;
	}

	// Update is called once per frame
	void Update () {
		UpdateHeroesPosition ();
		print (currentRoom + "-" + nextRoom);


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

				string[] hitRoomName = hit.transform.parent.name.Split (new string[] { "_" }, System.StringSplitOptions.None);
				currentRoom = int.Parse (hitRoomName [1]);

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

	public void GoToRoom (GameObject obj) {
		if (obj.name == "StartDoor") {
			GameManager.instance.heroesT.position = GameManager.instance.roomPositions [GameManager.instance.currentRoom].position;
			print("Going to  " + GameManager.instance.currentRoom);
		} else if (obj.name == "EndDoor") {
			GameManager.instance.heroesT.position = GameManager.instance.roomPositions [GameManager.instance.nextRoom].position;
			print("Going to  " + GameManager.instance.nextRoom);
		}
	}

	public void GoToCorridor (int _nextRoom) {

		Vector3 newPos = Vector3.right * 50;

		GameManager.instance.nextRoom = _nextRoom;

		string posName = GameManager.instance.currentRoom + "-" + GameManager.instance.nextRoom;

		for (int i = 0; i < GameManager.instance.corridorPositions.Length; i++) {
			print (GameManager.instance.corridorPositions [i].name);
			if (GameManager.instance.corridorPositions[i].name == posName) {
				GameManager.instance.heroesT.position = GameManager.instance.corridorPositions [i].position;
			}
		}
	}

	public void FadeToBattle() {

		Movement.canMove = false;
		explroerHeroes.SetActive (false);
		battleHeroes.SetActive (true);
	}

	public void FadeOffBattle() {

		Movement.canMove = true;
		explroerHeroes.SetActive (true);
		battleHeroes.SetActive (false);
	}
}
