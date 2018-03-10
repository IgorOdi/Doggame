using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int gold;


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
	public GameObject explorerHeroes;
	public GameObject battleHeroes;
	Vector3 screenCenter;

	string posName;

	public Camera cam;

	public static GameManager instance = null;

	public string currentLocation;

	public bool sceneLoaded;

	// Use this for initialization
	void OnEnable () {
		if (instance == null) instance = this;

		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);

		GameManager.instance.currentLocation = "";

		gold = 500;
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
		if (_nextRoom - 2 >= 0)
			GameManager.instance.currentCorridor = _nextRoom - 2;

		posName = GameManager.instance.currentRoom + "-" + GameManager.instance.nextRoom;

		for (int i = 0; i < GameManager.instance.corridorPositions.Length; i++) {
			if (GameManager.instance.corridorPositions[i].name == posName) {
				GameManager.instance.heroesT.position = GameManager.instance.corridorPositions [i].position;
			}
		}

	}

	public void FadeToBattle() {

		Movement.canMove = false;
		explorerHeroes.SetActive (false);
		battleHeroes.SetActive (true);
	}

	public IEnumerator FadeOffBattle() {

		float startTime = Time.time;
		while (Time.time < startTime + 2f)
			yield return null;

		Movement.canMove = true;
		explorerHeroes.SetActive (true);
		battleHeroes.SetActive (false);
	}


	public void OnChangeScene(string sceneName) {
		sceneLoaded = false;
		if (sceneName != SceneManager.GetActiveScene().name && !sceneLoaded) {
			StartCoroutine (ChangeScene(sceneName));
		}
	}


	IEnumerator ChangeScene(string sceneName) {
		sceneLoaded = true;

		SceneManager.LoadScene ("Loading");

		yield return new WaitForSeconds (0.2f);

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

		while (!asyncLoad.isDone) {
			yield return null;
		}

		sceneLoaded = false;

	}
}
