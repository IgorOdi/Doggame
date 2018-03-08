using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType {

	ATTACK,
	HEAL,
	BUFF,
	DEBUFF
}

public enum TargetType {

	OneTarget,
	MultiTarget
}

public enum EffectType {

	NULL,
	ATK,
	DEF,
	SPD,
	CRIT
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skill : ScriptableObject {

	public new string name;
	public int effect;
	public int range;
	public int cooldown;
	public SkillType skillType;
	public TargetType targetType;
	public EffectType effectType;

	public void CheckSkill(BattleAgent _user, BattleAgent _target) {

		switch (skillType) {

		case SkillType.ATTACK:

			Attack (_user, _target);
			break;

		case SkillType.HEAL:

			Heal (_user, _target);
			break;

		case SkillType.BUFF:

			Buff (_user, _target);
			break;

		case SkillType.DEBUFF:

			Debuff (_user, _target);
			break;
		}
	}

	private void Attack(BattleAgent _attacker, BattleAgent _target) {

		if (_target != null && _target.position < range) {

			float randomizador = Random.Range (0f, 1f);
			int _damage = effect;
			int critMultiplier = randomizador > _attacker.actualInfo.crt ? 2 : 1;

			if (_damage == 0)
				_damage = _attacker.actualInfo.atk - _target.actualInfo.def;

			if (targetType == TargetType.OneTarget) {

				_target.actualInfo.hp -= _damage * critMultiplier;

			} else {

				if (BattleManager.instance.heroParty.Contains (_attacker.GetComponent<HeroAgent> ())) {

					for (int i = 0; i < BattleManager.instance.enemyParty.Count; i++) {

						BattleManager.instance.enemyParty [i].actualInfo.hp -= _damage * critMultiplier;
					}
				} else {

					for (int i = 0; i < BattleManager.instance.enemyParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.hp -= _damage * critMultiplier;
					}
				}
			}

			EndAction (_attacker, _target);
		}
	}

	private void Heal(BattleAgent _healer, BattleAgent _target) {

		if (_target != null) {

			if (targetType == TargetType.OneTarget) {
				
				_target.actualInfo.hp += effect;
			} else {

				if (BattleManager.instance.heroParty.Contains (_healer.GetComponent<HeroAgent> ())) {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {
					
						BattleManager.instance.heroParty [i].actualInfo.hp += effect;
					}
				} else {

					for (int i = 0; i < BattleManager.instance.enemyParty.Count; i++) {

						BattleManager.instance.enemyParty [i].actualInfo.hp += effect;
					}
				}
			}
		}

		EndAction (_healer, _target);
	}

	private void Buff (BattleAgent _buffer, BattleAgent _target) {

		if (_target != null) {

			if (targetType == TargetType.OneTarget) {

				switch (effectType) {

				case EffectType.ATK:

					_target.actualInfo.atk += effect;
					break;

				case EffectType.DEF:

					_target.actualInfo.def += effect;
					break;

				case EffectType.SPD:

					_target.actualInfo.spd += effect;
					break;

				case EffectType.CRIT:

					_target.actualInfo.crt += effect;
					break;
				}
			}
		}
	}

	private void Debuff(BattleAgent _debuffer, BattleAgent _target) {

		if (_target != null) {

			if (targetType == TargetType.OneTarget) {

				switch (effectType) {

				case EffectType.ATK:

					_target.actualInfo.atk -= effect;
					break;

				case EffectType.DEF:

					_target.actualInfo.def -= effect;
					break;

				case EffectType.SPD:

					_target.actualInfo.spd -= effect;
					break;

				case EffectType.CRIT:

					_target.actualInfo.crt -= effect;
					break;
				}
			}
		}
	}

	private void EndAction(BattleAgent _user, BattleAgent _target) {

		for (int i = 0; i < BattleManager.instance.battleAgents.Length; i++) {

			if (BattleManager.instance.battleAgents [i].gameObject.activeSelf)
				BattleManager.instance.battleAgents [i].VerifyAlive ();
		}

		BattleManager.instance.NextTurn ();
	}
}
