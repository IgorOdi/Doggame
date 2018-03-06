using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public static HUDManager instance;

	[SerializeField]
	private Image heroImg;
	[SerializeField]
	private Image[] skillImg;
	[SerializeField]
	private Text[] statsText;

	void Awake() {

		if (instance == null) instance = this;
	}

	public void ChangeHeroHUD(Stats _heroStats) {

		statsText [0].text = "HP " + _heroStats.hp.ToString();
		statsText [1].text = "ATK " + _heroStats.atk.ToString();
		statsText [2].text = "DEF " + _heroStats.def.ToString();
		statsText [3].text = "SPD " + _heroStats.spd.ToString();
	}
}
