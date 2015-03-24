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
	switchHuman = 2
};

public class SpeechInterpreter : MonoBehaviour 
{

	string start = "";

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
		System.Diagnostics.Process.Start(Application.dataPath + "\\SpeechRecognitionServer_AnatomicallyKinected.exe");
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

	}

	public actionIDs GetActionID()
	{
		return actionToBeEnacted;
	}
	
}
