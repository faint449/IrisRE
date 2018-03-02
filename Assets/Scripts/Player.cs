using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterInterface
{
	public Player(int ID, int GID, int hp, int atk, float speed, GameObject Player)
	{
		Hp = hp;
		ID = _ID;
		GID = _GID;
		Atk = atk;
		Speed = speed;
		StateBehavior = new PlayerState(Player);
		Movebehavior = new PlayerMove(Speed,Player);
		Jumpbehavior = new PlayerJump(Player);
		Attackbehavior = new PlayerAttack(Player);
		OnHitbehavior = new PlayerOnHit(Player);
		EndActionBehavior = new PlayerEndAction(Player);
		RayCastBehavior = new PlayerRayCast();
		RayCastBehavior.character = this;
		
	}
	
}
