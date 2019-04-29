using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

    public static bool isPaused = false;

	// Use this for initialization
	void Start () {
        StartCoroutine("checkPause");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseGame()
    {
        isPaused = !isPaused ;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    IEnumerator checkPause()
    {
        while (gameObject.activeInHierarchy)
        {
            if(isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

            yield return null;
        }
        
    }

}
