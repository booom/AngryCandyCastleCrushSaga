using UnityEngine;
using System.Collections;

public class AniballsGenerator : MonoBehaviour 
{
	public GameObject[] aniballs = new GameObject[4];
	private GameObject currentAniball;
	private GameObject queuedAniball01;
	private GameObject queuedAniball02;
	private GameObject queuedAniball03;

	public Vector3 currentAniballPosition;
	public Vector3 queuedAniballPosition01;
	public Vector3 queuedAniballPosition02;
	public Vector3 queuedAniballPosition03;

	public float distanceThreshold = 0.1f;

	public float speed = 1f;

	void Start()
	{
		NotificationCenter.DefaultCenter.AddObserver(this, "Launch");
		NotificationCenter.DefaultCenter.AddObserver(this, "NextAniball");

		currentAniball = GenerateAniball(currentAniballPosition);
		queuedAniball01 = GenerateAniball(queuedAniballPosition01);
		queuedAniball02 = GenerateAniball(queuedAniballPosition02);
		queuedAniball03 = GenerateAniball(queuedAniballPosition03);
	}

	void Update()
	{
		if (currentAniball) currentAniball.transform.position = Vector3.Lerp(currentAniball.transform.position, currentAniballPosition, Time.deltaTime * speed);
		if (queuedAniball01) queuedAniball01.transform.position = Vector3.Lerp(queuedAniball01.transform.position, queuedAniballPosition01, Time.deltaTime * speed);
		if (queuedAniball02) queuedAniball02.transform.position = Vector3.Lerp(queuedAniball02.transform.position, queuedAniballPosition02, Time.deltaTime * speed);

		if (currentAniball)
		{
			if ((currentAniballPosition - currentAniball.transform.position).magnitude <= distanceThreshold)
			{
				currentAniball.transform.position = currentAniballPosition;
            }

			if (currentAniball.transform.position == currentAniballPosition && !currentAniball.collider2D.enabled)
			{
				currentAniball.collider2D.enabled = true;
			}
		}
	}

	GameObject GenerateAniball(Vector3 pos)
	{
		int i = Random.Range(0, aniballs.Length);
		GameObject newAniball = Instantiate(aniballs[i], pos, Quaternion.identity) as GameObject;
		return newAniball;
	}

	void Launch()
	{
		currentAniball = null;
	}

	void NextAniball()
	{
		UpdateAniballs();
	}

	void UpdateAniballs()
	{
		currentAniball = queuedAniball01;
		queuedAniball01 = queuedAniball02;
		queuedAniball02 = queuedAniball03;
		queuedAniball03 = GenerateAniball(queuedAniballPosition03);
	}
}
