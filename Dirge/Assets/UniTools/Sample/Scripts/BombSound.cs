using UnityEngine;
using System.Collections;
using System;

public class BombSound : LoopBehaviour
{

    public AudioClip oddSound ;
    public AudioClip evenSound ;
    public AudioClip zeroSound;

    public override void action()
    {
        Debug.Log("cLoop = " + cLoop);
    }

    public override void onEven()
    {
        if(cLoop != 0)
            GetComponent<AudioSource>().PlayOneShot(evenSound);
    }

    public override void onOdd()
    {
        GetComponent<AudioSource>().PlayOneShot(oddSound);
    }

    public override void onZero()
    {
        GetComponent<AudioSource>().PlayOneShot(zeroSound);
    }


}
