using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGM : MonoBehaviour {

	public GameObject gm;

	public GameObject music;

	// Use this for initialization
	void Start () {
		if (GameManager.instance == null) Instantiate (gm);

		GameObject Musica = Instantiate (music);
		DontDestroyOnLoad (Musica);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScene (string sceneName) {
		GameManager.instance.OnChangeScene (sceneName);
	}
}
