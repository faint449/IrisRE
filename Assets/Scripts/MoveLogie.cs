using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveInterFace 
{
	void Move(CharacterInterface character);
	void MoveBack(CharacterInterface character);
}
public class EnemyTest1Move : MoveInterFace
{
	private float speed;
	private GameObject EnemyObj;
	public EnemyTest1Move(float Speed,GameObject charObj)
	{
		speed = Speed;
		EnemyObj = charObj;
	}
	public void Move(CharacterInterface character)
	{
		EnemyObj.transform.Translate(Vector2.right *speed * Time.deltaTime);
	}
	public void MoveBack(CharacterInterface character)
	{

	}
}

