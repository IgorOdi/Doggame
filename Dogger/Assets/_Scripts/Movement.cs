using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {

			//Anda pra Direita
			if (Input.mousePosition.x >= Screen.width/2) {
				transform.position += Vector3.left * Time.deltaTime * walkSpeed;
			}

			//Anda pra Esquerda
			else {
				transform.position += Vector3.right * Time.deltaTime * walkSpeed;
			}
		}
	}
}
