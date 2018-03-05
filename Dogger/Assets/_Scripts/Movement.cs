using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkSpeed = 1;
	public Transform heroes;

	Animator[] an;
	Transform cameraT;

	float camMax;
	float camMin;

	bool reachedMaxPos;
	bool reachedMinPos;

	// Use this for initialization
	void Start () {
		an = heroes.GetComponentsInChildren<Animator>();
		cameraT = Camera.main.transform;
		camMax = (((GameObject.Find ("Walls").GetComponent<Teste> ().numberOfWalls + 1) * 1080) - 432) * -1;
		camMin = -432;
	}
	
	// Update is called once per frame
	void Update () {

		if  (gameObject.GetComponent<RectTransform>().localPosition.x <= camMax) {
			reachedMaxPos = true;
		} else if (gameObject.GetComponent<RectTransform>().localPosition.x >= camMin) {
			reachedMinPos = true;
		} else {
			reachedMaxPos = false;
			reachedMinPos = false;
		}

		//Se estiver clicando para andar
		if (Input.GetMouseButton (0)) {
			//Anda pra Direita
			if (Input.mousePosition.x >= Screen.width/2) {
				if (!reachedMaxPos) transform.position += Vector3.left * Time.deltaTime * walkSpeed;
			}
			//Anda pra Esquerda
			else {
				if (!reachedMinPos) transform.position += Vector3.right * Time.deltaTime * walkSpeed;
			}


			for (int i = 0; i < an.Length; i++) {
				an [i].SetBool ("Walking", true);
			}
		}

		else {
			for (int i = 0; i < an.Length; i++) {
				an [i].SetBool ("Walking", false);
			}
		}
	}
}
