using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_InterFace
{
	public enum State { Stay,Move, MoveB, Chase,Fight }
	public State _state;
	public GameObject target;
	public GameObject characterObj;
	public abstract void AI();
}

