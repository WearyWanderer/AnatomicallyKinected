using UnityEngine;
using System.Collections;
using System.IO;

public class SpeechManager : MonoBehaviour
{
		public SpeechInterpreter parser;
		// Use this for initialization
		void Start ()
		{
		parser = gameObject.AddComponent<SpeechInterpreter>();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (parser.GetActionID() != actionIDs.doNothing)
			{
			Debug.Log ("Do stuff Yay");
			}
		}

		public actionIDs GetActionID()
		{
			return parser.GetActionID();
		}
}
