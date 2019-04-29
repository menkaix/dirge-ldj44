using UnityEngine;
using System.Collections;

public  abstract class LoopBehaviour : MonoBehaviour {

	public float period ;
	public int loop ;

    protected float cLoop ;

    private float pTime ;
    private bool triggered = false ;

	// Use this for initialization
	void Start () {
        reset();
	}
	
	// Update is called once per frame
	void Update () {
		float cTime = Time.time;

		if (cTime - pTime >= period && cLoop != 0) {

            cLoop-- ;

            if (cLoop < 0)
                cLoop = -1 ;
                      
            action ();

            if(cLoop%2 == 0)
            {
                onEven();
            }
            else
            {
                onOdd();
            }



			pTime = cTime ;
            
		}
        if(cLoop == 0 && !triggered)
        {
            onZero();
            triggered = true ;
        }
	}

    public void reset()
    {
        pTime = Time.time;
        cLoop = loop;
        onIgnite();
    }

	public abstract void action () ;

    public virtual void onIgnite()
    {

    }

    public virtual void onZero()
    {

    }

    public virtual void onEven()
    {

    }

    public virtual void onOdd()
    {

    }


}
