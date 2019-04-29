using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
[RequireComponent(typeof(Animator))]
public class Ennemy : ActorBehaviour
{
	private int reincarnation = 0;
	bool pActiveAgent;
	private CapsuleCollider myCollider;

	NavMeshAgent agent;

	Animator anim;

	public bool activeAgent;
	public int lives = 10;

    // Start is called before the first frame update
    void Awake()
    {
		reincarnation = 0;

		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		myCollider = GetComponent<CapsuleCollider>();

		agent.isStopped = true;
		
    }

	private void OnEnable()
	{
		agent.destination = GameManager.randomTarget();
	}

	private IEnumerator damage()
	{
		myCollider.enabled = false;
		agent.isStopped = true;
		anim.SetTrigger("hit");

		yield return new WaitForSeconds(1.5f);

		lives--;

		if (lives > 0)
		{
			myCollider.enabled = true;
			agent.isStopped = false;
		}
		else
		{
			anim.SetTrigger("die");
			yield return new WaitForSeconds(3f);
			die();
		}
			

		

		
	}

	public void hit()
	{
		StartCoroutine(damage());
	}

	private void OnDisable()
	{
		activeAgent = false;
	}

	private IEnumerator activateAgent()
	{
		int t = UnityEngine.Random.Range(4, 20);
		for(int i = 0; i<t; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		
		activeAgent = true;
	}

	private void acivate()
	{
		
		agent.isStopped = false;
		anim.SetBool("walking", true);
		agent.destination = GameManager.randomTarget();
	}

	private void stop()
	{
		agent.isStopped = true;
		anim.SetBool("walking", false);
	}


	private void OnCollisionEnter(Collision collision)
	{
		Targets targ = collision.collider.GetComponent<Targets>();
		if (targ != null)
		{
			die();
		}

	}

	public override void init()
	{
		
	}

	public override void work()
	{
		if (activeAgent && !pActiveAgent)
		{
			acivate();
		}
		if (!activeAgent && pActiveAgent)
		{
			stop();
		}

		pActiveAgent = activeAgent;
	}

	public override void onBorn()
	{
		base.onBorn();
		transform.localPosition = Vector3.zero;
		StartCoroutine(activateAgent());
		reincarnation++;
		//Debug.Log("Rein " + reincarnation);
	}
}
