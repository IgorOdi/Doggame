using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAgent : MonoBehaviour {

	public Stats actualInfo;
	public Image hpBar;
	protected bool acted;

	public virtual void Update() {

		if (actualInfo.hp <= 0) {

			gameObject.SetActive (false);
		}
	}

	public virtual void DefineStats() {

	}

	public virtual IEnumerator ChooseAction() {

		return null;
	}

	public virtual void Attack(BattleAgent _target) {

		int damage = actualInfo.atk - _target.actualInfo.def;
		_target.actualInfo.hp -= damage;
		acted = true;
	}
}
