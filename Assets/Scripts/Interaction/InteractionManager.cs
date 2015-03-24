using UnityEngine;
using System.Collections;

public class InteractionManager : MonoBehaviour 
{
	public SpeechManager speechRef;
	public GestureManager gestRef;
	public MeshFilter avatar;

	public Mesh[] avatarLib;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (speechRef.GetActionID ()) 
		{
		case actionIDs.doNothing:
		{
			break;
		}
		case actionIDs.switchSkel:
		{
			avatar.mesh = avatarLib[0];
			break;
		}
		case actionIDs.switchHuman:
		{
			avatar.mesh = avatarLib[1];
			break;
		}
		case actionIDs.switchMusc:
		{
			avatar.mesh = avatarLib[2];
			break;
		}
		}
		
		speechRef.parser.actionToBeEnacted = actionIDs.doNothing;
	}
}
