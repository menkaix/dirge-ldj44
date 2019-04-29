using UnityEngine;
using System.Collections;

public class DisplayDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetKeyUp(KeyCode.E))
		//{

			
		//	UIScreenManager manager = FindObjectOfType<UIScreenManager>();

		//	if(manager != null)
		//	{
		//		Debug.Log("call dialog");

		//		manager.GenericDialog(DialogType.YES_NO_CANCEL,"Hello Dialog","Bla bla tsoin tsoin ?", work);
		//	}
		//	else
		//	{
		//		Debug.LogError("manager not found");
		//	}

		//}

	}

	public void work(DialogAnswer ans)
	{
		Debug.Log("you clicked : "+ans);
	}
}
