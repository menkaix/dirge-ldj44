using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UIScreen))]
public class UIScreenEditor : Editor {


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UIScreen screen = (UIScreen)target;

        Transform parent = screen.transform.parent;

        if (parent == null ||  parent.GetComponent<UIScreenManager>() == null)
        {
            Debug.LogError("UIScreen : no UIScreenManager found in parent !");
            
            EditorGUILayout.LabelField("!!!!! No UIScreenManager found in parent !!!!!");
            return;
        }


        if (GUILayout.Button("Solo Page"))
        {
            


            int chidCount = parent.childCount;

            for (int i = 0; i < chidCount; i++)
            {
                UIScreen tScreen = parent.GetChild(i).GetComponent<UIScreen>();

                if (tScreen != null)
                {
                    if (tScreen.enabled)
                    {
                        if(tScreen.GetInstanceID() == screen.GetInstanceID())
                        {
                            tScreen.gameObject.SetActive(true);
                        }
                        else
                        {
                            tScreen.gameObject.SetActive(false);
                        }
                    }
                }
            }


        }
        

    }

}
