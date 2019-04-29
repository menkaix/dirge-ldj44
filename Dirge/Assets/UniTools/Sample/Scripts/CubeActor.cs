using UnityEngine;
using System.Collections;


public class CubeActor : ActorBehaviour
{

    public override void init()
    {
        transform.position = new Vector3((Random.value - Random.value) * 10, 50, (Random.value - Random.value) * 10);
    }

    public override void work()
    {
        
    }
}
