using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarker : MonoBehaviour {

	public RectTransform[] roomPositions;
	public RectTransform[] corridorPositions;

	RectTransform rectT;

	// Use this for initialization
	void Start () {
		rectT = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.currentLocation == "Room") {
			rectT.anchoredPosition = roomPositions [GameManager.instance.currentRoom - 1].anchoredPosition;
		} else if (GameManager.instance.currentLocation == "Corridor") {
			print ("Current corridor: " + GameManager.instance.currentCorridor);
			rectT.anchoredPosition = corridorPositions [GameManager.instance.currentCorridor].anchoredPosition;
		}
	}


	public void ChooseRoom(int roomNum) {
		if (GameManager.instance.currentLocation == "Room") {
			if (GameManager.instance.currentRoom == 1 && (roomNum == 2 || roomNum == 3 || roomNum == 4)) {
				GameManager.instance.GoToCorridor (roomNum);
			} else if ((GameManager.instance.currentRoom == 2 || GameManager.instance.currentRoom == 3 || GameManager.instance.currentRoom == 4) && roomNum == 1) {
				GameManager.instance.GoToCorridor (roomNum);
			} else if (GameManager.instance.currentRoom == 4 && ((roomNum == 1 || roomNum == 5))) {
				GameManager.instance.GoToCorridor (roomNum);
			} else if (GameManager.instance.currentRoom == 5 && ((roomNum == 4 || roomNum == 6))) {
				GameManager.instance.GoToCorridor (roomNum);
			} else if (GameManager.instance.currentRoom == 6 && roomNum == 5) {
				GameManager.instance.GoToCorridor (roomNum);
			}
		}
	}

}
