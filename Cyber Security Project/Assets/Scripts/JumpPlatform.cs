using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class JumpPlatform : MonoBehaviour 
{
	public float JumpMagnitude = 20;

	public void ControllerEnter2D(CharacterController2D controller)
	{
		controller.setVerticalForce(JumpMagnitude);
	}
}
