using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;


public class Test1RayCast : RayCastBehaviour
{
	public override void CheckGroundRay()
	{
		
	}
	public override void CheckRightRay()
	{
		if (hitRight.collider != null)
		{
			if (hitRight.collider.tag == "wall")
			{
				character.CantMoveR = true;
			}
			else
			{
				character.CantMoveR = false;
			}
			if (hitRight.collider.tag == "turn")
			{
				character.AIbehavior._state = AI_InterFace.State.MoveB;
			}
		}
		else
		{
			character.CantMoveL = false;
		}

	}
	public override void CheckLeftRay()
	{
		if (hitLeft.collider != null)
		{
			if (hitLeft.collider.tag == "wall")
			{
				character.CantMoveL = true;
			}
			else
			{
				character.CantMoveL = false;
			}
			if (hitLeft.collider.tag == "turn")
			{
				character.AIbehavior._state = AI_InterFace.State.Move;
			}
		}
		else
		{
			character.CantMoveL = false;
		}
	}
	public override void CheckpatrolRay()
	{
		if (hitPatrol.collider != null&& hitPatrol.collider.tag == "Player"&&
			character.AIbehavior._state != AI_InterFace.State.Fight)
		{

			
			character.AIbehavior.target = hitPatrol.collider.gameObject;
			character.AIbehavior._state = AI_InterFace.State.Chase;

		}
	}
}
public class Test1AI : AI_InterFace
{
	private float timer;
	private int Actionount;
	private CharacterInterface character;
	private bool isChose = false;
	private float patrolTime,dis;
	public Test1AI(CharacterInterface _character,GameObject _characterObj)
	{
		character = _character;
		characterObj = _characterObj;
	}
	public override void AI()
	{
		
		if (target != null)
		{
			dis = Vector3.Distance(target.transform.position, characterObj.transform
					.position);
		}
		
		switch (_state)
		{
			case State.Stay:
				character.EndAction();
				
			
				break;
			case State.Move:
				character.Move();		
				break;
			case State.MoveB:
				character.MoveBack();
				break;
			case State.Chase:
				Chase();				
				break;
			case State.Fight:
				Fight();
				
				break;
		}
	}
	void Fight()
	{
		if(!isChose) character.Attack();		
		Timer(4f);
		if (dis > 2)
		{
			_state = State.Chase;
			timer = 0;
			isChose = false;
		}
	}
	private void Chase()
	{
		
		if (target.transform.position.x > characterObj.transform.position.x)
		{
			characterObj.GetComponent<SpriteRenderer>().flipX = false;
		}
		else
		{
			characterObj.GetComponent<SpriteRenderer>().flipX = true;
		}	
		if (dis <2)
		{			
			_state = State.Fight;
		}
	}
	private void Timer(float time)
	{
		if (timer < time)
		{
			isChose = true;
			timer += Time.deltaTime;
		}
		else
		{
			isChose = false;
			timer = 0;
			if(_state == State.Stay) _state = State.Move;
		}
	}
}
public class Enemy01Move : MoveInterFace
{
	private float speed;
	private GameObject obj;
	private Animator anim;

	public Enemy01Move(float Speed, GameObject _obj)
	{
		obj = _obj;
		speed = Speed;
		anim = obj.GetComponent<Animator>();
	}

	public void Move(CharacterInterface character)
	{
		if(!character.CantMoveR) obj.transform.Translate(Vector2.right * speed * Time.deltaTime);
		obj.GetComponent<SpriteRenderer>().flipX =false;
		anim.SetBool("Move", true);
	}

	public void MoveBack(CharacterInterface character)
	{
		if (!character.CantMoveL) obj.transform.Translate(Vector2.left * speed * Time.deltaTime);
		obj.GetComponent<SpriteRenderer>().flipX = true;
		anim.SetBool("Move", true);
	}
}
public class EnemyTest01Attack : AttackInterFace
{
	private GameObject CharacterObj;
	private Animator anim;
	public EnemyTest01Attack(GameObject characterObj)
	{
		CharacterObj = characterObj;
		anim = characterObj.GetComponent<Animator>();
	}
	public void Attack(CharacterInterface character)
	{
		anim.SetBool("Attack", true);
		
	}
	public void EndAttack(CharacterInterface character)
	{
		anim.SetBool("Attack", false);

	}
}
public class EnemyTest01EndAtcion : EndActionBehavior
{
	private GameObject obj;
	private Animator anim;
	public EnemyTest01EndAtcion(GameObject _obj)
	{
		obj = _obj;
		anim = _obj.GetComponent<Animator>();
	}
	public void EndAction(CharacterInterface character)
	{
		anim.SetBool("Move",false);
	}

	public void EndIinvincible(CharacterInterface character)
	{
		
	}
}
public class EnemyTest01Onhit : OnHitInterFace
{
	private GameObject characterObj;
	private Animator anim;
	private CharacterInterface character;
	public EnemyTest01Onhit(GameObject _characterObj)
	{
		characterObj = _characterObj;
		anim = _characterObj.GetComponent<Animator>();
	}

	public void OnHit(CharacterInterface _character)
	{
		anim.SetBool("Hurt", true);
		character = _character;
		
	}
	public void EndHurt()
	{
		anim.SetBool("Hurt", false);
		if (character.Hp < 0) anim.SetBool("Death",true);
	}
}
public class EnemyTest01Dead : DeadBehaviour
{
	private GameObject characterObj;
	public EnemyTest01Dead(GameObject _characterObj)
	{
		characterObj = _characterObj;
		
	}
	public override void Dead(CharacterInterface character)
	{
		QuestEventSystem.EnemyDie(character);
		Destroy(characterObj);		
	}
}