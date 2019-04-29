using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UIScreenManager))]
public class UIScreenManagerEditor : Editor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Enable all pages"))
        {
            SetChildrenActive(true);
        }

        if (GUILayout.Button("Disable all pages"))
        {
            SetChildrenActive(false);
        }

    }

    private void SetChildrenActive(bool active)
    {
        UIScreenManager theTarget = (UIScreenManager)target;

        int chidCount = theTarget.transform.childCount;

        for (int i = 0; i < chidCount; i++)
        {
            UIScreen screen = theTarget.transform.GetChild(i).GetComponent<UIScreen>();

            if (screen != null)
            {
                if (screen.enabled)
                {
                    screen.gameObject.SetActive(active);
                }
            }
        }
        
    }
}
