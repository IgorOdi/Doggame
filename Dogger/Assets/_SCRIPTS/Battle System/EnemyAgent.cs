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

	public override void VerifyAlive () {

		base.VerifyAlive ();

		if (actualInfo.hp <= 0) {

			int index = BattleManager.instance.enemyParty.FindIndex (d => d == this) + 4;
			BattleManager.instance.enemyParty.Remove (this);
			BattleManager.instance.ReQueue (index);
			if (BattleManager.instance.selectedEnemy == this) BattleManager.instance.selectedEnemy = null;
		}
			
		if (actualInfo != null && enemyInfo != null)
			hpBar.fillAmount = actualInfo.hp / enemyInfo.stats.hp;
	}

	private void OnMouseDown() {

		if (BattleManager.instance.selectedEnemy != this) BattleManager.instance.selectedEnemy = this;
		HUDManager.instance.ChangeEnemyHUD (BattleManager.instance.selectedEnemy.actualInfo);
	}

	public override IEnumerator ChooseAction () {

		float startTime = Time.time;
		while (Time.time < startTime + chooseTime)
			yield return null;

		BattleAgent target = RandomizeTarget ();
		Attack (target);
		HUDManager.instance.ChangeHeroHUD (target.actualInfo);
		BattleManager.instance.NextTurn ();
	}

	BattleAgent RandomizeTarget() {
		
		int randomizador = Random.Range (0, BattleManager.instance.heroParty.Count);

		BattleAgent target = BattleManager.instance.heroParty [randomizador];
		return target;
	}
}
