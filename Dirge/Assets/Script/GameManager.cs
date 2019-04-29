using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private SoundManager soundMan;
	private Canon[] canons;
	public int lives = 20;

	int c = 0;

	private Camera cam;


	public static GameManager instance;

	public static Targets[] targets;

	public AudioClip roarSound;

	public Spawner[] spawners;
	public GameObject[] ennemies;

	public static Vector3 randomTarget()
	{
		int i = UnityEngine.Random.Range(0, targets.Length - 1);

		return targets[i].transform.position;
	}

	internal void levelUp()
	{
		List<Spawner> lst = new List<Spawner>();

		foreach (Spawner spw in spawners)
		{
			if (!spw.gameObject.activeInHierarchy)
			{
				lst.Add(spw);
			}
		}

		if (lst.Count > 0)
		{
			int i = UnityEngine.Random.Range(0, lst.Count - 1);
			int j = UnityEngine.Random.Range(0, lst.Count - 1);

			lst[i].gameObject.SetActive(true);
			lst[j].gameObject.SetActive(true);

			StartCoroutine(playSound());
		}

	}

	IEnumerator playSound(){

		if (soundMan != null) soundMan.play(roarSound);

		Vector3 initPos = cam.transform.position;

		yield return new WaitForSeconds(0.33f);

		for (int i = 0; i< 33; i++)
		{
			cam.transform.Translate((UnityEngine.Random.value- UnityEngine.Random.value)*0.25f, (UnityEngine.Random.value- UnityEngine.Random.value)*.25f, 0);
			yield return new WaitForSeconds(0.05f);
		}
		cam.transform.position = initPos;
	}

	public static GameObject randdomEnnemy()
	{

		int i = UnityEngine.Random.Range(0, instance.ennemies.Length - 1);

		return instance.ennemies[i];
	}

    // Start is called before the first frame update
    void Awake()
    {
		soundMan = FindObjectOfType<SoundManager>();

		lives = 20;

		instance = this;
		targets = FindObjectsOfType<Targets>();
		canons = FindObjectsOfType<Canon>();
		spawners = FindObjectsOfType<Spawner>();
		cam = GetComponent<Camera>();

		foreach(Spawner spwn in spawners)
		{
			spwn.gameObject.SetActive(false);
		}
		
	}

	internal void hurt()
	{
		lives--;

		if (lives < 0)
		{
			foreach(Spawner sp in spawners)
			{
				Destroy(sp.gameObject);
			}

			GameStateManager stateman = FindObjectOfType<GameStateManager>();

			if (stateman != null) stateman.endGame();
		}

		
	}

	private void OnEnable()
	{
		StartCoroutine(changeGun());
	}

	IEnumerator changeGun()
	{
		while (gameObject.activeInHierarchy)
		{
			c = UnityEngine.Random.Range(0, canons.Length);
			yield return new WaitForSeconds(1.25f);
		}
		
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.5f));

		foreach (Canon canon in canons)
		{
			canon.transform.LookAt(pos);

			if (Input.GetMouseButton(0))
			{				
				canons[c].fire();
			}
		}
	}
}
