using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class NpcTest01Ray : RayCastBehaviour
{
	private GameObject characterObj;
	private int[] TalkCount;
	private bool istouch = false;
	public NpcTest01Ray(GameObject _characterObj,int[] talkCount)
	{
		TalkCount = talkCount;
		characterObj = _characterObj;		
	}

	public override void CheckGroundRay()
	{
		
	}

	public override void CheckLeftRay()
	{
		
	}

	public override void CheckpatrolRay()
	{
		if (hitPatrol.collider != null)
		{
			if (hitPatrol.collider.tag == "Player")
			{
				if (!dialogueSystem.instance.isTouch)
				{
					EventSystem.instance.NpcHaveEvents(character);
					int i = Random.Range(0, 3);
					dialogueSystem.instance.DialogID = TalkCount[i];
					Debug.Log("123456");
				} 
				dialogueSystem.instance.Showprompt(characterObj);
				istouch = dialogueSystem.instance.isTouch;
			}
			
		}
		else
		{
			if(istouch) dialogueSystem.instance.ByePrompt();
			istouch = false;
		}
	}

	public override void CheckRightRay()
	{
		
	}
}
public class NpcTest01_AI : AI_InterFace
{
	public override void AI()
	{
		
	}
}
