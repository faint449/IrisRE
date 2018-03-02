using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Talk : GoalInterface
{
	public bool Complete { get; set; }
	public int TargetID { get; set; }
	public int GoalAnim { get; set; }
	public int EventID;
	public Goal_Talk(int npcID,int aimsLyricCount,int eventID)
	{
		TargetID = npcID;
		GoalAnim = aimsLyricCount;
		EventID = eventID;
		GoalInit();
	}
	public void GoalInit()
	{
		QuestEventSystem.NpcEventHandler += Talk;
	}
	public void GoalProgess()
	{
		Debug.Log(TargetID);
	}
	public void Finsh()
	{
		dialogueSystem.instance.DialogID = GoalAnim;
		Complete = true;
	}
	public void Talk(int ID,Quest quest)
	{
		if (ID == TargetID && quest.EventID == EventID)
			Finsh();
	}
}
