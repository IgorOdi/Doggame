using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teste : MonoBehaviour {

	public Sprite[] walls;
	public Sprite door;
	public int numberOfWalls = 1;
	public GameObject wallPrefab;


	int lastImage;

	// Use this for initialization
	void Start () {

		for (int i = 1; i < numberOfWalls + 1; i++) {
			GameObject wall = Instantiate (wallPrefab, transform, false);
			wall.transform.localPosition = Vector3.right * i * 1080;
			wall.name = "Wall_" + i;

			int spriteIndex = Random.Range (0, walls.Length);

			if (spriteIndex == lastImage) {
				if (spriteIndex <= 0)
					spriteIndex++;
				else
					spriteIndex--;
			}

			wall.GetComponent <Image> ().sprite = walls [spriteIndex];

			lastImage = spriteIndex;
		}

		GameObject startDoor = Instantiate (wallPrefab, transform, false);
		startDoor.transform.localPosition = Vector3.zero;
		startDoor.name = "Start_Door";
		startDoor.GetComponent<Image>().sprite = door;

		GameObject endDoor = Instantiate (wallPrefab, transform, false);
		endDoor.transform.localPosition = Vector3.right * (numberOfWalls + 1) * 1080;
		endDoor.name = "End_Door";
		endDoor.GetComponent<Image>().sprite = door;
	}

}
