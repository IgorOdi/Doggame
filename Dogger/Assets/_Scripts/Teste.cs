using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teste : MonoBehaviour {

	public Sprite[] walls;
	public int numberOfWalls = 1;
	public GameObject wallPrefab;


	int lastImage;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < numberOfWalls; i++) {
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
			
	}

}
