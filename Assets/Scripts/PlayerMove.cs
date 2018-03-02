using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : RayCastBehaviour
{
	public override void CheckRightRay()
	{
		if (hitRight.collider != null)
		{
			if (hitRight.collider.tag == "wall")
			{
				character.CantMoveR = true;
			}
		}
		else
		{
			character.CantMoveR = false;
		}
		if (hitRight.collider != null && hitRight.collider.tag == "enemy" && character._State !=
			CharacterInterface.State.Hurt && !character.Iinvincible)
		{
			GameSystem.instance.Battle(hitRight.collider.GetComponent<Character>().character, character);
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
		}
		else
		{
			character.CantMoveL = false;
		}
		if (hitLeft.collider != null && hitLeft.collider.tag == "enemy" && character._State !=
			CharacterInterface.State.Hurt && !character.Iinvincible)
		{
			GameSystem.instance.Battle(hitRight.collider.GetComponent<Character>().character, character);
		}
	}
	public override void CheckGroundRay()
	{
		if (hitGround.collider != null)
		{
			if (hitGround.collider.tag == "ground" || hitGround.collider.tag == "wall")
			{
				if (character._State != CharacterInterface.State.Hurt && character._State != CharacterInterface.State.Fight)
					character._State = CharacterInterface.State.norml;
			}
			else if (character._State != CharacterInterface.State.Hurt)
			{
				character._State = CharacterInterface.State.Full;
			}
		}
		else if (character._State != CharacterInterface.State.Hurt)
		{
			character._State = CharacterInterface.State.Full;
		}
		if (hitGround.collider != null && hitGround.collider.tag == "enemy" && character._State !=
			CharacterInterface.State.Hurt && !character.Iinvincible)
		{
			GameSystem.instance.Battle(hitRight.collider.GetComponent<Character>().character, character);
		}
	}
	public override void CheckpatrolRay()
	{
		
	}
}
public class PlayerMove : MoveInterFace
{
	private float speed;
	private GameObject Player;
	public PlayerMove(float Speed,GameObject _Player)
	{
		speed = Speed;
		Player = _Player;
	}

	public void Move(CharacterInterface character)
	{
		if (character._State != CharacterInterface.State.Fight && character._State != CharacterInterface.State.Hurt)
		{
			if (!character.CantMoveR)
				Player.transform.Translate(Vector2.right * speed * Time.deltaTime);
			if (character._State != CharacterInterface.State.Jump)
			{
				Player.GetComponent<Animator>().SetBool("Walk", true);
				Player.GetComponent<Animator>().SetBool("WalkB", false);
			}
		}				
	}
	public void MoveBack(CharacterInterface character)
	{

		if (character._State != CharacterInterface.State.Fight && character._State != CharacterInterface.State.Hurt)
		{
			if (!character.CantMoveL)
				Player.transform.Translate(Vector2.left * speed * Time.deltaTime);
			if (character._State != CharacterInterface.State.Jump)
			{
				Player.GetComponent<Animator>().SetBool("Walk", false);
				Player.GetComponent<Animator>().SetBool("WalkB", true);
			}
		}
	}
}
public class PlayerJump : JumpInterface
{
	private GameObject Player;
	public PlayerJump(GameObject _Player)
	{
		Player = _Player;
	}
	public void Jump(CharacterInterface character)
	{
		if(character._State != CharacterInterface.State.Full && character._State != CharacterInterface.State.Hurt)
			Player.GetComponent<Animator>().SetBool("Jump", true);
	}
}
public class PlayerAttack:AttackInterFace
{
	private GameObject Player;
	public PlayerAttack(GameObject _Player)
	{
		Player = _Player;
	}
	public void Attack(CharacterInterface character)
	{
		if (character._State == CharacterInterface.State.norml)
		{
			Player.GetComponent<Animator>().SetBool("Atk1", true);
			character._State = CharacterInterface.State.Fight;
		}
	}
	public void EndAttack(CharacterInterface character)
	{
		character._State = CharacterInterface.State.norml;
		Player.GetComponent<Animator>().SetBool("Atk1", false);
	}
}
public class PlayerOnHit : OnHitInterFace
{
	private GameObject player;
	public PlayerOnHit(GameObject Player)
	{
		player = Player;
	}
	public void OnHit(CharacterInterface character)
	{
		if (character._State != CharacterInterface.State.Hurt)
		{
			player.GetComponent<Animator>().Play("Hurt");
			character.Iinvincible = true;
			character._State = CharacterInterface.State.Hurt;
			
		}
	}
	public void EndHurt()
	{

	}
}
public class PlayerEndAction : EndActionBehavior
{
	private GameObject player;
	private Animator anim;
	public PlayerEndAction(GameObject Player)
	{
		player = Player;
		anim = Player.GetComponent<Animator>();
	}
	public void EndAction(CharacterInterface character)
	{
		
		if (character._State == CharacterInterface.State.Hurt) anim.SetTrigger("Hurt");
		anim.Play("StandAnim");
		anim.SetBool("Full", false);
		anim.SetBool("Walk", false);
		anim.SetBool("WalkB", false);
		anim.SetBool("Atk1", false);
		anim.SetBool("Jump", false);
		character._State = CharacterInterface.State.norml;
	}
	public void EndIinvincible(CharacterInterface character)
	{
		anim.ResetTrigger("Hurt");
		character.Iinvincible = false;
	}
}
public class PlayerState : StateInterFace
{
	private Animator anim;
	public PlayerState(GameObject Player)
	{
		anim = Player.GetComponent<Animator>();
	}
	public void StateDo(CharacterInterface character)
	{
	
		switch (character._State)
		{
			case CharacterInterface.State.Full:
				anim.SetBool("Full", true);
				break;
			case CharacterInterface.State.norml:
				anim.SetBool("Full", false);
				break;
		}
	}
}