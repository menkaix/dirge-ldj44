using UnityEngine;
using System.Collections.Generic;
using System;

public class Spawner : LoopBehaviour
{

    [Serializable]
    public class SpawnUplet
    {
        
        public GameObject model;        
        public int weight;

        [HideInInspector]
        public float lowerLimit;
        [HideInInspector]
        public float higherLimit;

    }

    public SpawnUplet[] elements;

    private int cml = 0 ;
    //private int poolSize ;

    Dictionary<string,List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    Dictionary<string, int> lastIndexes = new Dictionary<string, int>();

	private void onLowMemory()
	{
		Debug.LogError("Low Memory Panic !");

		Destroy(gameObject);
	}

    public override void onIgnite()
    {
		base.onIgnite();

		Application.lowMemory += onLowMemory;		

        foreach (SpawnUplet uplet in elements)
        {
            uplet.lowerLimit = cml;
            uplet.higherLimit = cml + uplet.weight;
            cml += uplet.weight;
        }

        foreach (SpawnUplet uplet in elements)
        {
            if (uplet.model != null)
            {
                ActorBehaviour actorBehaviour = uplet.model.GetComponent<ActorBehaviour>();
                
                int poolSize = 5 + (int)((uplet.weight/cml) / (actorBehaviour.expireTime * period)) ;

                Debug.Log("Actor type : " + actorBehaviour.GetType().Name + "("+poolSize+")");

                List <GameObject> pool = new List<GameObject>();

                for(int i =0 ; i<poolSize ; i++)
                {
                    GameObject go = GameObject.Instantiate<GameObject>(uplet.model);
                    
                    go.transform.parent = transform ;
                    go.SetActive(false);

                    pool.Add(go);
                }

                pools.Add(actorBehaviour.GetType().Name, pool);
                lastIndexes.Add(actorBehaviour.GetType().Name,0);
            }
            
        }
    }


    public override void action()
    {
        float randi = UnityEngine.Random.value * cml;

        foreach (SpawnUplet uplet in elements)
        {
            if(uplet.lowerLimit<= randi && randi < uplet.higherLimit)
            {
                if(uplet.model != null)
                {
                    //GameObject go = GameObject.Instantiate<GameObject>(uplet.model);
                    //go.transform.parent = transform ;

                    ActorBehaviour actorBehaviour = uplet.model.GetComponent<ActorBehaviour>();
                    if(actorBehaviour != null)
                    {
                        Debug.Log("Spawn " + actorBehaviour.GetType().Name);
                        spawn(actorBehaviour.GetType().Name);
                    }
                    

                }
            }
        }
    }

    private void spawn(string className)
    {
        List<GameObject> pool;
        int lastIndex;

        if (pools.TryGetValue(className, out pool) && lastIndexes.TryGetValue(className, out lastIndex))
        {
            for(int i=0; i<pool.Count; i++)
            {
                GameObject go = pool[(lastIndex + i) % pool.Count];

                ActorBehaviour ab = go.GetComponent<ActorBehaviour>();
                if(ab != null)
                {
                    if (!ab.isAlive)
                    {
                        ab.born();
                        lastIndexes[className] = (lastIndex + i) % pool.Count;
                        return;
                    }
                }else
                {
                    Debug.LogError("Illegal element in pool");
                }
            }

            Debug.LogWarning("pool exhausted");

            GameObject newGO = GameObject.Instantiate<GameObject>(pool[0]);

            newGO.transform.parent = transform;
            
            pool.Add(newGO);

            newGO.GetComponent<ActorBehaviour>().born();

            lastIndexes[className] = 0;
        }
    }

}
