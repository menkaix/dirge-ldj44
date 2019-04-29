using UnityEngine;
using System.Collections;

public class UIScreen : MonoBehaviour {

	[HideInInspector]
	public UIScreenManager uiManager ;

	[HideInInspector]
    public int pageID;
	[HideInInspector]
    public int pageNumbers;

	public bool backOnEscape = true;

	// Use this for initialization
	void Start () {

		uiManager = transform.parent.gameObject.GetComponent<UIScreenManager>();

		if(uiManager == null)
		{
			Debug.LogWarning("UIScreenManager not found in parent : fall back to findObjectOfType");

			uiManager = FindObjectOfType<UIScreenManager>();

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (backOnEscape && Input.GetKeyUp(KeyCode.Escape))
		{
			uiManager.HistoryBack();
		}

	}

    public void EnableMe()
    {
        uiManager.EnableScreen(this);
    } 
}
