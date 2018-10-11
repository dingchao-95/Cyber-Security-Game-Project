using UnityEngine;
using System.Collections;

public class MiniGamePortal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && GameManager.Instance.Points >= 100)
		{
			Application.LoadLevel(1);
		}
		else if(other.tag == "Player" && GameManager.Instance.Points < 100)
		{
			GameManager.Instance.Reset();
			Application.LoadLevel(2);
		}
	}
}
