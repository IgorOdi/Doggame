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
			print (GameManager.instance.currentCorridor);
			rectT.anchoredPosition = corridorPositions [GameManager.instance.currentCorridor].anchoredPosition;
		}
	}

}
