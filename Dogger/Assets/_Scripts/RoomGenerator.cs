using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomGenerator : MonoBehaviour {

	public Sprite[] rooms;
	public int localId;

	public static int lastImage;

	// Use this for initialization
	void Start () {
		GenerateRoom ();
	}

	void GenerateRoom () {
		
		int spriteIndex = Random.Range (0, rooms.Length);

		if (spriteIndex == RoomGenerator.lastImage) {
			if (spriteIndex <= 0)
				spriteIndex++;
			else
				spriteIndex--;
		}

		gameObject.GetComponentInChildren <Image> ().sprite = rooms [spriteIndex];

		RoomGenerator.lastImage = spriteIndex;

		GameManager.instance.roomList.Add (gameObject.GetComponent<RoomGenerator>());
	}
}
