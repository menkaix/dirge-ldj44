using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour {

    Vector3 dragFrom ;
    Vector3 dragTo ;
    Vector3 cameraFrom ;
    Vector3 cameraTo ;
    Vector2 screenFrom ;
    Vector2 screenTo ;
    
    Camera cam;

    public float threshold = 0;

    public float moveFactor = 1;

    public float scrollSpeed = 1;

    // Use this for initialization
    void Start () {
        
        cam = GetComponent<Camera>();

        if(cam == null)
        {
            cam = Camera.main;
        }

        cameraTo = cam.transform.position ;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            Touch tch  = Input.GetTouch(0);
            if(tch.phase == TouchPhase.Began)
            {
                begindrag(tch.position);
            }
            if(tch.phase == TouchPhase.Moved)
            {
                drag(tch.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            begindrag(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            drag(Input.mousePosition);   
        }

        Vector3 deltacam = cameraTo - cam.transform.position;

        if (deltacam.magnitude >= threshold)
        {
            Vector3 newPosition = transform.position + moveFactor * deltacam;
            transform.position = newPosition;
        }


    }

    void begindrag(Vector2 pos)
    {
        Debug.Log("v" + pos.x + " ; " + pos.y);
        cameraFrom = cam.transform.position;
        dragFrom = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, cam.nearClipPlane));
        screenFrom = pos;
    }

    void drag(Vector2 pos)
    {
        screenTo = pos ;

        dragTo = cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, cam.nearClipPlane));

        Vector3 deltaDrag = -(dragTo - dragFrom) * scrollSpeed;
        //Vector2 deltaScreen = -(screenTo - screenFrom) * scrollSpeed ;
        
        //Debug.Log(deltaScreen.magnitude +"--"+deltaDrag.magnitude);
        
        cameraTo = cameraFrom + (deltaDrag);
        
        cameraTo.y = cameraFrom.y;

        cameraFrom = cameraTo ;
        dragFrom = dragTo;
        screenFrom = screenTo;



    }
}
