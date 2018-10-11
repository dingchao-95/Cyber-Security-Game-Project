using UnityEngine;
using System.Collections;

public class RandomizeBadPassword : MonoBehaviour {

	string[] myLines = new string[5]; //string array with 5 strings
	
	
	// Use this for initialization
	void Start () 
	{
		
		myLines[0] = "badpassword";
		myLines[1] = "lalala";
		myLines[2] = "123456";
		myLines[3] = "ryantin";
		myLines[4] = "keithgoh";
		this.GetComponent<TextMesh>().text = myLines[Random.Range(0,4)];
	}
	// Update is called once per frame
	void Update () {
	
	}
}
