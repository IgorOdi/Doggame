using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour {

	public static PartyManager instance;
	public List<string> partyList;
	public List<GameObject> partyListObject;

	public float[] hpPoints = new float[4];
	public Image[] hpBar;

	void Awake() {

		if (instance == null) {

			instance = this;
		}
	}

	void Start() {

		PositioningGoodBoys ();

		for (int i = 0; i < hpPoints.Length; i++) {

			HeroAgent heroAgent = BattleManager.instance.battleAgents [i].GetComponent<HeroAgent>();
			hpPoints [i] = heroAgent.heroInfo.stats.hp;
		}
	}

	public void PositioningGoodBoys() {

		for (int i = 0; i < 4; i++) {

			int index = partyList.FindIndex (d => d == BattleManager.instance.battleAgents [i].name);
			HeroAgent heroAgent = BattleManager.instance.battleAgents [i].GetComponent<HeroAgent> ();

			if (index >= 0) {
				BattleManager.instance.battleAgents [i].position = index;
				partyListObject [i].transform.localPosition = new Vector2 (BattleManager.instance.heroPositions [index], BattleManager.instance.yPos);
			} else {

				partyListObject [i].SetActive (false);
			}
		}
	}
}
