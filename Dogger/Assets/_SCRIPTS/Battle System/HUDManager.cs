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
	private Text[] enemyStatsText;

	[SerializeField]
	private RectTransform turnMarker;
	[SerializeField]
	private RectTransform targetMarker;

	void Awake() {

		if (instance == null) instance = this;
	}

	public void ChangeHeroHUD(Stats _heroStats) {

		statsText [0].text = "HP " + _heroStats.hp.ToString();
		statsText [1].text = "ATK " + _heroStats.atk.ToString();
		statsText [2].text = "DEF " + _heroStats.def.ToString();
		statsText [3].text = "SPD " + _heroStats.spd.ToString();
		statsText [4].text = "CRIT " + _heroStats.crt * 100 + "%";
	}

	public void ChangeEnemyHUD(Stats _enemyStats) {

		enemyStatsText [0].text = "HP " + _enemyStats.hp.ToString();
		enemyStatsText [1].text = "ATK " + _enemyStats.atk.ToString();
		enemyStatsText [2].text = "DEF " + _enemyStats.def.ToString();
		enemyStatsText [3].text = "SPD " + _enemyStats.spd.ToString();
		enemyStatsText [4].text = "CRIT " + _enemyStats.crt * 100 + "%";
	}

	public void ChangeTargetHUD(BattleAgent selectedAgent) {

		bool active = selectedAgent != null ? true : false;
		targetMarker.gameObject.SetActive (active);

		if (selectedAgent != null) {
			
			Vector2 pos = worldToUISpace (selectedAgent.transform.position);
			Vector2 newPos = targetMarker.transform.position;
			newPos.x = pos.x;
			targetMarker.transform.position = newPos;

			if (selectedAgent.GetComponent<EnemyAgent>() != null)
				ChangeEnemyHUD (selectedAgent.actualInfo);
		}
	}

	public void ChangeTurnHUD() {

		turnMarker.gameObject.SetActive (true);

		int _agentTurn = BattleManager.instance.agentTurn;
		Transform t = BattleManager.instance.battleAgents [_agentTurn].transform;

		Vector2 pos = worldToUISpace (t.position);
		Vector2 newPos = turnMarker.transform.position;
		newPos.x = pos.x;
		turnMarker.transform.position = newPos;
	}

	public void DeactivateTurnHUD() {

		turnMarker.gameObject.SetActive (false);
	}

	public Vector2 worldToUISpace(Vector2 worldPos) {

		Vector2 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		Vector2 movePos;

		RectTransformUtility.ScreenPointToLocalPointInRectangle (turnMarker, screenPos, Camera.main, out movePos);
		return turnMarker.transform.TransformPoint (movePos);
	}
}