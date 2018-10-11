using UnityEngine;
using System.Collections;

public class RandomizeGoodPassword : MonoBehaviour {


	public string[] myLines = new string[5]; //string array with 5 strings


	// Use this for initialization
	void Start () 
	{

		myLines[0] = "HorseCabbage9";
		myLines[1] = "jayparkit";
		myLines[2] = "threesix9";
		myLines[3] = "eatmyc0ck";
		myLines[4] = "evil9ice";
		this.GetComponent<TextMesh>().text = myLines[Random.Range(0,4)];
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
