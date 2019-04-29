using UnityEngine;
using System.Collections;

public class SphereActor : ActorBehaviour
{

    public override void init()
    {
        transform.position = new Vector3((Random.value - Random.value) * 10, 50, (Random.value - Random.value) * 10);
    }

    public override void work()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        SphereActor sa = collision.contacts[0].otherCollider.gameObject.GetComponent<SphereActor>();

        if (sa !=  null)
        {
            sa.die();
        }

    }
}
