using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Character : MonoBehaviour
{
	public enum CharacterSort { player, enemy, npc }
	public CharacterSort _CharacterSort;
	public Enemy.EnemyKinds enemyEnum;
	public CharacterInterface character;
	public int ID;
	public int GID;
	public int Hp;
	public int Atk;
	public float Speed;
	public int[] TalkCount;
	public Animator anim;

	public Rect RightLayRect, LeftLayRect, GroundLayRect,PatrolLayRect;
	public RaycastHit2D hitRight, hitLeft, hitGround, hitPatrol;
	public LayerMask RayLayer, PartolLayer;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube((Vector2)transform.position + RightLayRect.center, RightLayRect.size);
		Gizmos.DrawWireCube((Vector2)transform.position + LeftLayRect.center, LeftLayRect.size);
		Gizmos.DrawWireCube((Vector2)transform.position + GroundLayRect.center, GroundLayRect.size);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube((Vector2)transform.position + PatrolLayRect.center, PatrolLayRect.size);
	}
	
	void Start ()
	{
		anim = this.GetComponent<Animator>();
		switch (_CharacterSort)
		{
			case CharacterSort.player:
				character = new Player(ID,GID,Hp,Atk,Speed,gameObject);
				break;
			case CharacterSort.enemy:			
				character = new Enemy(ID, GID, Hp, Atk, Speed, enemyEnum,gameObject);				
				break;
			case CharacterSort.npc:
				character = new Npc(ID, GID, Hp, Atk, Speed,TalkCount,gameObject);
				
				break;
		}
	}
	public void Update()
	{

		CheckRay();

		if (_CharacterSort != CharacterSort.player)
		{
			character.AI();
		}
		else
		{
			character.StateDo();
		}
		character.ReRayCast();
		character.CheckRightRay();
		character.CheckLeftRay();
		character.CheckGroundRay();
		character.CheckPatrolRay();
		
	}
	public void CheckRay()
	{
		#region//射線
		character.raycast.hitRight = Physics2D.BoxCast((Vector2)transform.position + RightLayRect.center, RightLayRect.size, 0, new Vector2(0, 0), 0, ~RayLayer);
		character.raycast.hitLeft = Physics2D.BoxCast((Vector2)transform.position + LeftLayRect.center, LeftLayRect.size, 0, new Vector2(0, 0), 0, ~RayLayer);
		character.raycast.hitGround = Physics2D.BoxCast((Vector2)transform.position + GroundLayRect.center, GroundLayRect.size, 0, new Vector2(0, 0), 0, ~RayLayer);		
		character.raycast.hitPatrol = Physics2D.BoxCast((Vector2)transform.position + PatrolLayRect.center, PatrolLayRect.size, 0, new Vector2(0, 0), 0, ~PartolLayer);
		#endregion
	}
	public void Dead()
	{
		character.Dead();
	}
	public void EndHurt()
	{
		character.EndHurt();
	}
	public void EndAttack()
	{
		character.EndAttack();
	}
	public void EndAction()
	{
		character.EndAction();
	}
	public void EndIinvincible()
	{
		character.EndIinvincible();
	}
}
