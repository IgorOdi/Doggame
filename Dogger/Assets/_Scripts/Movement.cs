using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkSpeed = 1;
	public static Transform corridorToMove;
	public Transform startCorridor;
	public Transform startRoom;

	public RectTransform[] corridorsT;

	Animator[] an;
	Transform cameraT;
	RectTransform rectT;

	float camMax;
	float camMin;

	Vector3 lastPos;

	bool reachedMaxPos;
	bool reachedMinPos;
	public static bool canMove = true;

	GameObject[] corridors;
	RectTransform HUD;

	// Use this for initialization
	void Start () {
		corridorToMove = startCorridor;
		GameManager.instance.corridorsT = new RectTransform[corridorsT.Length];
		GameManager.instance.corridorsT = corridorsT;

		an = GetComponentsInChildren<Animator>();
		rectT = corridorToMove.gameObject.GetComponent<RectTransform> ();
		cameraT = Camera.main.transform;
		camMax = (((corridorToMove.gameObject.GetComponent<CorridorGenerator> ().numberOfWalls + 1) * 720) - 240) * -1;
		camMin = 0;

		GameManager.instance.currentCorridor = 0;

		corridors = new GameObject[GameManager.instance.corridorList.Count];

		for (int i = 0; i < GameManager.instance.corridorList.Count; i++) {
			corridors [i] = GameManager.instance.corridorList [i].gameObject;
		}

		GameManager.instance.heroesT = transform;

		GameManager.instance.currentRoom = 0;

		HUD = GameObject.Find ("HUD").GetComponent<RectTransform> ();

		transform.position = startRoom.position;
	}

	// Update is called once per frame
	void Update () {
		lastPos = corridorToMove.position;
		rectT = corridorToMove.gameObject.GetComponent<RectTransform> ();
		camMax = (((corridorToMove.gameObject.GetComponent<CorridorGenerator> ().numberOfWalls + 1) * 720) - 240) * -1;
		print (GameManager.instance.currentLocation);

		if (GameManager.instance.currentLocation == "Corridor" && canMove) {

			if (rectT.localPosition.x <= camMax) {
				reachedMaxPos = true;
			} else if (rectT.anchoredPosition.x >= 0) {
				reachedMinPos = true;
			} else {
				reachedMaxPos = false;
				reachedMinPos = false;
			}

			//Se estiver clicando para andar
			if (Input.GetMouseButton (0)) {
				//Anda pra Direita
				if (Input.mousePosition.x >= Screen.width / 2) {
					if (!reachedMaxPos) {
						corridorToMove.position += Vector3.left * Time.deltaTime * walkSpeed;
						LevelManager.instance.deltaSpace += walkSpeed * Time.deltaTime;
					}
				}
			//Anda pra Esquerda
				else {
					if (!reachedMinPos) {
						corridorToMove.position += Vector3.right * Time.deltaTime * walkSpeed;
						LevelManager.instance.deltaSpace += walkSpeed * Time.deltaTime;
					}
				}

			}

			if (lastPos != corridorToMove.position) {
				for (int i = 0; i < an.Length; i++) {
					an [i].SetBool ("Walking", true);
				}
			} else {
				for (int i = 0; i < an.Length; i++) {
					an [i].SetBool ("Walking", false);
				}
			}
		} 

		else {
			reachedMaxPos = reachedMinPos = false;
		}
	}

	void LateUpdate() {
		cameraT.position = GameManager.instance.heroesT.position + Vector3.forward * -10;
		HUD.position = GameManager.instance.heroesT.position + Vector3.forward * 10;
	}
}
