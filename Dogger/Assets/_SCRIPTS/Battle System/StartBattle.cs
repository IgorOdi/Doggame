using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour {

	public List<Enemy> enemies;

	void Start() {

		StartCoroutine (WaitBattle ());
	}

	IEnumerator WaitBattle() {

		yield return new WaitForSeconds(2);
		BattleManager.instance.StartBattle (enemies);
	}
}
