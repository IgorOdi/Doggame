using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAgent : MonoBehaviour {

	public Stats actualInfo;
	public int[] skillCooldown;

	public int position;
	public Image hpBar;
	public Animator anim;

	protected bool acted;
		
	private void OnMouseOver() {

		if (BattleManager.instance.selecting) {

			bool ally = BattleManager.instance.heroParty.Contains (this.GetComponent<HeroAgent> ());

			if ((BattleManager.instance.allySkill && !ally) || (!BattleManager.instance.allySkill && ally))
					HUDManager.instance.ChangeTargetHUD (this);
		}
	}

	private void OnMouseDown() {

		if (BattleManager.instance.selecting) {

			bool ally = BattleManager.instance.heroParty.Contains (this.GetComponent<HeroAgent> ());

			if ((BattleManager.instance.allySkill && !ally) || (!BattleManager.instance.allySkill && ally))
				BattleManager.instance.selectedTarget = this;
		}
	}

	public virtual void DefineStats() {

	}

	public virtual IEnumerator ChooseAction() {

		return null;
	}

	public virtual void VerifyAlive() {

		if (actualInfo.hp <= 0) {
			gameObject.SetActive (false);

			if (BattleManager.instance.selectedTarget == this) {
				BattleManager.instance.selectedTarget = null;
				HUDManager.instance.ChangeTargetHUD (null);
			}
		}
	}
}
