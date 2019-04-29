using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombal : MonoBehaviour
{
	public int lives = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void hit()
	{
		lives--;

		if (lives <= 0)
		{
			GameManager.instance.levelUp();

			gameObject.SetActive(false);
		}

		
	}

}
