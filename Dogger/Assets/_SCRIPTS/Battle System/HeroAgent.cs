using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAgent : BattleAgent {

	public Hero heroInfo;

	void OnEnable() {

		actualInfo.hp = heroInfo.stats.hp;
		actualInfo.atk = heroInfo.stats.atk;
		actualInfo.def = heroInfo.stats.def;
		actualInfo.spd = heroInfo.stats.spd;
		actualInfo.crt = heroInfo.stats.crt;
	}
		
	private void OnMouseDown() {

		if (!BattleManager.instance.changing && BattleManager.instance.selectedHero != this) BattleManager.instance.selectedHero = this;
		if (BattleManager.instance.changing && BattleManager.instance.selectedHero != null) BattleManager.instance.selectedChange = this;
		HUDManager.instance.ChangeHeroHUD (actualInfo);
	}

	public override void Update () {
		
		base.Update ();

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
		
		print (heroInfo.name + " Turn");

		while (!acted)
			yield return null;

		acted = false;
		
		BattleManager.instance.NextTurn ();
	}
}
