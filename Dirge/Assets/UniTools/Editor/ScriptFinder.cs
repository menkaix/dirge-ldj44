using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;


public class ScriptFinder : EditorWindow
{
    static ScriptFinder window;

    static string scriptValue = "";
    static string oldScriptValue;

    static List<Type> components;
    static List<string> componentNames;
    static List<GameObject> sceneObjects;
    static int selectedIndex = 0;
    static int prevIndex = 0;
    static Vector2 scrollValue = Vector2.zero;

    //public 
    // Use this for initialization
    [MenuItem("Unitools/Script Finder")]
    static void OpenScriptFinder()
    {
        //EditorUtility.FocusProjectWindow();        
        window = (ScriptFinder)EditorWindow.GetWindow(typeof(ScriptFinder));

        /*
        Assembly _asm = Assembly.GetAssembly(typeof(AnimationComponent));

        AssemblyName _asmName =  _asm.GetName();

        Debug.Log(_asmName.FullName);

        */

        Assembly _assembly = Assembly.Load("Assembly-CSharp");

        components = new List<Type>();
        componentNames = new List<string>();

        foreach (Type type in _assembly.GetTypes())
        {
            if (type.IsClass)
            {
                if (type.BaseType.FullName.Contains("MonoBehaviour"))
                {
                    components.Add(type);
                    componentNames.Add(type.Name);
                    //                    Debug.Log(type.Name);
                }
                else
                {
                    if (!type.BaseType.FullName.Contains("System"))
                    {
                        Type _type = type.BaseType;
                        components.Add(_type);
                        componentNames.Add(type.Name);
                        //                        Debug.Log(type.Name);
                    }
                }
            }
        }


        prevIndex = selectedIndex;
        SelectScript(components[selectedIndex]);

    }

    static void SelectScript(Type _type)
    {

        Debug.Log("Selected Type: " + _type);

        sceneObjects = new List<GameObject>();
        //        Debug.Log(GameObject.FindSceneObjectsOfType(typeof(GameObject)).Length);
        foreach (UnityEngine.Object _obj in GameObject.FindObjectsOfType(_type))
        {
            Debug.Log(_obj.name);

            sceneObjects.Add(GameObject.Find(_obj.name));
        }

        Selection.objects = sceneObjects.ToArray();
    }


    void OnGUI()
    {
        selectedIndex = EditorGUILayout.Popup(selectedIndex, componentNames.ToArray());

        if (selectedIndex != prevIndex)
        {
            SelectScript(components[selectedIndex]);
        }

        scrollValue = EditorGUILayout.BeginScrollView(scrollValue);

        foreach (GameObject _obj in sceneObjects)
        {

            if (GUILayout.Button(_obj.name, GUIStyle.none))
            {
                Selection.activeObject = _obj;
                EditorGUIUtility.PingObject(_obj);
            }

        }

        EditorGUILayout.EndScrollView();

        prevIndex = selectedIndex;
    }
}