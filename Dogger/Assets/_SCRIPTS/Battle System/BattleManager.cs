using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	public static BattleManager instance;
	public BattleAgent[] battleAgents;
	public List<HeroAgent> heroParty = new List<HeroAgent> ();
	public List<EnemyAgent> enemyParty = new List<EnemyAgent> ();

	[HideInInspector]
	public List<int> turnQueue;
	[HideInInspector]
	public BattleAgent selectedHero;
	[HideInInspector]
	public BattleAgent selectedChange;
	[HideInInspector]
	public BattleAgent selectedEnemy;
	[HideInInspector]
	public bool changing;

	private int agentTurn;

	private int[] heroPositions = { -1, -3, -5, -7 };
	private int[] enemyPositions = { 1, 3, 5, 7};

	private const float yPos = -0.5f;

	private const int maxEnemies = 4;
	private const int maxAgents = 8;

	void Awake() {

		if (instance == null) instance = this;
		turnQueue = new List<int> ();
	}

	public void StartBattle(List<Enemy> _enemies) {

		//canMove = false;

		for (int i = 0; i < _enemies.Count + maxEnemies; i++) {

			if (i < maxEnemies) {

				if (battleAgents [i].gameObject.activeSelf) {
					
					HeroAgent heroAgent = battleAgents [i].GetComponent<HeroAgent> ();
					heroParty.Add (heroAgent);
					heroAgent.position = i;
					battleAgents [i].transform.position = new Vector2 (heroPositions [i] - 0.5f, yPos);
				}

			} else {
				
				battleAgents [i].gameObject.SetActive (true);
				EnemyAgent enemyAgent = battleAgents [i].GetComponent<EnemyAgent> ();
				enemyAgent.enemyInfo = _enemies [i - maxEnemies];
				enemyAgent.position = i - maxEnemies;
				enemyAgent.DefineStats ();
				enemyParty.Add (enemyAgent);

				battleAgents [i].transform.position = new Vector2 (enemyPositions [i - maxEnemies] + 0.5f, yPos);
			}
		}
			
		EnqueueAgents ();
		NextTurn ();
	}

	private void EnqueueAgents() {

		List<int> speedList = new List<int> ();

		for (int i = 0; i < maxAgents; i++) {

			if (battleAgents [i].gameObject.activeSelf) {

				speedList.Add (battleAgents [i].actualInfo.spd);
			}
		}

		int activeAgents = heroParty.Count + enemyParty.Count;

		for (int i = 0; i < maxEnemies; i++) {

				HeroAgent heroAgent = battleAgents [i].GetComponent<HeroAgent> ();
				heroAgent.actionMarker.SetActive (false);
		}

		while (turnQueue.Count < activeAgents) {
			
			for (int i = 0; i < maxAgents; i++) {

				if (battleAgents [i].gameObject.activeSelf) {

					int higherValue = Mathf.Max (speedList.ToArray());

					if (battleAgents [i].actualInfo.spd == higherValue) {

						turnQueue.Add (i);
						speedList.Remove (higherValue);
					}
				}
			}
		}
	}

	public void ReQueue(int index) {

		turnQueue.Remove (index);
	}

	public void NextTurn() {

		if (turnQueue.Count > 0) {

			agentTurn = turnQueue [0];
			turnQueue.Remove (agentTurn);

			if (agentTurn < maxEnemies) {

				selectedHero = battleAgents [agentTurn];
				battleAgents [agentTurn].GetComponent<HeroAgent> ().ChangeHUD (battleAgents[agentTurn]);
				HUDManager.instance.ChangeHeroHUD (battleAgents [agentTurn].actualInfo);
			} else {
				
				selectedEnemy = battleAgents [agentTurn];
				HUDManager.instance.ChangeEnemyHUD (selectedEnemy.actualInfo);
			}

			StartCoroutine (battleAgents [agentTurn].ChooseAction ());
		} else {
			
			EnqueueAgents ();
			NextTurn ();
		}
	}

	public void ActiveChange() {

		StartCoroutine (ChangeOrder ());
	}

	public IEnumerator ChangeOrder() {

		changing = true;

		while (selectedHero == null || selectedChange == null)
			yield return null;

		float t = 0;
		Vector2 a = selectedHero.transform.position;
		Vector2 b = selectedChange.transform.position;

		HeroAgent heroAgent = selectedHero.GetComponent<HeroAgent> ();

		while (t <= 1) {

			selectedHero.transform.position = Vector2.Lerp (a, b, t);
			selectedChange.transform.position = Vector2.Lerp (b, a, t);
			heroAgent.ChangeHUD (selectedHero);
			t += Time.deltaTime * 2;
			yield return null;
		}

		selectedChange = null;
		changing = false;
	}

	public void Action() {

		if (selectedHero == battleAgents[agentTurn])
			battleAgents [agentTurn].Attack (selectedEnemy);
	}

	public void EndBattle() {


	}
}