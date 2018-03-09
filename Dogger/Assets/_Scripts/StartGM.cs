using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGM : MonoBehaviour {

	public GameObject gm;

	// Use this for initialization
	void Start () {
		if (GameManager.instance == null) Instantiate (gm);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene (string sceneName) {
		GameManager.instance.OnChangeScene (sceneName);
	}
}
