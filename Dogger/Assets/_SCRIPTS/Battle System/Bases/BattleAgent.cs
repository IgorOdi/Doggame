﻿using System.Collections;
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

		float randomizador = Random.Range (0, 1);
		int critMultiplier = randomizador > actualInfo.crt ? 2 : 1;

		int damage = (actualInfo.atk * critMultiplier) - _target.actualInfo.def;
		_target.actualInfo.hp -= damage;
		acted = true;
	}
}
