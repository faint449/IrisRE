using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterInterface
{
	public enum EnemyKinds {test1,test2 }
	public EnemyKinds _enemy;
	public Enemy(int ID,int GID,int hp,int atk, float speed,EnemyKinds EnemyKind, GameObject CharObj)
	{
		Hp = hp;
		_ID = ID;
		_GID = GID;
		Atk = atk;
		Speed = speed;
		_enemy = EnemyKind;
		switch (_enemy)
		{
			case EnemyKinds.test1:
				Movebehavior = new Enemy01Move(Speed,CharObj);
				AIbehavior = new Test1AI(this, CharObj);
				RayCastBehavior = new Test1RayCast();
				RayCastBehavior.character = this;
				EndActionBehavior = new EnemyTest01EndAtcion(CharObj);
				Attackbehavior = new EnemyTest01Attack(CharObj);
				OnHitbehavior = new EnemyTest01Onhit(CharObj);
				deadBehaviour = new EnemyTest01Dead(CharObj);
				break;
		}
		
	}
	
}
