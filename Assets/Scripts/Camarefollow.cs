using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camarefollow : MonoBehaviour
{
	public Transform target;//跟随的目标物体
	public float Xmargin = 0f; //設置角色與攝影機的X距離
	public float Ymargin = 3f; //設置角色與攝影機的Y距離
	public float xsmooth = 10f;
	public float ysmooth = 1f;
	public Vector2 maxXAndY;//攝影機可運動最大範圍
	public Vector2 minXAndY;//攝影機可運動最小範圍

	private Transform player; //獲得角色位置
	void Awake()
	{
		//player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	bool CheckXmargin()
	{
		return Mathf.Abs(transform.position.x - target.position.x) > Xmargin;
	}
	bool CheckYmargin()
	{
		return Mathf.Abs(transform.position.y - target.position.y) > Ymargin;
	}
	void Update()
	{
		TrackPlayer();
	}
	void TrackPlayer()
	{
		float targeX = transform.position.x;
		float targeY = transform.position.y;

		if (CheckXmargin())
		{
			targeX = Mathf.Lerp(transform.position.x, target.position.x, xsmooth * Time.deltaTime);
		}
		if (CheckYmargin())
		{
			targeY = Mathf.Lerp(transform.position.y, target.position.y, ysmooth * Time.deltaTime);
		}
		targeX = Mathf.Clamp(targeX, minXAndY.x, maxXAndY.x);
		targeY = Mathf.Clamp(targeY, minXAndY.y, maxXAndY.y);

		transform.position = new Vector3(targeX, targeY, transform.position.z);
	}
}
