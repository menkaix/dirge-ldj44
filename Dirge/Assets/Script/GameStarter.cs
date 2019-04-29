using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
	public UIScreen inGame;
	public AudioClip clicClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0 || Input.GetMouseButton(0))
		{
			SoundManager.instance.play(clicClip);
			inGame.EnableMe();
		}
    }
}
