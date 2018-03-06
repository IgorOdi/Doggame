using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : BattleAgent {

	public Enemy enemyInfo;
	private float chooseTime = 2;

	public override void DefineStats() {

		actualInfo.hp = enemyInfo.stats.hp;
		actualInfo.atk = enemyInfo.stats.atk;
		actualInfo.def = enemyInfo.stats.def;
		actualInfo.spd = enemyInfo.stats.spd;
		actualInfo.crt = enemyInfo.stats.crt;
	}

	public override void Update () {

		base.Update ();

		if (actualInfo.hp <= 0)
			BattleManager.instance.enemyParty.Remove (this);

		if (actualInfo != null && enemyInfo != null)
			hpBar.fillAmount = actualInfo.hp / enemyInfo.stats.hp;
	}

	private void OnMouseDown() {

		if (BattleManager.instance.selectedEnemy != this) BattleManager.instance.selectedEnemy = this;
	}

	public override IEnumerator ChooseAction () {

		print (enemyInfo.name + " Turn");

		float startTime = Time.time;
		while (Time.time < startTime + chooseTime)
			yield return null;

		BattleAgent target = RandomizeTarget ();
		Attack (target);
		HUDManager.instance.ChangeHeroHUD (target.actualInfo);
		BattleManager.instance.NextTurn ();
	}

	BattleAgent RandomizeTarget() {

		List<int> aliveHeroes = new List<int> ();

		for (int i = 0; i < 4; i++) {

			if (BattleManager.instance.battleAgents [i].gameObject.activeSelf) {

				aliveHeroes.Add (i);
			}
		}

		int randomizador = Random.Range (0, aliveHeroes.Count);

		BattleAgent target = BattleManager.instance.battleAgents [randomizador];
		return target;
	}
}
