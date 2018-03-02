using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using QuestSystem;


public class EventSystem : MonoBehaviour
{
	private static EventSystem _instance;
	public static EventSystem instance
	{
		get
		{
			return _instance;
		}
	}
	private List<Quest> questList = new List<Quest>();
	string trl;
	string json;
	JObject jobj;
	JArray jarry;
	public void Awake()
	{
		_instance = this;
		ReadEventJson();
		trl = Application.dataPath + "/eventTest.txt";
		json = File.ReadAllText(trl);
		jobj = JObject.Parse(json);
		jarry = (JArray)jobj["eventSheet"];
	}
	public void ReadEventJson()
	{
		trl = Application.dataPath + "/eventTest.txt";
		json = File.ReadAllText(trl);
		jobj = JObject.Parse(json);
		jarry = (JArray)jobj["eventSheet"];
		for (int i = 0; i < jarry.Count; i++)
		{
			int ID = (int)jarry[i]["ID"];
			int State = (int)jarry[i]["State"];
			int Sort = (int)jarry[i]["Sort"];
			string[] npc = jarry[i]["NpcID"].ToString().Split('/');
			string icon = jarry[i]["icon"].ToString();			
			Quest quest = new Quest(ID, Sort, State, int.Parse(npc[0]), int.Parse(npc[1]), icon);
			string[] aims = jarry[i]["Aims"].ToString().Split('/');
			string[] target = jarry[i]["Target"].ToString().Split('/');
			GoalInterface goal;
			for (int j = 0; j < aims.Length; j++)
			{
				switch ((int)jarry[i]["Sort"])
				{
					case 1:
						goal = new Goal_Kill(int.Parse(target[j]), int.Parse(aims[j]));
						quest.Goals.Add(goal);
						Debug.Log(int.Parse(aims[j]));
						break;
					case 2:
						goal = new Goal_Talk(int.Parse(target[j]), int.Parse(aims[j]), ID);
						quest.Goals.Add(goal);
						break;
					
				}
			}
			questList.Add(quest);
		}
	}
	public void ReWriteEventJson(int ID, string kind, int content)
	{
		JToken jt = jobj["eventSheet"];
		for (int i = 0; i < jarry.Count; i++)
		{
			if ((int)jarry[i]["ID"] == ID)
			{
				jt[i][kind] = content;
				jobj["eventSheet"] = jt;
				string output = JsonConvert.SerializeObject(jobj, Formatting.Indented);
				File.WriteAllText(trl, output);
			}
		}
	}
	public void eventDialouge(int ID)
	{
		for (int i = 0; i < jarry.Count; i++)
		{
			if ((int)jarry[i]["ID"] == ID)
			{
				print("ID");
				string[] dialouge = jarry[i]["Dialog"].ToString().Split('/');
				foreach (Quest quest in questList)
				{
					if(quest.EventID == ID)
						dialogueSystem.instance.DialogID = int.Parse(dialouge[quest.State]);
				}			
			}
		}
	}
	public void NpcHaveEvents(CharacterInterface npc)
	{
	
		foreach (Quest quest in questList)
		{
			if (quest.StartNpc == npc._ID && quest.State == 0)
			{
				dialogueSystem.instance.QuestStartNpc.Add(quest);
				
			}
			if (quest.EndNpc == npc._ID && quest.State > 0 && quest.State <2)
			{
				dialogueSystem.instance.QuestEndNpc.Add(quest);

			}
			/*for (int i=0;i<quest.Goals.Count;i++)
			{
				if (quest.Goals[i].TargetID == npc._ID && quest.State >0 && !quest.Goals[i].Complete)
				{
					
				}
			}*/
		}

	}

	public void CatchQuest(Quest quest)
	{
		for(int i=0;i<questList.Count;i++)
		{
			if (quest.EventID == questList[i].EventID)
			{
				if(questList[i].State == 0)
					questList[i].State = 1;
				Debug.Log(questList[i].State);
				
			}
		}
	}
	public void EventProgress(Quest quest)
	{
		for (int i = 0; i < questList.Count; i++)
		{
			if (quest.EventID == questList[i].EventID)
			{
				questList[i].QuestProgess();
				print(questList[i].State);
				if (questList[i].Sort == 0)
					questList[i].State = 2;
			}
		}		
	}
}
