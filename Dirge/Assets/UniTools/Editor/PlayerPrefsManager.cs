using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [MenuItem("Unitools/PlayerPrefs/Clear")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
