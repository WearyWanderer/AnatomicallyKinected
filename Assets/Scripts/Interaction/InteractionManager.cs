using UnityEngine;
using System.Collections;
using MIG;

public class InteractionManager : MonoBehaviour 
{
	public SpeechManager speechRef;
	public GestureManager gestRef;
	public GameObject[] avatarLib;
	private Vector3 camOrigin;
	private Quaternion camRotOrigin;
	private bool capturingOn = false;

	public float magnitudeMultSwipe;
	public float magnitudeMultPullPush;

	// Use this for initialization
	void Start () 
	{
		camOrigin = Camera.main.transform.position;
		camRotOrigin = Camera.main.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		#region GestureIntegration
		if(!capturingOn)
		{
			switch (gestRef.currentGesture)
			{
			case GestureType.OneHandSwipe:
			{
				float gestMag = gestRef.gestureMagnitude * magnitudeMultSwipe;
				GameObject.FindGameObjectWithTag("Podium").rigidbody.AddTorque(Vector3.up * gestMag);
				break;
			}
			case GestureType.TwoHandedPull:
			case GestureType.TwoHandedPush:
			{
				float gestMag2 = gestRef.gestureMagnitude * magnitudeMultPullPush;
				if(isInRange((Camera.main.transform.position + Camera.main.transform.forward * gestMag2).z, -13f, -8f))
					Camera.main.transform.position += Camera.main.transform.forward * gestMag2;
				break;
			}
			case GestureType.ArmsOverHead:
			{
				if (speechRef.GetActionID() == actionIDs.quit)
				{
					Application.Quit();
				}
				break;
			}
			}
		}
		#endregion

		#region SpeechIntegration
		switch (speechRef.GetActionID ()) 
		{
		case actionIDs.doNothing:
		{
			break;
		}
		case actionIDs.switchSkel:
		{
			if(!capturingOn)
			{
			Camera.main.transform.position = camOrigin;
			Camera.main.transform.rotation = camRotOrigin;
			ReturnFullModel();
			Transform[] childrenToTurnOn = avatarLib[0].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOn)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = true;
			}
			Transform[] childrenToTurnOff = avatarLib[1].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			Transform[] childrenToTurnOff2 = avatarLib[2].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff2)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			}
			break;
		}
		case actionIDs.switchHuman:
		{	
			if(!capturingOn)
			{
			Camera.main.transform.position = camOrigin;
			Camera.main.transform.rotation = camRotOrigin;
			Transform[] childrenToTurnOn = avatarLib[1].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOn)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = true;
			}
			Transform[] childrenToTurnOff = avatarLib[0].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			Transform[] childrenToTurnOff2 = avatarLib[2].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff2)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			}
			break;
		}
		case actionIDs.switchMusc:
		{	
				if(!capturingOn)
				{
			Camera.main.transform.position = camOrigin;
			Camera.main.transform.rotation = camRotOrigin;
			Transform[] childrenToTurnOn = avatarLib[2].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOn)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = true;
			}
			Transform[] childrenToTurnOff = avatarLib[0].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			Transform[] childrenToTurnOff2 = avatarLib[1].GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff2)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			}
			break;
		}
		case actionIDs.showSkull:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("Skull"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showPelvis:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("Pelvis"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showLArm:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("LArm"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showRLeg:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("RLeg"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showLLeg:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("LLeg"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showRArm:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("RArm"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showRibs:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("Ribs"));
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.showSpine:
		{
			if(!capturingOn)
			{
			CameraSlideTo(GameObject.FindGameObjectWithTag("Spine"));
			GameObject.FindGameObjectWithTag("RibToHide").renderer.enabled = false;
			}
			break;
		}
		case actionIDs.showFull: //return to basic pos for cam
		{
			if(!capturingOn)
			{
			Camera.main.transform.position = camOrigin;
			Camera.main.transform.rotation = camRotOrigin;
			ReturnFullModel();
			}
			break;
		}
		case actionIDs.postureOn: //return to basic pos for cam
		{
			capturingOn = true;
			Camera.main.transform.position = camOrigin;
			Camera.main.transform.rotation = camRotOrigin;
			Transform[] childrenToTurnOn = GameObject.FindGameObjectWithTag("PostureSkel").GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOn)
			{
				if(child.gameObject.renderer != null)
				{
				child.gameObject.renderer.enabled = true;
				}
			}
			ReturnFullModel();
			Transform[] childrenToTurnOff = GameObject.FindGameObjectWithTag("Podium").GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			break;
		}
		case actionIDs.postureOff: //return to basic pos for cam
		{
			capturingOn = false;
			Transform[] childrenToTurnOn = GameObject.FindGameObjectWithTag("SkelMode").GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOn)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = true;

				GameObject.FindGameObjectWithTag("Podium").renderer.enabled = true;
			}
			ReturnFullModel();
			Transform[] childrenToTurnOff = GameObject.FindGameObjectWithTag("PostureSkel").GetComponentsInChildren<Transform>();
			foreach(Transform child in childrenToTurnOff)
			{
				if(child.gameObject.renderer != null)
					child.gameObject.renderer.enabled = false;
			}
			break;
		}
		}
		speechRef.parser.actionToBeEnacted = actionIDs.doNothing;
		#endregion
	}

	void CameraSlideTo(GameObject targetObj)
	{
		Vector3 targTrans = targetObj.transform.position;
		Vector3 targFWVec = targetObj.transform.forward;

		Camera.main.transform.position = targTrans - (1.1f * targFWVec);
		Camera.main.transform.LookAt(targetObj.transform);
	}

	void ReturnFullModel() //return all the model nodes to a visible state
	{
		if(GameObject.FindGameObjectWithTag("RibToHide").renderer.enabled == false)
		{
			GameObject.FindGameObjectWithTag("RibToHide").renderer.enabled = true;
		}
	}

	bool isInRange(float value, float min, float max)
	{
		if (value > min && value < max)
			return true;
		else
			return false;
	}
	
}
