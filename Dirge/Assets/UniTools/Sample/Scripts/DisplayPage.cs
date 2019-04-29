using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(UIScreen))]
public class DisplayPage : MonoBehaviour {

    public Text t;

    UIScreen s;

	// Use this for initialization
	void Start () {
        s = GetComponent<UIScreen>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = "page " + (s.pageID + 1) + "/" + s.pageNumbers;
	}
}
