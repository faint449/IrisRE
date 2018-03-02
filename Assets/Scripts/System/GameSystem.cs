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

public class GameSystem : MonoBehaviour
{
	private static GameSystem _instance;
	public static GameSystem instance
	{
		get
		{			
			return _instance;
		}
	}
	public List<CharacterInterface> GameCharacterList = new List<CharacterInterface>();
	public GameObject OptionObj;
	public Action<CharacterInterface> characterHurt, characterDie;
	public Action< int, CharacterInterface> npcEventStatus;
	
	[HideInInspector]
	public KeyCode MoveF, MoveB, MoveU, MoveD, Jump, AtkKey, CheckKey;
	public Character _player;
	//private string trl, json, ss;
	private int DialogCount;
	private List<Quest> NpcQuest = new List<Quest>();
	private List<Quest> QuestGoals = new List<Quest>();
	private UnityAction myaction;
	public void Awake() 
	{
		_instance = this;
		
		LoadKeyBord();
	}
	void Update()
	{
		
		#region//按鍵設定
		if (Input.GetKey(MoveF))
		{
			_player.character.Move();
		}
		else if (Input.GetKey(MoveB))
		{
			_player.character.MoveBack();
		}
		else
		{
			_player.anim.SetBool("WalkB", false);
			_player.anim.SetBool("Walk", false);
		}
		if (Input.GetKeyDown(AtkKey))
		{
			_player.character.Attack();
		}
		if (Input.GetKeyDown(Jump))
		{
			_player.character.Jump();
		}
		if (Input.GetKeyDown(CheckKey))
		{
			
			if (dialogueSystem.instance.talk != null) dialogueSystem.instance.talk();	
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CallOption();
		}
		#endregion
	}	
	public void Battle(CharacterInterface attackCharacter, CharacterInterface defendCharacter)
	{
		defendCharacter.Hp -= attackCharacter.Atk;
		defendCharacter.OnHit();
	}
	void LoadKeyBord()
	{
		string trl = Application.dataPath + "/playerset.txt";
		string json = File.ReadAllText(trl);
		JObject jobj = JObject.Parse(json);
		JArray jarry = (JArray)jobj["PlayerSet"];
		MoveF = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["MoveF"].ToString());
		MoveB = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["MoveB"].ToString());
		MoveU = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["MoveU"].ToString());
		MoveD = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["MoveD"].ToString());
		Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["Jump"].ToString());
		AtkKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["AtkKey"].ToString());
		CheckKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), jarry[0]["CheckKey"].ToString());
	}
	public void CallOption()
	{
		if (!OptionObj.activeSelf)
			OptionObj.SetActive(true);
		else
			OptionObj.SetActive(false);
	}
	
}
