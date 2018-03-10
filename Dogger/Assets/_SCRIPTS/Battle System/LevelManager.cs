using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;
	public float deltaSpace;
	public float maxDeltaSpace;
	public float encounterChance = .5f;
	public int maxEnemies;
	public List<Enemy> enemies;

	void Awake() {

		if (instance == null) instance = this;
	}

	void Update() {

		if (deltaSpace >= maxDeltaSpace) {

			RandomizeEncounter ();
		}
	}

	private List<Enemy> RandomizeEncounter() {

		deltaSpace = 0;
		float randomizeEncounter = Random.Range (0f, 1f);

		if (randomizeEncounter < encounterChance) {

			int randomizeMaxEnemies = Random.Range (1, maxEnemies + 1);

			List<Enemy> enemyList = new List<Enemy> ();

			for (int i = 0; i < randomizeMaxEnemies; i++) {

				int randomEnemy = Random.Range (0, randomizeMaxEnemies);
				enemyList.Add (enemies [randomEnemy]);
			}

			BattleManager.instance.StartBattle (enemyList);
		}

		return null;
	}
}
