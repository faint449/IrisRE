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
public class dialogueSystem : MonoBehaviour
{
	private static dialogueSystem _instance;
	public static dialogueSystem instance
	{
		get
		{
			return _instance;
		}
	}
	public GameObject NormalObj, DialogObj, promptObj;
	public GameObject DialogueChoseObj;
	public GameObject ChoseButton;
	public Image LeftSprite, RightSprite;
	public Text Normallyric, Dialoglyric;
	public Canvas canvas;
	public int DialogID, DialogCount;
	public bool isTouch ;
	public delegate void Talk();
	public Talk talk;
	public List<Quest> QuestStartNpc = new List<Quest>();
	public List<Quest> QuestEndNpc = new List<Quest>();
	private enum State {notalk,begintalk,istalk}
	private State state;
	private string trl ;
	private string json ;
	private JObject jobj ;
	private JArray jarry;
	string[] TalkWay,lyric, Lsprite, Rsprite;
	
	public void Awake()
	{
		_instance = this;
		trl = Application.dataPath + "/lyricJson.txt";
		json = File.ReadAllText(trl);
		jobj = JObject.Parse(json);
		jarry = (JArray)jobj["LyricSheet"];
		state = State.notalk;
	}
	public void Showprompt(GameObject NpcTranform)
	{
		Vector2 pos;

		Vector3 Hpos = Camera.main.WorldToScreenPoint(NpcTranform.transform.position + new Vector3(0,1.5f, 0));
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform
			as RectTransform, Hpos, canvas.worldCamera, out pos) && state == State.notalk)
		{
			promptObj.GetComponent<RectTransform>().localPosition = pos;
			talk += Dialogue;
			isTouch = true;
		}
	}
	public void ByePrompt()
	{
		promptObj.GetComponent<RectTransform>().localPosition = new Vector3(999,999);
		talk = null;
		isTouch = false;
		state = State.notalk;
		EndDialogue();
	}
	public void DialogueTalkContent()
	{		
		for (int i = 0; i < jarry.Count; i++)
		{
			if ((int)jarry[i]["ID"] == DialogID)
			{
				//Dialoglyric.text = jarry[i]["String"].ToString();
				Rsprite = jarry[i]["RSprite"].ToString().Split('#');
				Lsprite = jarry[i]["LSprite"].ToString().Split('#');
				lyric = jarry[i]["String"].ToString().Split('#');
				TalkWay = jarry[i]["TalkWay"].ToString().Split('#');			
			}			
		}
	}
	
	public void Dialogue()
	{		
		if (state == State.notalk)
		{
			DialogueTalkContent();
			promptObj.GetComponent<RectTransform>().localPosition = new Vector3(999, 999);
			DialogObj.SetActive(true);
			state = State.begintalk;
			if (lyric.Count() > 1)
			{
				Dialogueing();
				talk += Dialogueing;
			}
			else
			{
				DeginTalk();
				talk += DeginTalk;
				DialogueChose();
			}
				
		}			

	}
	public void DeginTalk()
	{
		if (state != State.istalk)
		{
			Dialoglyric.text = lyric[0];
			if (Lsprite[0] != "null")
				LeftSprite.sprite = Resources.Load<Sprite>(Lsprite[0]);
			if (Rsprite[0] != "null")
				RightSprite.sprite = Resources.Load<Sprite>(Rsprite[0]);
			state = State.istalk;
		}
		else
		{
			EndDialogue();
		}
	}
	public void Dialogueing()
	{
		if (DialogCount < lyric.Count())
		{
			Dialoglyric.text = lyric[DialogCount];
			if (Lsprite[DialogCount] != "null")
				LeftSprite.sprite = Resources.Load<Sprite>(Lsprite[DialogCount]);
			if (Rsprite[DialogCount] != "null")
				RightSprite.sprite = Resources.Load<Sprite>(Rsprite[DialogCount]);
			state = State.istalk;
			DialogCount++;
		}
		else
		{
			EndDialogue();
		}
		
	}
	public void EndDialogue()
	{
		DialogObj.SetActive(false);
		talk = null;
		state = State.notalk;
		isTouch = false;
		DialogCount = 0;
		for (int i = 0; i < DialogueChoseObj.transform.childCount; i++)
		{
			GameObject obj = DialogueChoseObj.transform.GetChild(i).gameObject;
			Destroy(obj);
		}
		QuestStartNpc.Clear();
		QuestEndNpc.Clear();
	}
	public void DialogueChose()
	{
		for (int i = 0; i < QuestStartNpc.Count(); i++)
		{
			int j = i;						
			ChoseButton.GetComponentInChildren<Text>().text = "!"+j+QuestStartNpc[j].icon;			
			GameObject obj = Instantiate(ChoseButton, DialogueChoseObj.transform);
			obj.GetComponent<Button>().onClick.AddListener(() =>
			{				
				EventSystem.instance.eventDialouge(QuestStartNpc[j].EventID);
				DialogueTalkContent();
				EventSystem.instance.CatchQuest(QuestStartNpc[j]);
				EndDialogue();
				Dialogue();				
			});
		}
		for (int i = 0; i < QuestEndNpc.Count(); i++)
		{
			int j = i;
			ChoseButton.GetComponentInChildren<Text>().text = "?" + j + QuestEndNpc[j].icon;
			GameObject obj = Instantiate(ChoseButton, DialogueChoseObj.transform);
			obj.GetComponent<Button>().onClick.AddListener(() =>
			{
				EventSystem.instance.EventProgress(QuestEndNpc[j]);
				EventSystem.instance.eventDialouge(QuestEndNpc[j].EventID);
				DialogueTalkContent();
				EventSystem.instance.CatchQuest(QuestEndNpc[j]);			
				EndDialogue();
				Dialogue();
			});
		}
	}
}
