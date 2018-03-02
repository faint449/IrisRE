using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Character)),CanEditMultipleObjects]
public class EnumTest : Editor
{
	static List<string> layers;
	static string[] layerNames;
	public SerializedProperty
		CharacterEnum,
		EnemyEnum,
		talkCount,
		Speed_Prop;

	public Enemy.EnemyKinds e;
	void OnEnable()
	{
		CharacterEnum = serializedObject.FindProperty("_CharacterSort");
		Speed_Prop = serializedObject.FindProperty("Speed");
		talkCount = serializedObject.FindProperty("TalkCount");
	}


	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		Character script = (Character)target;
		EditorGUILayout.PropertyField(CharacterEnum);
		Character.CharacterSort st = (Character.CharacterSort)CharacterEnum.enumValueIndex;
		switch (st)
		{
			case Character.CharacterSort.player:
							
				break;
			case Character.CharacterSort.enemy:				
				script.enemyEnum = (Enemy.EnemyKinds)EditorGUILayout.EnumPopup("EnemyKind",script.enemyEnum);
				script.PartolLayer = LayerMaskField(script.PartolLayer,"PartolDontWorkLayer");
				script.PatrolLayRect = EditorGUILayout.RectField("PatrolLayRect", script.PatrolLayRect);				
				break;
			case Character.CharacterSort.npc:
				script.PartolLayer = LayerMaskField(script.PartolLayer, "TriggerDontWorkLayer");
				script.PatrolLayRect = EditorGUILayout.RectField("TriggerLayRect", script.PatrolLayRect);
				EditorGUILayout.PropertyField(talkCount,true);
				break;
		}
		script.RightLayRect = EditorGUILayout.RectField("RightLayRect", script.RightLayRect);
		script.LeftLayRect = EditorGUILayout.RectField("LeftLayRect", script.LeftLayRect);
		script.GroundLayRect = EditorGUILayout.RectField("GroundLayRect", script.GroundLayRect);
		script.Speed = EditorGUILayout.Slider("Speed", script.Speed, 0, 100);
		script.ID = EditorGUILayout.IntField("ID", script.ID);
		script.GID = EditorGUILayout.IntField("GID", script.GID);
		script.Hp = EditorGUILayout.IntField("Hp", script.Hp);
		script.Atk = EditorGUILayout.IntField("Atk", script.Atk);
		script.RayLayer = LayerMaskField(script.RayLayer, "DontWorkerLayer");
		serializedObject.ApplyModifiedProperties();
	}
	
	public static LayerMask LayerMaskField(LayerMask selected,string Name)
	{

		if (layers == null)
		{
			layers = new List<string>();
			layerNames = new string[4];
		}
		else
		{
			layers.Clear();
		}
		int emptyLayers = 0;
		for (int i = 0; i < 32; i++)
		{
			string layerName = LayerMask.LayerToName(i);

			if (layerName != "")
			{

				for (; emptyLayers > 0; emptyLayers--) layers.Add("Layer " + (i - emptyLayers));
				layers.Add(layerName);
			}
			else
			{
				emptyLayers++;
			}
		}

		if (layerNames.Length != layers.Count)
		{
			layerNames = new string[layers.Count];
		}
		for (int i = 0; i < layerNames.Length; i++) layerNames[i] = layers[i];
		selected.value = EditorGUILayout.MaskField(Name, selected.value, layerNames);
		return selected;
	}
}
