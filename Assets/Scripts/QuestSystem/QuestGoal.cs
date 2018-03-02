using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestSystem
{
	public interface GoalInterface
	{
		bool Complete { get; set; }
		int TargetID { get; set; }
		int GoalAnim { get; set; }
		void GoalInit();
		void GoalProgess();
		void Finsh();
	}
}

