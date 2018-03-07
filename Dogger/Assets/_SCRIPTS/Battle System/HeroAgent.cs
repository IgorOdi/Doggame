using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAgent : BattleAgent {

	public Hero heroInfo;
	public GameObject actionMarker;

	void OnEnable() {

		actualInfo.hp = heroInfo.stats.hp;
		actualInfo.atk = heroInfo.stats.atk;
		actualInfo.def = heroInfo.stats.def;
		actualInfo.spd = heroInfo.stats.spd;
		actualInfo.crt = heroInfo.stats.crt;
	}

	public void ChangeHUD(BattleAgent _agent) {

		if (!BattleManager.instance.selecting && BattleManager.instance.selectedHero != this) BattleManager.instance.selectedHero = this;
		HUDManager.instance.ChangeHeroHUD (actualInfo);
	}

	public override void VerifyAlive () {
		
		base.VerifyAlive ();

		if (actualInfo != null && heroInfo != null) {
		
			hpBar.fillAmount = actualInfo.hp / heroInfo.stats.hp;
			actualInfo.hp = Mathf.Clamp (actualInfo.hp, 0, heroInfo.stats.hp);
		}

		HUDManager.instance.ChangeHeroHUD (actualInfo);

		if (actualInfo.hp <= 0) {

			int index = BattleManager.instance.heroParty.FindIndex (d => d == this);
			BattleManager.instance.heroParty.Remove (this);
			BattleManager.instance.ReQueue (index, position, true);
			if (BattleManager.instance.selectedHero == this) BattleManager.instance.selectedHero = null;
		}
	}

	public override IEnumerator ChooseAction () {

		while (!acted)
			yield return null;

		actionMarker.SetActive (true);
		acted = false;
		BattleManager.instance.NextTurn ();
	}
}
