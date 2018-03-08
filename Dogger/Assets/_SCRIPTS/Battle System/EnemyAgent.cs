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

		if (actualInfo != null && enemyInfo != null) {

			hpBar.fillAmount = actualInfo.hp / enemyInfo.stats.hp;
			actualInfo.hp = Mathf.Clamp (actualInfo.hp, 0, enemyInfo.stats.hp);
		}

		HUDManager.instance.ChangeEnemyHUD (actualInfo);

		if (actualInfo.hp <= 0) {

			int index = BattleManager.instance.enemyParty.FindIndex (d => d == this) + 4;
			BattleManager.instance.enemyParty.Remove (this);
			BattleManager.instance.ReQueue (index, position, false);
			HUDManager.instance.ChangeTargetHUD (null);
			if (BattleManager.instance.selectedTarget == this) BattleManager.instance.selectedTarget = null;
		}
	}

	public override IEnumerator ChooseAction () {

		float startTime = Time.time;
		while (Time.time < startTime + chooseTime)
			yield return null;

		int randomizeSkill = Random.Range (0, enemyInfo.skillList.Length);
		int skillRange = enemyInfo.skillList [randomizeSkill].range;
		BattleAgent target = RandomizeTarget (skillRange);
		enemyInfo.skillList [randomizeSkill].CheckSkill (this, target);
	}

	BattleAgent RandomizeTarget(int _skillRange) {
		
		int randomizador = Random.Range (0, _skillRange);
		randomizador = Mathf.Clamp (randomizador, 0, BattleManager.instance.heroParty.Count);

		BattleAgent target = BattleManager.instance.heroParty [randomizador];
		return target;
	}
}
