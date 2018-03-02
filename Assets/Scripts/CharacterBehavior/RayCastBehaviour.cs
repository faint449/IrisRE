using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RayCastBehaviour
{
	public RaycastHit2D hitRight;
	public RaycastHit2D hitLeft;
	public RaycastHit2D hitGround;
	public RaycastHit2D hitPatrol;
	public CharacterInterface character;
	public abstract void CheckRightRay();
	public abstract void CheckLeftRay();
	public abstract void CheckGroundRay();
	public abstract void CheckpatrolRay();
}

