using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
[RequireComponent(typeof(Animator))]
public class Ennemy : ActorBehaviour
{
	private int undeath = 0;
	private bool pActiveAgent;
	private CapsuleCollider myCollider;
	private int iniLives;

	NavMeshAgent agent;

	Animator anim;

	public bool activeAgent;
	public int lives = 10;

    // Start is called before the first frame update
    void Awake()
    {
		undeath = 0;
		iniLives = lives;
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

			GameManager.instance.hurt();
			die();
		}

	}

	public override void init()
	{
		
	}

	public override void work()
	{
		if (lives <= 0)
		{
			undeath++;
		}
		if (undeath > 100)
		{
			gameObject.SetActive(false);
		}

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
		lives = iniLives;
		undeath = 0;
		base.onBorn();
		transform.localPosition = Vector3.zero;
		StartCoroutine(activateAgent());
		
		//Debug.Log("Rein " + reincarnation);
	}
}
