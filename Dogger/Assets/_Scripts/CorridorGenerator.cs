using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorridorGenerator : MonoBehaviour {

	public Sprite[] walls;
	public Sprite door;
	public int numberOfWalls = 1;
	public GameObject wallPrefab;
	public GameObject doorPrefab;

	public int localId;

	int lastImage;

	// Use this for initialization
	void Start () {
		//GenerateCorridor ();
	}

	void GenerateCorridor () {
		print (localId + "  " + gameObject.name);
		Corridor corridor = new Corridor(numberOfWalls, walls, door, wallPrefab, gameObject);
		GameManager.instance.corridorList.Add (corridor);
		corridor.id = localId;

		for (int i = 1; i < numberOfWalls + 1; i++) {
			GameObject wall = Instantiate (wallPrefab, transform, false);
			wall.transform.localPosition = Vector3.right * i * 720;
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

		GameObject startDoor = Instantiate (doorPrefab, transform, false);
		startDoor.transform.localPosition = Vector3.zero;
		startDoor.name = "Wall_0";
		startDoor.GetComponent<Image>().sprite = door;

		GameObject endDoor = Instantiate (doorPrefab, transform, false);
		endDoor.transform.localPosition = Vector3.right * (numberOfWalls + 1) * 720;
		endDoor.name = "Wall_" + (numberOfWalls + 1);
		endDoor.GetComponent<Image>().sprite = door;
	}

}

public class Corridor {
	
	public int numberOfWalls = 1;
	public Sprite[] walls;
	public Sprite door;
	public GameObject wallPrefab;
	public GameObject gameObject;

	public int id = 0;

	public Corridor(int _numberOfWalls, Sprite[] _walls, Sprite _door, GameObject _wallPrefab, GameObject _gameObject) {
		numberOfWalls = _numberOfWalls;
		walls = _walls;
		door = _door;
		wallPrefab = _wallPrefab;
		gameObject = _gameObject;
	}
}
