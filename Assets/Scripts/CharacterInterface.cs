using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterInterface
{
	private float speed;
	private int hp;
	private int atk;
	private int ID;
	private int GID;
	

	public bool CantMoveR, CantMoveL;
	public enum State { norml, Hurt, Jump, Fight, Full }
	public State _State;

	public CharacterInterface character;
	public StateInterFace StateBehavior;
	public MoveInterFace Movebehavior;
	public AI_InterFace AIbehavior;
	public DeadBehaviour deadBehaviour;
	public AttackInterFace Attackbehavior;
	public JumpInterface Jumpbehavior;
	public EndActionBehavior EndActionBehavior;
	public OnHitInterFace OnHitbehavior;
	public RayCastBehaviour RayCastBehavior;
	public struct RayCast
	{
		public RaycastHit2D hitRight;
		public RaycastHit2D hitLeft;
		public RaycastHit2D hitGround;
		public RaycastHit2D hitPatrol;
	}
	public RayCast raycast;
	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}
	public int Hp
	{
		get { return hp; }
		set { hp = value; }
	}
	public int Atk
	{
		get { return atk; }
		set { atk = value; }
	}
	public int _ID
	{
		get { return ID; }
		set { ID = value; }
	}
	public int _GID
	{
		get { return GID; }
		set { GID = value; }
	}
	public bool Iinvincible;
	public void StateDo()
	{
		StateBehavior.StateDo(this);
	}
	public void AI()
	{
		AIbehavior.AI();
	}
	public void Move()
	{
		Movebehavior.Move(this);
	}
	public void MoveBack()
	{
		Movebehavior.MoveBack(this);
	}
	public void Attack()
	{
		Attackbehavior.Attack(this);
	}
	public void Jump()
	{
		Jumpbehavior.Jump(this);
	}
	public void OnHit()
	{
		OnHitbehavior.OnHit(this);
	}
	public void EndHurt()
	{
		OnHitbehavior.EndHurt();
	}
	public void ReRayCast()
	{
		RayCastBehavior.hitRight = raycast.hitRight;
		RayCastBehavior.hitLeft = raycast.hitLeft;
		RayCastBehavior.hitGround = raycast.hitGround;
		RayCastBehavior.hitPatrol = raycast.hitPatrol;
	}
	public void CheckRightRay()
	{
		RayCastBehavior.CheckRightRay();
	}
	public void CheckLeftRay()
	{
		RayCastBehavior.CheckLeftRay();
	}
	public void CheckGroundRay()
	{
		RayCastBehavior.CheckGroundRay();
	}
	public void CheckPatrolRay()
	{
		RayCastBehavior.CheckpatrolRay();
	}
	public void EndAttack()
	{
		Attackbehavior.EndAttack(this);
	}
	public void EndAction()
	{
		EndActionBehavior.EndAction(this);
	}
	public void EndIinvincible()
	{
		EndActionBehavior.EndIinvincible(this);
	}
	public void Dead()
	{
		deadBehaviour.Dead(this);
	}
}

