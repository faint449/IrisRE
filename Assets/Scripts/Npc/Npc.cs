using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : CharacterInterface
{
	public Npc(int ID, int GID, int hp, int atk, float speed,int[] talkCount, GameObject CharObj)
	{
		Hp = hp;
		_ID = ID;
		_GID = GID;
		Atk = atk;
		Speed = speed;
		AIbehavior = new NpcTest01_AI();
		RayCastBehavior = new NpcTest01Ray(CharObj, talkCount);
		RayCastBehavior.character = this;
	}
}
