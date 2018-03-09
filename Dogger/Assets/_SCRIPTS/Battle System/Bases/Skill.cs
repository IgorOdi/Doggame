using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType {

	ATTACK,
	HEAL,
	AUTOBUFF
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
	public int value;
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

		case SkillType.AUTOBUFF:

			Autobuff (_user);
			break;
		}
	}

	private void Attack(BattleAgent _attacker, BattleAgent _target) {

		if (_target != null) {

			float randomizador = Random.Range (0f, 1f);
			int _damage = value;
			int critMultiplier = randomizador > _attacker.actualInfo.crt ? 2 : 1;

			if (_damage == 0)
				_damage = _attacker.actualInfo.atk - _target.actualInfo.def;

			if (_damage <= 0)
				_damage = 1;

			if (targetType == TargetType.OneTarget) {

				_target.actualInfo.hp -= _damage * critMultiplier;

				if (_target.anim != null)
				_target.anim.SetTrigger ("Hit");

				if (_attacker.anim != null)
				_attacker.anim.SetTrigger ("Attack");
				_attacker.GetComponentInChildren<SpriteRenderer> ().sortingOrder = 10;

				if (_attacker.soundpack != null) {

					_attacker.source.clip = _attacker.soundpack.attackSound [0];
					_attacker.source.Play ();
				}

				if (_target.source != null) {

					int randomizer = Random.Range (0, _target.soundpack.screamSound.Length);
					_target.source.clip = _target.soundpack.screamSound [randomizer];
					_target.source.Play ();
				}

			} else {

				if (BattleManager.instance.heroParty.Contains (_attacker.GetComponent<HeroAgent> ())) {

					for (int i = 0; i < BattleManager.instance.enemyParty.Count; i++) {

						BattleManager.instance.enemyParty [i].actualInfo.hp -= _damage * critMultiplier;
					}
				} else {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.hp -= _damage * critMultiplier;
					}
				}
			}

			if (effectType != EffectType.NULL)
				Debuff (_attacker, _target);

			EndAction (_attacker, _target);
		}
	}

	private void Heal(BattleAgent _healer, BattleAgent _target) {

		if (_target != null) {

			if (targetType == TargetType.OneTarget) {
				
				_target.actualInfo.hp += value;
			} else {

				if (BattleManager.instance.heroParty.Contains (_healer.GetComponent<HeroAgent> ())) {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {
					
						BattleManager.instance.heroParty [i].actualInfo.hp += value;
					}
				} else {

					for (int i = 0; i < BattleManager.instance.enemyParty.Count; i++) {

						BattleManager.instance.enemyParty [i].actualInfo.hp += value;
					}
				}
			}

			if (effectType != EffectType.NULL)
				Buff (_healer, _target);
		}

		EndAction (_healer, _target);
	}

	private void Buff (BattleAgent _buffer, BattleAgent _target) {

		if (_target != null) {

			int _effect = effect;

			switch (effectType) {

			case EffectType.ATK:

				if (targetType == TargetType.OneTarget) {
					
					_target.actualInfo.atk += _effect;
				} else {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.atk += effect;
					}
				}
				break;

			case EffectType.DEF:

				if (targetType == TargetType.OneTarget) {

					_target.actualInfo.def += _effect;
				} else {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.def += effect;
					}
				}
				break;

			case EffectType.SPD:

				if (targetType == TargetType.OneTarget) {

					_target.actualInfo.spd += _effect;
				} else {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.spd += effect;
					}
				}
				break;

			case EffectType.CRIT:

				if (targetType == TargetType.OneTarget) {

					_target.actualInfo.crt += _effect;
				} else {

					for (int i = 0; i < BattleManager.instance.heroParty.Count; i++) {

						BattleManager.instance.heroParty [i].actualInfo.crt += effect;
					}
				}
				break;
			}
		}
	}

	private void Debuff(BattleAgent _debuffer, BattleAgent _target) {

		if (_target != null) {

			if (targetType == TargetType.OneTarget) {

				int _effect = effect;

				switch (effectType) {

				case EffectType.ATK:

					_target.actualInfo.atk -= _effect;
					break;

				case EffectType.DEF:

					_target.actualInfo.def -= _effect;
					break;

				case EffectType.SPD:

					_target.actualInfo.spd -= _effect;
					break;

				case EffectType.CRIT:

					_target.actualInfo.crt -= _effect;
					break;
				}
			}
		}
	}

	private void Autobuff(BattleAgent _buffer) {

		int _effect = effect;

		switch (effectType) {

			case EffectType.ATK:

				_buffer.actualInfo.atk += _effect;
				break;

			case EffectType.DEF:

				_buffer.actualInfo.def += _effect;
				break;

			case EffectType.SPD:

				_buffer.actualInfo.spd += _effect;
				break;

			case EffectType.CRIT:

				_buffer.actualInfo.crt += _effect;
				break;
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
