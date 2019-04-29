using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

	private float lastFire;
	private SoundManager soundMan;

	public float period = 0.333f;

	public AudioClip gunSound;

	public GameObject FX;

	private IEnumerator playFX()
	{
		FX.SetActive(true);

		if (soundMan != null) soundMan.play(gunSound);

		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		FX.SetActive(false);

	}

    // Start is called before the first frame update
    void Awake()
    {
		FX.SetActive(false);
		soundMan = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
		
    }


	private void bang()
	{
		StartCoroutine(playFX());

		RaycastHit hit;

		if (Physics.SphereCast(transform.position + transform.forward,0.25f,transform.forward, out hit))
		{
			Ennemy ennemy = hit.collider.GetComponent<Ennemy>();
			if(ennemy != null)
			{
				ennemy.hit();
			}

			Tombal tombal = hit.collider.GetComponent<Tombal>();
			if (tombal != null)
			{
				tombal.hit();
			}
		}
	}

	public void fire()
	{
		float now = Time.time;
		if(now - lastFire > period)
		{
			bang();

			lastFire = now;
		}
	}
}
