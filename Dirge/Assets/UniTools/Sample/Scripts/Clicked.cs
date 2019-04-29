using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Clicked : MonoBehaviour, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + " > I was clicked !");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
