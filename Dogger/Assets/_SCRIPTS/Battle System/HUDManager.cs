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

	[SerializeField]
	private RectTransform selectedHero;
	[SerializeField]
	private RectTransform selectedEnemy;

	void Awake() {

		if (instance == null) instance = this;
	}

	public void ChangeHeroHUD(Stats _heroStats) {

		statsText [0].text = "HP " + _heroStats.hp.ToString();
		statsText [1].text = "ATK " + _heroStats.atk.ToString();
		statsText [2].text = "DEF " + _heroStats.def.ToString();
		statsText [3].text = "SPD " + _heroStats.spd.ToString();

		bool active = BattleManager.instance.selectedHero != null ? true : false;
		selectedHero.gameObject.SetActive (active);

		if (BattleManager.instance.selectedHero != null) {

			Vector2 pos = worldToUISpace (BattleManager.instance.selectedHero.transform.position);
			Vector2 newPos = selectedHero.transform.position;
			newPos.x = pos.x;
			selectedHero.transform.position = newPos;
		}
	}

	public void ChangeEnemyHUD() {

		bool active = BattleManager.instance.selectedEnemy != null ? true : false;
		selectedEnemy.gameObject.SetActive (active);
		Vector2 pos = worldToUISpace (BattleManager.instance.selectedEnemy.transform.position);
		Vector2 newPos = selectedEnemy.transform.position;
		newPos.x = pos.x;
		selectedEnemy.transform.position = newPos;
	}

	public Vector2 worldToUISpace(Vector2 worldPos) {

		Vector2 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		Vector2 movePos;

		RectTransformUtility.ScreenPointToLocalPointInRectangle (selectedHero, screenPos, Camera.main, out movePos);
		return selectedHero.transform.TransformPoint (movePos);
	}
}
