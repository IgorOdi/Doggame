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

	private void OnMouseDown() {

		if (BattleManager.instance.changing && BattleManager.instance.selectedHero != null) BattleManager.instance.selectedChange = this;
	}

	public void ChangeHUD(BattleAgent _agent) {

		if (!BattleManager.instance.changing && BattleManager.instance.selectedHero != this) BattleManager.instance.selectedHero = this;
		HUDManager.instance.ChangeHeroHUD (actualInfo);
	}

	public override void VerifyAlive () {
		
		base.VerifyAlive ();

		if (actualInfo.hp <= 0) {

			int index = BattleManager.instance.heroParty.FindIndex (d => d == this);
			BattleManager.instance.heroParty.Remove (this);
			BattleManager.instance.ReQueue (index);
			if (BattleManager.instance.selectedHero == this) BattleManager.instance.selectedHero = null;
		}

		if (actualInfo != null && heroInfo != null)
			hpBar.fillAmount = actualInfo.hp / heroInfo.stats.hp;
	}

	public override IEnumerator ChooseAction () {

		while (!acted)
			yield return null;

		actionMarker.SetActive (true);
		acted = false;
		BattleManager.instance.NextTurn ();
	}
}
