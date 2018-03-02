using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace QuestSystem
{
	public interface QuestInterFace
	{
		List<GoalInterface> Goals { get; set; }
		int EventID { get; set; }
		int State { get; set; }
		int StartNpc { get; set; }
		int EndNpc { get; set; }
		int Sort { get; set; }
		string icon { get; set; }
		bool Complete { get; set; }
		void QuestProgess();
		void QuestComplete();
	}
	public static class QuestEventSystem
	{
		public static Action<CharacterInterface> EnemyEventHandler;
		public static Action<int,Quest> NpcEventHandler;
		public static List<QuestInterFace> questList { get; set; }
		public static void EnemyDie(CharacterInterface enemy)
		{
			if (EnemyEventHandler != null)
			{
				EnemyEventHandler(enemy);
				Debug.Log(enemy._ID);
			}
		}
		public static void NpcTalk(int npcID,Quest quest)
		{
			if (NpcEventHandler != null)
			{
				NpcEventHandler(npcID, quest);
				Debug.Log("123456");
			}
		}
	}
	public class Quest : QuestInterFace
	{
		public bool Complete { get; set; }
		public int EventID	{ get; set; }
		public int StartNpc { get; set; }
		public int EndNpc { get; set; }
		public int State { get; set; }
		public int Sort { get; set; }
		public string icon { get; set; }
		public List<GoalInterface> Goals { get; set; }
		public Quest(int eventID,int sort,int state,int startNpc,int endNpc,string icon)
		{
			EventID = eventID;
			Sort = sort;
			State = state;
			StartNpc = startNpc;
			EndNpc = endNpc;

			Goals = new List<GoalInterface>();
			this.icon = icon;
		}
		public void QuestProgess()
		{
			foreach (GoalInterface goal in Goals)
			{
				goal.GoalProgess();
				Complete = goal.Complete;
				if (!goal.Complete)
					break;
			}
			//if (Sort == 0 && State==1) Complete = true;
			QuestComplete();
		}
		public void QuestComplete()
		{
			if (Complete)
			{
				State = 2;
				Debug.Log("Quest:"+State);
			}
		}
	}
}


