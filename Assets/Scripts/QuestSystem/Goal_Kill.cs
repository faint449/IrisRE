using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
	public class Goal_Kill : GoalInterface
	{
		public bool Complete { get; set; }
		public int TargetID { get; set; }
		public int GoalAnim { get; set; }
		protected int GoalSchedule;

		public Goal_Kill(int enemyID,int goalAnim)
		{
			this.TargetID = enemyID;
			this.GoalAnim = goalAnim;
			GoalInit();
		}

		public void GoalInit()
		{
			QuestEventSystem.EnemyEventHandler += EnemyDie;

		}
		public void GoalProgess()
		{
			Debug.Log("莎莎莎莎");
			if (GoalSchedule >= GoalAnim)
			{
				Finsh();
			}
		}
		public void Finsh()
		{
			Complete = true;
			Debug.Log("達成任務條件");
		}
		public void EnemyDie(CharacterInterface enemy)
		{
			if (enemy._ID == this.TargetID)
			{
				GoalSchedule++;
				GoalProgess();
			}
		}
	}

}

