using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Canon[] canons;

	int c = 0;

	private Camera cam;


	public static GameManager instance;

	public static Targets[] targets;


	public GameObject[] ennemies;

	public static Vector3 randomTarget()
	{
		int i = Random.Range(0, targets.Length - 1);

		return targets[i].transform.position;
	}

	public static GameObject randdomEnnemy()
	{

		int i = Random.Range(0, instance.ennemies.Length - 1);

		return instance.ennemies[i];
	}

    // Start is called before the first frame update
    void Awake()
    {
		instance = this;
		targets = FindObjectsOfType<Targets>();
		canons = FindObjectsOfType<Canon>();
		cam = GetComponent<Camera>();
	}

	private void OnEnable()
	{
		StartCoroutine(changeGun());
	}

	IEnumerator changeGun()
	{
		while (gameObject.activeInHierarchy)
		{
			c = Random.Range(0, canons.Length);
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
