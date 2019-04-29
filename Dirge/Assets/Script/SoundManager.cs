using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private AudioSource[] sour;

	public static SoundManager instance;

    // Start is called before the first frame update
    void Awake()
    {
		instance = this ;
		sour = GetComponentsInChildren<AudioSource>();
    }

	public void play(AudioClip clip)
	{
		foreach(AudioSource src in sour)
		{
			if (src.isPlaying)
			{
				continue;
			}
			else
			{
				src.PlayOneShot(clip);
				break;
			}
			
		}
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
