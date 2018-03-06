using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	public static BattleManager instance;
	public BattleAgent[] battleAgents;
	public Queue<int> turnQueue;
	public List<HeroAgent> heroParty = new List<HeroAgent> ();
	public List<EnemyAgent> enemyParty = new List<EnemyAgent> ();

	[HideInInspector]
	public BattleAgent selectedHero;
	[HideInInspector]
	public BattleAgent selectedEnemy;

	private int agentTurn;

	private const int maxEnemies = 4;
	private const int maxAgents = 8;

	void Awake() {

		if (instance == null) instance = this;
		turnQueue = new Queue<int> ();
	}

	public void StartBattle(List<Enemy> _enemies) {

		//canMove = false;

		for (int i = 0; i < _enemies.Count + maxEnemies; i++) {

			if (i < maxEnemies) {

				HeroAgent heroAgent = battleAgents [i].GetComponent<HeroAgent> ();
				heroParty.Add (heroAgent);

			} else {
				
				battleAgents [i].gameObject.SetActive (true);
				EnemyAgent enemyAgent = battleAgents [i].GetComponent<EnemyAgent> ();
				enemyAgent.enemyInfo = _enemies [i - maxEnemies];
				enemyAgent.DefineStats ();
				enemyParty.Add (enemyAgent);
			}
		}

//		for (int i = maxEnemies; i < _enemies.Count + maxEnemies; i++) {
//
//			battleAgents [i].gameObject.SetActive (true);
//			EnemyAgent enemyAgent = battleAgents [i].GetComponent<EnemyAgent> ();
//			enemyAgent.enemyInfo = _enemies [i - maxEnemies];
//			enemyAgent.DefineStats ();
//		}
			
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

		int activeAgents = speedList.Count;

		while (turnQueue.Count < activeAgents) {
			
			for (int i = 0; i < maxAgents; i++) {

				if (battleAgents [i].gameObject.activeSelf) {

					int higherValue = Mathf.Max (speedList.ToArray());

					if (battleAgents [i].actualInfo.spd == higherValue) {

						turnQueue.Enqueue (i);
						speedList.Remove (higherValue);
					}
				}
			}
		}
	}

	public void NextTurn() {

		if (turnQueue.Count > 0) {
			
			agentTurn = turnQueue.Dequeue ();
			StartCoroutine (battleAgents [agentTurn].ChooseAction ());
		} else {
			
			EnqueueAgents ();
			NextTurn ();
		}
	}

	public void Action() {

		if (selectedHero == battleAgents[agentTurn])
			battleAgents [agentTurn].Attack (selectedEnemy);
	}

	public void EndBattle() {


	}
}
