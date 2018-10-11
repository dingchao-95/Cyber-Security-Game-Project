using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour 
{
	public void Update()
	{
		GameManager.Instance.Reset();
	}

	public void OnMouseDown()
	{
		Application.LoadLevel(1);
	}

}
