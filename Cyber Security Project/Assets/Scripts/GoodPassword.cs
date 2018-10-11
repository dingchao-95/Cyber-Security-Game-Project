using UnityEngine;
using System.Collections;

public class GoodPassword : MonoBehaviour {

	public int pointsToGivePlayer;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

//	public void OnMouseDown()
//	{
//		GameManager.Instance.AddPoints(pointsToGivePlayer);
//		Destroy(this.gameObject);
//	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameManager.Instance.AddPoints(pointsToGivePlayer);
			Destroy(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
