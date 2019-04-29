using UnityEngine;
using System.Collections;

public class ItBehaviour : MonoBehaviour {

    public GameObject target;
    public float threshold = 0;

    public float moveFactor = 1 ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 diff = target.transform.position - transform.position;

        if (diff.magnitude >= threshold)
        {
            Vector3 newPosition = transform.position + moveFactor * diff;
            transform.position = newPosition;
        }
        
	}
}
