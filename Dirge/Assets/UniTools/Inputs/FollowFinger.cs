using UnityEngine;
using System.Collections;

public class FollowFinger : MonoBehaviour {

    public FollowFingerFilter filter;
    public int touchIndex = 0 ;
    public bool touchIsActive = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(touchIndex +1 <= Input.touchCount)
        {
            Vector2 touchPosition = Input.GetTouch(touchIndex).position;
            moveAccording(touchPosition);
            touchIsActive = true;
        }

        else
        {
            touchIsActive = false ;
        }

        if (Input.GetMouseButton(touchIndex))
        {
            Vector2 touchPosition = Input.mousePosition;
            moveAccording(touchPosition);
            touchIsActive = true;
        }

        else
        {
            touchIsActive = false;
        }


    }

    private void moveAccording(Vector2 screenPoint)
    {
        if(filter != null && !filter.TouchOn)
        {
            return;
        }

        Debug.Log("("+screenPoint.x+";"+screenPoint.y+")");

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, transform.position.z - Camera.main.transform.position.z));

        Vector3 newPosition = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        transform.position = newPosition;
        
    }
}
