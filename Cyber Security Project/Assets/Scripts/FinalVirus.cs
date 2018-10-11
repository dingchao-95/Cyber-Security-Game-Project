using UnityEngine;
using System.Collections;

public class FinalVirus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.tag == "VirusDestroyer")
		{
			if(GameManager.Instance.Points >= 30)
			{
				Application.LoadLevel(1);
			}
			else if(GameManager.Instance.Points < 30)
			{
				GameManager.Instance.Reset();
				Application.LoadLevel(3);
			}
		}
	}
}
