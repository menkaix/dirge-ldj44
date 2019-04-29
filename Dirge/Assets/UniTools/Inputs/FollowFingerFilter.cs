using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class FollowFingerFilter : MonoBehaviour {

    

    public bool touchOn;

    public bool TouchOn
    {
        get
        {
            return touchOn;
        }
    }
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void touchDown()
    {
        touchOn = true;
    }
    public void touchUp()
    {
        touchOn = false;
    }

}
