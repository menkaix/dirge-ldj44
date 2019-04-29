using UnityEngine;
using System.Collections;

/// <summary>
/// Is born, works, is killed or expires
/// </summary>

public abstract class ActorBehaviour : MonoBehaviour {


    public float expireTime ;

    public bool isAlive ;
    
    private float pTime;
    
    // Update is called once per frame
    void Update()
    {
        float cTime = Time.time;

        if (cTime - pTime >= expireTime)
        {
            expire();
        }

        if (isAlive)
        {
            work();
        }
            
    }

    public void expire()
    {
        die();
        onExpires();
    }

    public void die()
    {
        gameObject.SetActive(false);
        isAlive = false;
        init();
        onKilled();
        
    }


    public void born()
    {
        init();
        isAlive = true ;
        gameObject.SetActive(true);
        pTime = Time.time;
        onBorn();
    }

    public abstract void init();
    public abstract void work();

    public virtual void onExpires()
    {

    }

    public virtual void onKilled()
    {

    }

    public virtual void onBorn()
    {

    }


}
