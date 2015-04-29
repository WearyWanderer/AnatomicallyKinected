using UnityEngine;
using System.Collections;

public class AlignBoneBetweenNodes : MonoBehaviour {

	public Transform joint1;
	public Transform joint2;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Find midpoint between both nodes

		Vector3 midpoint = Vector3.Lerp (joint1.position, joint2.position, 0.5f);

		transform.position = midpoint;
	}
}
