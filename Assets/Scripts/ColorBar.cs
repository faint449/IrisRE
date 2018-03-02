using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBar : MonoBehaviour
{
	private Canvas canvas;
	public GameObject ColorSider;
	void Start ()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}
	void Update ()
	{
		Vector2 pos;
		Vector3 Hpos = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(0, 1.5f, 0));
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform
			as RectTransform, Hpos, canvas.worldCamera, out pos))
		{
			ColorSider.GetComponent<RectTransform>().localPosition = pos;
		}
	}
}
