using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public enum actionIDs
{
	doNothing = -1,
	switchSkel = 0,
	switchMusc = 1,
	switchHuman = 2,
	showSkull = 3,
	showPelvis = 4,
	showLArm = 5,
	showRLeg = 6,
	showLLeg = 7,
	showRArm = 8,
	showRibs = 9,
	showSpine = 10,
	showFull = 11,
	postureOn = 12,
	postureOff = 13,
	quit = 14
};

public class SpeechInterpreter : MonoBehaviour 
{
	string start = "";
	System.Diagnostics.Process speechEXE;

	public actionIDs actionToBeEnacted = actionIDs.doNothing;
	// Use this for initialization
	void Start() 
	{ 
		start = Environment.CurrentDirectory + "\\";
		//run the deletion check once on startup to prevent the previous loaded recog from being enacted
		if (File.Exists(start + "commandStream.txt")) 
		{
			//enact the relevant integer check here
			File.Delete(start + "commandStream.txt");
		}
		//start up the speech recog application
		speechEXE = System.Diagnostics.Process.Start(Application.dataPath + "\\SpeechRecognitionServer_AnatomicallyKinected.exe");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (File.Exists (start + "commandStream.txt")) 
		{
			actionToBeEnacted = (actionIDs) Convert.ToInt32(File.ReadAllText(start + "commandStream.txt"));
			//enact the relevant integer check here
			File.Delete (start + "commandStream.txt");
		} 
		else
		{
			actionToBeEnacted = actionIDs.doNothing;
		}

	}

	void OnApplicationQuit()
	{
		speechEXE.Kill ();
	}

	public actionIDs GetActionID()
	{
		return actionToBeEnacted;
	}
	
}
