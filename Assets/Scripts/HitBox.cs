using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	public Rect HitRect;
	public LayerMask RayLayer;
	public RaycastHit2D hitBox;
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube((Vector2)transform.position + HitRect.center, HitRect.size);
	}
	public Character atkCharacter;
	public void Update()
	{		
		hitBox = Physics2D.BoxCast((Vector2)transform.position + HitRect.center, HitRect.size, 0, new Vector2(0, 0), 0, ~RayLayer);
		if (hitBox.collider != null )
		{
			
			CharacterInterface defendCharacter = hitBox.collider.GetComponent<Character>().character;
			GameSystem.instance.Battle(atkCharacter.character, defendCharacter);
		}
	}
}
