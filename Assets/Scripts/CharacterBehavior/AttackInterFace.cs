using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackInterFace
{
	void Attack(CharacterInterface character);
	void EndAttack(CharacterInterface character);
}
