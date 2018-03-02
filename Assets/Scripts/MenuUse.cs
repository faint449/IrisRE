using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public class MenuUse : MonoBehaviour
{
	//public static KeyCode MoveR,MoveL;
	public GameObject MoveF, MoveB, MoveD, MoveU, Jump,AtkKey, CheckKey;
	public GameObject ShowOBJ;
	string KeyName;
	public bool isChange;
	Event e;
	string trl;
	string json;
	JObject jobj;
	JToken jt;
	void Start ()
	{
		trl = Application.dataPath + "/playerset.txt";
		json = File.ReadAllText(trl);
		jobj = JObject.Parse(json);
		jt = jobj["PlayerSet"];
		TestStart();
	}
	void OnGUI()
	{
		e = Event.current;
       
        if (e.isKey && isChange)
		{
			string s = e.keyCode.ToString();
			switch (KeyName)
			{
				case "MoveF":
					MoveF.GetComponentInChildren<Text>().text = s;
                    
                    jt[0]["MoveF"] = s;                   
                    break;
				case "MoveB":
					MoveB.GetComponentInChildren<Text>().text = s;
                    
                    jt[0]["MoveB"] = s;
                    break;
				case "MoveU":
					MoveU.GetComponentInChildren<Text>().text = s;
                    
                    jt[0]["MoveU"] = s;
                    break;
				case "MoveD":
					MoveD.GetComponentInChildren<Text>().text = s;
                    
                    jt[0]["MoveD"] = s;
                    break;
				case "Jump":
					Jump.GetComponentInChildren<Text>().text = s;
                    
                    jt[0]["Jump"] = s;
                    break;
				case "AtkKey":
					AtkKey.GetComponentInChildren<Text>().text = s;
					
					jt[0]["AtkKey"] = s;
					break;
				case "CheckKey":
					CheckKey.GetComponentInChildren<Text>().text = s;
					
					jt[0]["CheckKey"] = s;
					break;
			}
            jobj["PlayerSet"] = jt;
            string output = JsonConvert.SerializeObject(jobj, Formatting.Indented);
            File.WriteAllText(trl, output);
            ChangEnd();
			print(s);
		}
	}
	void Update ()
	{
		
			
	}
	void TestStart()
	{		
        MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
		MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
		MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
		MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
		Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
		AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
		CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
	}
	public void isCheck(string keyname)
	{
        KeyName = keyname;
		if (isChange)
		{
			print(KeyName);
			switch (KeyName)
			{
				case "MoveF":
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "MoveB":
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "MoveU":
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();	
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "MoveD":
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "Jump":
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "AtkKey":
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					CheckKey.GetComponentInChildren<Text>().text = jt[0]["CheckKey"].ToString();
					break;
				case "CheckKey":
					MoveF.GetComponentInChildren<Text>().text = jt[0]["MoveF"].ToString();
					MoveB.GetComponentInChildren<Text>().text = jt[0]["MoveB"].ToString();
					MoveU.GetComponentInChildren<Text>().text = jt[0]["MoveU"].ToString();
					MoveD.GetComponentInChildren<Text>().text = jt[0]["MoveD"].ToString();
					Jump.GetComponentInChildren<Text>().text = jt[0]["Jump"].ToString();
					AtkKey.GetComponentInChildren<Text>().text = jt[0]["AtkKey"].ToString();
					break;
			}

		}
		if (!isChange)
		{			
			isChange = true;
		}
	}
	public void ChangEnd()
	{
		MoveF.GetComponent<Button>().interactable = true;
		MoveB.GetComponent<Button>().interactable = true;
		MoveU.GetComponent<Button>().interactable = true;
		MoveD.GetComponent<Button>().interactable = true;
		Jump.GetComponent<Button>().interactable = true;
		AtkKey.GetComponent<Button>().interactable = true;
		CheckKey.GetComponent<Button>().interactable = true;
		TestStart();
		isChange = false;
	}
}
