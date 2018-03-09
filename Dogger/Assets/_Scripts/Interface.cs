using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {

	public GameObject borksVilleText;
	public GameObject libraryText;
	public GameObject dogsmithText;

	public Text goldText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		goldText.text = GameManager.instance.gold.ToString ();
	}

	public void ShowHUD (GameObject Overlay) {
		Overlay.SetActive (true);
		borksVilleText.SetActive (false);

		if (Overlay.name.Contains ("Smith"))
			dogsmithText.SetActive (true);
		else if (Overlay.name.Contains ("Liv"))
			libraryText.SetActive (true);
	}

	public void HideHUD (GameObject Overlay) {
		Overlay.SetActive (false);
		borksVilleText.SetActive (true);
		libraryText.SetActive (false);
		dogsmithText.SetActive (false);
	}

	public void ChangeScene (string sceneName) {
		GameManager.instance.OnChangeScene (sceneName);
	}
}
