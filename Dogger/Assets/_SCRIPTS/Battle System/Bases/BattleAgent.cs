using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAgent : MonoBehaviour {

	public Stats actualInfo;
	public int position;
	public Image hpBar;
	protected bool acted;

	private void OnMouseDown() {

		BattleManager.instance.selectedTarget = this;
		HUDManager.instance.ChangeTargetHUD (this);
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
